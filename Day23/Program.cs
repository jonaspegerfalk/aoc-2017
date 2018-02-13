using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day18
{
    public class Program
    {
        static int part2()
        {
            int a = 1;
            int b = 99;
            int c = b;
            int h = 0;
            if (a == 1)
            {
                b = b * 100 + 100000;
                c = b + 17000;
            }
            while (true)
            {
                int f = 1;
                int d = 2;
                int e = 0;
                while (d - b != 0)
                {
                    //e = 2;
                    //while (e - b != 0)
                    //{
                    //    if (d * e != b)
                    //    {
                    //        f = 0;
                    //    }
                    //    e++;
                    //}

                    //--------------------
                    if ((b % d == 0))
                    {
                        f = 0;
                        break;
                    }
                    //--------------------
                    d++;
                }
                if (f == 0) { h++; }
               // Console.WriteLine($"{b - c}");
                if (b == c) return h;
                b += 17;
            }
        }
        static internal int part1()
        {
            var pr1 = new Pr(0);

            var done = false;
            do
            {
                done = pr1.Step();
            }
            while (!done);
            return pr1.count;
        }

        static void Main(string[] args)
        {

            Console.WriteLine(part1());
            Console.WriteLine(part2());
            Console.ReadKey();
        }

    }


    class Pr
    {
        public int SentValues = 0;
        public Dictionary<char, long> reg = new Dictionary<char, long>();
        public List<string> lines;
        public long lastPlayed { get; set; }
        public bool IsWaiting
        {
            get
            {
                var lineData = lines[(int)counter].Split(' ');
                var inst = lineData[0];
                return (inst == "rcv" && p.q.Count == 0);
            }
        }

        public Pr p;
        public List<long> q = new List<long>();

        public int counter = 0;
        public Pr(long start)
        {
            int res = 0;
            string input = @"..\..\input.txt";
            lines = File.ReadLines(input).ToList();

            foreach (var l in lines)
            {
                var c = l.Split(' ')[1][0];
                if (!reg.ContainsKey(c) && !int.TryParse(l.Split(' ')[1], out res)) reg[c] = 0;
            }

            reg['1'] = 1;
            lastPlayed = 4;
        }
        public int count { get; set; }
        public bool Step()
        {
            var lineData = lines[(int)counter].Split(' ');
            long data = 0;
            var inst = lineData[0];
            char r = lineData[1][0];
            //Console.WriteLine($"{counter}: {String.Join(",", reg.Values)}  {lines[counter]}  - {q.Count()}");
            //Console.WriteLine($"{counter}: {String.Join(",", reg.Values)}");
            if (lineData.Count() > 2)
                if (reg.ContainsKey(lineData[2][0]))
                    data = reg[lineData[2][0]];
                else
                    data = int.Parse(lineData[2]);
            counter++;
            switch (inst)
            {
                case "set":
                    reg[r] = data;
                    break;
                case "add":
                    reg[r] += data;
                    break;
                case "sub":
                    reg[r] -= data;
                    break;
                case "mul":
                    count++;
                    reg[r] *= data;
                    break;
                case "jnz":
                    int num3 = 0;
                    if (!int.TryParse(r.ToString(), out num3))
                    {
                        num3 = (int)reg[r];
                    }
                    if (num3 != 0)
                    {
                        counter += (int)data;
                        counter--;
                    }

                    break;
            }
            return (counter >= lines.Count);
        }
    }
}


