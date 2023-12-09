using gameCenter.Projects.SimonSays.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace gameCenter.Projects.SimonSays
{
    /// <summary>
    /// Interaction logic for SimonSaysView.xaml
    /// </summary>
    public partial class SimonSaysView : Window
    {
        SimonGame game = new();

        bool gameOver = false;

        bool playerHasFinishedMove = true;

        public enum Difficulty
        {
            Easy = 1100,
            Medium = 800,
            Hard = 450
        }

        public Difficulty difficulty;

        static string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
        static string soundsFolderDir = GetSoundsFolderPath();


        public SimonSaysView()
        {
            InitializeComponent();
            LoadScores();
            
        }
        static string GetSoundsFolderPath()
        {
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (currentDirectory.EndsWith("bin\\Debug\\") || currentDirectory.EndsWith("bin\\Release\\"))
            {
                return Path.Combine(currentDirectory, "..", "..", "..", "Projects", "SimonSays", "Assets", "Sounds");
            }
            else
            {
                return Path.Combine(currentDirectory, "Projects", "SimonSays", "Assets", "Sounds");
            }
        }

        private void mainGameLoop()
        {

            if (!gameOver)
            {
                if (playerHasFinishedMove)
                {
                    game.playerSequence = new List<string>();
                    playerHasFinishedMove = false;
                    game.AddColorToSequence();
                    DisplaySequence();
                }
            }
            else  if (gameOver)
            {
                StartButton.IsEnabled = true;
                DifficultyComboBox.IsEnabled = true;
                GameGrid.Visibility = Visibility.Collapsed;
                GameOverGrid.Visibility = Visibility.Visible;
                FinalScoreDisplay.Content = game.score.ToString();
                ScoreDisplay.Content = "0";
            }
        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            game = new SimonGame();
            StartButton.IsEnabled = false;
            DifficultyComboBox.IsEnabled = false;
            gameOver = false;
            playerHasFinishedMove = true;
            mainGameLoop();
        }

        private void HandleColorClick(object sender, RoutedEventArgs e)
        {
            Button? thisButton = sender as Button;
            string clickedColor = thisButton.Name;
            game.playerSequence.Add(clickedColor);
            CompareSequence(clickedColor);
        }

        private void CompareSequence(string playerMove)
        {
            int currentMove = game.playerSequence.Count;
            string gameMove = game.gameSequence[currentMove - 1].name;

            if (gameMove != playerMove)
            {
                PlaySound("WrongBuzz.WAV");
                gameOver = true;
            }
            if (currentMove == game.gameSequence.Count && gameMove == playerMove)
            {
                playerHasFinishedMove = true;
                switch (difficulty)
                {
                    case Difficulty.Easy:
                        game.score += 3; break;
                    case Difficulty.Medium:
                        game.score += 4; break;
                    case Difficulty.Hard:
                        game.score += 5; break;
                }
                ScoreDisplay.Content = game.score.ToString();
            }
            mainGameLoop();
        }

        private async void DisplaySequence()
        {
            Green.IsEnabled = false;
            Red.IsEnabled = false;
            Yellow.IsEnabled = false;
            Blue.IsEnabled = false;

            Green.IsHitTestVisible = false;
            Red.IsHitTestVisible = false;
            Yellow.IsHitTestVisible = false;
            Blue.IsHitTestVisible = false;

            await Task.Delay((int)difficulty/2);

            foreach (SimonModel simonColor in game.gameSequence)
            {
                if (simonColor.name == "Red")
                {
                    PlaySound("Ding1.WAV");
                    HighLightUnhighlightColor(Red);
                }
                else if (simonColor.name == "Green")
                {
                    PlaySound("Ding2.WAV");
                    HighLightUnhighlightColor(Green);
                }
                else if (simonColor.name == "Yellow")
                {
                    PlaySound("Ding3.WAV");
                    HighLightUnhighlightColor(Yellow);
                }
                else if (simonColor.name == "Blue")
                {
;                   PlaySound("Ding4.WAV");
                    HighLightUnhighlightColor(Blue);
                }
                await Task.Delay((int)difficulty);
            }

            Green.IsEnabled = true;
            Red.IsEnabled = true;
            Yellow.IsEnabled = true;
            Blue.IsEnabled = true;

            Green.IsHitTestVisible = true;
            Red.IsHitTestVisible = true;
            Yellow.IsHitTestVisible = true;
            Blue.IsHitTestVisible = true;

        }

        private void PlaySound(string soundFileName)
        {

            try
            {
                SoundPlayer soundPlayer = new SoundPlayer(Path.Combine(soundsFolderDir,soundFileName));
                soundPlayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing sound: {ex.Message}");
            }
        }
        private async void HighLightUnhighlightColor(Button btn)
        {
            btn.IsEnabled = true;
            await Task.Delay((int)difficulty / 2);
            btn.IsEnabled = false;

        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            StartGrid.Visibility = Visibility.Collapsed;
            GameOverGrid.Visibility = Visibility.Collapsed;
            GameGrid.Visibility = Visibility.Visible;
            DifficultyComboBox.SelectedItem = DifficultyComboBox.Items[1];
            DifficultyComboBox.IsEnabled = true;
            ScoreSubmitBtn.IsEnabled = true;


            Green.IsHitTestVisible = false;
            Red.IsHitTestVisible = false;
            Yellow.IsHitTestVisible = false;
            Blue.IsHitTestVisible = false;
        }

        private void SubmitBtnOnclick(object sender, RoutedEventArgs e)
        {
            string playerName = PlayerNameInput.Text;
            string score = game.score.ToString();
            string filePath = Path.Combine(AppContext.BaseDirectory, "simonScores.txt");

            if (!string.IsNullOrEmpty(playerName))
            {
                using (StreamWriter streamWriter = new StreamWriter(filePath,true))
                {
                    streamWriter.WriteLine($"{playerName} : {score} : Difficulty - {difficulty}");
                }
                ScoreSubmitBtn.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Please enter a name");
            }

        }

        private void LoadScores()
        {
            ScoreBoardList.Items.Clear();
            string filePath = Path.Combine(System.AppContext.BaseDirectory, "simonScores.txt");

            if (!File.Exists(filePath))
            {
                using (File.Create(filePath))
                {

                };
            }
            try
            {
                List<string> scores = new List<string>();

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        scores.Add(line);
                    }
                }

                scores.Sort((a, b) =>
                {
                    int scoreA = int.Parse(a.Split(':')[1].Trim());
                    int scoreB = int.Parse(b.Split(':')[1].Trim());
                    return scoreB.CompareTo(scoreA);
                });


                foreach (string score in scores)
                {
                    ScoreBoardList.Items.Add(score);
                }
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Scores file not found.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }


        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadScores();
            ScoreDisplay.Content = "0";
            game = new SimonGame();
            StartButton.IsEnabled = true;

            GameGrid.Visibility = Visibility.Collapsed;
            StartGrid.Visibility = Visibility.Visible;
        }


        private void DifficultyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetGameDifficulty();
        }

        private void SetGameDifficulty()
        {
            if (DifficultyComboBox.SelectedItem is ComboBoxItem selectedComboBoxItem)
            {
                if (Enum.TryParse(selectedComboBoxItem.Content.ToString(), out Difficulty selectedDifficulty))
                {
                    int numericValue = (int)selectedDifficulty;
                    difficulty = selectedDifficulty;
                }
            }
        }
    }
}
