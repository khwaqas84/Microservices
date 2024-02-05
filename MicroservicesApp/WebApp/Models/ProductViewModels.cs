﻿namespace WebApp.Models
{
    public class ProductViewModels
    {
        public int ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        public int CategoryId { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
