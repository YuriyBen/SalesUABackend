using SalesUA.Entities;
using SalesUA.Helpers.ATB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesUA.Helpers
{

    public static class ParsingPagesUsingShopId
    {
        public static List<Product> GetListOfProductViaShopTitle( string title)
        {
            var listOfProducts = new List<Product>();
            switch(title)
            {
                case "АТБ":
                    {
                        listOfProducts = ParsingATBpage.Parsing();
                        break;
                    }


            }
            return listOfProducts;
        }
    }
}
