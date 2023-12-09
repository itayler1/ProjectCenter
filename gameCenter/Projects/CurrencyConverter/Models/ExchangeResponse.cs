using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCenter.Projects.CurrencyConverter.Models
{
    internal class ExchangeResponse
    {
        public bool Success { get; set; }
        public long Timestamp { get; set; }
        public string Base { get; set; }
        public string Date { get; set; }
        public Dictionary<string, double> Rates { get; set; }
    }
}
