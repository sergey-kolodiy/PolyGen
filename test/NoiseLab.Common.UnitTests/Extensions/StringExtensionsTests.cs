using System;
using NoiseLab.Common.Extensions;
using Xunit;

namespace NoiseLab.Common.UnitTests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void ThrowIfNullOrWhitespace_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var value = (string)null;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => value.ThrowIfNullOrWhitespace(nameof(value)));
        }

        [Fact]
        public void ThrowIfNullOrWhitespace_ArgumentIsEmptyString_ThrowsArgumentNullException()
        {
            // Arrange
            var value = string.Empty;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => value.ThrowIfNullOrWhitespace(nameof(value)));
        }

        [Fact]
        public void ThrowIfNullOrWhitespace_ArgumentIsWhitespaceString_ThrowsArgumentNullException()
        {
            // Arrange
            var value = " \r\n\t";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => value.ThrowIfNullOrWhitespace(nameof(value)));
        }

        [Fact]
        public void ThrowIfNullOrWhitespace_ArgumentIsNotOrWhitespace_DoesNotThrow()
        {
            // Arrange
            var value = "test";

            // Act & Assert
            value.ThrowIfNullOrWhitespace(nameof(value));
        }
    }
}