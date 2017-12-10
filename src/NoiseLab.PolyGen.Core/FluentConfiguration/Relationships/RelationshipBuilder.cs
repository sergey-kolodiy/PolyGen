using System;
using System.Collections.Generic;
using System.Linq;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.Relationships
{
    public class RelationshipBuilder : BuilderBase
    {
        public FromRelationshipEntityBuilder From(string entityNamespace, string entityName)
        {
            return _fromRelationshipEntityBuilder = new FromRelationshipEntityBuilder(this, entityNamespace, entityName);
        }

        internal RelationshipBuilder(ModelBuilder modelBuilder, string name)
        {
            _name = name;
            _modelBuilder = modelBuilder;
        }

        private readonly string _name;
        private FromRelationshipEntityBuilder _fromRelationshipEntityBuilder;
        private ToRelationshipEntityBuilder _toRelationshipEntityBuilder;
        private bool _onDeleteCascade;
        private bool _onDeleteSetNull;
        private readonly List<ReferenceBuilder> _referenceBuilders = new List<ReferenceBuilder>();
        private readonly ModelBuilder _modelBuilder;

        internal ToRelationshipEntityBuilder To(string entityNamespace, string entityName)
        {
            return _toRelationshipEntityBuilder = new ToRelationshipEntityBuilder(this, entityNamespace, entityName);
        }

        internal ReferenceBuilder Reference(string foreignKeyPropertyName, string primaryKeyPropertyName)
        {
            foreignKeyPropertyName.ThrowIfNullOrWhitespace(nameof(foreignKeyPropertyName));
            primaryKeyPropertyName.ThrowIfNullOrWhitespace(nameof(primaryKeyPropertyName));

            var referenceBuilder = new ReferenceBuilder(this, primaryKeyPropertyName, foreignKeyPropertyName);
            _referenceBuilders.Add(referenceBuilder);
            return referenceBuilder;
        }

        internal RelationshipBuilder OnDeleteCascade()
        {
            if (_onDeleteSetNull)
            {
                throw new InvalidOperationException($"Cannot set up Cascade delete behavior for relationship \"{_name}\" because SetNull delete behavior has already been specified.");
            }
            _onDeleteCascade = true;
            return this;
        }

        internal RelationshipBuilder OnDeleteSetNull()
        {
            if (_onDeleteCascade)
            {
                throw new InvalidOperationException($"Cannot set up SetNull delete behavior for relationship \"{_name}\" because Cascade delete behavior has already been specified.");
            }
            foreach (var referenceBuilder in _referenceBuilders)
            {
                var property = _modelBuilder.GetPropertyBuilder(_fromRelationshipEntityBuilder.EntityNamespace, _fromRelationshipEntityBuilder.EntityName,
                    referenceBuilder.ForeignKeyPropertyName);
                if (!property.IsNullable())
                {
                    throw new InvalidOperationException($"Cannot set up SetNull delete behavior for relationship \"{_name}\" bacause foreign key property \"{referenceBuilder.ForeignKeyPropertyName}\" is not nullable.");
                }
            }

            _onDeleteSetNull = true;
            return this;
        }

        internal RelationshipBuilder Relationship(string name)
        {
            return _modelBuilder.Relationship(name);
        }

        internal Model Build()
        {
            return _modelBuilder.Build();
        }

        internal Relationship BuildRelationship()
        {
            var primaryKeyEntityBuilder = _modelBuilder.GetEntityBuilder(_toRelationshipEntityBuilder.EntityNamespace, _toRelationshipEntityBuilder.EntityName);
            if (primaryKeyEntityBuilder == null)
            {
                throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Primary key entity \"{_toRelationshipEntityBuilder.EntityNamespace}.{_toRelationshipEntityBuilder.EntityName}\" is not defined.");
            }

            var foreignKeyEntityBuilder = _modelBuilder.GetEntityBuilder(_fromRelationshipEntityBuilder.EntityNamespace, _fromRelationshipEntityBuilder.EntityName);
            if (foreignKeyEntityBuilder == null)
            {
                throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Foreign key entity \"{_fromRelationshipEntityBuilder.EntityNamespace}.{_fromRelationshipEntityBuilder.EntityName}\" is not defined.");
            }

            var references = _referenceBuilders.Select(r => BuildReference(r, primaryKeyEntityBuilder, foreignKeyEntityBuilder)).ToList();

            var primaryKeyEntity = primaryKeyEntityBuilder.BuildEntity();
            var foreignKeyEntity = foreignKeyEntityBuilder.BuildEntity();

            return new Relationship(_name, _onDeleteCascade, _onDeleteSetNull, primaryKeyEntity, foreignKeyEntity, references);

            Reference BuildReference(ReferenceBuilder referenceBuilder, EntityBuilder pkEntityBuilder, EntityBuilder fkEntityBuilder)
            {
                var primaryKeyProperty = pkEntityBuilder.GetPropertyBuilder(referenceBuilder.PrimaryKeyPropertyName);
                if (primaryKeyProperty == null)
                {
                    throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Property \"{referenceBuilder.PrimaryKeyPropertyName}\" is not defined in primary key entity \"{_toRelationshipEntityBuilder.EntityNamespace}.{_toRelationshipEntityBuilder.EntityName}\".");
                }

                var foreignKeyProperty = fkEntityBuilder.GetPropertyBuilder(referenceBuilder.ForeignKeyPropertyName);
                if (foreignKeyProperty == null)
                {
                    throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Property \"{referenceBuilder.ForeignKeyPropertyName}\" is not defined in foreign key entity \"{_fromRelationshipEntityBuilder.EntityNamespace}.{_fromRelationshipEntityBuilder.EntityName}\".");
                }

                primaryKeyProperty.CheckReference(foreignKeyProperty);

                return new Reference(primaryKeyProperty.BuildProperty(), foreignKeyProperty.BuildProperty());
            }
        }

        internal bool DefinesRelationship(string name)
        {
            return _name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
