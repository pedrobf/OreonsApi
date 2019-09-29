using System;

namespace OreonsApi.Models
{
    public class ImageDTO
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public byte[] Content { get; set; }
        public string Name { get; set; }
    }
}
