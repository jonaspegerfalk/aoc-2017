using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day11
{
    public class Program
    {

        static int Walk(List<string> directions)
        {
            int currentX = 0;
            int currentY = 0;
            int maxDistance = 0;
            while (directions.Count > 0)
            {
                var direction = directions.First();
                switch (direction)
                {
                    case "nw": currentX--; break;
                    case "n": currentY--; break;
                    case "ne": currentX++; currentY--; break;
                    case "se": currentX++; break;
                    case "s": currentY++; break;
                    case "sw": currentX--; currentY++; break;
                }
                directions = directions.Skip(1).ToList();
                maxDistance = Math.Max(maxDistance, (Math.Abs(currentY) + (Math.Abs(currentX) - Math.Abs(currentY))));
            }
            Console.WriteLine($"Max: {maxDistance}");

            return (Math.Abs(currentY) + (Math.Abs(currentX) - Math.Abs(currentY)));
        }

        static void Main(string[] args)
        {
            string input = @"..\..\input.txt";
            var data = File.ReadLines(input).First().Split(',').ToList();

            Console.WriteLine(Walk(data));
            Console.ReadKey();
        }


        [Fact]
        public static void Test()
        {
            Assert.Equal(0, Walk("ne,ne,sw,sw".Split(',').ToList()));
            Assert.Equal(0, Walk("n,n,s,s".Split(',').ToList()));
            Assert.Equal(0, Walk("nw,nw,se,se".Split(',').ToList()));
            Assert.Equal(1, Walk("ne".Split(',').ToList()));
            Assert.Equal(3, Walk("ne,ne,ne".Split(',').ToList()));
            Assert.Equal(2, Walk("ne,ne,s,s".Split(',').ToList()));
        }
    }
}
