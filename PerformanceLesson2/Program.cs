using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceLesson2
{
    class Program
    {
        static void Main(string[] args)
        {
            // ランダムな数値が格納されている要素数100000の2つの配列の同じ数字だけを表示する
            var num1 = new int[100000];
            var num2 = new int[100000];
            for (var i = 0; i < 100000; i++)
            {
                num1[i] = new Random().Next(1, 100000);
                num2[i] = new Random().Next(1, 100000);
            }
            var results = new List<int>();
            
            var sw = new Stopwatch();
            sw.Start();
            foreach (var x in num1)
            {
                foreach (var y in num2)
                {
                    if (x == y) results.Add(x);
                }
            }
            Console.WriteLine(string.Join(",", results.Distinct().OrderBy(x => x).Take(10)));
            sw.Stop();
            Console.WriteLine($"item search:{sw.ElapsedMilliseconds}");
        }
    }
}
