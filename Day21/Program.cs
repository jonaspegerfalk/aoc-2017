using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AocUtil;
using Xunit;

namespace Day21
{

    public class Rule
    {
        public string Match;
        public string output;
    }
    public class Program
    {
        static List<string> image = new List<string>();
        static List<Rule> rules = new List<Rule>();

        static void Main(string[] args)
        {
            int res = 0;
            string input = @"..\..\input.txt";
            var lines = File.ReadLines(input).ToList();

            image = new List<string>()
            {
                ".#.",
                "..#",
                "###",
            };

            var i = 0;
            foreach (var l in lines)
            {
                var data = l.Split(' ');
                rules.Add(new Rule()
                {
                    Match = data[0],
                    output = data[2]
                });


                i++;
            }
            for (var j = 0; j < 18; j++)
            {
                if (image.Count % 2 == 0)
                {
                    var newImage = new List<string>();
                    for (var a = 0; a < image.Count / 2; a++)
                    {
                        newImage.Add("");
                        newImage.Add("");
                        newImage.Add("");
                    }
                    for (var a = 0; a < image.Count / 2; a++)
                    {
                        for (var b = 0; b < image.Count / 2; b++)
                        {
                            var smallImage = new List<String>();
                            smallImage.Add(image[a * 2].Substring(b * 2, 2));
                            smallImage.Add(image[a * 2 + 1].Substring(b * 2, 2));

                            var smallPermutation = FindAllPermutation2(smallImage);
                            var match = rules.Where(r => smallPermutation.Any(sp => r.Match == sp)).First();
                            var m = match.output.Split('/');
                            newImage[a * 3] += m[0];
                            newImage[a * 3 + 1] += m[1];
                            newImage[a * 3 + 2] += m[2];

                        }
                    }
                    image = newImage;
                }
                else if (image.Count % 3 == 0)
                {
                    var newImage = new List<string>();
                    for (var a = 0; a < image.Count / 3; a++)
                    {
                        newImage.Add("");
                        newImage.Add("");
                        newImage.Add("");
                        newImage.Add("");
                    }
                    for (var a = 0; a < image.Count / 3; a++)
                    {
                        for (var b = 0; b < image.Count / 3; b++)
                        {
                            var smallImage = new List<String>();
                            smallImage.Add(image[a * 3].Substring(b * 3, 3));
                            smallImage.Add(image[a * 3 + 1].Substring(b * 3, 3));
                            smallImage.Add(image[a * 3 + 2].Substring(b * 3, 3));

                            var smallPermutation = FindAllPermutations(smallImage);
                            var match = rules.Where(r => smallPermutation.Any(sp => r.Match == sp)).First();
                            var m = match.output.Split('/');
                            newImage[a * 4] += m[0];
                            newImage[a * 4 + 1] += m[1];
                            newImage[a * 4 + 2] += m[2];
                            newImage[a * 4 + 3] += m[3];

                        }
                    }
                    image = newImage;
                }
                //                DrawImage(image);
                Console.WriteLine($"{j + 1} {image.Sum(r => r.Count(c => c == '#'))}");
            }
            Debug.WriteLine(res);
            Console.ReadKey();
        }
        private static void DrawImage(List<string> smallImage)
        {
            smallImage.ForEach(img => Console.WriteLine(img));
            Console.WriteLine("---------------------------------------");
        }

        private static List<List<string>> FindAllRotations(List<string> smallImage)

        {
            var res = new List<List<string>>();
            res.Add(new List<string>() {
                smallImage[0][2].ToString() + smallImage[1][2].ToString() + smallImage[2][2].ToString(),
                smallImage[0][1].ToString() + smallImage[1][1].ToString() + smallImage[2][1].ToString(),
                 smallImage[0][0].ToString() + smallImage[1][0].ToString() + smallImage[2][0].ToString(),
            });
            res.Add(new List<string>() {
                smallImage[2][0].ToString() + smallImage[1][0].ToString() + smallImage[0][0].ToString(),
                smallImage[2][1].ToString() + smallImage[1][1].ToString() + smallImage[0][1].ToString(),
                 smallImage[2][2].ToString() + smallImage[1][2].ToString() + smallImage[0][2].ToString(),
            });
            res.Add(new List<string>() {
                smallImage[2].ReverseString(),
                smallImage[1].ReverseString(),
                 smallImage[0].ReverseString(),
            });
            return res;
        }
        private static List<string> FindAllPermutations(List<string> smallImage)
        {

            var res = new List<string>();

            res.Add(String.Join("/", smallImage));
            //Flip
            var f1 = new List<string>() { smallImage[2], smallImage[1], smallImage[0] };
            var f2 = new List<string>() { smallImage[0].ReverseString(), smallImage[1].ReverseString(), smallImage[2].ReverseString() };
            res.Add(String.Join("/", f1));
            res.Add(String.Join("/", f2));
            //Rotate
            res.AddRange(FindAllRotations(smallImage).Select(r => String.Join("/", r)));
            res.AddRange(FindAllRotations(f1).Select(r => String.Join("/", r)));
            res.AddRange(FindAllRotations(f2).Select(r => String.Join("/", r)));
            return res;
        }

        private static List<string> FindAllPermutation2(List<string> smallImage)
        {

            var res = new List<string>();
            // start
            res.Add(String.Join("/", smallImage));
            // Flip
            res.Add(String.Join("/", new List<string>() { smallImage[1], smallImage[0] }));
            res.Add(String.Join("/", new List<string>() { smallImage[0].ReverseString() }));
            // Rotate
            res.Add(String.Join("/", new List<string>() {
                smallImage[0][1].ToString() + smallImage[1][1].ToString(),
                smallImage[0][0].ToString() + smallImage[1][0].ToString(),
            }));
            res.Add(String.Join("/", new List<string>() {
                smallImage[1][0].ToString() + smallImage[0][0].ToString(),
                smallImage[1][1].ToString() + smallImage[0][1].ToString(),
            }));
            res.Add(String.Join("/", new List<string>() {
                smallImage[1].ReverseString(),
                 smallImage[0].ReverseString(),
            }));

            return res;
        }

    }



}
