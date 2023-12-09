using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace gameCenter.Projects._2048game.Models
{
    internal class TileModel
    {
        public int Value {  get; set; }
        
        public TextBlock tileText { get; set; }

        public TranslateTransform TranslateTransform { get; set; }

        public TileModel(int val) 
        {
            Value = val;

            tileText = tileText = new TextBlock
            {
                Text = Value.ToString(),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                TextAlignment = TextAlignment.Center,
                FontWeight = FontWeights.Bold,
                FontSize = 16,
                Width = 100,
                Height = 100,
            };
        }
    }
}
