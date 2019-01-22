using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringCalcKata1;
using Xunit;

namespace StringCalculatorTests
{
    public class StringCalculatorAdd
    {
        private readonly StringCalculator _stringCalculator = new StringCalculator();

        [Fact]
        public void EmptyStringReturns0()
        {
            var result = _stringCalculator.Add("");

            Assert.Equal(0, result);
        }

        [Fact]
        public void OneNumberReturnsOne()
        {
            var result = _stringCalculator.Add("1");

            Assert.Equal(1, result);
        }

        [Fact]
        public void OnePlusTwoReturnsThree()
        {
            var result = _stringCalculator.Add("1,2");

            Assert.Equal(3, result);
        }

        [Fact]
        public void OneWithNewLinePlusTwoPlusThreeReturnsSix()
        {
            var result = _stringCalculator.Add("1\n2,3");

            Assert.Equal(6, result);
        }

        [Theory]
        [InlineData("-1,2")]
        public void OneNegativeThrowsException(string input)
        {
            var ex = Assert.Throws<ArgumentException>(() => _stringCalculator.Add(input));

            Assert.Equal("Negatives not allowed: -1", ex.Message);
        }

        [Theory]
        [InlineData("2,-4,3,-5")]
        public void MultipleNegativeThrowsException(string input)
        {
            var ex = Assert.Throws<ArgumentException>(() => _stringCalculator.Add(input));

            Assert.Equal("Negatives not allowed: -4,-5", ex.Message);
        }

        [Fact]
        public void IgnoreGreaterThan1000()
        {
            // eg. "1001,2" returns 2
            var result = _stringCalculator.Add("1001,2");

            Assert.Equal(2, result);
        }

        [Fact]
        public void DelimiterAnyLength()
        {
            var result = _stringCalculator.Add("//[|||]\n1|||2|||3");

            Assert.Equal(6, result);
        }

        [Theory]
        [InlineData("//[|][%]\n1|2%3")]
        [InlineData("//[|||][%%]\n1|||2%%3")]
        public void MultipleDelimiters(string input)
        {
            var result = _stringCalculator.Add(input);

            Assert.Equal(6, result);
        }
    }
}
