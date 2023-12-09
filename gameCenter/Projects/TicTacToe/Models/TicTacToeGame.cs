using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCenter.Projects.TicTacToe.Models
{
    internal class TicTacToeGame
    {
        public char _currentPlayer { get; set; }
        public char[,] _gameBoard { get; set; }

        public TicTacToeGame()
        {
            _currentPlayer = 'X';
            _gameBoard = new char[3, 3];
        }


        public bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (_gameBoard[i, j] == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool CheckForWin()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_gameBoard[i, 0] == _currentPlayer && _gameBoard[i, 1] == _currentPlayer && _gameBoard[i, 2] == _currentPlayer)
                    return true;
            }

            for (int i = 0; i < 3; i++)
            {
                if (_gameBoard[0, i] == _currentPlayer && _gameBoard[1, i] == _currentPlayer && _gameBoard[2, i] == _currentPlayer)
                    return true;
            }

            if (_gameBoard[0, 0] == _currentPlayer && _gameBoard[1, 1] == _currentPlayer && _gameBoard[2, 2] == _currentPlayer)
                return true;

            if (_gameBoard[0, 2] == _currentPlayer && _gameBoard[1, 1] == _currentPlayer && _gameBoard[2, 0] == _currentPlayer)
                return true;

            return false;
        }
    }
}
