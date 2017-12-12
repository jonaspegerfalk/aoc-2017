using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day12
{
    public class Program
    {
        static Dictionary<int, List<int>> pipes = new Dictionary<int, List<int>>();
        static Dictionary<int, bool> seen = new Dictionary<int, bool>();

        static void Main(string[] args)
        {
            string input = @"..\..\input.txt";
            var lines = File.ReadLines(input).ToList();

            pipes = lines.Select(l => l.Split(' ')[0]).ToDictionary(key => int.Parse(key), value => new List<int>());
            foreach (var l in lines)
            {
                var data = l.Split(' ');
                var p1 = int.Parse(data[0]);
                pipes[p1] = data.Skip(2).Select(p=>int.Parse(p.Replace(",", ""))).ToList();
            }

            Walk(0);
            Console.WriteLine(seen.Keys.Count);

            seen.Clear();
            int n = 0;
            foreach (var p in pipes.Keys)
            {
                if (!seen.ContainsKey(p))
                {
                    n++;
                    Walk(p);
                }
            }
            Console.WriteLine(n);

            Console.ReadKey();
        }

        static void Walk(int pipe) {
            if (seen.ContainsKey(pipe)) return;
            seen[pipe] = true;
            pipes[pipe].ForEach(Walk);
         }
        
    }
}
