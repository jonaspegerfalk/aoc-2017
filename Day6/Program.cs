using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day6
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "14	0	15	12	11	11	3	5	1	6	8	4	9	1	8	4";
            var seen = new Dictionary<String, int>();

            var d = input.Split(null).Select(int.Parse).ToList();
            var steps = 0;
            do
            {
                seen[String.Join(",", d)] = steps;
                var max = d.Max();
                var indexOfMax = d.IndexOf(max);
                d[indexOfMax] = 0;
                for (var i = 1; i <= max; i++)
                {
                    d[(indexOfMax + i) % d.Count]++;
                }
                steps++;
            } while (!seen.ContainsKey(String.Join(",", d)));
            
            Console.WriteLine(steps);
            Console.WriteLine(steps-seen[String.Join(",", d)]);
            Console.ReadKey();
        }
    }
}
