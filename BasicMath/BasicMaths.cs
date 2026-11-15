
namespace BasicMath
{
    public class BasicMaths
    {
        // Phép cộng
        public double Add(double num1, double num2)
        {
            return num1 + num2;
        }

        // Phép trừ
        public double Subtract(double num1, double num2)
        {
            return num1 - num2;
        }

        // Phép chia
        public double Divide(double num1, double num2)
        {
            if (num2 == 0)
                throw new DivideByZeroException(
                    "Division by zero is not allowed.");

            return num1 / num2;
        }

        // Phép nhân
        public double Multiply(double num1, double num2)
        {
            return num1 * num2;
        }
    }
}
