using System;
using NoiseLab.Common.Extensions;
using Xunit;

namespace NoiseLab.Common.UnitTests.Extensions
{
    public class ObjectExtensionsTests
    {
        [Fact]
        public void ThrowIfNull_ArgumentIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            var value = (object) null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => value.ThrowIfNull(nameof(value)));
        }

        [Fact]
        public void ThrowIfNull_ArgumentIsNotNull_DoesNotThrow()
        {
            // Arrange
            var value = new object();

            // Act & Assert
            value.ThrowIfNull(nameof(value));
        }
    }
}