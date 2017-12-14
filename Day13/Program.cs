using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day13
{
    public class Program
    {

        static List<int> Walls = new List<int>();
        static List<int> ScannerPosition = new List<int>();
        static List<bool> ScannerMovingDown = new List<bool>();
        static List<int> ScannerPosition2 = new List<int>();
        static List<bool> ScannerMovingDown2 = new List<bool>();

        static void Main(string[] args)
        {
            int res = 0;
            string input = @"..\..\input.txt";
            var lines = File.ReadLines(input).ToList();
            var max = lines.Max(l => l.Split(':').Select(a => int.Parse(a.Trim())).First()) + 1;

            Walls = Enumerable.Repeat(0, max).ToList();
            ScannerPosition = Enumerable.Repeat(0, max).ToList(); ;
            ScannerMovingDown = Enumerable.Repeat(true, max).ToList(); ;
            ScannerPosition2 = Enumerable.Repeat(0, max).ToList(); ;
            ScannerMovingDown2 = Enumerable.Repeat(true, max).ToList(); ;

            foreach (var l in lines)
            {
                var data = l.Split(':').Select(a => int.Parse(a.Trim())).ToList();
                Walls[data[0]] = data[1];
            }
            for (var startTime = 1; startTime < 10000000; startTime++)
            {
                ScannerPosition = ScannerPosition2.Select(o => o).ToList();
                ScannerMovingDown = ScannerMovingDown2.Select(o => o).ToList();
                Step(1);
                ScannerPosition2 = ScannerPosition.Select(o => o).ToList();
                ScannerMovingDown2 = ScannerMovingDown.Select(o => o).ToList();

                res = 0;
                var i = 0;
                foreach (var w in Walls)
                {
                    if (ScannerPosition[i] == 0)
                    {
                        res += i * w;
                        if (i == 0) res++;
                    }
                    if (res > 0) continue;
                    Step(1);
                    i++;
                }
                if (startTime % 10000 == 0) Console.WriteLine($"{startTime}, {res}");
                if (res == 0)
                {
                    Console.WriteLine($"{startTime}, {res}");
                    break;
                }
            }
            Debug.WriteLine(res);
            Console.ReadKey();
        }
        static void Step(int steps)
        {
            for (var a = 0; a < steps; a++)
            {
                for (var j = 0; j < Walls.Count; j++)
                {
                    if (Walls[j] != 0)
                    {
                        if (ScannerMovingDown[j] && ScannerPosition[j] < Walls[j] - 1)
                        {
                            ScannerPosition[j]++;
                        }
                        else if (ScannerMovingDown[j] && ScannerPosition[j] == Walls[j] - 1)
                        {
                            ScannerMovingDown[j] = false;
                            ScannerPosition[j]--;
                        }
                        else if (!ScannerMovingDown[j] && ScannerPosition[j] > 0)
                        {
                            ScannerPosition[j]--;
                        }
                        else if (!ScannerMovingDown[j] && ScannerPosition[j] == 0)
                        {
                            ScannerMovingDown[j] = true;
                            ScannerPosition[j]++;
                        }
                    }
                }
            }
        }

        [Fact]
        public static void Test()
        {
            Assert.True(true);

        }
    }
}
