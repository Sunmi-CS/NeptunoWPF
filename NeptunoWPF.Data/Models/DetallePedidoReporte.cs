using System;
using System.Collections.Generic;
using System.Text;

namespace NeptunoWPF.Data.Models;

public class DetallePedidoReporte
{
    public int PedidoID { get; set; }

    public DateTime FechaPedido { get; set; }

    public int ProductoID { get; set; }

    public string NombreProducto { get; set; } = string.Empty;

    public decimal PrecioUnidad { get; set; }

    public short Cantidad { get; set; }

    public decimal Descuento { get; set; }

    public decimal Subtotal { get; set; }
}