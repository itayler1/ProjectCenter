using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace gameCenter.Projects.SimonSays.Models
{
    internal class ColorList
    {
        public List<SimonModel> colorList = new List<SimonModel>();

        public SimonModel Red = new("Red", Colors.Red);
        public SimonModel Green = new("Green", Colors.Green);
        public SimonModel Yellow = new("Yellow", Colors.Yellow);
        public SimonModel Blue = new("Blue", Colors.Blue);


        public ColorList()
        {
            colorList.Add(Red); colorList.Add(Green); colorList.Add(Yellow); colorList.Add(Blue);
        }
    }
}
