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
                    if (HoleRow < Status.Count - 1)
                    {
                        Status[HoleRow][HoleCol] = Status[HoleRow + 1][HoleCol];
                        Status[HoleRow + 1][HoleCol] = 0;
                        HoleRow++;
                        MainWindow.movedTime++;
                        return true;
                    }
                    else
                        return false;

                case Key.Down:
                    if (HoleRow > 0)
                    {
                        Status[HoleRow][HoleCol] = Status[HoleRow - 1][HoleCol];
                        Status[HoleRow - 1][HoleCol] = 0;
                        HoleRow--;
                        MainWindow.movedTime++;
                        return true;
                    }
                    else
                        return false;

                case Key.Left:
                    if (HoleCol < Status[0].Count - 1)
                    {
                        Status[HoleRow][HoleCol] = Status[HoleRow][HoleCol + 1];
                        Status[HoleRow][HoleCol + 1] = 0;
                        HoleCol++;
                        MainWindow.movedTime++;
                        return true;
                    }
                    else
                        return false;

                case Key.Right:
                    if (HoleCol > 0)
                    {
                        Status[HoleRow][HoleCol] = Status[HoleRow][HoleCol - 1];
                        Status[HoleRow][HoleCol - 1] = 0;
                        HoleCol--;
                        MainWindow.movedTime++;
                        return true;
                    }
                    else
                        return false;

                default:
                    return false;

            }
        }

        public void MoveTileWithMouse(int tag)
        {
            // int rowSize = Status.Count;
            int colSize = Status[0].Count;
            int buttonRow = tag / colSize;
            int buttonCol = tag % colSize;

            if (buttonRow == HoleRow)
            {
                if (buttonCol > HoleCol)
                {
                    for (int i = buttonCol - HoleCol; i > 0; i--)
                        MoveTile(Key.Left);
                }
                else // if (buttonCol < HoleCol)
                {
                    for (int i = HoleCol - buttonCol; i > 0; i--)
                        MoveTile(Key.Right);
                }
            }
            else if (buttonCol == HoleCol)
            {
                if (buttonRow > HoleRow)
                {
                    for (int i = buttonRow - HoleRow; i > 0; i--)
                        MoveTile(Key.Up);
                }
                else // if (buttonRow < HoleRow)
                {
                    for (int i = HoleRow - buttonRow; i > 0; i--)
                        MoveTile(Key.Down);
                }
            }
        }

        public void MoveOnceRandom()
        {
            Random random = new Random();
            List<Key> list = new List<Key> { Key.Up, Key.Down, Key.Left, Key.Right };
            for (int i = 4; i > 0; i--)
            {
                int ran = random.Next(0, i);
                Key dir = list[ran];
                list.RemoveAt(ran);
                if (MoveTile(dir) == true)
                {
                    return;
                }
            }
        }

        public List<Key> Solve()
        {
            PriorityQueue4AStar pq = new PriorityQueue4AStar();
            BoardNode answer = pq.Find(this);
            return answer.path;
        }
    }


}
