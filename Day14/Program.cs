using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day14
{
    public class Program
    {
        static List<string> data = new List<string>();
        static List<List<int>> area = new List<List<int>>();

        static void Main(string[] args)
        {
            string input = @"ugkiagan";
            data = Enumerable.Range(0, 128)
                               .Select(i => $"{input}-{i}")
                               .Select(Knot)
                               .ToList();
            area = Enumerable.Range(0, 128)
                               .Select(i => Enumerable.Repeat(0, 128).ToList())
                               .ToList();

            var n = 1;
            for (var i = 0; i < 128; i++)
            {

                for (var j = 0; j < 128; j++)
                {
                    if (data[i][j] == '1' && area[i][j] == 0)
                    {
                        Walk(i, j, n);
                        n++;
                    }
                }
            }

            Console.WriteLine(data.Sum(a => a.Count(c => c == '1')));
            Console.WriteLine(n - 1);
            Console.ReadKey();
        }

        static void Walk(int x, int y, int n)
        {
            if (x < 0 || y < 0 || x == 128 || y == 128) return;
            if (data[x][y] == '0' || area[x][y] != 0) return;
            area[x][y] = n;
            Walk(x + 1, y, n);
            Walk(x, y + 1, n);
            Walk(x - 1, y, n);
            Walk(x, y - 1, n);
        }

        private static List<int> CreateInput(string data)
        {
            var d = new List<int>();
            d = data.Select(c => (int)c).ToList();
            d.AddRange(new List<int>() { 17, 31, 73, 47, 23 });
            return d;
        }

        private static string Knot(string data)
        {
            var inputData = CreateInput(data);
            var d = Enumerable.Range(0, 256).ToList();
            var skip = 0;
            var current = 0;
            for (var j = 0; j < 64; j++)
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
            for (var i = 0; i < 16; i++)
            {
                denseHashList.Add(d.Take(16).Aggregate((a, b) => a ^ b));
                d = d.Skip(16).ToList();
            }

            return string.Join("", denseHashList.Select(v => v.ToBinaryString(8)));
        }
    }
}
