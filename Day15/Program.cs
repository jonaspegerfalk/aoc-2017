using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day15
{
    public class Program
    {

        static int Solve(long iterations, long generatorA, long generatorB, long multiplierA = 1, long multiplierB = 1)
        {
            int res = 0;
            for (long i = 0; i < iterations; i++)
            {
                do { generatorA = (16807 * generatorA) % 2147483647; } while (multiplierA != 1 && generatorA % multiplierA != 0);
                do { generatorB = (48271 * generatorB) % 2147483647; } while (multiplierB != 1 && generatorB % multiplierB != 0);
                if ((generatorA & 0xffff) == (generatorB & 0xffff)) res++;
            }
            return res;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(Solve(40000000, 65, 8921));
            Console.WriteLine(Solve(5000000, 516, 190, 4, 8));
            Console.ReadKey();
        }
        

        [Fact]
        public static void Test()
        {
            Assert.Equal(309, Solve(5000000, 65, 8921, 4, 8));
            Assert.Equal(588, Solve(40000000, 65, 8921));
        }
    }
}
