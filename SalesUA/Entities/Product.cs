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

        public Product(string title, string description, string imagePath,
            decimal oldPrice, decimal newPrice, byte discountPercent)
        {
            this.Title = RefactoringText(title);
            this.Description = RefactoringText(description);
            this.ImagePath = imagePath;
            this.OldPrice = oldPrice;
            this.NewPrice = newPrice;
            this.DiscountPercent = discountPercent;
        }
        string RefactoringText(string wordToRefactor)
        {
            return wordToRefactor.Replace('?', 'i').Replace('<', '"').Replace('>', '"').Replace("&#x27;","'");
        }

    }
}
