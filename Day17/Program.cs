using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day17
{
    public class Program
    {
        static int Solve1(int input)
        {
            List<int> data = new List<int>();
            data.Add(0);
            var current = 0;
            var length = 1;
            for (var i = 1; i <= 2017; i++)
            {
                current = (current + input) % data.Count;
                //Console.WriteLine($"{current}, {lengt}");
                length++;
                if (current == data.Count - 1)
                {
                    data.Add(i);
                }
                else
                {
                    data.Insert(current + 1, i);
                }
                current++;
            }
            return data[data.IndexOf(2017)+1];
        }

        static int Solve2(int input)
        {
            List<int> data = new List<int>();
            data.Add(0);
            var current = 0;
            var length = 1;
            for (var i = 1; i <= 50000000; i++)
            {
                current = (current + input) % length;
                length++;
                current++;

                if (current != 1) continue;
                data.Add(i);
               // Console.WriteLine(i);
            }
            return data.Last();
        }


        static void Main(string[] args)
        {
            Console.WriteLine(Solve1(359));
            Console.WriteLine(Solve2(359));
            Console.ReadKey();
        }





        [Fact]
        public static void Test()
        {
            Assert.True(true);
        }
    }
}
