using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EightPuzzleWPF
{
    class BoardGame : Board
    {
        public BoardGame(int row, int col) : base(row, col)
        {

        }

        public void ResetTiles()
        {
            int num = 1;
            int rowSize = Status.Count;
            int colSize = Status[0].Count;

            foreach (var i in Status)
            {
                for (int j = 0; j < colSize; j++)
                {
                    i[j] = num;
                    num++;
                }
            }

            Status[rowSize - 1][colSize - 1] = 0;
            HoleRow = rowSize - 1;
            HoleCol = colSize - 1;

            return;
        }


        public bool MoveTile(Key dir)
        {
            switch (dir)
            {
                case Key.Up:
                    if (HoleRow < Status[0].Count)
                    {
                        Status[HoleRow][HoleCol] = Status[HoleRow + 1][HoleCol];
                        Status[HoleRow + 1][HoleCol] = 0;
                        HoleRow++;
                        return true;
                    }
                    else
                        return false;

                case Key.Down:
                    if (HoleRow < Status[0].Count)
                    {
                        Status[HoleRow][HoleCol] = Status[HoleRow + 1][HoleCol];
                        Status[HoleRow + 1][HoleCol] = 0;
                        HoleRow++;
                        return true;
                    }
                    else
                        return false;

                case Key.Left:
                    if (HoleRow < Status[0].Count)
                    {
                        Status[HoleRow][HoleCol] = Status[HoleRow + 1][HoleCol];
                        Status[HoleRow + 1][HoleCol] = 0;
                        HoleRow++;
                        return true;
                    }
                    else
                        return false;

                case Key.Right:
                    if (HoleRow < Status[0].Count)
                    {
                        Status[HoleRow][HoleCol] = Status[HoleRow + 1][HoleCol];
                        Status[HoleRow + 1][HoleCol] = 0;
                        HoleRow++;
                        return true;
                    }
                    else
                        return false;

                default:
                    return false;

            }
        }

        public void MoveTileWithMouse()
        {

        }

        public void MoveOnceRandom()
        {
            Random random = new Random();
            List<Key> list = new List<Key> { Key.Up, Key.Down, Key.Left, Key.Right };
            for (int i = 4; true; i--)
            {
                Key dir = list[random.Next(0, i)];
                if (MoveTile(dir) == true)
                {
                    return;
                }
            }
        }

        public void ShuffleTiles()
        {
            for (int i = 0; i < 100; i++)
            {
                MoveOnceRandom();
            }
        }

        public void Solve()
        {

        }
    }


}
