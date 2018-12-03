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
        static void Main(string[] args)
        {
            calcRandoms(1000, 100);
        }

        private static void calcRandoms(int runs, int max)
        {
            List<int> result = new List<int>();

            for (int i = 0; i < runs; i++)
            {
                result.Add(CalcRandom() % max);
            }
        }

        private static int CalcRandom()
        {
            string filename = "D:\\test.txt";
            Task writeTask = new Task(() =>
            {
                File.WriteAllText(filename, GetTestString());
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

        private static string GetTestString()
        {
            int rand = new Random().Next(1000, 10000);
            string result = "";
            for (int i = 0; i < rand; i++)
            {
                result += "x";
            }
            return result;
        }
    }
}
