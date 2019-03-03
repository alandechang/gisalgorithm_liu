using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace 最佳工作序列
{
    class Program
    {
        static List<int> Require = new List<int>();//费时
        static List<int> Limit = new List<int>();//最后完成期限
        static List<int> sum = new List<int>();//前面n个任务的费时和
        static List<double> valueAverage = new List<double>();//价值
        static List<int> order = new List<int>();//最佳序列
        static string[] readText;


       
        static int Search(int[] Limit, int value)
        {
            int i = -1;
            foreach (int t in Limit)
            {
                i++;
                if (t == value)
                    return i;
            }
            return i;
        }

      
        static void Con(int[] order, int nwork, int[] sum, double[] valueAverage, int[] RequireTime, int[] Limit)
        {
           
            if (valueAverage[order[order.Length - 2]] < valueAverage[nwork])
            {
                
                if (sum[sum.Length - 1] < Limit[order[order.Length - 2]])
                {
                    int temp = order[order.Length - 2];
                    order[order.Length - 1] = temp;
                    order[order.Length - 2] = nwork;

                    if (sum.Length < 3)
                    {
                        int s = sum[sum.Length - 2];
                        sum[sum.Length - 2] = RequireTime[nwork];
                        sum[sum.Length - 1] = s;
                    }
                    else
                    {
                        int s = sum[sum.Length - 2] - sum[sum.Length - 3];
                        sum[sum.Length - 2] = sum[sum.Length - 3] + RequireTime[nwork];
                        sum[sum.Length - 1] = sum[sum.Length - 2] + s;
                    }

                    if (order.Length > 2)
                    {
                        int[] torder = new int[order.Length - 1];
                        int[] tsum = new int[sum.Length - 1];
                        for (int j = 0; j < order.Length - 1; j++)
                        {
                            torder[j] = order[j];
                            tsum[j] = sum[j];
                        }

                        Con(torder, nwork, tsum, valueAverage, RequireTime, Limit);

                        for (int j = 0; j < torder.Length; j++)
                        {
                            order[j] = torder[j];
                            sum[j] = tsum[j];
                        }
                    }
                }
            }
        }


        public static void read()
        {
            StreamReader sw = new StreamReader(@"D:\Bruce\class2second\地理信息系统算法基础\跳马\dataoftest.txt");
            string st = string.Empty;
            int m = 0;
            while (!sw.EndOfStream)
            {
                m++;
                string s = sw.ReadLine();
                Console.WriteLine(s);
            }
            Console.WriteLine("");

            readText = File.ReadAllLines(@"D:\Bruce\class2second\地理信息系统算法基础\跳马\dataoftest.txt");

            try
            {

                for (int i = 0; i < m; i++)
                {
                    string data = Convert.ToString(readText[i]);

                    if (i != 0)
                    {
                        data = data.Substring(data.IndexOf('\t') + 1, data.Length - 1 - data.IndexOf('\t'));
                        string d;
                        d = data.Substring(0, data.IndexOf('\t'));
                        Require.Add(int.Parse(d));
                        data = data.Substring(data.IndexOf('\t') + 1, data.Length - 1 - data.IndexOf('\t'));
                        d = data.Substring(0, data.IndexOf("\t"));
                        Limit.Add(int.Parse(d));
                        d = data.Substring(data.IndexOf('\t') + 1, data.Length - 1 - data.IndexOf('\t'));
                        double x = Convert.ToDouble(int.Parse(d)) / Convert.ToDouble((Require[Require.Count - 1]));
                        valueAverage.Add(x);
                    }

                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("文件读取错误！");
                Console.WriteLine(ex.Message);
            }
        }


        static void Main(string[] args)
        {
            
            read();


            int[] array;
            array = new int[Limit.Count];
            double[] valueAver = new double[Limit.Count];
            int[] limit = new int[Limit.Count];
            Limit.CopyTo(limit, 0);

            for (int i = 0; i < Limit.Count; i++)
            {
                valueAver[i] = valueAverage[i];
            }


            while (order.Count < Limit.Count)
            {
               
                int i = Search(limit, limit.Min());

                limit[i] = 10000;
                order.Add(i);

                if (order.Count > 1)
                {
                    array = order.ToArray();
                    Con(array, i, sum.ToArray(), valueAver, Require.ToArray(), Limit.ToArray());
                    order = array.ToList();
                }
                else if (order.Count < 2)
                {
                    sum.Add(sum.Sum() + Require[i]);
                }
                else
                {
                    sum.Add(sum[sum.Count - 1] + Require[i]);
                }
            }

            
            Console.WriteLine("最佳工作序列为：");
            foreach (int i in order)
            {
                Console.Write("{0} ", i + 1);
            }
            Console.ReadLine();
        }
    }
}

