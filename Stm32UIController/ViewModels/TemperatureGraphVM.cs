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
        public ObservableCollection<ObservablePoint> TemperatureValues { get; } = [];
        public ISeries[] Series { get; }
        private int index;
        public TemperatureGraphVM(Stm32Device device, DHTdataService DHTservice)
        {
            _device = device;
            Series =
            [
                new LineSeries<ObservablePoint>
                {
                    Values = TemperatureValues
                }
            ];
            DHTservice.TemperatureChanged += OnTemperatureChanged;
        }
        private void OnTemperatureChanged(Dht11Data data)
        {
            if (int.TryParse(data.Temperature.Replace("Температура = ", ""), out var t))
            {
                TemperatureValues.Add(new ObservablePoint(index++, t));
                if (TemperatureValues.Count > 100)
                    TemperatureValues.RemoveAt(0);
            }
        }
    }
}
