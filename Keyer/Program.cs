using Keyer;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Keyer.Model;
using Keyer.ProjectsPresenter;

namespace Keyer.Views;

public class Program
{
    [STAThread]
    public static void Main()
    {
        // создаем хост приложения
        var host = Host.CreateDefaultBuilder()
            // внедряем сервисы
            .ConfigureServices(services =>
            {
                services.AddSingleton<App>();
                services.AddSingleton<Image>();
                services.AddSingleton<Presenter>();
                services.AddSingleton<MainWindow>();
            })
            .Build();
        // получаем сервис - объект класса App
        var app = host.Services.GetService<App>();
        // запускаем приложения
        app?.Run();
    }
}