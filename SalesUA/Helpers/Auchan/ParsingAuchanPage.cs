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
            HtmlWeb webSiteToParse = new HtmlWeb();
            List<Product> productsAuchan = new List<Product>();
            int pageNum = 2;
            while (pageNum != 7)
            {
                HtmlDocument document = webSiteToParse.Load($"https://auchan.zakaz.ua/uk/promotions/?page={pageNum++}");

                List<Product> listOfItemsFromPage = GetListOfProductsViaHTMLdocument(document);

                productsAuchan.AddRange(listOfItemsFromPage);

            }
            return productsAuchan;
        }
        static List<Product> GetListOfProductsViaHTMLdocument(HtmlDocument document)
        {
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            List<Product> AuchanProducts = new List<Product>();

            string xPathToAshanCommon = "//div[@class='jsx-899526249 products-box__list']//div[@class='jsx-899526249 products-box__list-item']";

            var nodeForEach = document.DocumentNode.SelectNodes(xPathToAshanCommon).ToArray();

            foreach (var item in nodeForEach)
            {
                string title = item.ChildNodes.FirstOrDefault(x => x.Name == "a")
                    .Attributes["title"]
                    .Value;
                string imageUrl = item.ChildNodes.FirstOrDefault(x => x.Name == "a")
                    .ChildNodes.FirstOrDefault(x => x.Name == "div")
                    .ChildNodes.FirstOrDefault(x => x.Name == "img")
                    .Attributes["src"]
                    .Value;
                string pricesText = item.ChildNodes.FirstOrDefault(x => x.Name == "a")
                    .ChildNodes.LastOrDefault(x => x.Name == "div")
                    .ChildNodes.FirstOrDefault(x => x.Name == "div")
                    .InnerText;
                decimal.TryParse(pricesText.Split("грн")[0], out decimal newPrice);
                decimal.TryParse(pricesText.Split("грн")[1], out decimal oldPrice);
                byte discount = (byte)(100 - Math.Round(newPrice / oldPrice * 100m, 0));
                AuchanProducts.Add(new Product(title, "", imageUrl, oldPrice, newPrice, discount));
            }
            return AuchanProducts;
        }

       
    }
}
