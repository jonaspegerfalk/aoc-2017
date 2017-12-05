using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day5
{
    public class Program
    {
        static int Solve1(List<int> data)
        {
            int steps = 0;
            int current = 0;
            do
            {
                int offset = data[current];
                data[current]++;
                current += offset;
                steps++;
            } while (current >= 0 && current < data.Count);
            return steps;
        }

        static int Solve2(List<int> data)
        {
            int steps = 0;
            int current = 0;
            do
            {
                int offset = data[current];
                if (offset >= 3)
                    data[current]--;
                else data[current]++;
                current += offset;
                steps++;
            } while (current >= 0 && current < data.Count);
            return steps;
        }

        static void Main(string[] args)
        {
            string input = @"..\..\input.txt";
            var lines = File.ReadLines(input).ToList();
            var data = lines.Select(int.Parse).ToList();

            Console.WriteLine(Solve1(new List<int>(data)));
            Console.WriteLine(Solve2(new List<int>(data)));

            Console.ReadKey();
        }

        [Fact]
        static public void Day5Test()
        {
            Assert.Equal(5, Solve1("0 3 0 1 -3".Split(null).Select(int.Parse).ToList()));
            Assert.Equal(10, Solve2("0 3 0 1 -3".Split(null).Select(int.Parse).ToList()));

        }

    }
}
