using System;
using System.Collections.Generic;
using System.Text;

namespace Papaya.Models
{
    public class PlatilloModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Img { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
