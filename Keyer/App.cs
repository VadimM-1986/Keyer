using Keyer.Model;
using Keyer.ProjectsPresenter;
using Keyer.Views;
using System.Windows;

namespace Keyer;

public class App : Application
{
    private readonly MainWindow _mainWindow;

    // через систему внедрения зависимостей получаем объект главного окна
    public App(MainWindow mainWindow, Presenter presenter, Image service)
    {
        _mainWindow = mainWindow;
    }
    protected override void OnStartup(StartupEventArgs e)
    {
        _mainWindow.Show();  // отображаем главное окно на экране
        base.OnStartup(e);
    }
}