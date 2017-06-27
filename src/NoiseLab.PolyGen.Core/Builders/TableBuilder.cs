using System;
using System.Collections.Generic;
using System.Linq;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.Builders.Relationships;
using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders
{
    public class TableBuilder : BuilderBase
    {
        private readonly string _schema;
        private readonly string _name;
        private readonly List<ColumnBuilder> _columnFactories = new List<ColumnBuilder>();
        private int _ordinal;
        private readonly SchemaBuilder _schemaFactory;
        private Table _table;
        private string FullName => $"{_schema}.{_name}";

        internal TableBuilder(SchemaBuilder schemaFactory, string schema, string name)
        {
            _schema = schema;
            _name = name;
            _schemaFactory = schemaFactory;
        }

        public ColumnBuilder Column(string name)
        {
            name.ThrowIfNullOrWhitespace(nameof(name));
            DefaultNamePattern.ThrowIfDoesNotMatch(name, nameof(name));

            if (DefinesColumn(name))
            {
                throw new InvalidOperationException($"Column \"{name}\" is already defined for table \"{FullName}\".");
            }

            var columnFactory = new ColumnBuilder(this, _ordinal++, name);
            _columnFactories.Add(columnFactory);
            return columnFactory;
        }

        internal RelationshipBuilder Relationship(string name)
        {
            name.ThrowIfNullOrWhitespace(nameof(name));
            DefaultNamePattern.ThrowIfDoesNotMatch(name, nameof(name));

            return _schemaFactory.Relationship(name);
        }

        internal TableBuilder Table(string schema, string name)
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

        internal ColumnBuilder GetColumnFactory(string columnName)
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