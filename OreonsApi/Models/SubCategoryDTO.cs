using System;

namespace OreonsApi.Models
{
    public class SubCategoryDTO
    {
        public string SubCategoryId { get; set; }
        public string CategoryId { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
    }
}
