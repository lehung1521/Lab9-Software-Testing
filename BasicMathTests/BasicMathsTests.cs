
using Xunit;
using BasicMath;
using System;

namespace BasicMathTests
{
    public class BasicMathsTests
    {
        private readonly BasicMaths math = new BasicMaths();

        // Kiểm thử cộng
        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(-1, -1, -2)]
        [InlineData(0, 0, 0)]
        [InlineData(2147483647, 1, 2147483648.0)]
        public void Test_Add(double a, double b, double expected)
        {
            double actual = math.Add(a, b);
            Assert.Equal(expected, actual);
        }

        // Kiểm thử trừ
        [Theory]
        [InlineData(5, 3, 2)]
        [InlineData(-5, -3, -2)]
        [InlineData(0, 0, 0)]
        public void Test_Subtract(double a, double b, double expected)
        {
            Assert.Equal(expected, math.Subtract(a, b));
        }

        // Kiểm thử nhân
        [Theory]
        [InlineData(2, 3, 6)]
        [InlineData(-2, -3, 6)]
        [InlineData(0, 5, 0)]
        public void Test_Multiply(double a, double b, double expected)
        {
            Assert.Equal(expected, math.Multiply(a, b));
        }

        // Kiểm thử chia
        [Theory]
        [InlineData(6, 3, 2)]
        [InlineData(-6, -3, 2)]
        [InlineData(0, 1, 0)]
        public void Test_Divide(double a, double b, double expected)
        {
            Assert.Equal(expected, math.Divide(a, b));
        }

        // Kiểm tra lỗi chia cho 0
        [Fact]
        public void Test_DivideByZero()
        {
            Assert.Throws<DivideByZeroException>(
                () => math.Divide(10, 0));
        }
    }
}
