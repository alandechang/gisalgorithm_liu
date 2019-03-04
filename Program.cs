using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 跳马练习
{
    class Program
    {
        static void Main(string[] args)
        {
            Vault v = new Vault(4, 3);
            IEnumerable<Vault.Node[]> paths = v.Run(2, 2);
            int count = 0;
            foreach(Vault.Node[] path in paths)
            {
                count++;
                System.Console.WriteLine("Path{0}:", count);
                for(int i=0;i<path.Length;i++)
                {
                    Vault.Node n = path[i];
                    if (i == path.Length - 1)
                    {
                        Console.WriteLine("({0},{1},{2})", n.Y, n.X, n.Operater);
                    }
                    else
                    {
                        Console.Write("({0},{1},{2})->", n.Y, n.X, n.Operater);
                    }
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
