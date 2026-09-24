using System;
using System.Collections.Generic;
using System.Text;

using NeptunoWPF.Data;
using NeptunoWPF.Data.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace NeptunoWPF.ViewModels;

public class PedidoEditViewModel : ViewModelBase
{
    private readonly IPedidoRepository _repository;

    public Pedido Pedido { get; }

    public bool EsEdicion { get; }

    public ObservableCollection<Cliente> Clientes { get; }

    public ObservableCollection<Empleado> Empleados { get; }

    public ObservableCollection<Transportista> Transportistas { get; }

    public PedidoEditViewModel(
        Pedido pedido,
        bool esEdicion,
        ObservableCollection<Cliente> clientes,
        ObservableCollection<Empleado> empleados,
        ObservableCollection<Transportista> transportistas)
    {
        _repository = new PedidoRepository();

        Pedido = pedido;
        EsEdicion = esEdicion;

        Clientes = clientes;
        Empleados = empleados;
        Transportistas = transportistas;

        if (!EsEdicion &&
            Pedido.FechaPedido == default)
        {
            Pedido.FechaPedido = DateTime.Today;
        }
    }

    public async Task GuardarAsync()
    {
        if (Pedido.FechaPedido == default)
        {
            MessageBox.Show(
                "La fecha del pedido es obligatoria.");

            return;
        }

        if (EsEdicion)
        {
            await _repository.ActualizarAsync(Pedido);
        }
        else
        {
            await _repository.InsertarAsync(Pedido);
        }
    }
}
