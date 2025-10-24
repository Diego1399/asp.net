using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidad
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public int IdCliente { get; set; }
        public decimal Total { get; set; }
        public int TotalProducto { get; set; }

        public List<DetalleVenta> oDetalleVenta { get; set; }
    }
}
