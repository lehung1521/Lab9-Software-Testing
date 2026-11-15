
using System;
using BankAccountApp;
using Xunit;

namespace BankAccountTests
{
    public class BankAccountTests
    {
        // DEBIT: EP + BVA

        [Theory]
        [InlineData(100, 50, 50)]  // EP: so tien hop le
        [InlineData(100, 1, 99)]   // BVA: gan muc duoi
        [InlineData(100, 100, 0)]  // BVA: bang so du
        public void Debit_ValidAmount(
            decimal initial, decimal amount, decimal expected)
        {
            var account = new BankAccount("Test", initial);

            account.Debit(amount);

            Assert.Equal(expected, account.Balance);
        }

        [Theory]
        [InlineData(100, 101)] // BVA: vuot so du 1 don vi
        public void Debit_InsufficientFunds(
            decimal initial, decimal amount)
        {
            var account = new BankAccount("Test", initial);

            Assert.Throws<InvalidOperationException>(
                () => account.Debit(amount));
        }

        [Theory]
        [InlineData(0)]  // EP: bang 0
        [InlineData(-1)] // EP: so am
        public void Debit_InvalidAmount(decimal amount)
        {
            var account = new BankAccount("Test", 100);

            Assert.Throws<ArgumentOutOfRangeException>(
                () => account.Debit(amount));
        }

        // CREDIT: EP + BVA

        [Theory]
        [InlineData(100, 50, 150)] // EP: so tien hop le
        [InlineData(100, 1, 101)]  // BVA: so tien nho nhat duong
        public void Credit_ValidAmount(
            decimal initial, decimal amount, decimal expected)
        {
            var account = new BankAccount("Test", initial);

            account.Credit(amount);

            Assert.Equal(expected, account.Balance);
        }

        [Theory]
        [InlineData(0)]   // EP: bang 0
        [InlineData(-1)]  // EP: so am
        public void Credit_InvalidAmount(decimal amount)
        {
            var account = new BankAccount("Test", 100);

            Assert.Throws<ArgumentOutOfRangeException>(
                () => account.Credit(amount));
        }

        // WITHDRAW: EP + BVA

        [Theory]
        [InlineData(100, 50, 50)]  // EP: so tien hop le
        [InlineData(100, 1, 99)]   // BVA: gan muc duoi
        [InlineData(100, 100, 0)]  // BVA: bang so du
        public void Withdraw_ValidAmount(
            decimal initial, decimal amount, decimal expected)
        {
            var account = new BankAccount("Test", initial);

            account.Withdraw(amount);

            Assert.Equal(expected, account.Balance);
        }

        [Theory]
        [InlineData(100, 101)] // BVA: vuot so du
        public void Withdraw_InsufficientFunds(
            decimal initial, decimal amount)
        {
            var account = new BankAccount("Test", initial);

            Assert.Throws<InvalidOperationException>(
                () => account.Withdraw(amount));
        }

        [Theory]
        [InlineData(0)]   // EP: bang 0
        [InlineData(-1)]  // EP: so am
        public void Withdraw_InvalidAmount(decimal amount)
        {
            var account = new BankAccount("Test", 100);

            Assert.Throws<ArgumentOutOfRangeException>(
                () => account.Withdraw(amount));
        }
    }
}
