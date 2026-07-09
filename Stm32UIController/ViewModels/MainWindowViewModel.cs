
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Stm32UIController.Services;
using Tmds.DBus.Protocol;

namespace Stm32UIController.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _temperature;
        [ObservableProperty]
        private string _humidity;
        private readonly Stm32Device _device;
        public MainWindowViewModel()
        {
            _device = new Stm32Device("COM4");
            _device.Open();
            _temperature = "";
            _humidity = "";
        }

        [RelayCommand]
        private void LedOn()
        {
            _device.LedOn();
        }

        [RelayCommand]
        private void LedOff()
        {
            _device.LedOff();
        }
        [RelayCommand]
        private void GetDataDHT()
        {
            var data = _device.GetDataDht();
            Temperature = data.Temperature;
            Humidity = data.Humidity;
        }
    }
}
