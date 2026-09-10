using System;
using System.Collections.Generic;
using System.Text;

namespace NeptunoWPF.ViewModels;

public class MainViewModel
{
    public ProductoViewModel Productos { get; }

    public CategoriaViewModel Categorias { get; }

    public ProveedorViewModel Proveedores { get; }

    public PedidoViewModel Pedidos { get; }

    public MainViewModel()
    {
        Productos = new ProductoViewModel();
        Categorias = new CategoriaViewModel();
        Proveedores = new ProveedorViewModel();
        Pedidos = new PedidoViewModel();
    }
}
