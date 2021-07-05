using System;
using System.Collections.Generic;
using System.Text;

namespace Papaya.Models
{
    public class Ejercicio
    {

        public string Nombre { get; set; }
        public string Implementacion { get; set; }
        public string Img { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
