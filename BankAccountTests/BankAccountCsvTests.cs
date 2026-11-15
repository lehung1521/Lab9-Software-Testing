
using BankAccountApp;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Xunit;

namespace BankAccountTests
{
    // Khai báo dữ liệu đọc từ CSV
    [CsvRecord]
    public partial class TestData
    {
        public string CustomerName { get; set; } = "";
        public decimal InitialBalance { get; set; }
        public decimal DebitAmount { get; set; }
        public string ExpectedBalance { get; set; } = "";
    }

    public class BankAccountCsvTests
    {
        // Đọc dữ liệu từ file CSV
        public static IEnumerable<object[]> GetTestData()
        {
            string path = Path.Combine(
                AppContext.BaseDirectory,
                "BankAccountTestData.csv");

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                    "Không tìm thấy file CSV: " + path);
            }

            using var csv = CsvReader.FromFile(path);

            foreach (var record in csv.GetRecords<TestData>())
            {
                yield return new object[]
                {
                    record.CustomerName,
                    record.InitialBalance,
                    record.DebitAmount,
                    record.ExpectedBalance
                };
            }
        }

        // Kiểm thử chức năng rút tiền
        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Test_DebitCsv(
            string name,
            decimal initial,
            decimal amount,
            string expected)
        {
            var account = new BankAccount(name, initial);

            if (expected == "Insufficient funds")
            {
                Assert.Throws<InvalidOperationException>(
                    () => account.Debit(amount));
            }
            else
            {
                account.Debit(amount);

                Assert.Equal(
                    decimal.Parse(
                        expected,
                        CultureInfo.InvariantCulture),
                    account.Balance);
            }
        }
    }
}
