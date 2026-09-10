using System;
using System.Collections.Generic;
using System.Text;

using NeptunoWPF.Data;
using NeptunoWPF.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace NeptunoWPF.ViewModels;

public class CategoriaViewModel : ViewModelBase
{
    private readonly ICategoriaRepository _repository;

    public ObservableCollection<Categoria> Categorias { get; } = new();

    private Categoria? _categoriaSeleccionada;

    public Categoria? CategoriaSeleccionada
    {
        get => _categoriaSeleccionada;
        set => SetProperty(ref _categoriaSeleccionada, value);
    }

    public CategoriaViewModel()
    {
        _repository = new CategoriaRepository();
    }

    public async Task CargarAsync()
    {
        try
        {
            var lista = await _repository.ListarAsync();

            Categorias.Clear();

            foreach (var categoria in lista)
                Categorias.Add(categoria);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    public async Task EliminarAsync()
    {
        if (CategoriaSeleccionada == null)
            return;

        var respuesta = MessageBox.Show(
            "¿Deseas eliminar la categoría seleccionada?",
            "Confirmar",
            MessageBoxButton.YesNo);

        if (respuesta != MessageBoxResult.Yes)
            return;

        try
        {
            await _repository.EliminarAsync(
                CategoriaSeleccionada.CategoriaID);

            await CargarAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.Message,
                "No se pudo eliminar",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);
        }
    }
}
