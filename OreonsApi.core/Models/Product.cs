using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OreonsApi.core.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SellingPrice { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ChildrenCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public Image Images { get; set; }
    }
}
