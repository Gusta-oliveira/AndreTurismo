using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismo.Models
{
    public class Passage
    {
        public int Id { get; set; }
        public Address Origin { get; set; }
        public Address Destiny { get; set; }
        public Client Client { get; set; }
        public DateTime DatePassage { get; set; }
        public decimal Values { get; set; }
    }
}
