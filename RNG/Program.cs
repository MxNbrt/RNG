using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RNG
{
    class Program
    {
        private static string RandomString = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
        static void Main(string[] args)
        {
            calcRandoms(1000);
        }

        private static void calcRandoms(int runs)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < runs; i++)
            {
                result.Add(CalcRandom());
            }
        }

        private static int CalcRandom()
        {
            string filename = "D:\\test.txt";
            Task writeTask = new Task(() =>
            {
                File.WriteAllText(filename, RandomString);
                File.Delete(filename);
            });
            Task<int> readTask = new Task<int>(() =>
            {
                int tries = 0;
                while (!File.Exists(filename))
                {
                    tries++;
                }
                return tries;
            });

            readTask.Start();
            writeTask.Start();
            readTask.Wait();
            writeTask.Wait();
            return readTask.Result;
        }
    }
}
