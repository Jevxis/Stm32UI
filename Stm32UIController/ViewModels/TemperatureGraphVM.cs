using Stm32UIController.Models;
using Stm32UIController.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Defaults;
using Avalonia.Data;

namespace Stm32UIController.ViewModels
{
    public partial class TemperatureGraphVM : ViewModelBase
    {
        private Stm32Device _device;
        public ObservableCollection<ObservablePoint> Values { get; } = [];
        public ISeries[] Series { get; }
        private int index;
        public TemperatureGraphVM(Stm32Device device, DHTdataService DHTservice)
        {
            _device = device;
            Series =
        [
            new LineSeries<ObservablePoint>
            {
                Values = Values
            }
        ];
            DHTservice.TemperatureChanged += OnTemperatureChanged;
        }
        private void OnTemperatureChanged(Dht11Data data)
        {
            if(double.TryParse(data.Temperature.Replace("Температура = ", "").Replace(".", ","), out var t))
            {
                Values.Add(new ObservablePoint(index++, t));
                if (Values.Count > 100)
                    Values.RemoveAt(0);
            }
        }
    }
}
