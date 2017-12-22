using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day22
{
    public class Program
    {
        static Dictionary<int, Dictionary<int, Infected>> map = new Dictionary<int, Dictionary<int, Infected>>();
        static int currentX;
        static int currentY;
        static int infected = 0;
        enum Infected { Clean, Weakend, Infected, Flagged };
        static int currentDir = 0;

        static void Main(string[] args)
        {
            string input = @"..\..\input.txt";
            var lines = File.ReadLines(input).ToList();

            for (var x = -1000; x < 1000; x++)
            {
                var d = new Dictionary<int, Infected>();
                for (var y = -1000; y < 1000; y++)
                {
                    d[y] = Infected.Clean;
                }
                map[x] = d;
            }

            var i = 0;
            foreach (var l in lines)
            {
                int j = 0;
                foreach (var d in l)
                {
                    if (d == '#') map[i][j] = Infected.Infected; else map[i][j] = Infected.Clean; ;
                    j++;
                }
                i++;
            }

            currentX = lines.Count / 2;
            currentY = lines.Count / 2;
            i = 1;
            do
            {
                switch (map[currentX][currentY])
                {
                    case Infected.Infected:
                        map[currentX][currentY] = Infected.Flagged;
                        currentDir = (currentDir + 1) % 4;
                        break;
                    case Infected.Clean:
                        map[currentX][currentY] = Infected.Weakend;
                        currentDir = (currentDir - 1 + 4) % 4;
                        break;
                    case Infected.Weakend:
                        map[currentX][currentY] = Infected.Infected;
                        infected++;
                        break;
                    case Infected.Flagged:
                        map[currentX][currentY] = Infected.Clean;
                        currentDir = (currentDir + 2) % 4;
                        break;
                }
                switch (currentDir)
                {
                    case 0: currentX--; break;
                    case 1: currentY++; break;
                    case 2: currentX++; break;
                    case 3: currentY--; break;
                }

                //if (i % 10000 == 0) Console.WriteLine($"{i} {infected}");
                i++;
            }
            while (i <= 10000000);

            Console.WriteLine(infected);
            Console.ReadKey();
        }

    }
}
