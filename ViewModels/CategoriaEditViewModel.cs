using System;
using System.Threading.Tasks;
using System.Windows;

using NeptunoWPF.Data;
using NeptunoWPF.Data.Models;

namespace NeptunoWPF.ViewModels;

public class CategoriaEditViewModel : ViewModelBase
{
    private readonly ICategoriaRepository _repository;

    public Categoria Categoria { get; }

    public bool EsEdicion { get; }

    public CategoriaEditViewModel(
        Categoria categoria,
        bool esEdicion)
    {
        _repository = new CategoriaRepository();

        Categoria = categoria;
        EsEdicion = esEdicion;
    }

    public async Task<bool> GuardarAsync()
    {
        if (string.IsNullOrWhiteSpace(
            Categoria.NombreCategoria))
        {
            MessageBox.Show(
                "El nombre de la categoría es obligatorio.",
                "Validación",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return false;
        }

        try
        {
            if (EsEdicion)
            {
                await _repository.ActualizarAsync(Categoria);
            }
            else
            {
                await _repository.InsertarAsync(Categoria);
            }

            MessageBox.Show(
                EsEdicion
                    ? "Categoría actualizada correctamente."
                    : "Categoría registrada correctamente.",
                "Éxito",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No se pudo guardar la categoría.\n\n"
                + ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            return false;
        }
    }
}