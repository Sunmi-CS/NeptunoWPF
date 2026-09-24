using System;
using System.Collections.Generic;
using System.Text;

namespace NeptunoWPF.Data.Models;

public class Cliente
{
    public int ClienteID { get; set; }

    public string Empresa { get; set; } = string.Empty;

    public override string ToString()
    {
        return Empresa;
    }
}
