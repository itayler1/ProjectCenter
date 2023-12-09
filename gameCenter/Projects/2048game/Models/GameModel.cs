using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace gameCenter.Projects._2048game.Models
{
    internal class GameModel
    {
        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
        public int Score {  get; set; }
        public bool gameOver {  get; set; }
        public bool gameWon { get; set; }
        public int Size { get; private set; }
        public TileModel[,] Tiles { get; private set; }
        public GameModel(int size) 
        {
            Score = 0;
            gameOver = false;
            gameWon = false;
            Size = size;
            Tiles = new TileModel[size, size];
        }


        public void SpawnTile(int row, int column, int value)
        {
            if (Tiles[row, column] == null)
            {
                Tiles[row, column] = new TileModel(value);
            }
            else
            {
                Random rand = new Random();
                SpawnTile(rand.Next(Size), rand.Next(Size), value);
            }
        }

        public void MoveTiles(Direction direction)
        {
            int startRow, endRow, startCol, endCol, stepRow, stepCol;

            switch (direction)
            {
                case Direction.Up:
                    startRow = 1;
                    endRow = Size - 1;
                    startCol = 0;
                    endCol = Size - 1;
                    stepRow = 1;
                    stepCol = 1;
                    break;
                case Direction.Down:
                    startRow = Size - 2;
                    endRow = 0;
                    startCol = 0;
                    endCol = Size - 1;
                    stepRow = -1;
                    stepCol = 1;
                    break;
                case Direction.Left:
                    startRow = 0;
                    endRow = Size - 1;
                    startCol = 1;
                    endCol = Size - 1;
                    stepRow = 1;
                    stepCol = 1;
                    break;
                case Direction.Right:
                    startRow = 0;
                    endRow = Size - 1;
                    startCol = Size - 2;
                    endCol = 0;
                    stepRow = 1;
                    stepCol = -1;
                    break;
                default:
                    return;
            }

            bool validMove = false;

            for (int row = startRow; row != endRow + stepRow; row += stepRow)
            {
                for (int col = startCol; col != endCol + stepCol; col += stepCol)
                {
                    TileModel tile = Tiles[row, col];

                    if (tile != null)
                    {
                        switch (direction)
                        {
                            case Direction.Up:
                                if (IsValidMoveUp(tile, row, col))
                                {
                                    validMove = true;
                                    break;
                                }
                                break;
                            case Direction.Down:
                                if (IsValidMoveDown(tile, row, col))
                                {
                                    validMove = true;
                                    break;
                                }
                                break;
                            case Direction.Left:
                                if (IsValidMoveLeft(tile, row, col))
                                {
                                    validMove = true;
                                    break;
                                }
                                break;
                            case Direction.Right:
                                if (IsValidMoveRight(tile, row, col))
                                {
                                    validMove = true;
                                    break;
                                }
                                break;
                        }
                    }
                }

                if (validMove)
                {
                    break;
                }
            }

            if (!validMove) return;

            for (int row = startRow; row != endRow + stepRow; row += stepRow)
            {
                for (int col = startCol; col != endCol + stepCol; col += stepCol)
                {
                    TileModel tile = Tiles[row, col];

                    if (tile != null && validMove)
                    {
                        switch (direction)
                        {
                            case Direction.Up:
                                MoveTileUp(tile, row, col);
                                break;
                            case Direction.Down:
                                MoveTileDown(tile, row, col);
                                break;
                            case Direction.Left:
                                MoveTileLeft(tile, row, col);
                                break;
                            case Direction.Right:
                                MoveTileRight(tile, row, col);
                                break;
                        }
                    }
                }
            }

            Random rand = new Random();
            SpawnTile(rand.Next(Size), rand.Next(Size), rand.Next(1, 3) * 2);
        }

        private bool IsValidMoveUp(TileModel tile, int row, int col)
        {
            int targetRow = row - 1;

            return targetRow >= 0 && (Tiles[targetRow, col] == null || Tiles[targetRow, col].Value == tile.Value);
        }

        private bool IsValidMoveDown(TileModel tile, int row, int col)
        {
            int targetRow = row + 1;

            return targetRow < Size && (Tiles[targetRow, col] == null || Tiles[targetRow, col].Value == tile.Value);
        }

        private bool IsValidMoveLeft(TileModel tile, int row, int col)
        {
            int targetCol = col - 1;

            return targetCol >= 0 && (Tiles[row, targetCol] == null || Tiles[row, targetCol].Value == tile.Value);
        }

        private bool IsValidMoveRight(TileModel tile, int row, int col)
        {
            int targetCol = col + 1;

            return targetCol < Size && (Tiles[row, targetCol] == null || Tiles[row, targetCol].Value == tile.Value);
        }

        private void MoveTileUp(TileModel tile, int row, int col)
        {
            int targetRow = row - 1;

            if (targetRow >= 0 && (Tiles[targetRow, col] == null || Tiles[targetRow, col].Value == tile.Value))
            {
                while (targetRow >= 0 && (Tiles[targetRow, col] == null || Tiles[targetRow, col].Value == tile.Value))
                {
                    if (Tiles[targetRow, col] == null)
                    {
                        Tiles[targetRow, col] = tile;
                        Tiles[row, col] = null;
                        row = targetRow;
                        targetRow--;
                    }
                    else if (Tiles[targetRow, col].Value == tile.Value)
                    {
                        MergeTiles(tile, targetRow, col, Direction.Up);
                        break;
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void MoveTileDown(TileModel tile, int row, int col)
        {
            int targetRow = row + 1;

            if (targetRow < Size && (Tiles[targetRow, col] == null || Tiles[targetRow, col].Value == tile.Value))
            {
                while (targetRow < Size && (Tiles[targetRow, col] == null || Tiles[targetRow, col].Value == tile.Value))
                {
                    if (Tiles[targetRow, col] == null)
                    {
                        Tiles[targetRow, col] = tile;
                        Tiles[row, col] = null;
                        row = targetRow;
                        targetRow++;
                    }
                    else if (Tiles[targetRow, col].Value == tile.Value)
                    {
                        MergeTiles(tile, targetRow, col, Direction.Down);
                        break;
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void MoveTileLeft(TileModel tile, int row, int col)
        {
            int targetCol = col - 1;

            if (targetCol >= 0 && (Tiles[row, targetCol] == null || Tiles[row, targetCol].Value == tile.Value))
            {
                while (targetCol >= 0 && (Tiles[row, targetCol] == null || Tiles[row, targetCol].Value == tile.Value))
                {
                    if (Tiles[row, targetCol] == null)
                    {
                        Tiles[row, targetCol] = tile;
                        Tiles[row, col] = null;
                        col = targetCol;
                        targetCol--;
                    }
                    else if (Tiles[row, targetCol].Value == tile.Value)
                    {
                        MergeTiles(tile, row, targetCol, Direction.Left);
                        break;
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void MoveTileRight(TileModel tile, int row, int col)
        {
            int targetCol = col + 1;

            if (targetCol < Size && (Tiles[row, targetCol] == null || Tiles[row, targetCol].Value == tile.Value))
            {
                while (targetCol < Size && (Tiles[row, targetCol] == null || Tiles[row, targetCol].Value == tile.Value))
                {
                    if (Tiles[row, targetCol] == null)
                    {
                        Tiles[row, targetCol] = tile;
                        Tiles[row, col] = null;
                        col = targetCol;
                        targetCol++;
                    }
                    else if (Tiles[row, targetCol].Value == tile.Value)
                    {
                        MergeTiles(tile, row, targetCol, Direction.Right);
                        break;
                    }
                }
            }
            else
            {
                return;
            }
        }

        private void MergeTiles(TileModel tile, int row, int col, Direction direction)
        {
            tile.Value *= 2;
            Score += tile.Value;
            Tiles[row, col] = tile;

            switch (direction)
            {
                case Direction.Up:
                    Tiles[row + 1, col] = null;
                    break;
                case Direction.Down:
                    Tiles[row - 1, col] = null;
                    break;
                case Direction.Left:
                    Tiles[row, col + 1] = null;
                    break;
                case Direction.Right:
                    Tiles[row, col - 1] = null;
                    break;
            }
        }


        public bool IsGameOver()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (Tiles[row, col] == null ||
                        (row > 0 && Tiles[row - 1, col] != null && Tiles[row - 1, col].Value == Tiles[row, col].Value) ||
                        (row < Size - 1 && Tiles[row + 1, col] != null && Tiles[row + 1, col].Value == Tiles[row, col].Value) ||
                        (col > 0 && Tiles[row, col - 1] != null && Tiles[row, col - 1].Value == Tiles[row, col].Value) ||
                        (col < Size - 1 && Tiles[row, col + 1] != null && Tiles[row, col + 1].Value == Tiles[row, col].Value))
                    {
                        return false; 
                    }
                }
            }
            return true; 
        }

        public bool IsGameWon()
        {
            for (int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++)
                {
                    if (Tiles[row,col].Value == 2048)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
