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

        public static int CheckWrongTiles(Board board)
        {
            int num = 0, wrong = -1; // 구멍은 제외합니다.
            foreach (var i in board.Status)
            {
                foreach (int j in i)
                {
                    num++;
                    if (j != num)
                        wrong++;
                }
            }
            return wrong;
        }

        public static bool IsSolved(Board board)
        {
            int num = 0;
            foreach (var i in board.Status)
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

        public static bool IsMovable(Board board, Key dir)
        {
            switch (dir)
            {
                case Key.Up:
                    if (board.HoleRow < board.Status.Count - 1)
                        return true;
                    return false;

                case Key.Down:
                    if (board.HoleRow > 0)
                        return true;
                    return false;

                case Key.Left:
                    if (board.HoleCol < board.Status[0].Count - 1)
                        return true;
                    return false;

                case Key.Right:
                    if (board.HoleCol > 0)
                        return true;
                    return false;

                default:
                    return false;
            }
        }

        public static void MoveTileOnly(Board board, Key dir)
        {
            switch (dir)
            {
                case Key.Up:
                    board.Status[board.HoleRow][board.HoleCol] = board.Status[board.HoleRow + 1][board.HoleCol];
                    board.Status[board.HoleRow + 1][board.HoleCol] = 0;
                    board.HoleRow++;
                    break;

                case Key.Down:
                    board.Status[board.HoleRow][board.HoleCol] = board.Status[board.HoleRow - 1][board.HoleCol];
                    board.Status[board.HoleRow - 1][board.HoleCol] = 0;
                    board.HoleRow--;
                    break;

                case Key.Left:
                    board.Status[board.HoleRow][board.HoleCol] = board.Status[board.HoleRow][board.HoleCol + 1];
                    board.Status[board.HoleRow][board.HoleCol + 1] = 0;
                    board.HoleCol++;
                    break;

                case Key.Right:
                    board.Status[board.HoleRow][board.HoleCol] = board.Status[board.HoleRow][board.HoleCol - 1];
                    board.Status[board.HoleRow][board.HoleCol - 1] = 0;
                    board.HoleCol--;
                    break;

                default:
                    return;
            }
        }

        public static Board DeepCopy(Board board)
        {
            Board newBoard = new Board(board.Status.Count, board.Status[0].Count)
            {
                HoleRow = board.HoleRow,
                HoleCol = board.HoleCol
            };

            for (int i = 0; i < board.Status.Count; i++)
            {
                for (int j = 0; j < board.Status[0].Count; j++)
                {
                    newBoard.Status[i][j] = board.Status[i][j];
                }
            }
            return newBoard;
        }


    }


}
