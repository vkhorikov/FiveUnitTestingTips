using FluentAssertions;
using Xunit;

namespace Module1
{
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }

    public class CalculatorController
    {
        private int _memorizedDividend;

        public Envelope<int> Divide(int dividend, int divisor)
        {
            if (divisor == 0)
                return Envelope<int>.Error(Errors.DivisionByZero);

            int result = dividend / divisor;

            return Envelope<int>.Ok(result);
        }

        public void Memorize(int dividend)
        {
            _memorizedDividend = dividend;
        }

        public Envelope<int> Divide(int divisor)
        {
            if (divisor == 0)
                return Envelope<int>.Error(Errors.DivisionByZero);

            int result = _memorizedDividend / divisor;

            return Envelope<int>.Ok(result);
        }
    }

    public static class Errors
    {
        public const string DivisionByZero = "division.by.zero";
    }

    public class Envelope<T>
    {
        public T Result { get; }
        public string ErrorCode { get; }

        public bool IsError => ErrorCode != null;

        private Envelope(T result, string errorCode)
        {
            Result = result;
            ErrorCode = errorCode;
        }

        public static Envelope<T> Ok(T result)
        {
            return new Envelope<T>(result, null);
        }

        public static Envelope<T> Error(string errorCode)
        {
            return new Envelope<T>(default(T), errorCode);
        }
    }

    public class Tests
    {
        [Fact]
        public void Division_by_zero_dependency_on_production_code()
        {
            // Arrange
            int dividend = 10;
            int divisor = 0;
            var calculator = new CalculatorController();

            // Act
            Envelope<int> response = calculator.Divide(dividend, divisor);

            // Assert
            response.IsError.Should().BeTrue();
            response.ErrorCode.Should().Be(Errors.DivisionByZero);
        }

        [Fact]
        public void Division_by_zero_correct()
        {
            // Arrange
            int dividend = 10;
            int divisor = 0;
            var calculator = new CalculatorController();

            // Act
            Envelope<int> response = calculator.Divide(dividend, divisor);

            // Assert
            response.IsError.Should().BeTrue();
            response.ErrorCode.Should().Be("division.by.zero");
        }

        [Fact]
        public void Division_of_two_values_dependency_on_production_code()
        {
            // Arrange
            int dividend = 10;
            int divisor = 2;
            var calculator = new CalculatorController();

            // Act
            Envelope<int> response = calculator.Divide(dividend, divisor);

            // Assert
            response.IsError.Should().BeFalse();
            response.Result.Should().Be(dividend / divisor);
        }

        [Fact]
        public void Division_of_two_values_correct()
        {
            // Arrange
            int dividend = 10;
            int divisor = 2;
            var calculator = new CalculatorController();

            // Act
            Envelope<int> response = calculator.Divide(dividend, divisor);

            // Assert
            response.IsError.Should().BeFalse();
            response.Result.Should().Be(5);
        }

        readonly int _dividend = 10;
        readonly CalculatorController _calculator = new CalculatorController();

        [Fact]
        public void Division_by_zero_dependency_on_another_test()
        {
            // Arrange
            int divisor = 0;

            // Act
            Envelope<int> response = _calculator.Divide(_dividend, divisor);

            // Assert
            response.IsError.Should().BeTrue();
            response.ErrorCode.Should().Be("division.by.zero");
        }

        [Fact]
        public void Division_of_two_values_dependency_on_another_test()
        {
            // Arrange
            int divisor = 2;

            // Act
            Envelope<int> response = _calculator.Divide(_dividend, divisor);

            // Assert
            response.IsError.Should().BeFalse();
            response.Result.Should().Be(5);
        }

        [Fact]
        public void Division_by_zero_with_memorization()
        {
            int divisor = 0;
            CalculatorController calculator = CreateCalculatorWithMemorizedDividend(10);

            Envelope<int> response = calculator.Divide(divisor);

            response.IsError.Should().BeTrue();
            response.ErrorCode.Should().Be("division.by.zero");
        }

        [Fact]
        public void Division_of_two_values_with_memorization()
        {
            int divisor = 2;
            CalculatorController calculator = CreateCalculatorWithMemorizedDividend(10);

            Envelope<int> response = calculator.Divide(divisor);

            response.IsError.Should().BeFalse();
            response.Result.Should().Be(5);
        }

        private CalculatorController CreateCalculatorWithMemorizedDividend(int dividend)
        {
            var calculator = new CalculatorController();
            calculator.Memorize(dividend);
            return calculator;
        }
    }
}
