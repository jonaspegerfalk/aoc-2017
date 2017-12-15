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

        [Fact]
        public static void HexToBinaryTest()
        {
            Assert.Equal("1111", "F".HexToBinary());
            Assert.Equal("11111111", "FF".HexToBinary());
        }

        [Fact]
        public static void IntToBinaryStringTest()
        {
            Assert.Equal("1", 1.ToBinaryString(1));
            Assert.Equal("101", 5.ToBinaryString(0));
            Assert.Equal("1111", 15.ToBinaryString(4));
            Assert.Equal("00001111", 15.ToBinaryString(8));
            Assert.Equal("0000000000001111", 15.ToBinaryString());
        }

    }
}
