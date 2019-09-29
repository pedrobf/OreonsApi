using System;

namespace OreonsApi.core.Models
{
    public class SubCategory
    {
        public string SubCategoryId { get; set; }
        public string CategoryId { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
    }
}
