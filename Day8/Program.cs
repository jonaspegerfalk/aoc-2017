using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day8
{
    public class Program
    {
        static Dictionary<string, int> reg = new Dictionary<string, int>();
        
        static void Main(string[] args)
        {
            string input = @"..\..\input.txt";
            var lines = File.ReadLines(input).ToList();

            foreach (var l in lines)
            {
                var data = l.Split(' ');
                reg[data[0]] = 0;
            }

            var max = 0;
            foreach (var l in lines)
            {
                var data = l.Split(' ');

                var r = data[0];
                var condReg = data[4];
                var cond = data[5];
                var condValue = int.Parse(data[6]);
                var action = data[1];
                var value = int.Parse(data[2]);
                var condRegValue = reg[condReg];

                bool update = false;
                switch (cond)
                {
                    case ">": update = (condRegValue > condValue); break;
                    case "<": update = (condRegValue < condValue); break;
                    case "==": update = (condRegValue == condValue); break;
                    case "!=": update = (condRegValue != condValue); break;
                    case ">=": update = (condRegValue >= condValue); break;
                    case "<=": update = (condRegValue <= condValue); break;
                    default: Console.WriteLine("Something is wrong"); break;
                }
                if (update)
                    reg[r] += (action == "inc" ? 1 : -1) * value;
                max = Math.Max(reg.Values.Max(), max);
            }

            Console.WriteLine(reg.Values.Max());
            Console.WriteLine(max);
            Console.ReadKey();
        }
    }
}
