using System;
using System.Collections.Generic;

namespace OreonsApi.Models
{
    public class ProductListDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal SellingPrice { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
