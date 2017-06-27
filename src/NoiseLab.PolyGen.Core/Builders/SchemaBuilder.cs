using System;
using System.Collections.Generic;
using System.Linq;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.Builders.Relationships;
using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders
{
    public class SchemaBuilder : BuilderBase
    {
        public static SchemaBuilder Create()
        {
            return new SchemaBuilder();
        }
        
        private readonly List<TableBuilder> _tableFactories = new List<TableBuilder>();
        private readonly List<RelationshipBuilder> _relationshipFactories = new List<RelationshipBuilder>();
        
        internal RelationshipBuilder Relationship(string name)
        {
            // Relationship name must be unique.
            if (DefinesRelationship(name))
            {
                throw new InvalidOperationException($"Relationship \"{name}\" is already defined for this database.");
            }

            var relationshipFactory = new RelationshipBuilder(this, name);
            _relationshipFactories.Add(relationshipFactory);
            return relationshipFactory;
        }

        private bool DefinesRelationship(string name)
        {
            return _relationshipFactories.Any(rf => rf.DefinesRelationship(name));
        }

        internal TableBuilder GetTableFactory(string schema, string name)
        {
            return _tableFactories.FirstOrDefault(tf => tf.DefinesTable(schema, name));
        }

        internal ColumnBuilder GetColumnFactory(string tableSchema, string tableName, string columnName)
        {
            ColumnBuilder result = null;
            var tableFactory = GetTableFactory(tableSchema, tableName);
            if (tableFactory != null)
            {
                result = tableFactory.GetColumnFactory(columnName);
            }
            return result;
        }

        internal Schema Build()
        {
            // TODO: Validate table relationships here.
            // Define validation rules.
            // - Primary key table & column must exist.
            // - Computed columns cannot contribute to foreign keys.

            var tables = _tableFactories.Select(tf => tf.BuildTable()).ToList();
            var relationships = _relationshipFactories.Select(rf => rf.BuildRelationship()).ToList();

            foreach (var relationship in relationships)
            {
                relationship.Apply();
            }

            return new Schema(tables, relationships);
        }

        public TableBuilder Table(string name)
        {
            return Table("dbo", name);
        }

        public TableBuilder Table(string schema, string name)
        {
            schema.ThrowIfNullOrWhitespace(nameof(schema));
            name.ThrowIfNullOrWhitespace(nameof(name));
            DefaultNamePattern.ThrowIfDoesNotMatch(schema, nameof(schema));
            DefaultNamePattern.ThrowIfDoesNotMatch(name, nameof(name));

            if (_tableFactories.Any(tf => tf.DefinesTable(schema, name)))
            {
                throw new InvalidOperationException($"Table \"{schema}.{name}\" already exists.");
            }

            var tableFactory = new TableBuilder(this, schema, name);
            _tableFactories.Add(tableFactory);
            return tableFactory;
        }
    }
}