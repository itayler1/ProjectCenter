using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace gameCenter.Projects.SimonSays.Models
{
    internal class SimonModel
    {
        public string name { get; set; }
        public Color color { get; set; }
        public double opacity { get; set; }

        public SimonModel(string name, Color color)
        {
            this.name = name;
            this.color = color;
            opacity = 0.85;
        }
    }
}
