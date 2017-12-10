using System;
using NoiseLab.PolyGen.Core.FluentConfiguration;
using Xunit;

namespace NoiseLab.PolyGen.Core.UnitTests.FluentConfiguration
{
    public class EntityBuilderTests
    {
        [Fact]
        public void PrimaryKeyProperty_NameIsNull_ThrowsArgumentException()
        {
            // Arrange
            var builder = ModelBuilder.Create()
                .Entity("Test", "Test");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.PrimaryKeyProperty(null));
        }

        [Fact]
        public void PrimaryKeyProperty_NameDoesNotMatchDefaultPattern_ThrowsArgumentException()
        {
            // Arrange
            var builder = ModelBuilder.Create()
                .Entity("Test", "Test");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.PrimaryKeyProperty("1Test"));
        }

        [Fact]
        public void PrimaryKeyProperty_PropertyAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var builder = ModelBuilder.Create()
                .Entity("Test", "Test");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => builder
                .PrimaryKeyProperty("Test").String()
                .PrimaryKeyProperty("Test"));
        }
    }
}
