using System;
using System.Collections.Generic;
using System.Text;

namespace NeptunoWPF.Data.Models;

public class Transportista
{
    public int TransportistaID { get; set; }

    public string CompaniaNombre { get; set; } = string.Empty;

    public override string ToString()
    {
        return CompaniaNombre;
    }
}
