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

        static int CountGroups(string data, int score)
        {
            Console.WriteLine($"{score} {new string(' ', score)} {data}");
            if (data.Length == 2) return score;
            var open = 0;
            var res = score;
            var openTag = 1;
            for (var i = 1; i < data.Length; i++)
                {
                    if (data[i] == '{') open++; else open--;
                    if (open == 0)
                {
                    res += CountGroups(data.Substring(openTag, i-openTag+1), score + 1);
                    openTag = i+1;
                }
            }
            return res;
        }
        static void Main(string[] args)
        {
            string input = @"..\..\input.txt";
            var lines = File.ReadLines(input).ToList();

            Console.WriteLine(CountGroups(CleanData(lines[0]), 1));
            Console.WriteLine(CountGarbage(lines[0]));
            Console.ReadKey();
        }


        [Fact]
        public static void Test()
        {
            Assert.Equal(1, CountGroups("{}", 1));
            Assert.Equal(6, CountGroups("{{{}}}",1));
            Assert.Equal(5, CountGroups(CleanData("{{},{}}"),1));
            Assert.Equal(16, CountGroups(CleanData("{{{},{},{{}}}}"), 1));
            Assert.Equal(1, CountGroups(CleanData("{<a>,<a>,<a>,<a>}"),1));
            Assert.Equal(9, CountGroups(CleanData("{{<!!>},{<!!>},{<!!>},{<!!>}}"), 1));
            Assert.Equal(9, CountGroups(CleanData("{{<ab>},{<ab>},{<ab>},{<ab>}}"), 1));
            Assert.Equal(3, CountGroups(CleanData("{{<a!>},{<a!>},{<a!>},{<ab>}}"), 1));

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
                    var end  = data2.IndexOf('>', i);
                    total += (end - i-1);
                    i = end;
                    continue;

                }
            }
            return total;
        }
    }
}
