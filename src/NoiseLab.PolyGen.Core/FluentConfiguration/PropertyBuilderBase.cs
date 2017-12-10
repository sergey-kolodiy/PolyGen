using System;
using NoiseLab.PolyGen.Core.Domain;
using NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications;
using NoiseLab.PolyGen.Core.FluentConfiguration.Relationships;

namespace NoiseLab.PolyGen.Core.FluentConfiguration
{
    public abstract class PropertyBuilderBase : BuilderBase
    {
        internal PropertyBuilder Property(string name)
        {
            return _entityBuilder.Property(name);
        }

        internal PrimaryKeyPropertyBuilder PrimaryKeyProperty(string name)
        {
            return _entityBuilder.PrimaryKeyProperty(name);
        }

        internal EntityBuilder Entity(string @namespace, string name)
        {
            return _entityBuilder.Entity(@namespace, name);
        }

        internal RelationshipBuilder Relationship(string name)
        {
            return _entityBuilder.Relationship(name);
        }

        internal Model Build()
        {
            return _entityBuilder.Build();
        }

        internal Property BuildProperty()
        {
            if (_property == null)
            {
                // TODO: Validation.
                _property = new Property(
                    Ordinal,
                    Name,
                    PropertySpecificationBuilder.DataType,
                    PropertySpecificationBuilder.MaxLengthInternal,
                    PropertySpecificationBuilder.NullableInternal,
                    IsPrimaryKey,
                    PropertySpecificationBuilder.IdentityInternal,
                    PropertySpecificationBuilder.ComputedInternal);
            }
            return _property;
        }

        internal bool IsNamed(string name)
        {
            return Name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }

        internal bool IsComputed()
        {
            return PropertySpecificationBuilder.ComputedInternal;
        }

        internal bool IsRowVersion()
        {
            return PropertySpecificationBuilder.DataType == AbstractDataType.RowVersion;
        }

        internal bool IsNullable()
        {
            return PropertySpecificationBuilder.NullableInternal;
        }

        internal void CheckReference(PropertyBuilderBase foreignKeyProperty)
        {
            // TODO: Validation - referenced property should be primary key or have unique constraint
            PropertySpecificationBuilder.CheckReference(foreignKeyProperty.PropertySpecificationBuilder);
        }

        internal PropertyBuilderBase(EntityBuilder entityBuilder, int ordinal, string name)
        {
            _entityBuilder = entityBuilder;
            Ordinal = ordinal;
            Name = name;
        }

        internal int Ordinal { get; }
        internal string Name { get; }
        internal abstract bool IsPrimaryKey { get; }
        private readonly EntityBuilder _entityBuilder;
        protected PropertySpecificationBuilderBase PropertySpecificationBuilder;
        private Property _property;
    }
}
