using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Day5
{
    public class Program
    {
        static int Solve1(int input)
        {
            int size = 1;
            do { size += 2; } while (size * size <= input);
            var sideLastLevel = size - 2;
            var stepsInLastLevel = input - sideLastLevel * sideLastLevel;
            var stepsInLastSide = stepsInLastLevel % (size - 1);

            var stepsBack = (size - 1) / 2;
            stepsBack += Math.Abs((size - 1) / 2 - stepsInLastSide);

            return stepsBack;
        }

        static int Solve2(int input)
        {
            int CreateSum(Dictionary<Tuple<int, int>, int> memory, Tuple<int, int> tuple)
            {
                var x = tuple.Item1;
                var y = tuple.Item2;

                var sum = 0;
                if (memory.ContainsKey(Tuple.Create(x + 1, y))) sum += memory[Tuple.Create(x + 1, y)];
                if (memory.ContainsKey(Tuple.Create(x - 1, y))) sum += memory[Tuple.Create(x - 1, y)];
                if (memory.ContainsKey(Tuple.Create(x, y + 1))) sum += memory[Tuple.Create(x, y + 1)];
                if (memory.ContainsKey(Tuple.Create(x, y - 1))) sum += memory[Tuple.Create(x, y - 1)];
                if (memory.ContainsKey(Tuple.Create(x + 1, y + 1))) sum += memory[Tuple.Create(x + 1, y + 1)];
                if (memory.ContainsKey(Tuple.Create(x + 1, y - 1))) sum += memory[Tuple.Create(x + 1, y - 1)];
                if (memory.ContainsKey(Tuple.Create(x - 1, y + 1))) sum += memory[Tuple.Create(x - 1, y + 1)];
                if (memory.ContainsKey(Tuple.Create(x - 1, y - 1))) sum += memory[Tuple.Create(x - 1, y - 1)];
                memory[tuple] = sum;
                return sum;
            };

            var mem = new Dictionary<Tuple<int, int>, int>();
            mem[Tuple.Create(0, 0)] = 1;
            var s = 1;
            var lastSum = 0;
            var currentX = 0;
            var currentY = 0;
            do
            {
                s += 2;
                do
                {
                    currentX++;
                    lastSum = CreateSum(mem, Tuple.Create(currentX, currentY));
                } while (lastSum < input && currentX < (s - 1) / 2);
                if (lastSum < input) do
                    {
                        currentY++;
                        lastSum = CreateSum(mem, Tuple.Create(currentX, currentY));
                    } while (lastSum < input && currentY < (s - 1) / 2);
                if (lastSum < input) do
                    {
                        currentX--;
                        lastSum = CreateSum(mem, Tuple.Create(currentX, currentY));
                    } while (lastSum < input && currentX > -(s - 1) / 2);
                if (lastSum < input) do
                    {
                        currentY--;
                        lastSum = CreateSum(mem, Tuple.Create(currentX, currentY));
                    } while (lastSum < input && currentY > -(s - 1) / 2);

            } while (lastSum < input);

            return lastSum;
        }


        [Fact]
        public static void TestPart1()
        {
            Assert.Equal(3, Solve1(12));
            Assert.Equal(31, Solve1(1024));
            Assert.Equal(2, Solve1(23));
        }


        static void Main(string[] args)
        {
            Console.WriteLine(Solve1(265149));
            Console.WriteLine(Solve2(265149));
            Console.ReadKey();

        }
    }
}
