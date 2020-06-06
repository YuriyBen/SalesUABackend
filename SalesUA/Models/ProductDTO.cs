using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesUA.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public byte DiscountPercent { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }

    }
}
