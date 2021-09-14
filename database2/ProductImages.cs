using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database2
{
   public class ProductImages
    {
        public int Id { get; set; }
        public string Printer { get; set; }
        public string Laser { get; set; }
        public string Laminator { get; set; } 
        public byte[] Pic { get; set; }
         public string Picbase64String { get; set; }
    }
}
