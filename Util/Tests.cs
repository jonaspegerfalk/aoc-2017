using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AocUtil
{
    public static class Tests
    {
        [Fact]
        public static void ReverseStringTests()
        {
            Assert.Equal("cba", "abc".ReverseString());
            Assert.Equal("qw ert 123", "321 tre wq".ReverseString());
        }

        [Fact]
        public static void StringPermutationTest()
        {
            Assert.Equal(6, "abc".Permutations().ToList().Count);
            Assert.Contains("bac", "abc".Permutations());
        }

        [Fact]
        public static void ListPermutationTest()
        {
            var l = new List<string>() { "123", "abc", "def", "ghij" };
            Assert.Equal(24, l.Permutations().ToList().Count);
        }


    }
}
