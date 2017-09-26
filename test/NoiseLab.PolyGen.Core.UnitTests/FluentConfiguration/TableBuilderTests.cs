using System;
using NoiseLab.PolyGen.Core.FluentConfiguration;
using Xunit;

namespace NoiseLab.PolyGen.Core.UnitTests.FluentConfiguration
{
    public class TableBuilderTests
    {
        [Fact]
        public void Column_NameIsNull_ThrowsArgumentException()
        {
            // Arrange
            var builder = DatabaseBuilder.Create()
                .Table("Test", "Test");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.Column(null));
        }

        [Fact]
        public void Column_NameDoesNotMatchDefaultPattern_ThrowsArgumentException()
        {
            // Arrange
            var builder = DatabaseBuilder.Create()
                .Table("Test", "Test");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.Column("1Test"));
        }

        [Fact]
        public void Column_ColumnAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var builder = DatabaseBuilder.Create()
                .Table("Test", "Test");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => builder
                .Column("Test").String()
                .Column("Test"));
        }
    }
}
