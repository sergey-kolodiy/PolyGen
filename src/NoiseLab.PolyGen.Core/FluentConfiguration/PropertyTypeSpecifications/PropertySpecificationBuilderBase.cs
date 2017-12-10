using System;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.Domain;
using NoiseLab.PolyGen.Core.FluentConfiguration.Relationships;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.PropertyTypeSpecifications
{
    public abstract class PropertySpecificationBuilderBase
    {
        public PropertyBuilder Property(string name)
        {
            return _propertyBuilder.Property(name);
        }

        public PrimaryKeyPropertyBuilder PrimaryKeyProperty(string name)
        {
            return _propertyBuilder.PrimaryKeyProperty(name);
        }

        public EntityBuilder Entity(string @namespace, string name)
        {
            return _propertyBuilder.Entity(@namespace, name);
        }

        public RelationshipBuilder Relationship(string name)
        {
            return _propertyBuilder.Relationship(name);
        }

        public Model Build()
        {
            return _propertyBuilder.Build();
        }

        protected internal abstract AbstractDataType DataType { get; }
        protected internal int? MaxLengthInternal { get; private set; }
        protected internal bool NullableInternal { get; private set; }
        protected internal bool IdentityInternal { get; private set; }
        protected internal bool ComputedInternal { get; private set; }

        protected void MaxLength(int maxLength)
        {
            maxLength.ThrowIfLessThan(1, nameof(maxLength));

            MaxLengthInternal = maxLength;
        }

        protected void Nullable()
        {
            if (IdentityInternal)
            {
                throw new InvalidOperationException(
                    $"Property \"{_propertyBuilder.Name}\" cannot be nullable because it is an identity proeprty.");
            }

            NullableInternal = true;
        }

        protected void Identity()
        {
            if (NullableInternal)
            {
                throw new InvalidOperationException($"Nullable property \"{_propertyBuilder.Name}\" cannot be set as an identity property.");
            }
            if (ComputedInternal)
            {
                throw new InvalidOperationException($"Computed property \"{_propertyBuilder.Name}\" cannot be set as an identity property.");
            }

            IdentityInternal = true;
        }

        protected void Computed()
        {
            if (IdentityInternal)
            {
                throw new InvalidOperationException($"Identity property \"{_propertyBuilder.Name}\" cannot be set as a computed property.");
            }

            ComputedInternal = true;
        }

        internal PropertySpecificationBuilderBase(PropertyBuilderBase propertyBuilder)
        {
            _propertyBuilder = propertyBuilder;
        }

        internal void CheckReference(PropertySpecificationBuilderBase propertySpecification)
        {
            if (DataType != propertySpecification.DataType)
            {
                throw new InvalidOperationException($"Primary key property \"{_propertyBuilder.Name}\" cannot reference foreign key property \"{propertySpecification._propertyBuilder.Name}\". Data type mismatch: {DataType} and {propertySpecification.DataType}.");
            }
            // TODO: Add other validations here.
        }

        private readonly PropertyBuilderBase _propertyBuilder;
    }
}