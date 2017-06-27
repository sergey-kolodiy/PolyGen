using System;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.Builders.Relationships;
using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
{
    public abstract class ColumnSpecificationBuilderBase
    {
        public ColumnBuilder Column(string name)
        {
            return _columnFactory.Column(name);
        }

        public TableBuilder Table(string schema, string name)
        {
            return _columnFactory.Table(schema, name);
        }

        public RelationshipBuilder Relationship(string name)
        {
            return _columnFactory.Relationship(name);
        }

        protected internal abstract AbstractDataType DataType { get; }
        protected internal int? MaxLengthInternal { get; private set; }
        protected internal bool NullableInternal { get; private set; }
        protected internal bool PrimaryKeyInternal { get; private set; }
        protected internal bool IdentityInternal { get; private set; }
        protected internal bool ComputedInternal { get; private set; }
        private readonly ColumnBuilder _columnFactory;

        protected void MaxLength(int maxLength)
        {
            maxLength.ThrowIfLessThan(1, nameof(maxLength));

            MaxLengthInternal = maxLength;
        }

        protected void Nullable()
        {
            if (PrimaryKeyInternal)
            {
                throw new InvalidOperationException(
                    $"Column \"{_columnFactory.Name}\" cannot be nullable because it is a part of the primary key.");
            }
            if (IdentityInternal)
            {
                throw new InvalidOperationException(
                    $"Column \"{_columnFactory.Name}\" cannot be nullable because it is an identity column.");
            }

            NullableInternal = true;
        }

        protected void PrimaryKey()
        {
            if (NullableInternal)
            {
                throw new InvalidOperationException(
                    $"Nullable column \"{_columnFactory.Name}\" cannot be set as a part of the primary key.");
            }

            PrimaryKeyInternal = true;
        }

        protected void Identity()
        {
            if (NullableInternal)
            {
                throw new InvalidOperationException($"Nullable column \"{_columnFactory.Name}\" cannot be set as an identity column.");
            }
            if (ComputedInternal)
            {
                throw new InvalidOperationException($"Computed column \"{_columnFactory.Name}\" cannot be set as an identity column.");
            }

            IdentityInternal = true;
        }

        protected void Computed()
        {
            if (IdentityInternal)
            {
                throw new InvalidOperationException($"Identity column \"{_columnFactory.Name}\" cannot be set as a computed column.");
            }

            ComputedInternal = true;
        }

        internal ColumnSpecificationBuilderBase(ColumnBuilder columnFactory)
        {
            _columnFactory = columnFactory;
        }

        internal void CheckReference(ColumnSpecificationBuilderBase columnSpecification)
        {
            if (DataType != columnSpecification.DataType)
            {
                throw new InvalidOperationException($"Primary key column \"{_columnFactory.Name}\" cannot reference foreign key column \"{columnSpecification._columnFactory.Name}\". Data type mismatch: {DataType} and {columnSpecification.DataType}.");
            }
            // TODO: Add other validations here.
        }
    }
}