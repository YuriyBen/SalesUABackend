using HtmlAgilityPack;
using SalesUA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesUA.Helpers.Metro
{
    public static class ParsingMetroPage
    {
        internal static List<Product> Parsing()
        {
            string url = "https://www.metro.ua/promotions/od/open-doors-20";
            HtmlWeb webSiteToParse = new HtmlWeb();

            HtmlDocument document = webSiteToParse.Load(url);


            string xPathToRukavichka = "//div[@class='row']//div[@class='col-sm-4']//div[@class='picture ']//picture//img";

            var listItems = document.DocumentNode.SelectNodes(xPathToRukavichka).ToList();

            List<Product> productsRukavichka = new List<Product>();
            foreach (var item in listItems)
            {
                string urlWithoutHttps = item.GetAttributeValue("src", "");
                string imageUrl = $"https://www.metro.ua{urlWithoutHttps}";

                productsRukavichka.Add(new Product("", "", imageUrl, 0, 0, 0));
            }

            
            
            return productsRukavichka;
        }
    }
}
