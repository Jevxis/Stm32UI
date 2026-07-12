using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using Stm32UIController.Models;
using Stm32UIController.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stm32UIController.ViewModels
{
    
    public partial class HumidityGraphVM : ViewModelBase
    {
        private Stm32Device _device;
        public ObservableCollection<ObservablePoint> HumidityValues { get; } = [];
        public ISeries[] Series { get; }
        private int index;
        public HumidityGraphVM(Stm32Device device, DHTdataService DHTservice) 
        { 
            _device = device;
            Series =
            [
                new LineSeries<ObservablePoint>
                {
                    Values = HumidityValues,
                    Stroke = new SolidColorPaint(SKColors.Blue) { StrokeThickness = 4 },
                    Fill = null,
                    GeometryFill = null,
                    GeometryStroke = null
                }
            ];
            DHTservice.HumidityChanged += OnHumidityChanged;
        }
        private void OnHumidityChanged(Dht11Data data)
        {
            if (int.TryParse(data.Humidity.Replace("Влажность = ", ""), out var t))
            {
                HumidityValues.Add(new ObservablePoint(index++, t));
                if (HumidityValues.Count > 100)
                    HumidityValues.RemoveAt(0);
            }
        }
    }
}
