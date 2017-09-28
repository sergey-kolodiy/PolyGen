using System;
using NoiseLab.PolyGen.Core.FluentConfiguration;
using Xunit;

namespace NoiseLab.PolyGen.Core.UnitTests.FluentConfiguration
{
    public class TableBuilderTests
    {
        [Fact]
        public void PrimaryKeyColumn_NameIsNull_ThrowsArgumentException()
        {
            // Arrange
            var builder = DatabaseBuilder.Create()
                .Table("Test", "Test");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.PrimaryKeyColumn(null));
        }

        [Fact]
        public void PrimaryKeyColumn_NameDoesNotMatchDefaultPattern_ThrowsArgumentException()
        {
            // Arrange
            var builder = DatabaseBuilder.Create()
                .Table("Test", "Test");

            // Act & Assert
            Assert.Throws<ArgumentException>(() => builder.PrimaryKeyColumn("1Test"));
        }

        [Fact]
        public void PrimaryKeyColumn_ColumnAlreadyExists_ThrowsInvalidOperationException()
        {
            // Arrange
            var builder = DatabaseBuilder.Create()
                .Table("Test", "Test");

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => builder
                .PrimaryKeyColumn("Test").String()
                .PrimaryKeyColumn("Test"));
        }
    }
}
