using System;
using System.Collections.Generic;
using System.Linq;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.FluentConfiguration.Relationships;
using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration
{
    public class EntityBuilder : BuilderBase
    {
        private readonly string _namespace;
        private readonly string _name;
        private readonly List<PrimaryKeyPropertyBuilder> _primaryKeyPropertyBuilders = new List<PrimaryKeyPropertyBuilder>();
        private readonly List<PropertyBuilder> _propertyBuilders = new List<PropertyBuilder>();
        private readonly List<PropertyBuilderBase> _allPropertyBuilders = new List<PropertyBuilderBase>();
        private int _ordinal;
        private readonly ModelBuilder _namespaceBuilder;
        private Entity _entity;
        private string FullName => $"{_namespace}.{_name}";

        internal EntityBuilder(ModelBuilder namespaceBuilder, string @namespace, string name)
        {
            _namespace = @namespace;
            _name = name;
            _namespaceBuilder = namespaceBuilder;
        }

        public PrimaryKeyPropertyBuilder PrimaryKeyProperty(string name)
        {
            name.ThrowIfNullOrWhitespace(nameof(name));
            DefaultNamePattern.ThrowIfDoesNotMatch(name, nameof(name));

            if (DefinesProperty(name))
            {
                throw new InvalidOperationException($"Property \"{name}\" is already defined for entity \"{FullName}\".");
            }

            var propertyBuilder = new PrimaryKeyPropertyBuilder(this, _ordinal++, name);
            _primaryKeyPropertyBuilders.Add(propertyBuilder);
            _allPropertyBuilders.Add(propertyBuilder);
            return propertyBuilder;
        }

        internal PropertyBuilder Property(string name)
        {
            name.ThrowIfNullOrWhitespace(nameof(name));
            DefaultNamePattern.ThrowIfDoesNotMatch(name, nameof(name));

            if (DefinesProperty(name))
            {
                throw new InvalidOperationException($"Property \"{name}\" is already defined for entity \"{FullName}\".");
            }

            var propertyBuilder = new PropertyBuilder(this, _ordinal++, name);
            _propertyBuilders.Add(propertyBuilder);
            _allPropertyBuilders.Add(propertyBuilder);
            return propertyBuilder;
        }

        internal RelationshipBuilder Relationship(string name)
        {
            name.ThrowIfNullOrWhitespace(nameof(name));
            DefaultNamePattern.ThrowIfDoesNotMatch(name, nameof(name));

            return _namespaceBuilder.Relationship(name);
        }

        internal EntityBuilder Entity(string @namespace, string name)
        {
            ThrowIfEntityContainsSingleComputedProperty();
            ThrowIfEntityContainsMoreThanOneRowVersionProperty();

            return _namespaceBuilder.Entity(@namespace, name);
        }

        internal Model Build()
        {
            ThrowIfEntityContainsSingleComputedProperty();
            ThrowIfEntityContainsMoreThanOneRowVersionProperty();

            return _namespaceBuilder.Build();
        }

        internal Entity BuildEntity()
        {
            if (_entity == null)
            {
                var allProperties = _allPropertyBuilders.Select(cf => cf.BuildProperty()).ToList();
                var primaryKeyProperties = _primaryKeyPropertyBuilders.Select(cf => cf.BuildProperty()).ToList();
                var primaryKey = new PrimaryKey(primaryKeyProperties);
                _entity = new Entity(_namespace, _name, allProperties, primaryKey);
            }
            return _entity;
        }

        internal bool DefinesEntity(string @namespace, string name)
        {
            return _namespace.Equals(@namespace, StringComparison.OrdinalIgnoreCase) && _name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }

        internal PropertyBuilderBase GetPropertyBuilder(string propertyName)
        {
            return _allPropertyBuilders.FirstOrDefault(cf => cf.IsNamed(propertyName));
        }

        private bool DefinesProperty(string name)
        {
            return _allPropertyBuilders.Any(cf => cf.IsNamed(name));
        }

        private void ThrowIfEntityContainsSingleComputedProperty()
        {
            if (_allPropertyBuilders.Count == 1 && _allPropertyBuilders[0].IsComputed())
            {
                throw new InvalidOperationException(
                    $"The entity {FullName} must have at least one property that is not computed.");
            }
        }

        private void ThrowIfEntityContainsMoreThanOneRowVersionProperty()
        {
            var rowVersionPropertyBuilders = _allPropertyBuilders.Where(cf => cf.IsRowVersion()).ToList();
            if (rowVersionPropertyBuilders.Count > 1)
            {
                throw new InvalidOperationException(
                    $"An entity can only have one row version property. Entity '{FullName}' defines {rowVersionPropertyBuilders.Count} row version properties.");
            }
        }
    }
}