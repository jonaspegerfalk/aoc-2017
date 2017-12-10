using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Day10
{
    public class Program
    {
        static int Solve(int length, List<int> data)
        {
            var d = Solve1(length, data);
            return d[0] * d[1];
        }

        static List<int> Solve1(int length, List<int> data)
        {
            var d = Enumerable.Range(0, length).ToList();
            var skip = 0;
            var current = 0;
           
            foreach (int a in data)
            {
                for (var i = 0; i < a/2; i++)
                {
                    var t = d[(current+i)%d.Count];
                    d[(current + i) % d.Count] = d[(current + a - i-1) % d.Count];
                    d[(current + a - i - 1) % d.Count] = t;
                }
                current = (current + skip + a) % d.Count;
                skip++;
            }
            return d;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Solve(256, new List<int>() { 197, 97, 204, 108, 1, 29, 5, 71, 0, 50, 2, 255, 248, 78, 254, 63 }));
            Console.WriteLine(Solve2(256, "197,97,204,108,1,29,5,71,0,50,2,255,248,78,254,63"));
            Console.ReadKey();
        }

        private static string Solve2(int v1, string data)
        {
            var inputData = CreateInput(data);
            var d = Enumerable.Range(0, 256).ToList();
            var skip = 0;
            var current = 0;
            for (var j = 0; j<64; j++)
            {
                foreach (int a in inputData)
                {
                    for (var i = 0; i < a / 2; i++)
                    {
                        var t = d[(current + i) % d.Count];
                        d[(current + i) % d.Count] = d[(current + a - i - 1) % d.Count];
                        d[(current + a - i - 1) % d.Count] = t;
                    }
                    current = (current + skip + a) % d.Count;
                    skip++;
                }
            }

            var denseHashList = new List<int>();
            for (var i = 0; i< 16; i++)
            {
                denseHashList.Add(d.Take(16).Aggregate((a, b) => a ^ b));
                d = d.Skip(16).ToList();
            }

            return string.Join("", denseHashList.Select(v => v.ToString("x2")));
        }

        private static List<int> CreateInput(string data) {
            var d = new List<int>();
            d = data.Select(c => (int)c).ToList();
            d.AddRange(new List<int>() { 17, 31, 73, 47, 23 });
            return d;
        }

        [Fact]
        public static void Test()
        {
            Assert.Equal(12, Solve(5, new List<int>() { 3, 4, 1, 5 }));
            Assert.Equal(40132, Solve(256, new List<int>() { 197, 97, 204, 108, 1, 29, 5, 71, 0, 50, 2, 255, 248, 78, 254, 63 }));
        }

        [Fact]
        public static void Test2()
        {
            Assert.Equal(64, (new List<int>() { 65, 27, 9, 1, 4, 3, 40, 50, 91, 7, 6, 0, 2, 5, 68, 22 }).Aggregate((a, b) => a ^ b));
            Assert.Contains(49, CreateInput("1,2,3"));
            Assert.Contains(44, CreateInput("1,2,3"));
            Assert.Contains(50, CreateInput("1,2,3"));
            Assert.Contains(17, CreateInput("1,2,3"));
            Assert.Equal(32, Solve2(256, "1,2,3").Length);
            Assert.Equal(32, Solve2(256, "").Length);
            Assert.Equal("3efbe78a8d82f29979031a4aa0b16a9d", Solve2(256, "1,2,3"));
            Assert.Equal("a2582a3a0e66e6e86e3812dcb672a272", Solve2(256, ""));
        }
    }
}
