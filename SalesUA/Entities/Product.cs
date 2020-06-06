using System;
using System.Collections.Generic;

namespace SalesUA.Entities
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal OldPrice { get; set; }
        public decimal NewPrice { get; set; }
        public byte DiscountPercent { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public int ShopId { get; set; }

        public virtual Shop Shop { get; set; }
    }
}
