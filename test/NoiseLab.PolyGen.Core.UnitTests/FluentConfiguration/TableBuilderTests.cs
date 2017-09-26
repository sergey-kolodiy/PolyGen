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
            // Act & Assert
            Assert.Throws<ArgumentException>(() => DatabaseBuilder
                .Create()
                .Table("Test", "Test")
                    .Column(null));
        }

        [Fact]
        public void Column_NameDoesNotMatchDefaultPattern_ThrowsArgumentException()
        {
            // Arrange
            // Act & Assert
            Assert.Throws<ArgumentException>(() => DatabaseBuilder
                .Create()
                .Table("Test", "Test")
                    .Column("1Test"));
        }

        [Fact]
        public void Column_ColumnAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => DatabaseBuilder
                .Create()
                .Table("Test", "Test")
                    .Column("Test").String()
                    .Column("Test"));
        }
    }
}
