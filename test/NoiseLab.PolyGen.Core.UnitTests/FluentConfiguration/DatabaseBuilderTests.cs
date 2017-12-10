using System;
using NoiseLab.PolyGen.Core.FluentConfiguration;
using Xunit;

namespace NoiseLab.PolyGen.Core.UnitTests.FluentConfiguration
{
    public class DatabaseBuilderTests
    {
        [Fact]
        public void Entity_NamespaceIsNull_ThrowsArgumentException()
        {
            // Arrange
            var builder = ModelBuilder.Create();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.Entity(null, "Test"));
        }

        [Fact]
        public void Entity_NameIsNull_ThrowsArgumentException()
        {
            // Arrange
            var builder = ModelBuilder.Create();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.Entity("Test", null));
        }

        [Fact]
        public void Entity_NamespaceDoesNotMatchDefaultPattern_ThrowsArgumentException()
        {
            // Arrange
            var builder = ModelBuilder.Create();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.Entity("1Test", "Test"));
        }

        [Fact]
        public void Entity_NameDoesNotMatchDefaultPattern_ThrowsArgumentException()
        {
            // Arrange
            var builder = ModelBuilder.Create();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.Entity("Test", "1Test"));
        }

        [Fact]
        public void Entity_ValidArguments_DoesNotThrow()
        {
            // Arrange
            var builder = ModelBuilder.Create();

            // Act & Assert
            builder.Entity("Test", "Test");
        }

        [Fact]
        public void Entity_EntityAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var builder = ModelBuilder.Create();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                builder
                    .Entity("Test", "Test")
                        .PrimaryKeyProperty("Test").String()
                    .Entity("Test", "Test"));
        }
    }
}
