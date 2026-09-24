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

using NeptunoWPF.Data.Models;
using NeptunoWPF.ViewModels;

using System.Windows;

using NeptunoWPF.ViewModels;

namespace NeptunoWPF.Views;

public partial class CategoriaEditWindow : Window
{
    private readonly CategoriaEditViewModel _viewModel;

    public CategoriaEditWindow(
        Categoria categoria,
        bool esEdicion)
    {
        InitializeComponent();

        _viewModel = new CategoriaEditViewModel(
            categoria,
            esEdicion);

        DataContext = _viewModel;
    }

    private async void Guardar_Click(
        object sender,
        RoutedEventArgs e)
    {
        bool guardado = await _viewModel.GuardarAsync();

        if (guardado)
        {
            DialogResult = true;
        }
    }

    private void Cancelar_Click(
        object sender,
        RoutedEventArgs e)
    {
        DialogResult = false;
    }
}