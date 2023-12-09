using gameCenter.Projects.TicTacToe.Models;
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

namespace gameCenter.Projects.TicTacToe
{
    /// <summary>
    /// Interaction logic for TicTacToeView.xaml
    /// </summary>
    public partial class TicTacToeView : Window
    {

        TicTacToeGame game = new();

        public TicTacToeView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button != null && string.IsNullOrEmpty(button.Content as string))
            {
                button.Content = game._currentPlayer.ToString();
                int row = Grid.GetRow(button);
                int column = Grid.GetColumn(button);
                game._gameBoard[row, column] = game._currentPlayer;

                if (game.CheckForWin())
                {
                    MessageBox.Show($"{game._currentPlayer} wins!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetGame();
                }
                else
                {
                    if (game.IsBoardFull())
                    {
                        MessageBox.Show("It's a draw!", "Game Over", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetGame();
                    }
                    else
                    {
                        game._currentPlayer = game._currentPlayer == 'X' ? 'O' : 'X';
                    }
                }
            }
        }

       

        private void ResetGame()
        {
            game = new TicTacToeGame();

            foreach (Button button in MainGrid.Children)
            {
                button.Content = "";
            }
        }
        
    }
}