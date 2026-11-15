
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace LoginAutomation
{
    public class LoginTests
    {
        public class TestData
        {
            public string Username { get; set; } = "";
            public string Password { get; set; } = "";
        }

        public static IEnumerable<object[]> GetTestData()
        {
            string path = Path.Combine(
                AppContext.BaseDirectory,
                "LoginTestData.csv");

            using var reader = new StreamReader(path);
            using var csv = new CsvReader(
                reader, CultureInfo.InvariantCulture);

            foreach (var record in csv.GetRecords<TestData>())
            {
                yield return new object[]
                {
                    record.Username,
                    record.Password
                };
            }
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Login_WithValidCredentials_ShouldSucceed(
            string username, string password)
        {
            using IWebDriver driver = new ChromeDriver();

            string path = Path.Combine(
                AppContext.BaseDirectory, "login.html");

            driver.Navigate().GoToUrl(
                new Uri(path).AbsoluteUri);

            driver.FindElement(By.Id("username"))
                .SendKeys(username);

            driver.FindElement(By.Id("password"))
                .SendKeys(password);

            driver.FindElement(By.Id("loginButton"))
                .Click();

            string result = driver.FindElement(
            By.Id("result")).Text;

            // Kiểm tra kết quả đăng nhập
            Assert.Equal("Login successful", result);

            // Giữ Chrome mở thêm 5 giây để quan sát
            System.Threading.Thread.Sleep(5000);
        }
    }
}
