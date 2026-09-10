using System;
using System.Collections.Generic;
using System.Text;

namespace NeptunoWPF.Models;

public class Empleado
{
    public int EmpleadoID { get; set; }

    public string NombreCompleto { get; set; } = string.Empty;

    public override string ToString()
    {
        return NombreCompleto;
    }
}
