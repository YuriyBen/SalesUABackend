using System;
using System.Collections.Generic;

namespace SalesUA.Entities
{
    public partial class Shop
    {
        public Shop()
        {
            Product = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
