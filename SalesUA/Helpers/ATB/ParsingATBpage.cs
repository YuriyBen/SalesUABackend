using HtmlAgilityPack;
using SalesUA.Entities;
using SalesUA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SalesUA.Helpers.ATB
{
    public static class ParsingATBpage
    {
        public static  List<Product> Parsing(/*List<Product> ATB*/)
        {
            List<Product> ATB = new List<Product>();
            HtmlWeb webSiteToParse = new HtmlWeb();
            HtmlDocument document = webSiteToParse.Load("https://www.atbmarket.com/hot/akcii/economy/");
            string xpathCommon = "//div[@class='container']//div[@class='list_wrapper']//div" +
                "//ul[@id='cat0']//li";

            string xPathToImageUrl = xpathCommon + "//div[@class='promo_image_wrap']//a[@class='promo_image_link']//img";
            var listOfImageUrls = ImagesOfAllItems(document, xPathToImageUrl);

            string xPathToInfo = xpathCommon + "//div[@class='promo_info']";

            string xPathToTitle = xPathToInfo + "//span[@class='promo_info_text']";
            var listOfTitle = TitlesOfAllItems(document, xPathToTitle);

            string xPathToDescription = xPathToTitle + "//span";
            var listOfDescription = DescriptionOfAllItems(document, xPathToDescription);


            string xPathToPricesCommon = xPathToInfo + "//div[@class='price_box small_box red_box floated_right']";


            string xPathToOldrice = xPathToPricesCommon + "//span[@class='promo_old_price']";
            var listOfOldPrices = OldPricesOfAllItems(document, xPathToOldrice);

            string xPathToDiscount = xPathToPricesCommon + "//div[@class='economy_price_container']//div[@class='economy_price']//span";
            var listOfDiscounts = DiscountsOfAllItems(document, xPathToDiscount, listOfOldPrices);

            for (int i = 0; i < listOfTitle.Count; i++)
            {
                decimal newPrice = listOfOldPrices[i].ComputeDiscountedPrice(listOfDiscounts[i]);

                ATB.Add(new Product(listOfTitle[i], listOfDescription[i], listOfImageUrls[i],
                    listOfOldPrices[i], newPrice, listOfDiscounts[i]));

            }
            return ATB.ToList();
        }
       
        //public static List<Product> Parsing (this ICollection<Product> ATB)
        //{
        //    //List<ProductDTO> ATB = new List<ProductDTO>();
        //    HtmlWeb webSiteToParse = new HtmlWeb();
        //    HtmlDocument document = webSiteToParse.Load("https://www.atbmarket.com/hot/akcii/economy/");
        //    string xpathCommon = "//div[@class='container']//div[@class='list_wrapper']//div" +
        //        "//ul[@id='cat0']//li";

        //    string xPathToImageUrl = xpathCommon + "//div[@class='promo_image_wrap']//a[@class='promo_image_link']//img";
        //    var listOfImageUrls = ImagesOfAllItems(document, xPathToImageUrl);

        //    string xPathToInfo = xpathCommon + "//div[@class='promo_info']";

        //    string xPathToTitle = xPathToInfo + "//span[@class='promo_info_text']";
        //    var listOfTitle = TitlesOfAllItems(document, xPathToTitle);

        //    string xPathToDescription = xPathToTitle + "//span";
        //    var listOfDescription = DescriptionOfAllItems(document, xPathToDescription);


        //    string xPathToPricesCommon = xPathToInfo + "//div[@class='price_box small_box red_box floated_right']";


        //    string xPathToOldrice = xPathToPricesCommon + "//span[@class='promo_old_price']";
        //    var listOfOldPrices = OldPricesOfAllItems(document, xPathToOldrice);

        //    string xPathToDiscount = xPathToPricesCommon + "//div[@class='economy_price_container']//div[@class='economy_price']//span";
        //    var listOfDiscounts = DiscountsOfAllItems(document, xPathToDiscount, listOfOldPrices);

        //    for (int i = 0; i < listOfTitle.Count; i++)
        //    {
        //        decimal newPrice = listOfOldPrices[i].ComputeDiscountedPrice(listOfDiscounts[i]);

        //        ATB.Add(new Product(listOfTitle[i], listOfDescription[i], listOfImageUrls[i],
        //            listOfOldPrices[i], newPrice, listOfDiscounts[i]));

        //    }
        //    return ATB.ToList();
        //}
        static List<string> ImagesOfAllItems(HtmlDocument document, string xPathToImageUrl)
        {
            HtmlNode[] nodesToImageUrl = document.DocumentNode.SelectNodes(xPathToImageUrl).ToArray();
            List<string> listOfImageUrls = new List<string>();
            foreach (var item in nodesToImageUrl)
            {
                string urlToComplete = item.Attributes["src"].Value;
                string fullUrl = $"https://www.atbmarket.com/{urlToComplete}";
                listOfImageUrls.Add(fullUrl);
            }
            return listOfImageUrls;
        }

        static List<string> TitlesOfAllItems(HtmlDocument document, string xPathToTitle)
        {
            HtmlNode[] nodesToTitle = document.DocumentNode.SelectNodes(xPathToTitle).ToArray();
            List<string> listOfTitles = new List<string>();
            foreach (var item in nodesToTitle)
            {
                string title = item.SelectSingleNode("text()").InnerText.Trim();
                title = title.Replace('?', 'i');
                listOfTitles.Add(title);
            }
            return listOfTitles;
        }

        static List<string> DescriptionOfAllItems(HtmlDocument document, string xPathToDescription)
        {
            HtmlNode[] nodesToDescription = document.DocumentNode.SelectNodes(xPathToDescription).ToArray();
            List<string> listOfDescription = new List<string>();
            foreach (var item in nodesToDescription)
            {
                string description = item.InnerText.Trim();
                listOfDescription.Add(description);
            }
            return listOfDescription;
        }
        static List<byte> DiscountsOfAllItems(HtmlDocument document, string xPathToDiscount, List<decimal> listOfOldPrices)
        {
            HtmlNode[] nodesToDiscount = document.DocumentNode.SelectNodes(xPathToDiscount).ToArray();
            List<byte> listOfDiscounts = new List<byte>();
            foreach (var item in nodesToDiscount)
            {
                string discountText = Regex.Match(item.InnerText, @"\d+").Value;
                if (!byte.TryParse(discountText, out byte discount))
                {
                    discount = 0;
                }
                listOfDiscounts.Add(discount);
            }

            int startIndexOfZero = listOfOldPrices.IndexOf(0);
            int lastIndexOfZero = listOfOldPrices.LastIndexOf(0);
            var ZerosToList = new List<byte>(new byte[lastIndexOfZero - startIndexOfZero + 1]);
            listOfDiscounts.InsertRange(startIndexOfZero, ZerosToList);

            return listOfDiscounts;
        }
        static List<decimal> OldPricesOfAllItems(HtmlDocument document, string xPathToOldrice)
        {
            HtmlNode[] nodesToOldPrice = document.DocumentNode.SelectNodes(xPathToOldrice).ToArray();
            List<decimal> listOfOldPrices = new List<decimal>();
            foreach (var item in nodesToOldPrice)
            {
                string OldPriceText = item.InnerText.Replace('.', ',');
                if (!decimal.TryParse(OldPriceText, out decimal OldPrice))
                {
                    OldPrice = 0;

                }
                listOfOldPrices.Add(OldPrice);
            }
            return listOfOldPrices;
        }
    }
}
