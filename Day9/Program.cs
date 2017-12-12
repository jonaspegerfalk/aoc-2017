using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Day9
{
    public class Program
    {
        static string CleanData(String data)
        {
            var data2 = "";
            for (var i = 0; i < data.Length; i++)
            {
                if (data[i] == '!')
                {
                    i++;
                    continue;
                }
                data2 += data[i];
            }
            var data22 = "";
            for (var i = 0; i < data2.Length; i++)
            {
                if (data2[i] == '<')
                {
                    i = data2.IndexOf('>', i);
                    continue;
                }
                data22 += data2[i];
            }
            return data22.Replace(",", "");
        }



        static int CountGroups(string d)
        {
            int score = 0;
            void CountGroupRec(string data, int level)
            {
                Console.WriteLine($"{score} {level} {new string(' ', level)} {data}");
                if (data.Length == 1) return;
                if (data[0] == '{') { score += level; CountGroupRec(data.Substring(1), level + 1); }
                else CountGroupRec(data.Substring(1), level - 1);
            }
            CountGroupRec(d, 1);
            return score;
        }


        static void Main(string[] args)
        {
            string input = @"..\..\input.txt";
            var lines = File.ReadLines(input).ToList();

            Console.WriteLine(CountGroups(CleanData(lines[0])));
            Console.WriteLine(CountGarbage(lines[0]));
            Console.ReadKey();
        }


        [Fact]
        public static void Test()
        {
            Assert.Equal(1, CountGroups("{}"));
            Assert.Equal(6, CountGroups("{{{}}}"));
            Assert.Equal(5, CountGroups(CleanData("{{},{}}")));
            Assert.Equal(16, CountGroups(CleanData("{{{},{},{{}}}}")));
            Assert.Equal(1, CountGroups(CleanData("{<a>,<a>,<a>,<a>}")));
            Assert.Equal(9, CountGroups(CleanData("{{<!!>},{<!!>},{<!!>},{<!!>}}")));
            Assert.Equal(9, CountGroups(CleanData("{{<ab>},{<ab>},{<ab>},{<ab>}}")));
            Assert.Equal(3, CountGroups(CleanData("{{<a!>},{<a!>},{<a!>},{<ab>}}")));

        }
        [Fact]
        public static void Test2()
        {
            Assert.Equal(0, CountGarbage("<>"));
            Assert.Equal(17, CountGarbage("<random characters>"));
            Assert.Equal(3, CountGarbage("<<<<>"));
            Assert.Equal(0, CountGarbage("<!!!>>"));
            Assert.Equal(10, CountGarbage("<{o\"i!a,<{i<a>"));

        }

        private static int CountGarbage(string data)
        {
            var data2 = "";
            for (var i = 0; i < data.Length; i++)
            {
                if (data[i] == '!')
                {
                    i++;
                    continue;

                }
                data2 += data[i];
            }
            var total = 0;
            for (var i = 0; i < data2.Length; i++)
            {
                if (data2[i] == '<')
                {
                    var end = data2.IndexOf('>', i);
                    total += (end - i - 1);
                    i = end;
                    continue;

                }
            }
            return total;
        }
    }
}
