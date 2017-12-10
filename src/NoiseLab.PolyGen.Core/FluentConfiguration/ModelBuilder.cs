using System;
using System.Collections.Generic;
using System.Linq;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.FluentConfiguration.Relationships;
using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration
{
    public class ModelBuilder : BuilderBase
    {
        public static ModelBuilder Create()
        {
            return new ModelBuilder();
        }
        
        private readonly List<EntityBuilder> _entityBuilders = new List<EntityBuilder>();
        private readonly List<RelationshipBuilder> _relationshipBuilders = new List<RelationshipBuilder>();
        
        internal RelationshipBuilder Relationship(string name)
        {
            // Relationship name must be unique.
            if (DefinesRelationship(name))
            {
                throw new InvalidOperationException($"Relationship \"{name}\" is already defined for this model.");
            }

            var relationshipBuilder = new RelationshipBuilder(this, name);
            _relationshipBuilders.Add(relationshipBuilder);
            return relationshipBuilder;
        }

        private bool DefinesRelationship(string name)
        {
            return _relationshipBuilders.Any(rf => rf.DefinesRelationship(name));
        }

        internal EntityBuilder GetEntityBuilder(string @namespace, string name)
        {
            return _entityBuilders.FirstOrDefault(tf => tf.DefinesEntity(@namespace, name));
        }

        internal PropertyBuilderBase GetPropertyBuilder(string entityNamespace, string entityName, string propertyName)
        {
            PropertyBuilderBase result = null;
            var entityBuilder = GetEntityBuilder(entityNamespace, entityName);
            if (entityBuilder != null)
            {
                result = entityBuilder.GetPropertyBuilder(propertyName);
            }
            return result;
        }

        internal Model Build()
        {
            // TODO: Validate entity relationships here.
            // Define validation rules.
            // - Primary key entity & property must exist.
            // - Computed properties cannot contribute to foreign keys.

            var entities = _entityBuilders.Select(tf => tf.BuildEntity()).ToList();
            var relationships = _relationshipBuilders.Select(rf => rf.BuildRelationship()).ToList();

            foreach (var relationship in relationships)
            {
                relationship.Apply();
            }

            return new Model(entities, relationships);
        }

        public EntityBuilder Entity(string name)
        {
            return Entity("dbo", name);
        }

        public EntityBuilder Entity(string @namespace, string name)
        {
            @namespace.ThrowIfNullOrWhitespace(nameof(@namespace));
            name.ThrowIfNullOrWhitespace(nameof(name));
            DefaultNamePattern.ThrowIfDoesNotMatch(@namespace, nameof(@namespace));
            DefaultNamePattern.ThrowIfDoesNotMatch(name, nameof(name));

            if (_entityBuilders.Any(tf => tf.DefinesEntity(@namespace, name)))
            {
                throw new InvalidOperationException($"Entity \"{@namespace}.{name}\" already exists.");
            }

            var entityBuilder = new EntityBuilder(this, @namespace, name);
            _entityBuilders.Add(entityBuilder);
            return entityBuilder;
        }
    }
}