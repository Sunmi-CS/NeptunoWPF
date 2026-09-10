using System;
using System.Collections.Generic;
using System.Text;

using NeptunoWPF.Data;
using NeptunoWPF.Models;
using System.Windows;

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

    public async Task GuardarAsync()
    {
        if (string.IsNullOrWhiteSpace(
            Categoria.NombreCategoria))
        {
            MessageBox.Show(
                "El nombre de la categoría es obligatorio.");

            return;
        }

        if (EsEdicion)
        {
            await _repository.ActualizarAsync(Categoria);
        }
        else
        {
            await _repository.InsertarAsync(Categoria);
        }
    }
}
