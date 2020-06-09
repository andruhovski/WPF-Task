using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    [Table("ImageResource")]
    public class ImageResource : CardResource
    {
        public byte[] Image { get; set; }
        public string Description { get; set; }
    }
}