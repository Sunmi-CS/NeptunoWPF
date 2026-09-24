using Microsoft.IdentityModel.Protocols;
using System.Configuration;

namespace NeptunoWPF.Data;

public static class DbConfig
{
    public static string ConnectionString =>
        ConfigurationManager
            .ConnectionStrings["NeptunoDB"]
            ?.ConnectionString
        ?? throw new InvalidOperationException(
            "No se encontró la cadena de conexión 'NeptunoDB' en App.config.");
}
