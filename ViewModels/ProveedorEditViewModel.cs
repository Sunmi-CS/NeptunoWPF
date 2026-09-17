using System;
using System.Threading.Tasks;
using System.Windows;

using NeptunoWPF.Data;
using NeptunoWPF.Models;

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

    public async Task<bool> GuardarAsync()
    {
        if (string.IsNullOrWhiteSpace(
            Proveedor.CompaniaNombre))
        {
            MessageBox.Show(
                "El nombre de la compañía es obligatorio.",
                "Validación",
                MessageBoxButton.OK,
                MessageBoxImage.Warning);

            return false;
        }

        try
        {
            if (EsEdicion)
            {
                await _repository.ActualizarAsync(
                    Proveedor);
            }
            else
            {
                await _repository.InsertarAsync(
                    Proveedor);
            }

            MessageBox.Show(
                EsEdicion
                    ? "Proveedor actualizado correctamente."
                    : "Proveedor registrado correctamente.",
                "Éxito",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No se pudo guardar el proveedor.\n\n"
                + ex.Message,
                "Error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            return false;
        }
    }
}