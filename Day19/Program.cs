using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day19
{
    public class Program
    {
        static List<string> map = new List<string>();
        static int currentX = 0;
        static int currentY = 0;

        enum diretion { up, left, right, down }

        static string res = "";

        static bool Valid(int x, int y)
        {
            if (x < 0 || y < 0 || x >= map[0].Length || y >= map.Count) return false;
            if (map[y][x] == ' ') return false;
            return true;

        }

        static diretion currentD = diretion.down;
        static int counter = 0;
        static void Walk(int x, int y, diretion d)
        {
            if ("ABCDEFGHIJKLMNOPQRSTUVWXYZ".Contains(map[y][x]))
            {
                res += map[y][x];
            }
        //    Console.WriteLine($"{x} {y} {d} {res}");
            counter++;
            if (map[y][x] == '+' && (d == diretion.up || d == diretion.down))
            {
                if (Valid(x - 1, y)) { currentX = x - 1; currentD = diretion.left; }
                if (Valid(x + 1, y)) { currentX = x + 1; currentD = diretion.right; }
            }
            else if (map[y][x] == '+' && (d == diretion.right || d == diretion.left))
            {
                if (Valid(x, y - 1)) { currentY = y - 1; currentD = diretion.up; }

                if (Valid(x, y + 1)) { currentY = y + 1; currentD = diretion.down; }
            }
            else if (d == diretion.down && Valid(x, y + 1)) currentY = y + 1; 
            else if (d == diretion.up && Valid(x, y - 1)) currentY = y - 1; 
            else if (d == diretion.left &&  Valid(x - 1, y)) currentX = x - 1; 
            else if (d == diretion.right && Valid(x + 1, y)) currentX = x + 1; 
        }

        static void Main(string[] args)
        {
            string input = @"..\..\input.txt";
            map = File.ReadLines(input).ToList();
            currentX = map[0].IndexOf('|');
            int prevX = currentX; int prevY = currentY;
            do
            {
                prevX = currentX; prevY = currentY;
                Walk(currentX, currentY, currentD);

            } while (prevX != currentX || prevY != currentY);
            Console.WriteLine(res);
            Console.WriteLine(counter);
            Console.ReadKey();
        }





        [Fact]
        public static void Test()
        {
            Assert.True(true);
        }
    }
}
