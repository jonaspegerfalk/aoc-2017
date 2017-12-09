using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day7
{
    public class Program
    {

        static Dictionary<string, ProgramEntry> programs = new Dictionary<string, ProgramEntry>();
     
        static void Main(string[] args)
        {
            int res = 0;
            string input = @"..\..\input.txt";
            var lines = File.ReadLines(input).ToList();

            var i = 0;
            foreach (var l in lines)
            {
                var data = l.Split(' ');
                var p = new ProgramEntry() { Name = data[0]};
                p.Weight = int.Parse(data[1].Replace("(", "").Replace(")", ""));
                programs[data[0]] = p;
                i++;
            }
            foreach (var l in lines)
            {
                var data = l.Split();
                if (data.Length > 2)
                    for (var j = 3; j<data.Length; j++)
                {
                        var a = data[j].Replace(",", "");
                    programs[a].parent = programs[data[0]];
                }
            }

            foreach (var p in programs)
            {
                //if (p.Value.parent != null)
                {
                    var v = p.Value;
                    var children = programs.Values.Where(a => a.parent != null && a.parent.Name == v.Name);
                    var childrenW = children.Select(ch => ch.TotalWeight()).ToList();
                    if (children.Count() != 0 && childrenW.Max() != childrenW.Min() ) {
                        Debug.WriteLine(v.Name);
                    }

                }
            }


            ch("gexwzw");

            Debug.WriteLine(res);
            Console.ReadKey();
        }

        static void ch(string name)
        {
            var b = programs[name];
            var children = programs.Values.Where(pr => pr.parent != null && pr.parent.Name == name);
            var childrenW = children.Select(p => p.TotalWeight()).ToList();
        }

        public class ProgramEntry
        {
            public ProgramEntry parent { get; set; }
            public string Name { get; set; }
            public int Weight { get; set; }

            public int TotalWeight()
            {
                var children = programs.Values.Where(a => a.parent != null && a.parent.Name == Name);
                if (children.Count() == 0) return Weight;
                return Weight + children.Sum(a => a.TotalWeight());
            }
        }


        [Fact]
        public static void Test()
        {
            Assert.True(true);
        }
    }
}
