using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AocUtil;



namespace Day4
{

    public class Program
    {

        static bool Valid1(string inp)
        {
            var data = inp.Split(null).ToList();
            return data.Count == data.Distinct().Count();
        }


        static bool Valid2(string inp)
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var pass in inp.Split(null).ToList())
            {
                var perm = pass.Permutations().Select(p=>new String(p.ToArray()));
                if (perm.Any(p => dic.ContainsKey(p)))
                    return false;
                dic[pass] = 1;
            };
            return true;
        }

        [Fact]
        public static void ValidProblem2()
        {
            Assert.True(Valid2("iiii oiii ooii oooi oooo"));
            Assert.True(Valid2("abcde fghij"));
            Assert.False(Valid2("abcde xyz ecdab"));
            Assert.True(Valid2("a ab abc abd abf abj"));
            Assert.True(Valid2("iiii oiii ooii oooi oooo"));
            Assert.False(Valid2("oiii ioii iioi iiio"));
            Assert.NotEmpty("abc".Permutations());
        }

        static void Main(string[] args)
        {
            string input = @"..\..\input.txt";
            var lines = File.ReadLines(input).ToList();

            Console.WriteLine(lines.Where(Valid1).Count());
            Console.WriteLine(lines.Where(Valid2).Count());

            Console.ReadKey();

        }
    }
}
