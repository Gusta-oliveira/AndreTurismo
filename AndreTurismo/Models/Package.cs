using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndreTurismo.Models
{
    public class Package
    {
        public int Id { get; set; }
        public Hotel Hotel { get; set; }
        public Passage Passage { get; set; }
        public DateTime DateCadastre { get; set; }
        public  double Value { get; set; }
        public Client Client { get; set; }
    }
}
