using System;
using System.Collections.Generic;

namespace OreonsApi.core.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public List<SubCategory> ChildrensCategory { get; set; }
        public int RelatedProducts { get; set; }
    }
}
