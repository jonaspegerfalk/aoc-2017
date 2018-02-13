using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day20
{
    public class Program
    {
        class Particle
        {
            public List<long> Position;
            public string PositionHash => String.Join(":", Position);
            public long AbsoluteVelocity => Velocity[0] * Velocity[0] + Velocity[1] * Velocity[1] + Velocity[2] * Velocity[2];
            public long AbsoluteAcceleration => Acceleration[0] * Acceleration[0] + Acceleration[1] * Acceleration[1] + Acceleration[2] * Acceleration[2];
            public List<long> Velocity;
            public List<long> Acceleration;
            internal int Index;

            public long Distance => Position[0] * Position[0] + Position[1] * Position[1] + Position[2] * Position[2];
        }

        static void Main(string[] args)
        {
            long res = 0;
            string input = @"..\..\input.txt";
            var lines = File.ReadLines(input).ToList();

            Particle x = new Particle();
            Particle y = new Particle();
            Particle z = new Particle();

            List<Particle> particles = new List<Particle>();
            var i = 0;
            foreach (var l in lines)
            {
                var data = l.Split(' ').ToList();
                var p = new Particle();
                var v1 = data[0].Substring(3).Replace(">", "");
                p.Position = v1.Substring(0,v1.Length-1).Split(',').Select(long.Parse).ToList();

                var v2 = data[1].Substring(3).Replace(">", "");
                p.Velocity = v2.Substring(0, v2.Length - 1).Split(',').Select(long.Parse).ToList(); ;

                var v3 = data[2].Substring(3).Replace(">", "");
                p.Acceleration = v3.Split(',').Select(long.Parse).ToList(); ;
                p.Index = i;
                particles.Add(p);
                i++;
            }

            var pp = particles.OrderBy(p => p.AbsoluteAcceleration).ThenBy(p => p.AbsoluteVelocity);
            Console.WriteLine(pp.First().Index); // Solution part 1. Lowest accelaration will stay longest

            int noCollisionsCounter = 0;
            do
            {
                foreach (var part in particles)
                {
                    for (var j = 0; j < 3; j++)
                    {
                        part.Velocity[j] += part.Acceleration[j];
                        part.Position[j] += part.Velocity[j];
                    }
                }

                var min = particles.Min(p => p.Distance);
                var minP = particles.Where(p => p.Distance == min).First();
                if (particles.Count == particles.Select(p => p.PositionHash).Distinct().Count()) noCollisionsCounter++;
                Console.WriteLine($"{particles.Count} {min} {minP.Index} {noCollisionsCounter}");
                particles = particles.Where(p => particles.Count(p2 => p2.PositionHash == p.PositionHash) == 1).ToList();
                if (noCollisionsCounter > 30)
                {
                    break;
                }
            }
            while (particles.Count > 1);
            Console.WriteLine(particles.Count);
            Console.ReadKey();
        }

        [Fact]
        public static void Test()
        {
            Assert.True(true);
        }
    }
}
