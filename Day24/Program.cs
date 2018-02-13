using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;


namespace Day24
{
    public class Program
    {
        class Component
        {
            public int Port1 { get; set; }
            public int Port2 { get; set; }
        }

        class Bridge
        {
            public int Strength { get; set; }
            public int Length { get; set; }
        }

        static void Main(string[] args)
        {
            string input = @"..\..\input.txt";
            var lines = File.ReadLines(input).ToList();
            var data = lines.Select(line => line.Split('/')).Select(o => new Component() { Port1 = int.Parse(o[0]), Port2 = int.Parse(o[1]) }).ToList();

            var allBridges = GenerateBridges(data, 0, new Bridge() { Strength = 0, Length = 0 });

            Console.WriteLine(allBridges.Select(o => o.Strength).Max());
            Console.WriteLine(allBridges.OrderByDescending(o => o.Length).ThenByDescending(o => o.Strength).First().Strength);

            Console.ReadKey();
        }

        private static List<Bridge> GenerateBridges(List<Component> components, int port, Bridge bridge)
        {
            var bridges = new List<Bridge>();
            for (var i = 0; i < components.Count; i++)
            {
                if (components[i].Port1 == port || components[i].Port2 == port)
                {
                    var newBridge = new Bridge()
                    {
                        Strength = bridge.Strength + components[i].Port1 + components[i].Port2,
                        Length = bridge.Length + 1
                    };

                    bridges.Add(newBridge);
                    var newPort = components[i].Port1 == port ? components[i].Port2 : components[i].Port1;
                    List<Component> componentsLeft = new List<Component>(components);
                    componentsLeft.RemoveAt(i);
                    bridges.AddRange(GenerateBridges(componentsLeft, newPort, newBridge));
                }
            }

            return bridges;
        }

    }
    
}
