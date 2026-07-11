using Stm32UIController.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stm32UIController.ViewModels
{
    public partial class TemperatureGraphVM : ViewModelBase
    {
        private Stm32Device _device;
        public TemperatureGraphVM(Stm32Device device)
        {
            _device = device;
        }
    }
}
