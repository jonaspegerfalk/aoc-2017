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

        static void Main(string[] args)
        {

            var pr1 = new Pr(0);
            var pr2 = new Pr(1);

            pr1.p = pr2;
            pr2.p = pr1;

            do
            {
                pr1.Step();
                pr2.Step();
            }
            while (!pr1.IsWaiting || !pr2.IsWaiting);
            Console.WriteLine(pr2.SentValues);
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

            reg['p'] = start;
            lastPlayed = 4;
        }

        public void Step()
        {
            var lineData = lines[(int)counter].Split(' ');
            long data = 0;
            var inst = lineData[0];
            char r = lineData[1][0];
            Console.WriteLine($"{counter}: {String.Join(",", reg.Values)}  {lines[counter]}  - {q.Count()}");
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
                case "mul":
                    reg[r] *= data;
                    break;
                case "mod":
                    reg[r] = reg[r] % data;

                    break;
                case "snd":
                    int num2 = 0;
                    if (!int.TryParse(r.ToString(), out num2))
                    {
                        num2 = (int)reg[r];
                    }

                    p.q.Add(num2);
                    SentValues++;
                    break;
                case "rcv":
                    if (q.Count == 0)
                    {
                        counter--;
                    }
                    else
                    {
                        reg[r] = q.First();
                        q = q.Skip(1).ToList();
                    }
                    break;
                case "jgz":
                    int num = 0;
                    if (!int.TryParse(r.ToString(), out num))
                    {
                        num = (int)reg[r];
                    }
                    if (num != 0)
                    {
                        counter += (int)data;
                        counter--;
                    }

                    break;
            }
        }
    }
}



