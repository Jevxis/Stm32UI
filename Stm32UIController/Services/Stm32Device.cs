using Avalonia.Media.TextFormatting.Unicode;
using Stm32UIController.Interfaces;
using Stm32UIController.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Stm32UIController.Services
{
    public sealed class Stm32Device : IStm32Device, IDisposable
    {

        private readonly SerialPort _port;
        public Stm32Device(string comPort)
        {
            _port = new SerialPort(comPort, 115200)
            {
                NewLine = "\n",
                ReadTimeout = 5000,
                WriteTimeout = 5000
                
            };
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
            try
            {
                if(!_port.IsOpen)
                {
                    _port.DtrEnable = false;
                    _port.RtsEnable = false;
                    _port.Open();
                    Thread.Sleep(1000);

                    _port.DiscardInBuffer();
                    _port.DiscardOutBuffer();
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message);
            }
            
        }

        public string Send(string command)
        {
            try
            {
                if(_port.IsOpen)
                {
                    _port.WriteLine(command);
                    return _port.ReadLine();
                }
                throw new Exception("Порт закрыт");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public Dht11Data GetDataDht()
        {
            if (_port.IsOpen) 
            {
                string response = Send("GET TEMP");

                string[] parts = response.Split(' ');
                try
                {
                    return new Dht11Data
                    {
                        Temperature = $"Температура = {parts[0].Replace("Temp=", "")}",
                        Humidity = $"Влажность = {parts[1].Replace("Hum=", "")}"
                    };
                }
                catch (Exception)
                {
                    return new Dht11Data
                    {
                        Temperature = "Not Found DHT",
                        Humidity = "Not Found DHT"
                    };
                }
            }
            else
                return new Dht11Data
                {
                    Temperature = "Not Found DHT",
                    Humidity = "Not Found DHT"
                }; ;
            
        }

    }
}
