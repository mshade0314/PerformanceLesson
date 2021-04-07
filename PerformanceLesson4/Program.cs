using System;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PerformanceLesson4
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var sw = new Stopwatch();
            sw.Start();
            var sb = new StringBuilder();
            using var con = new SQLiteConnection("Data Source=mydb.sqlite;Version=3;");
            con.Open();
            string sql = @"SELECT * FROM bkdata;";
            SQLiteCommand cmd = new SQLiteCommand(sql, con);
            using var rdr = await cmd.ExecuteReaderAsync();
            while (rdr.Read())
            {
                (int bkNo, string bkName, int ken_no, int si_no) bk =
                    (int.Parse(rdr["bk_no"].ToString()),
                    rdr["bk_name"].ToString(),
                    int.Parse(rdr["addr_kenno"].ToString()),
                    int.Parse(rdr["addr_sino"].ToString()));
                var kenName = await GetKenNameAsync(con, bk.ken_no);
                var siName = await GetSiNameAsync(con, bk.si_no);
                sb.AppendLine($"{bk.bkNo},{bk.bkName},{kenName + siName}");
            }
            rdr.Close();
            con.Close();
            sw.Stop();
            Console.WriteLine(sb.ToString());
            Console.WriteLine($"Lesson4:{sw.ElapsedMilliseconds}");
        }

        private static async Task<string> GetKenNameAsync(SQLiteConnection con, int kenNo)
        {
            using var cmd = con.CreateCommand();
            string sql = @"
                SELECT
                  m_ken.ken_name
                FROM
                  m_ken 
                WHERE
                  m_ken.ken_no = $ken";
            cmd.CommandText = sql;
            var kenParm = cmd.CreateParameter();
            kenParm.ParameterName = "$ken";
            kenParm.Value = kenNo;
            cmd.Parameters.Add(kenParm);
            var result = await cmd.ExecuteScalarAsync();
            return result.ToString();
        }

        private static async Task<string> GetSiNameAsync(SQLiteConnection con, int siNo)
        {
            using var cmd = con.CreateCommand();
            string sql = @"
                SELECT
                  m_si.si_name
                FROM
                  m_si 
                WHERE
                  m_si.si_no = $si";
            cmd.CommandText = sql;
            var siParm = cmd.CreateParameter();
            siParm.ParameterName = "$si";
            siParm.Value = siNo;
            cmd.Parameters.Add(siParm);
            var result = await cmd.ExecuteScalarAsync();
            return result.ToString();
        }
    }
}
