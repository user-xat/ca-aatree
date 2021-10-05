using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AATree
{
    class AANode<T> where T : IComparable<T>
    {
        public T value { get; set; }
        public int level { get; set; }
        public AANode<T> left { get; set; }
        public AANode<T> right { get; set; }

        public AANode(T value, int level = 1, AANode<T> left = null, AANode<T> right = null)
        {
            this.value = value;
            this.level = level;
            this.left = left;
            this.right = right;
        }
    }
}
