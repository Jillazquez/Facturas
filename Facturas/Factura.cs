﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturas
{
    internal class Factura
    {
        public string Nombre { get; set; }
        public int PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
    }
}
