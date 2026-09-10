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
using System.Windows;

namespace NeptunoWPF.Views;

public partial class ProveedorEditWindow : Window
{
    private readonly ProveedorEditViewModel _viewModel;

    public ProveedorEditWindow(
        Proveedor proveedor,
        bool esEdicion)
    {
        InitializeComponent();

        _viewModel =
            new ProveedorEditViewModel(
                proveedor,
                esEdicion);

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