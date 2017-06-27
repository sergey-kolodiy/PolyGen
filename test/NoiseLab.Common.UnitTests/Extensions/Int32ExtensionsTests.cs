using System;
using NoiseLab.Common.Extensions;
using Xunit;

namespace NoiseLab.Common.UnitTests.Extensions
{
    public class Int32ExtensionsTests
    {
        [Theory]
        [InlineData(0, 10)]
        [InlineData(int.MinValue, int.MaxValue)]
        public void ThrowIfLessThan_ArgumentIsLessThanMinValue_ThrowsArgumentException(int value, int minValue)
        {
            // Arrange
            // Act & Assert
            Assert.Throws<ArgumentException>(() => value.ThrowIfLessThan(minValue, nameof(value)));
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(int.MaxValue, int.MinValue)]
        public void ThrowIfNullOrEmpty_ArgumentIsNotLessThanMinValue_DoesNotThrow(int value, int minValue)
        {
            // Arrange
            // Act & Assert
            value.ThrowIfLessThan(minValue, nameof(value));
        }
    }
}
