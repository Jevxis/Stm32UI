
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stm32UIController.Services;
using Stm32UIController.Views;
using System.IO.Ports;
using Tmds.DBus.Protocol;

namespace Stm32UIController.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ViewModelBase? currentView;
        public TemperatureViewModel TemperatureVM { get; }
        public TemperatureGraphVM TemperatureGraphVM { get; }
        public HumidityGraphVM HumidityGraphVM { get; }
        private readonly Stm32Device _device;
        public MainWindowViewModel(
        TemperatureViewModel temperatureVm,
        TemperatureGraphVM temperatureGraphVm,
        HumidityGraphVM humidityGraphVm)
        {
            TemperatureVM = temperatureVm;
            TemperatureGraphVM = temperatureGraphVm;
            HumidityGraphVM = humidityGraphVm;
            currentView = TemperatureVM;
            TemperatureVM.Start();
        }
        [RelayCommand]
        private void GetDataDHT()
        {
            TemperatureVM.Start();
            var data = _device.GetDataDht();
        }
        [RelayCommand]
        private void OpenTemperature()
        {
            TemperatureVM.Start();
            CurrentView = TemperatureVM;
            
        }
        [RelayCommand]
        private void OpenTemperatureGraph()
        {
            TemperatureVM?.Stop();
            CurrentView = TemperatureGraphVM;
        }
        [RelayCommand]
        private void OpenHumidityGraph()
        {
            TemperatureVM?.Stop();
            CurrentView = HumidityGraphVM;
        }
    }
}
