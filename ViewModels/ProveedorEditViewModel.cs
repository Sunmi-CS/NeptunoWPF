using System;
using System.Collections.Generic;
using System.Text;

using NeptunoWPF.Data;
using NeptunoWPF.Models;
using System.Windows;

namespace NeptunoWPF.ViewModels;

public class ProveedorEditViewModel : ViewModelBase
{
    private readonly IProveedorRepository _repository;

    public Proveedor Proveedor { get; }

    public bool EsEdicion { get; }

    public ProveedorEditViewModel(
        Proveedor proveedor,
        bool esEdicion)
    {
        _repository = new ProveedorRepository();

        Proveedor = proveedor;
        EsEdicion = esEdicion;
    }

    public async Task GuardarAsync()
    {
        if (string.IsNullOrWhiteSpace(
            Proveedor.CompaniaNombre))
        {
            MessageBox.Show(
                "El nombre de la compañía es obligatorio.");

            return;
        }

        if (EsEdicion)
        {
            await _repository.ActualizarAsync(Proveedor);
        }
        else
        {
            await _repository.InsertarAsync(Proveedor);
        }
    }
}