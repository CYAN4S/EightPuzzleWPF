using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EightPuzzleWPF
{
    class Board
    {
        public int HoleRow { get; set; }
        public int HoleCol { get; set; }
        public List<List<int>> Status { get; set; }

        public Board(int row, int col)
        {
            HoleRow = row - 1;
            HoleCol = col - 1;
            Status = new List<List<int>>();

            int num = 1;
            for (int i = 0; i < row; i++)
            {
                Status.Add(new List<int>());
                for (int j = 0; j < col; j++)
                {
                    Status[i].Add(num++);
                }
            }

            Status[HoleRow][HoleCol] = 0;
        }

        public int CheckWrongTiles()
        {
            int num = 0, wrong = -1; // 구멍은 제외합니다.
            foreach (var i in Status)
            {
                foreach (int j in i)
                {
                    num++;
                    //if (j == 0)
                    //{
                    //    continue;
                    //}

                    //else if (j != num)
                    //{
                    //    wrong++;
                    //}
                    if (j != num)
                        wrong++;
                }
            }
            return wrong;
        }

        public bool IsSolved()
        {
            int num = 0;
            foreach (var i in Status)
            {
                foreach (int j in i)
                {
                    num++;
                    if (j == 0)
                        continue;
                    else if (j != num)
                        return false;
                }
            }
            return true;
        }

        public bool IsMovable(Key dir)
        {
            switch (dir)
            {
                case Key.Up:
                    if (HoleRow < Status.Count - 1)
                        return true;
                    return false;

                case Key.Down:
                    if (HoleRow > 0)
                        return true;
                    return false;

                case Key.Left:
                    if (HoleCol < Status[0].Count - 1)
                        return true;
                    return false;

                case Key.Right:
                    if (HoleCol > 0)
                        return true;
                    return false;

                default: return false;
            }
        }

        public void MoveTileOnly(Key dir)
        {
            switch (dir)
            {
                case Key.Up:
                    Status[HoleRow][HoleCol] = Status[HoleRow + 1][HoleCol];
                    Status[HoleRow + 1][HoleCol] = 0;
                    HoleRow++;
                    break;

                case Key.Down:
                    Status[HoleRow][HoleCol] = Status[HoleRow - 1][HoleCol];
                    Status[HoleRow - 1][HoleCol] = 0;
                    HoleRow--;
                    break;

                case Key.Left:
                    Status[HoleRow][HoleCol] = Status[HoleRow][HoleCol + 1];
                    Status[HoleRow][HoleCol + 1] = 0;
                    HoleCol++;
                    break;

                case Key.Right:
                    Status[HoleRow][HoleCol] = Status[HoleRow][HoleCol - 1];
                    Status[HoleRow][HoleCol - 1] = 0;
                    HoleCol--;
                    break;

                default:
                    return;
            }
        }

        public Board DeepCopy()
        {
            Board newBoard = new Board(Status.Count, Status[0].Count)
            {
                HoleRow = HoleRow,
                HoleCol = HoleCol
            };

            for (int i = 0; i < Status.Count; i++)
            {
                for (int j = 0; j < Status[0].Count; j++)
                {
                    newBoard.Status[i][j] = Status[i][j];
                }
            }
            return newBoard;
        }


    }


}
