using System;
using NoiseLab.PolyGen.Core.FluentConfiguration;
using Xunit;

namespace NoiseLab.PolyGen.Core.UnitTests.FluentConfiguration
{
    public class DatabaseBuilderTests
    {
        [Fact]
        public void Table_SchemaIsNull_ThrowsArgumentException()
        {
            // Arrange
            var builder = DatabaseBuilder.Create();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.Table(null, "Test"));
        }

        [Fact]
        public void Table_NameIsNull_ThrowsArgumentException()
        {
            // Arrange
            var builder = DatabaseBuilder.Create();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.Table("Test", null));
        }

        [Fact]
        public void Table_SchemaDoesNotMatchDefaultPattern_ThrowsArgumentException()
        {
            // Arrange
            var builder = DatabaseBuilder.Create();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.Table("1Test", "Test"));
        }

        [Fact]
        public void Table_NameDoesNotMatchDefaultPattern_ThrowsArgumentException()
        {
            // Arrange
            var builder = DatabaseBuilder.Create();

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.Table("Test", "1Test"));
        }

        [Fact]
        public void Table_ValidArguments_DoesNotThrow()
        {
            // Arrange
            var builder = DatabaseBuilder.Create();

            // Act & Assert
            builder.Table("Test", "Test");
        }

        [Fact]
        public void Table_TableAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var builder = DatabaseBuilder.Create();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                builder
                    .Table("Test", "Test")
                        .PrimaryKeyColumn("Test").String()
                    .Table("Test", "Test"));
        }
    }
}
