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
        public Board board;
        public List<Key> path;
        public int heur;

        public BoardNode(Board board, List<Key> path, int heur)
        {
            this.board = board;
            this.path = path;
            this.heur = heur;
        }

        // 동적 할당을 해줍니다.
        public BoardNode Copy()
        {
            BoardNode result = new BoardNode
            {
                board = Board.DeepCopy(board),
                path = new List<Key>(path),
                heur = heur
            };
            return result;
        }
    }

    // 최소 힙 트리
    class PriorityQueue4AStar
    {
        // 완전 이진 트리
        public List<BoardNode> Tree { get; set; }

        public PriorityQueue4AStar()
        {
            // 완전 이진 트리의 구현
            Tree = new List<BoardNode>
            {
                new BoardNode() 
                // 계산의 편의를 위해 0번 인덱스를 채움
            };
        }

        public void Enqueue(BoardNode node)
        {
            Tree.Add(node);
            for (int i = Tree.Count() - 1; i > 1; i /= 2)
            {
                if (Tree[i / 2].heur > Tree[i].heur)
                {
                    Swap(Tree, i / 2, i);
                }
                else
                    return;
            }
        }

        public BoardNode Dequeue()
        {
            int size = Tree.Count();
            BoardNode tmp;

            if (size < 2)
                return new BoardNode(null, null, -1);

            tmp = Tree[1].Copy();

            if (size == 2)
            {
                Tree.RemoveAt(1);
                return tmp;
            }

            // 맨 뒤의 노드 끌어오기
            Tree[1] = Tree[size - 1];
            Tree.RemoveAt(size - 1);
            size--;
            

            int index = 1;
            while (true)
            {
                if (index * 2 + 1 > size)
                {
                    return tmp;
                }
                else if (index * 2 + 2 <= size)
                {
                    int smaller = (Tree[index * 2].heur > Tree[index * 2 + 1].heur) ? (index * 2 + 1) : (index * 2);
                    BoardNode smallerNode = Tree[smaller];
                    if (Tree[index].heur >= Tree[smaller].heur)
                    {
                        Swap(Tree, index, smaller);
                    }
                    else
                    {
                        return tmp;
                    }
                    index = smaller;
                }
                else // if (index * 2 + 1 == size)
                {
                    if (Tree[index].heur >= Tree[index * 2].heur)
                    {
                        Swap(Tree, index, index * 2);
                    }
                    else
                    {
                        return tmp;
                    }
                    index *= 2;
                }
            }
        }

        public static void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        // 동적 할당을 해줍니다.
        public BoardNode Find(BoardGame game)
        {
            BoardNode answer;
            List<Key> keys = new List<Key> { Key.Up, Key.Down, Key.Left, Key.Right };
            List<Key> reverse = new List<Key> { Key.Down, Key.Up, Key.Right, Key.Left };

            BoardNode first = new BoardNode(game, new List<Key>(), Board.CheckWrongTiles(game));
            Enqueue(first);

            while (Tree.Count > 1)
            {
                BoardNode pop = Dequeue();
                if (Board.IsSolved(pop.board))
                {
                    answer = pop.Copy();
                    return answer;
                }

                for (int i = 0; i < 4; i++)
                {
                    if (pop.path.Count > 0)
                    {
                        if (pop.path[pop.path.Count - 1] == reverse[i])
                            continue;
                    }
                    if (Board.IsMovable(pop.board, keys[i]) == true)
                    {
                        BoardNode moved = pop.Copy();
                        Board.MoveTileOnly(moved.board, keys[i]);
                        moved.path.Add(keys[i]);
                        moved.heur = moved.path.Count + Board.CheckWrongTiles(moved.board);
                        Enqueue(moved);
                    }
                }
                
            }
            return new BoardNode(null, null, -1);
        }
    }
}
