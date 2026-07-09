using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stm32UIController.Models
{
    public class Dht11Data
    {
        public string Temperature {  get; set; } = string.Empty;
        public string Humidity { get; set; } = string.Empty;
    }
}
