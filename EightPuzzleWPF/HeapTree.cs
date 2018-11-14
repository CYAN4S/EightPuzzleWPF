using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EightPuzzleWPF
{
    struct HeapTreeNode<T>
    {
        public T data;
        public int key;
    }

    class HeapTree<T>
    {
        public List<HeapTreeNode<T>> Tree { get; set; }
        public bool isMaxHeap;

        public HeapTree(bool isMaxHeap)
        {
            this.isMaxHeap = isMaxHeap;
            Tree = new List<HeapTreeNode<T>>();
        }

        public void Swap(int a, int b)
        {
            HeapTreeNode<T> tmp = Tree[a];
            Tree[a] = Tree[b];
            Tree[b] = tmp;
        }

        public void Insert(HeapTreeNode<T> node)
        {
            Tree.Add(node);
            for (int i = Tree.Count(); i > 0; i = (i-1)/2)
            {
                bool have2Change = isMaxHeap ? 
                    Tree[(i - 1) / 2].key > Tree[i].key :
                    Tree[(i - 1) / 2].key < Tree[i].key;
                


            }
        }

    }
}
