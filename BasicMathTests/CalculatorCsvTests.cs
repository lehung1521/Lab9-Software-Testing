
using BasicMath;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace BasicMathTests
{
    public partial class CalculatorCsvTests
    {
        [CsvRecord]
        public partial class TestData
        {
            public double A { get; set; }
            public double B { get; set; }
            public double Expected { get; set; }
        }

        public static IEnumerable<object[]> GetTestData(
            string fileName)
        {
            string path = Path.Combine(
                AppContext.BaseDirectory, fileName);

            using var csv = CsvReader.FromFile(path);

            foreach (var record in csv.GetRecords<TestData>())
            {
                yield return new object[]
                {
                    record.A,
                    record.B,
                    record.Expected
                };
            }
        }

        [Theory]
        [MemberData(nameof(GetTestData),
            parameters: "add_testdata.csv")]
        public void Test_AddCsv(
            double a, double b, double expected)
        {
            var math = new BasicMaths();
            Assert.Equal(expected, math.Add(a, b));
        }

        [Theory]
        [MemberData(nameof(GetTestData),
            parameters: "subtract_testdata.csv")]
        public void Test_SubtractCsv(
            double a, double b, double expected)
        {
            var math = new BasicMaths();
            Assert.Equal(expected, math.Subtract(a, b));
        }

        [Theory]
        [MemberData(nameof(GetTestData),
            parameters: "multiply_testdata.csv")]
        public void Test_MultiplyCsv(
            double a, double b, double expected)
        {
            var math = new BasicMaths();
            Assert.Equal(expected, math.Multiply(a, b));
        }

        [Theory]
        [MemberData(nameof(GetTestData),
            parameters: "divide_testdata.csv")]
        public void Test_DivideCsv(
            double a, double b, double expected)
        {
            var math = new BasicMaths();
            Assert.Equal(expected, math.Divide(a, b));
        }
    }
}
