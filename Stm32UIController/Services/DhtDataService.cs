using Stm32UIController.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stm32UIController.Services
{
    public partial class DHTdataService
    {
        private readonly Stm32Device _device;
        public DHTdataService(Stm32Device device)
        {
            _device = device;
        }
        public event Action<Dht11Data>? TemperatureChanged;
        public event Action<Dht11Data>? HumidityChanged;
        public async Task StartMeasurement(CancellationToken cts)
        {
            while(!cts.IsCancellationRequested)
            {
                var data = _device.GetDataDht();

                TemperatureChanged?.Invoke(data);
                HumidityChanged?.Invoke(data);

                await Task.Delay(500);
            }
        }
    }
}
