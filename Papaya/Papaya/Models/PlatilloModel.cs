using System;
using System.Collections.Generic;
using System.Text;

namespace Papaya.Models
{
    public class PlatilloModel
    {
        public int id { get; set; }
        public string nombre { get; set; }

        public string energia { get; set; }

        public string img { get; set; }

        public override string ToString()
        {
            return nombre;
        }
    }
}
