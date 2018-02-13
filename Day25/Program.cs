using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Day25
{
    public class Program
    {

        class State
        {
            public int ZeroVal;
            public int OneVal;
            public int ZeroMov;
            public int OneMov;
            public char ZeroNext;
            public char OneNext;
            public int Val;
            public State(int a, char c, int m0, int b, char d, int m1)
            {
                ZeroVal = a;
                OneVal = b;
                ZeroNext = c;
                OneNext = d;
                ZeroMov = m0;
                OneMov = m1;
                Val = 0;
            }
        }

        static Dictionary<char, State> states = new Dictionary<char, State>();
        static Dictionary<int, int> values = new Dictionary<int, int>();

        static char current = 'A';
        static int pos = 0;
        static void Main(string[] args)
        {
            states['A'] = new State(1, 'B', 1, 0, 'B', -1);
            states['B'] = new State(0, 'C', 1, 1, 'B', -1);
            states['C'] = new State(1, 'D', 1, 0, 'A', -1);
            states['D'] = new State(1, 'E', -1, 1, 'F', -1);
            states['E'] = new State(1, 'A', -1, 0, 'D', -1);
            states['F'] = new State(1, 'A', 1, 1, 'E', -1);
         
            for (int i = 0; i < 12629077; i++)
            {
                values[i] = 0;
                values[-i] = 0;
            }

            for (int i = 0; i < 12629077; i++)
            {
                State a = states[current];
                if (values[pos] == 0)
                {
                    values[pos] = a.ZeroVal;
                    pos += a.ZeroMov;
                    current = a.ZeroNext;
                }
                else
                {
                    values[pos] = a.OneVal;
                    pos += a.OneMov;
                    current = a.OneNext;

                }
            }


            Console.WriteLine(values.Values.Count(v => v == 1));
            Console.ReadKey();
        }


    }
}
