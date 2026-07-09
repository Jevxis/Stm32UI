using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stm32UIController.Interfaces
{
    public interface IStm32Device
    {
        public void Open();
        public void Close();
        public void LedOn();
        public void LedOff();
        public string Send(string command);
        public bool isConnected {  get; }
    }
}
