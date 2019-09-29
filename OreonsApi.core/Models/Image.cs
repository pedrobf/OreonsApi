using System;

namespace OreonsApi.core.Models
{
    public class Image
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public byte[] Content { get; set; }
        public string Name { get; set; }
    }
}
