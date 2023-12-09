using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace gameCenter.Projects.SimonSays.Models
{
    internal class SimonGame
    {
        public List<SimonModel> gameSequence { get; set; }

        public List<string> playerSequence { get; set; }
        public int score { get; set; }

        public ColorList simonColors = new();
        
        public Random random = new Random();

        public void AddColorToSequence() 
        {
            int rand = random.Next(0, 4);
            SimonModel colorToAdd = simonColors.colorList[rand];
            gameSequence.Add(colorToAdd);
        }


        public SimonGame()
        {
            playerSequence = new List<string>();
            gameSequence = new List<SimonModel>();
            score = 0;
        }
    }
}
