using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using Stm32UIController.Models;
using Stm32UIController.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stm32UIController.ViewModels
{
    public partial class TemperatureViewModel : ViewModelBase
    {
        private readonly Stm32Device _device;
        private CancellationTokenSource? _cts;
        [ObservableProperty]
        private string _temperature;

        [ObservableProperty]
        private string _humidity;

        public TemperatureViewModel(Stm32Device device, DHTdataService DHTservice)
        {
            _device = device;
            DHTservice.TemperatureChanged += OnTemperatureChanged;
        }
        private void OnTemperatureChanged(Dht11Data data)
        {
            Temperature = data.Temperature;
            Humidity = data.Humidity;
        }
        public void Start()
        {
            _cts = new CancellationTokenSource();

            _ = UpdateLoop(_cts.Token);
        }

        public void Stop()
        {
            _cts?.Cancel();
            _cts = null;
        }

        private async Task UpdateLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var data = _device.GetDataDht();

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    Temperature = data.Temperature;
                    Humidity = data.Humidity;
                });

                await Task.Delay(500, token);
            }
        }

    }
}
