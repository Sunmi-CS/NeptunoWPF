using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows;

using NeptunoWPF.Views;
using System.Windows;

namespace NeptunoWPF;

public partial class App : Application
{
    protected override void OnStartup(
        StartupEventArgs e)
    {
        base.OnStartup(e);

        var mainWindow = new MainWindow();

        mainWindow.Show();
    }
}
