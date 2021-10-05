using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AATree
{
    class AATree<T> where T : IComparable<T>
    {
        private AANode<T> root;

        public AATree()
        {
            
        }

        // Устранение левого горизонтального ребра
        private static AANode<T> skew(AANode<T> node)
        {
            if (node == null)
                return null;
            if (node.left == null)
                return node;
            if (node.left.level == node.level)
                return new AANode<T>(node.left.value,
                                     node.left.level,
                                     node.left.left,
                                     new AANode<T>(node.value, node.level, node.left.right, node.right));
            return node;
        }

        // Устранение двух последовательных правых ребер
        private static AANode<T> split(AANode<T> node)
        {
            if (node == null)
                return null;
            if (node.right == null || node.right.right == null)
                return node;
            if (node.level == node.right.right.level)
                return new AANode<T>(node.right.value,
                                     node.right.level + 1,
                                     new AANode<T>(node.value, node.level, node.left, node.right.left),
                                     node.right.right);
            return node;
        }

        private static AANode<T> rebalanceOnInsert(AANode<T> node)
        {
            node = skew(node);
            node = split(node);
            return node;
        }

        private static AANode<T> insertSub(T value, AANode<T> node)
        {
            if (node == null)
                return new AANode<T>(value);
            if (node.value.CompareTo(value) > 0)
                node.left = insertSub(value, node.left);
            else
                node.right = insertSub(value, node.right);

            node = rebalanceOnInsert(node);

            return node;
        }

        public void Insert(T value)
        {
            root = insertSub(value, root);
        }

        private static AANode<T> successor(AANode<T> node)
        {
            if (node.left == null)
                return node;
            return successor(node.left);
        }

        private static AANode<T> predecessor(AANode<T> node)
        {
            if (node.right == null)
                return node;
            return predecessor(node.right);
        }

        private static AANode<T> decreaseLevel(AANode<T> node)
        {
            int shouldBe;
            if (node.left != null)
                shouldBe = node.left.level + 1;
            else if (node.right != null)
                shouldBe = node.right.level;
            else
                shouldBe = 1;

            if (shouldBe < node.level)
            {
                node.level = shouldBe;
                if (node.right != null && shouldBe < node.right.level)
                    node.right.level = shouldBe;
            }
            return node;
        }

        private static AANode<T> rebalanceOnRemove(AANode<T> node)
        {
            node = decreaseLevel(node);
            node = skew(node);
            node.right = skew(node.right);
            if (node.right != null)
                node.right.right = skew(node.right.right);
            node = split(node);
            node.right = split(node.right);
            return node;
        }

        private static AANode<T> deleteSub(T value, AANode<T> node)
        {
            if (node == null)
                return null;
            
            if (node.value.CompareTo(value) > 0)
                node.left = deleteSub(value, node.left);
            else if (node.value.CompareTo(value) < 0)
                node.right = deleteSub(value, node.right);
            else
            {   // Если искомый элемент является листом
                if (node.left == null && node.right == null)
                    return null;
                else if (node.right == null)
                {
                    AANode<T> leaf = predecessor(node.left);
                    node.value = leaf.value;
                    node.left = deleteSub(leaf.value, node.left);
                }
                else
                {
                    AANode<T> leaf = successor(node.right);
                    node.value = leaf.value;
                    node.right = deleteSub(leaf.value, node.right);
                }
            }
            node = rebalanceOnRemove(node);
            return node;
        }

        public void Delete(T value)
        {
            root = deleteSub(value, root);
        }

        private static AANode<T> findSub(T value, AANode<T> node)
        {
            if (node == null)
                return null;
            if (node.value.CompareTo(value) > 0)
                return findSub(value, node.left);
            if (node.value.CompareTo(value) < 0)
                return findSub(value, node.right);
            return node;
        }

        public AANode<T> Find(T value)
        {
            return findSub(value, root);
        }

        private static void printSub(AANode<T> node, int level = 1)
        {
            if (node == null)
                return;
            if (node.left != null)
                printSub(node.left);
            Console.Write(node.value + " ");
            if (node.right != null)
                printSub(node.right);
        }

        public void Print()
        {
            printSub(root);
            Console.WriteLine();
        }
    }
}
