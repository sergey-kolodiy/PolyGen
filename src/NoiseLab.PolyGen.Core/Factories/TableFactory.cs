using System;
using System.Collections.Generic;
using System.Linq;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Factories
{
    public class TableFactory : FactoryBase
    {
        private readonly string _schema;
        private readonly string _name;
        private readonly List<ColumnFactory> _columnFactories = new List<ColumnFactory>();
        private int _ordinal;
        private readonly SchemaFactory _schemaFactory;
        private Table _table;
        private string FullName => $"{_schema}.{_name}";

        internal TableFactory(SchemaFactory schemaFactory, string schema, string name)
        {
            _schema = schema;
            _name = name;
            _schemaFactory = schemaFactory;
        }

        public ColumnFactory Column(string name, AbstractDataType dataType)
        {
            name.ThrowIfNullOrWhitespace(nameof(name));
            DefaultNamePattern.ThrowIfDoesNotMatch(name, nameof(name));
            dataType.ThrowIfNull(nameof(dataType));

            if (DefinesColumn(name))
            {
                throw new InvalidOperationException($"Column \"{name}\" is already defined for table \"{FullName}\".");
            }

            var columnFactory = new ColumnFactory(this, _ordinal++, name, dataType);
            _columnFactories.Add(columnFactory);
            return columnFactory;
        }

        internal RelationshipFactory Relationship(string name, string primaryKeyTableSchema, string primaryKeyTableName, string foreignKeyTableSchema, string foreignKeyTableName)
        {
            name.ThrowIfNullOrWhitespace(nameof(name));
            DefaultNamePattern.ThrowIfDoesNotMatch(name, nameof(name));
            primaryKeyTableSchema.ThrowIfNullOrWhitespace(nameof(primaryKeyTableSchema));
            primaryKeyTableName.ThrowIfNullOrWhitespace(nameof(primaryKeyTableName));
            foreignKeyTableSchema.ThrowIfNullOrWhitespace(nameof(foreignKeyTableSchema));
            foreignKeyTableName.ThrowIfNullOrWhitespace(nameof(foreignKeyTableName));

            return _schemaFactory.Relationship(name, primaryKeyTableSchema, primaryKeyTableName, foreignKeyTableSchema, foreignKeyTableName);
        }

        internal TableFactory Table(string schema, string name)
        {
            ThrowIfTableContainsSingleComputedColumn();
            ThrowIfTableContainsMoreThanOneRowVersionColumn();

            return _schemaFactory.Table(schema, name);
        }

        internal Schema Build()
        {
            ThrowIfTableContainsSingleComputedColumn();
            ThrowIfTableContainsMoreThanOneRowVersionColumn();

            return _schemaFactory.Build();
        }

        internal Table BuildTable()
        {
            if (_table == null)
            {
                var columns = _columnFactories.Select(cf => cf.BuildColumn()).ToList();
                var primaryKey = new PrimaryKey(columns.Where(c => c.PrimaryKey).ToList());
                _table = new Table(_schema, _name, columns, primaryKey);
            }
            return _table;
        }

        internal bool DefinesTable(string schema, string name)
        {
            return _schema.Equals(schema, StringComparison.OrdinalIgnoreCase) && _name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }

        internal ColumnFactory GetColumnFactory(string columnName)
        {
            return _columnFactories.FirstOrDefault(cf => cf.IsNamed(columnName));
        }

        private bool DefinesColumn(string name)
        {
            return _columnFactories.Any(cf => cf.IsNamed(name));
        }

        private void ThrowIfTableContainsSingleComputedColumn()
        {
            if (_columnFactories.Count == 1 && _columnFactories[0].IsComputed())
            {
                throw new InvalidOperationException(
                    $"The table {FullName} must have at least one column that is not computed.");
            }
        }

        private void ThrowIfTableContainsMoreThanOneRowVersionColumn()
        {
            var rowVersionColumnFactories = _columnFactories.Where(cf => cf.IsRowVersion()).ToList();
            if (rowVersionColumnFactories.Count > 1)
            {
                throw new InvalidOperationException(
                    $"A table can only have one row version column. Table '{FullName}' defines {rowVersionColumnFactories.Count} row version columns.");
            }
        }
    }
}