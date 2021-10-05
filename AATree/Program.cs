using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AATree
{
    class Program
    {
        static void Main(string[] args)
        {
            int N = 50;
            Random rand = new Random();
            AATree<double> root = new AATree<double>();
            for (int i = 0; i < N; i++)
            {
                root.Insert(rand.Next(1, 500));
            }
            root.Print();
            double num;
            do
            {
                Console.Write("Введите удаляемое значение: ");
                num = Convert.ToDouble(Console.ReadLine());
                root.Delete(num);
                //root.Print();
            } while (num >= 0);
            do
            {
                Console.Write("Введите искомое значение: ");
                num = Convert.ToInt32(Console.ReadLine());
                var node = root.Find(num);
                Console.WriteLine($"Value: {node?.value}\nLevel: {node?.level}\nLeft: {node?.left}\nRight: {node?.right}");
            } while (num >= 0);
            Console.ReadKey();
        }
    }
}
