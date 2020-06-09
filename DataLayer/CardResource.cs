using System.ComponentModel.DataAnnotations;

namespace DataLayer
{
    public abstract class CardResource
    {
        [Key]
        public int Id { get; set; }
        public int Kind { get; set; }
        public virtual Card Card { get; set; }
    }
}