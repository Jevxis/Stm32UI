using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using Stm32UIController.Services;
using Stm32UIController.ViewModels;
using Stm32UIController.Views;
using System;
using System.Linq;

namespace Stm32UIController
{
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; } = null!;
        public override void OnFrameworkInitializationCompleted()
        {
            var services = new ServiceCollection();

            ConfigureServices(services);

            Services = services.BuildServiceProvider();

            var device = Services.GetRequiredService<Stm32Device>();
            device.Open();
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.Exit += (_, _) =>
                {
                    Services.GetRequiredService<Stm32Device>().Dispose();
                };
                desktop.MainWindow = Services.GetRequiredService<MainWindow>();
            }

            base.OnFrameworkInitializationCompleted();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Stm32Device>(_ => new Stm32Device("COM4"));

            services.AddSingleton<TemperatureViewModel>();
            services.AddSingleton<TemperatureGraphVM>();
            services.AddSingleton<HumidityGraphVM>();

            services.AddSingleton<MainWindowViewModel>();

            services.AddSingleton<MainWindow>();
            
        }
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}