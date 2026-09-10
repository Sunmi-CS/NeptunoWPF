using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using NeptunoWPF.Models;
using NeptunoWPF.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace NeptunoWPF.Views;

public partial class PedidoEditWindow : Window
{
    private readonly PedidoEditViewModel _viewModel;

    public PedidoEditWindow(
        Pedido pedido,
        bool esEdicion,
        ObservableCollection<Cliente> clientes,
        ObservableCollection<Empleado> empleados,
        ObservableCollection<Transportista> transportistas)
    {
        InitializeComponent();

        _viewModel =
            new PedidoEditViewModel(
                pedido,
                esEdicion,
                clientes,
                empleados,
                transportistas);

        DataContext = _viewModel;
    }

    private async void Guardar_Click(
        object sender,
        RoutedEventArgs e)
    {
        try
        {
            await _viewModel.GuardarAsync();

            DialogResult = true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void Cancelar_Click(
        object sender,
        RoutedEventArgs e)
    {
        DialogResult = false;
    }
}