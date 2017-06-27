using System;
using System.Collections.Generic;
using System.Linq;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Factories
{
    public class SchemaFactory : FactoryBase
    {
        public static SchemaFactory Create()
        {
            return new SchemaFactory();
        }
        
        private readonly List<TableFactory> _tableFactories = new List<TableFactory>();
        private readonly List<RelationshipFactory> _relationshipFactories = new List<RelationshipFactory>();
        
        internal RelationshipFactory Relationship(string name, string primaryKeyTableSchema, string primaryKeyTableName, string foreignKeyTableSchema, string foreignKeyTableName)
        {
            // Relationship name must be unique.
            if (DefinesRelationship(name))
            {
                throw new InvalidOperationException($"Relationship \"{name}\" is already defined for this database.");
            }

            var relationshipFactory = new RelationshipFactory(this, name, primaryKeyTableSchema, primaryKeyTableName, foreignKeyTableSchema, foreignKeyTableName);
            _relationshipFactories.Add(relationshipFactory);
            return relationshipFactory;
        }

        private bool DefinesRelationship(string name)
        {
            return _relationshipFactories.Any(rf => rf.DefinesRelationship(name));
        }

        internal TableFactory GetTableFactory(string schema, string name)
        {
            return _tableFactories.FirstOrDefault(tf => tf.DefinesTable(schema, name));
        }

        internal ColumnFactory GetColumnFactory(string tableSchema, string tableName, string columnName)
        {
            ColumnFactory result = null;
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

        public TableFactory Table(string schema, string name)
        {
            schema.ThrowIfNullOrWhitespace(nameof(schema));
            name.ThrowIfNullOrWhitespace(nameof(name));
            DefaultNamePattern.ThrowIfDoesNotMatch(schema, nameof(schema));
            DefaultNamePattern.ThrowIfDoesNotMatch(name, nameof(name));

            if (_tableFactories.Any(tf => tf.DefinesTable(schema, name)))
            {
                throw new InvalidOperationException($"Table \"{schema}.{name}\" already exists.");
            }

            var tableFactory = new TableFactory(this, schema, name);
            _tableFactories.Add(tableFactory);
            return tableFactory;
        }
    }
}