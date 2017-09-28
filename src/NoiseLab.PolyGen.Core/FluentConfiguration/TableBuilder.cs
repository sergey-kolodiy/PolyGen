using System;
using System.Collections.Generic;
using System.Linq;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.FluentConfiguration.Relationships;
using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration
{
    public class TableBuilder : BuilderBase
    {
        private readonly string _schema;
        private readonly string _name;
        private readonly List<PrimaryKeyColumnBuilder> _primaryKeyColumnBuilders = new List<PrimaryKeyColumnBuilder>();
        private readonly List<ColumnBuilder> _columnBuilders = new List<ColumnBuilder>();
        private readonly List<ColumnBuilderBase> _allColumnBuilders = new List<ColumnBuilderBase>();
        private int _ordinal;
        private readonly DatabaseBuilder _schemaBuilder;
        private Table _table;
        private string FullName => $"{_schema}.{_name}";

        internal TableBuilder(DatabaseBuilder schemaBuilder, string schema, string name)
        {
            _schema = schema;
            _name = name;
            _schemaBuilder = schemaBuilder;
        }

        public PrimaryKeyColumnBuilder PrimaryKeyColumn(string name)
        {
            name.ThrowIfNullOrWhitespace(nameof(name));
            DefaultNamePattern.ThrowIfDoesNotMatch(name, nameof(name));

            if (DefinesColumn(name))
            {
                throw new InvalidOperationException($"Column \"{name}\" is already defined for table \"{FullName}\".");
            }

            var columnBuilder = new PrimaryKeyColumnBuilder(this, _ordinal++, name);
            _primaryKeyColumnBuilders.Add(columnBuilder);
            _allColumnBuilders.Add(columnBuilder);
            return columnBuilder;
        }

        internal ColumnBuilder Column(string name)
        {
            name.ThrowIfNullOrWhitespace(nameof(name));
            DefaultNamePattern.ThrowIfDoesNotMatch(name, nameof(name));

            if (DefinesColumn(name))
            {
                throw new InvalidOperationException($"Column \"{name}\" is already defined for table \"{FullName}\".");
            }

            var columnBuilder = new ColumnBuilder(this, _ordinal++, name);
            _columnBuilders.Add(columnBuilder);
            _allColumnBuilders.Add(columnBuilder);
            return columnBuilder;
        }

        internal RelationshipBuilder Relationship(string name)
        {
            name.ThrowIfNullOrWhitespace(nameof(name));
            DefaultNamePattern.ThrowIfDoesNotMatch(name, nameof(name));

            return _schemaBuilder.Relationship(name);
        }

        internal TableBuilder Table(string schema, string name)
        {
            ThrowIfTableContainsSingleComputedColumn();
            ThrowIfTableContainsMoreThanOneRowVersionColumn();

            return _schemaBuilder.Table(schema, name);
        }

        internal Database Build()
        {
            ThrowIfTableContainsSingleComputedColumn();
            ThrowIfTableContainsMoreThanOneRowVersionColumn();

            return _schemaBuilder.Build();
        }

        internal Table BuildTable()
        {
            if (_table == null)
            {
                var allColumns = _allColumnBuilders.Select(cf => cf.BuildColumn()).ToList();
                var primaryKeyColumns = _primaryKeyColumnBuilders.Select(cf => cf.BuildColumn()).ToList();
                var primaryKey = new PrimaryKey(primaryKeyColumns);
                _table = new Table(_schema, _name, allColumns, primaryKey);
            }
            return _table;
        }

        internal bool DefinesTable(string schema, string name)
        {
            return _schema.Equals(schema, StringComparison.OrdinalIgnoreCase) && _name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }

        internal ColumnBuilderBase GetColumnBuilder(string columnName)
        {
            return _allColumnBuilders.FirstOrDefault(cf => cf.IsNamed(columnName));
        }

        private bool DefinesColumn(string name)
        {
            return _allColumnBuilders.Any(cf => cf.IsNamed(name));
        }

        private void ThrowIfTableContainsSingleComputedColumn()
        {
            if (_allColumnBuilders.Count == 1 && _allColumnBuilders[0].IsComputed())
            {
                throw new InvalidOperationException(
                    $"The table {FullName} must have at least one column that is not computed.");
            }
        }

        private void ThrowIfTableContainsMoreThanOneRowVersionColumn()
        {
            var rowVersionColumnBuilders = _allColumnBuilders.Where(cf => cf.IsRowVersion()).ToList();
            if (rowVersionColumnBuilders.Count > 1)
            {
                throw new InvalidOperationException(
                    $"A table can only have one row version column. Table '{FullName}' defines {rowVersionColumnBuilders.Count} row version columns.");
            }
        }
    }
}