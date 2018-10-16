using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int rowSize = _status.Count();
            int colSize = _status[0].Count();

            foreach (var i in _status)
            {
                for (int j = 0; j < colSize; j++)
                {
                    i[j] = num;
                    num++;
                }
            }

            _status[rowSize - 1][colSize - 1] = 0;
            HoleRow = rowSize - 1;
            HoleCol = colSize - 1;

            return;
        }
    }

    
}
