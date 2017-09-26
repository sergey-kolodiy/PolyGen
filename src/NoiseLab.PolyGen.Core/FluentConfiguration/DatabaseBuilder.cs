using System;
using System.Collections.Generic;
using System.Linq;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.FluentConfiguration.Relationships;
using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration
{
    public class DatabaseBuilder : BuilderBase
    {
        public static DatabaseBuilder Create()
        {
            return new DatabaseBuilder();
        }
        
        private readonly List<TableBuilder> _tableBuilders = new List<TableBuilder>();
        private readonly List<RelationshipBuilder> _relationshipBuilders = new List<RelationshipBuilder>();
        
        internal RelationshipBuilder Relationship(string name)
        {
            // Relationship name must be unique.
            if (DefinesRelationship(name))
            {
                throw new InvalidOperationException($"Relationship \"{name}\" is already defined for this database.");
            }

            var relationshipBuilder = new RelationshipBuilder(this, name);
            _relationshipBuilders.Add(relationshipBuilder);
            return relationshipBuilder;
        }

        private bool DefinesRelationship(string name)
        {
            return _relationshipBuilders.Any(rf => rf.DefinesRelationship(name));
        }

        internal TableBuilder GetTableBuilder(string schema, string name)
        {
            return _tableBuilders.FirstOrDefault(tf => tf.DefinesTable(schema, name));
        }

        internal ColumnBuilder GetColumnBuilder(string tableSchema, string tableName, string columnName)
        {
            ColumnBuilder result = null;
            var tableBuilder = GetTableBuilder(tableSchema, tableName);
            if (tableBuilder != null)
            {
                result = tableBuilder.GetColumnBuilder(columnName);
            }
            return result;
        }

        internal Database Build()
        {
            // TODO: Validate table relationships here.
            // Define validation rules.
            // - Primary key table & column must exist.
            // - Computed columns cannot contribute to foreign keys.

            var tables = _tableBuilders.Select(tf => tf.BuildTable()).ToList();
            var relationships = _relationshipBuilders.Select(rf => rf.BuildRelationship()).ToList();

            foreach (var relationship in relationships)
            {
                relationship.Apply();
            }

            return new Database(tables, relationships);
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

            if (_tableBuilders.Any(tf => tf.DefinesTable(schema, name)))
            {
                throw new InvalidOperationException($"Table \"{schema}.{name}\" already exists.");
            }

            var tableBuilder = new TableBuilder(this, schema, name);
            _tableBuilders.Add(tableBuilder);
            return tableBuilder;
        }
    }
}