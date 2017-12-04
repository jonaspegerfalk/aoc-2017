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
            bool valid = true;
            Dictionary<string, int> dic = new Dictionary<string, int>();
            inp.Split(null).ToList().ForEach(pass =>
            {
                    if (dic.ContainsKey(pass))
                        valid = false;

                    dic[pass] = 1;

            });

            return valid;
        }
   

        static bool Valid2(string inp)
        {
            bool valid = true;
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var pass in inp.Split(null).ToList())
            {
                var perm = pass.Permutations();
                foreach (var p in perm)
                {
                    var permutatedPassword = new String(p.ToArray());
                    if (dic.ContainsKey(permutatedPassword))
                        return false;
                }
                dic[pass] = 1;
            };

            return valid;
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
            int res = 0;
            string input = @"d:\dev_prvt\aoc-2017\Aoc2017\Day4\input.txt";
            var lines = File.ReadLines(input).ToList();

            var i = 0;
            foreach (var l in lines)
            {
                Console.WriteLine($"{i} {l}");
                if (Valid1(l)) res++;
                i++;
            }
            Debug.WriteLine(res);


            i = 0;
            res = 0;
            foreach (var l in lines)
            {
                Console.WriteLine($"{i} {l}");
                if (Valid2(l)) res++;
                i++;
            }
            Debug.WriteLine(res);

        }
    }
}
