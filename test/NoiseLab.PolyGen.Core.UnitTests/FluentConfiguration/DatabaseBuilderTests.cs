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
            // Act & Assert
            Assert.Throws<ArgumentException>(() => DatabaseBuilder
                .Create()
                .Table(null, "Test"));
        }

        [Fact]
        public void Table_NameIsNull_ThrowsArgumentException()
        {
            // Arrange
            // Act & Assert
            Assert.Throws<ArgumentException>(() => DatabaseBuilder
                .Create()
                .Table("Test", null));
        }

        [Fact]
        public void Table_SchemaDoesNotMatchDefaultPattern_ThrowsArgumentException()
        {
            // Arrange
            // Act & Assert
            Assert.Throws<ArgumentException>(() => DatabaseBuilder
                .Create()
                .Table("1Test", "Test"));
        }

        [Fact]
        public void Table_NameDoesNotMatchDefaultPattern_ThrowsArgumentException()
        {
            // Arrange
            // Act & Assert
            Assert.Throws<ArgumentException>(() => DatabaseBuilder
                .Create()
                .Table("Test", "1Test"));
        }

        [Fact]
        public void Table_ValidArguments_DoesNotThrow()
        {
            // Arrange
            // Act & Assert
            DatabaseBuilder
                .Create()
                .Table("Test", "Test");
        }

        [Fact]
        public void Table_TableAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => DatabaseBuilder
                .Create()
                .Table("Test", "Test")
                    .Column("Test").String()
                .Table("Test", "Test"));
        }
    }
}
