using System;
using NoiseLab.PolyGen.Core.Domain;
using NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications;
using NoiseLab.PolyGen.Core.FluentConfiguration.Relationships;

namespace NoiseLab.PolyGen.Core.FluentConfiguration
{
    public abstract class ColumnBuilderBase : BuilderBase
    {
        internal ColumnBuilder Column(string name)
        {
            return _tableBuilder.Column(name);
        }

        internal PrimaryKeyColumnBuilder PrimaryKeyColumn(string name)
        {
            return _tableBuilder.PrimaryKeyColumn(name);
        }

        internal TableBuilder Table(string schema, string name)
        {
            return _tableBuilder.Table(schema, name);
        }

        internal RelationshipBuilder Relationship(string name)
        {
            return _tableBuilder.Relationship(name);
        }

        internal Database Build()
        {
            return _tableBuilder.Build();
        }

        internal Column BuildColumn()
        {
            if (_column == null)
            {
                // TODO: Validation.
                _column = new Column(
                    Ordinal,
                    Name,
                    ColumnSpecificationBuilder.DataType,
                    ColumnSpecificationBuilder.MaxLengthInternal,
                    ColumnSpecificationBuilder.NullableInternal,
                    IsPrimaryKey,
                    ColumnSpecificationBuilder.IdentityInternal,
                    ColumnSpecificationBuilder.ComputedInternal);
            }
            return _column;
        }

        internal bool IsNamed(string name)
        {
            return Name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }

        internal bool IsComputed()
        {
            return ColumnSpecificationBuilder.ComputedInternal;
        }

        internal bool IsRowVersion()
        {
            return ColumnSpecificationBuilder.DataType == AbstractDataType.RowVersion;
        }

        internal bool IsNullable()
        {
            return ColumnSpecificationBuilder.NullableInternal;
        }

        internal void CheckReference(ColumnBuilderBase foreignKeyColumn)
        {
            // TODO: Validation - referenced column should be primary key or have unique constraint
            ColumnSpecificationBuilder.CheckReference(foreignKeyColumn.ColumnSpecificationBuilder);
        }

        internal ColumnBuilderBase(TableBuilder tableBuilder, int ordinal, string name)
        {
            _tableBuilder = tableBuilder;
            Ordinal = ordinal;
            Name = name;
        }

        internal int Ordinal { get; }
        internal string Name { get; }
        internal abstract bool IsPrimaryKey { get; }
        private readonly TableBuilder _tableBuilder;
        protected ColumnSpecificationBuilderBase ColumnSpecificationBuilder;
        private Column _column;
    }
}
