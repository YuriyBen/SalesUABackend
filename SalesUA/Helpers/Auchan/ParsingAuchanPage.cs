using HtmlAgilityPack;
using SalesUA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SalesUA.Helpers.Auchan
{
    public static class ParsingAuchanPage
    {
        public static List<Product> Parsing()
        {
            List<Product> AuchanProducts = new List<Product>();

            HtmlWeb webSiteToParse = new HtmlWeb();

            HtmlDocument document = webSiteToParse.Load("https://auchan.ua/ua/superceny/?limit=96");

            string xPathToAshanCommon = "//div[@class='main-container col2-left-layout container']//div[@class='main row']" +
                "//div[@class='col-md-12']//div[@class='col-main']//div[@class='category-products']" +
                "//ul[@class='products-grid products-grid--max-4-col']//li";

            var nodeForEach = document.DocumentNode.SelectNodes(xPathToAshanCommon).ToArray();

            for (int i = 0; i < nodeForEach.Length-1; i++)
            {
                var item = nodeForEach[i];
                string url = item.ChildNodes.FirstOrDefault(x => x.Name == "a")
                    .ChildNodes.FirstOrDefault(x => x.Name == "picture")
                    .ChildNodes.FirstOrDefault(x => x.Name == "img")
                    .Attributes["src"]
                    .Value;
                string title = item.ChildNodes.FirstOrDefault(x => x.Name == "div")
                    .ChildNodes.FirstOrDefault(x => x.Name == "strong")
                    .ChildNodes.FirstOrDefault(x => x.Name == "a")
                    .InnerText;


                string xPathToNewPrice = xPathToAshanCommon + "//div[@class='product-info']//div[@class='price-box']//p[@class='special-price']//span[@class='price']";
                var nodesToImagePath = document.DocumentNode.SelectNodes(xPathToNewPrice).ToArray();

                string newPriceText = nodesToImagePath[i].InnerText.Replace("грн", "").Trim();

                decimal.TryParse(newPriceText, out decimal newPrice);

                string discountText = item.ChildNodes.FirstOrDefault(x => x.Name == "div")
                    .ChildNodes.LastOrDefault(x => x.Name == "div")
                    .InnerText;

                byte.TryParse(Regex.Match(discountText, @"\d+").Value, out byte discount);
                decimal oldPrice = Math.Round(newPrice * 100m / (100m-discount), 2);
                AuchanProducts.Add(new Product(title, "", url, oldPrice, newPrice, discount));
            }
            return AuchanProducts;
        }

       
    }
}
