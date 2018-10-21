using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EightPuzzleWPF
{
    struct BoardNode
    {
        BoardGame boardGame;
        List<Key> path;
        int heur;
    }

    // Min Heap.
    class PriorityQueue4AStar
    {
        public List<BoardNode> Tree { get; set; }

        public PriorityQueue4AStar()
        {
            Tree = new List<BoardNode>
            {
                new BoardNode()
            };
        }

        public void Enqueue(BoardNode node)
        {

        }

        //public BoardNode Dequeue()
        //{

        //}

    }
}
