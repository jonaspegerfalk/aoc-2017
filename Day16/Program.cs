using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day16
{
    public class Program
    {
        static Dictionary<string, int> seen = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            string input = @"..\..\input.txt";
            var data = "abcdefghijklmnop".ToArray();
            var lines = File.ReadLines(input).ToList().First();

            var i = 0;
            var len = data.Count();
            for (int r = 0; r<1000; r++) { 
            foreach (var l in lines.Split(','))
            {
                var instrLine = l;
                var instr = l[0];
                instrLine = l.Substring(1);
                switch (instr) {
                    case 's':
                        var a = int.Parse(instrLine);
                        data = ShiftRight(data, a).ToArray();
                        break;
                    case 'p':
                        var indA = Array.IndexOf(data, instrLine.Split('/')[0][0]);
                        var indB = Array.IndexOf(data, instrLine.Split('/')[1][0]);
                        var tmp = data[indA];
                        data[indA] = data[indB];
                        data[indB] = tmp;
                        break;
                    case 'x':
                        var indAs = int.Parse(instrLine.Split('/')[0]);
                        var indBs = int.Parse(instrLine.Split('/')[1]);
                        var atmp = data[indAs];
                        data[indAs] = data[indBs];
                        data[indBs] = atmp;
                        break;

                }
                i++;
                // Console.WriteLine($"{i}: {l} -- {new string(data)}");
            }

                if (seen.ContainsKey(new string(data)))
                {
                    var a1 = 1000000000 % seen.Count;
                    Console.WriteLine(seen.ToList().First().Key);
                    Console.WriteLine(seen.ToList()[a1-1].Key);
                    break;
                }
                else seen[new string(data)] = r;


            }

            Console.ReadKey();
        }


        public static int MathMod(int a, int b)
        {
            int c = ((a % b) + b) % b;
            return c;
        }
        public static IEnumerable<T> ShiftRight<T>(IList<T> values, int shift)
        {
            for (int index = 0; index < values.Count; index++)
            {
                yield return values[MathMod(index - shift, values.Count)];
            }
        }


        [Fact]
        public static void Test()
        {
            Assert.True(true);
        }
    }
}
