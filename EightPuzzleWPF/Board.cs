using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int num = 0, wrong = 0;
            foreach (var i in Status)
            {
                foreach (int j in i)
                {
                    num++;
                    if (j == 0)
                    {
                        continue;
                    }
                    else if (j != num)
                    {
                        wrong++;
                    }
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
                    {
                        continue;
                    }
                    else if (j != num)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        
    }


}
