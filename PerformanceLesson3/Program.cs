using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace PerformanceLesson3
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance); // memo: Shift-JISを扱うためのおまじない            

            // 課題: bkdata.csvのaddr_kennoとaddr_sinoをm_ken.ken_name+m_si.si_nameに置換して画面に表示する
            // 出力例: 910504,×××サンシティつくば,宮城県仙台市泉区
            
            var sw = new Stopwatch();
            sw.Start();
            var sb = new StringBuilder();
            using var sr = new StreamReader("bkdata.csv", Encoding.GetEncoding("Shift_JIS"));
            sr.ReadLine(); // ヘッダを読み飛ばす
            while (sr.Peek() != -1)
            {
                var line = sr.ReadLine();
                var items = line.Split(",");
                (int bkNo, string bkName, int ken_no, int si_no) bk = (int.Parse(items[0]), items[1], int.Parse(items[2]), int.Parse(items[3]));
                sb.AppendLine($"{bk.bkNo},{bk.bkName},{GetKenName(bk.ken_no) + GetSiName(bk.si_no)}");
            }
            sr.Close();
            sw.Stop();
            Console.WriteLine(sb.ToString());
            Console.WriteLine($"Lesson3:{sw.ElapsedMilliseconds}");
        }

        static string GetKenName(int kenNo)
        {
            var result = "";
            using var sr = new StreamReader("m_ken.csv", Encoding.GetEncoding("Shift_JIS"));
            sr.ReadLine();
            while (sr.Peek() != -1)
            {
                var line = sr.ReadLine();
                if (line.Split(",")[0] == kenNo.ToString())
                {
                    result = line.Split(",")[1];
                    break;
                }
            }
            sr.Close();
            return result;
        }

        static string GetSiName(int siNo)
        {
            var result = "";
            using var sr = new StreamReader("m_si.csv", Encoding.GetEncoding("Shift_JIS"));
            sr.ReadLine();
            while (sr.Peek() != -1)
            {
                var line = sr.ReadLine();
                if (line.Split(",")[0] == siNo.ToString())
                {
                    result = line.Split(",")[1];
                    break;
                }
            }
            sr.Close();
            return result;
        }
    }
}
