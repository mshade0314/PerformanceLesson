using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PerformanceLesson
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = "";
            var s = Enumerable.Repeat('a', 100);
            var sw = new Stopwatch();
            sw.Start();
            for (var i = 0; i < 10000; i++)
            {
                result += s;
            }
            sw.Stop();
            Console.WriteLine($"string concat:{sw.ElapsedMilliseconds}");
        }
    }
}
