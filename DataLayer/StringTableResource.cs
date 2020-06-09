using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    [Table("StringTable")]
    public class StringTableResource : CardResource
    {
        public ICollection<Row> Rows { get; set; } = new List<Row>();
    }
}