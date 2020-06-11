using HtmlAgilityPack;
using SalesUA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesUA.Helpers.Rukavichka
{
    public static class ParsingRukavichkaPage
    {
        internal static List<Product> Parsing()
        {
            HtmlWeb webSiteToParse = new HtmlWeb();

            HtmlDocument document = webSiteToParse.Load("http://rukavychka.ua/akcii-z-gazetky/");


            string xPathToRukavichka = "//div[@class='card-tiles']//div[@class='card card--promotion']//img";

            var listItems = document.DocumentNode.SelectNodes(xPathToRukavichka).ToList();


            List<Product> productsRukavichka = new List<Product>();
            foreach (var item in listItems)
            {
                var imageUrl = item.Attributes["data-src"].Value;

                productsRukavichka.Add(new Product("", "", imageUrl, 0, 0, 0));
            }
            return productsRukavichka;
        }
    }
}
