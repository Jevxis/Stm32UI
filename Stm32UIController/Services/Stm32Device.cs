using Stm32UIController.Interfaces;
using Stm32UIController.Models;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stm32UIController.Services
{
    public sealed class Stm32Device : IStm32Device, IDisposable
    {

        private readonly SerialPort _port = new();
        public Stm32Device(string comPort)
        {
            _port.PortName = comPort;
            _port.BaudRate = 115200;
            _port.NewLine = "\r\n";
            _port.ReadTimeout = 1000;
            _port.WriteTimeout = 1000;
        }

        public bool isConnected => _port.IsOpen;
        public void Close()
        {
            _port.Close();
        }

        public void Dispose()
        {
            if (_port.IsOpen)
            {
                _port.Close();
            }
            _port.Dispose();
        }

        public void LedOff()
        {
            Send("LED OFF");
        }

        public void LedOn()
        {
            Send("LED ON");
        }

        public void Open()
        {
            _port.Open();
        }

        public string Send(string command)
        {
            _port.WriteLine(command);
            return _port.ReadLine();
        }

        public Dht11Data GetDataDht()
        {
            string response = Send("GET TEMP");
            string[] parts = response.Split(' ');
            return new Dht11Data
            {
                Temperature = parts[0],
                Humidity = parts[1]
            };
        }

    }
}
