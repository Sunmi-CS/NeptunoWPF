using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;

using NeptunoWPF.Data;
using NeptunoWPF.Data.Models;

namespace NeptunoWPF.ViewModels;

public class CategoriaViewModel : ViewModelBase
{
    private readonly ICategoriaRepository _repository;

    public ObservableCollection<Categoria> Categorias { get; }
        = new();

    private Categoria? _categoriaSeleccionada;

    public Categoria? CategoriaSeleccionada
    {
        get => _categoriaSeleccionada;
        set => SetProperty(
            ref _categoriaSeleccionada,
            value);
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
            {
                Categorias.Add(categoria);
            }

            CategoriaSeleccionada = null;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No se pudieron cargar las categorías.\n\n"
                + ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    public async Task EliminarAsync()
    {
        if (CategoriaSeleccionada == null)
        {
            MessageBox.Show(
                "Selecciona una categoría.",
                "Aviso",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return;
        }

        var respuesta = MessageBox.Show(
            "¿Deseas eliminar la categoría seleccionada?",
            "Confirmar eliminación",
            MessageBoxButton.YesNo,
            MessageBoxImage.Question);

        if (respuesta != MessageBoxResult.Yes)
            return;

        try
        {
            await _repository.EliminarAsync(
                CategoriaSeleccionada.CategoriaID);

            await CargarAsync();

            MessageBox.Show(
                "Categoría eliminada correctamente.",
                "Éxito",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No se pudo eliminar la categoría.\n\n"
                + ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}