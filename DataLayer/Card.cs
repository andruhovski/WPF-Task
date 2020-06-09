using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        public int Parent { get; set; } = -1;

        public string Header { get; set; }
        public ICollection<CardResource> CardResources { get; set; } = new List<CardResource>();
    }
}
