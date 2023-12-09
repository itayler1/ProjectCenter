using gameCenter.Projects._2048game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
using static gameCenter.Projects._2048game.Models.GameModel;

namespace gameCenter.Projects._2048game
{
    /// <summary>
    /// Interaction logic for _2048View.xaml
    /// </summary>
    public partial class _2048View : Window
    {
        private GameModel board;

        public _2048View()
        {
            InitializeComponent();
            InitializeGame();

            this.PreviewKeyDown += Window_KeyDown;
        }

        private void InitializeGame()
        {
            board = new GameModel(4);
            gameOverBorder.Visibility = Visibility.Collapsed;
            SpawnInitialTiles();
            UpdateUI();
        }
        private void SpawnInitialTiles()
        {
            Random rand = new Random();

            board.SpawnTile(rand.Next(board.Size), rand.Next(board.Size), rand.Next(1, 3) * 2);
            board.SpawnTile(rand.Next(board.Size), rand.Next(board.Size), rand.Next(1, 3) * 2);
            UpdateUI();
        }

        public void UpdateUI()
        {
            gameGrid.Children.Clear();

            ScoreText.Text = $"Score: {board.Score}";

            for (int row = 0; row < board.Size; row++)
            {
                for (int col = 0; col < board.Size; col++)
                {
                    TileModel tile = board.Tiles[row, col];

                    if (tile != null)
                    {
                        Border cellBorder = new Border
                        {
                            BorderBrush = new SolidColorBrush(Color.FromArgb(128, 0, 139, 139)),
                            BorderThickness = new Thickness(2),
                        };

                        TextBlock tileText = new TextBlock
                        {
                            Text = tile.Value.ToString(),
                            Background = GetTileColor(tile.Value),
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            TextAlignment = TextAlignment.Center,
                            Padding = new Thickness(50),
                            FontSize = 30,
                            FontWeight = FontWeights.Bold,
                        };

                        cellBorder.Child = tileText;

                        Grid.SetRow(cellBorder, row);
                        Grid.SetColumn(cellBorder, col);

                        gameGrid.Children.Add(cellBorder);
                    }
                    else
                    {
                        Border cellBorder = new Border
                        {
                            BorderBrush = new SolidColorBrush(Color.FromArgb(128, 0, 139, 139)),
                            BorderThickness = new Thickness(2),
                        };
                        Grid.SetRow(cellBorder, row);
                        Grid.SetColumn(cellBorder, col);

                        gameGrid.Children.Add(cellBorder);
                    }
                }
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            if (board.gameWon)
            {
                board.gameWon = true;
                gameOverBorder.Visibility = Visibility.Visible;
                gameOverText.Text = "You Won!";
            }

            if (board.IsGameOver())
            {
                board.gameOver = true;
                gameOverBorder.Visibility = Visibility.Visible;
                gameOverText.Text = "Game Over";
                return;
            }

            switch (e.Key)
            {
                case Key.Up:
                    board.MoveTiles(GameModel.Direction.Up);
                    break;
                case Key.Down:
                    board.MoveTiles(GameModel.Direction.Down);
                    break;
                case Key.Left:
                    board.MoveTiles(GameModel.Direction.Left);
                    break;
                case Key.Right:
                    board.MoveTiles(GameModel.Direction.Right);
                    break;
            }

            UpdateUI();
        }

        private SolidColorBrush GetTileColor(int value)
        {

            return new SolidColorBrush(value switch
            {
                2 => Colors.LightGray,
                4 => Colors.LightGreen,
                8 => Colors.YellowGreen,
                16 => Colors.LightYellow,
                32 => Colors.Yellow,
                64 => Colors.Orange,
                128 => Colors.PaleVioletRed,
                256 => Colors.MediumVioletRed,
                512 => Colors.Red,
                1024 => Colors.DarkOrange,
                2048 => Colors.OrangeRed,

                _ => Colors.LightGray,
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            board.gameOver = false;
            InitializeGame();
        }
    }
}
