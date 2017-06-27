using System;
using System.Collections.Generic;
using System.Linq;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.Relationships
{
    public class RelationshipBuilder : BuilderBase
    {
        public FromRelationshipTableBuilder From(string tableSchema, string tableName)
        {
            return _fromRelationshipTableBuilder = new FromRelationshipTableBuilder(this, tableSchema, tableName);
        }

        internal RelationshipBuilder(SchemaBuilder databaseBuilder, string name)
        {
            _name = name;
            _databaseBuilder = databaseBuilder;
        }

        private readonly string _name;
        private FromRelationshipTableBuilder _fromRelationshipTableBuilder;
        private ToRelationshipTableBuilder _toRelationshipTableBuilder;
        private bool _onDeleteCascade;
        private bool _onDeleteSetNull;
        private readonly List<ReferenceBuilder> _referenceBuilders = new List<ReferenceBuilder>();
        private readonly SchemaBuilder _databaseBuilder;

        internal ToRelationshipTableBuilder To(string tableSchema, string tableName)
        {
            return _toRelationshipTableBuilder = new ToRelationshipTableBuilder(this, tableSchema, tableName);
        }

        internal ReferenceBuilder Reference(string foreignKeyColumnName, string primaryKeyColumnName)
        {
            foreignKeyColumnName.ThrowIfNullOrWhitespace(nameof(foreignKeyColumnName));
            primaryKeyColumnName.ThrowIfNullOrWhitespace(nameof(primaryKeyColumnName));

            var referenceBuilder = new ReferenceBuilder(this, primaryKeyColumnName, foreignKeyColumnName);
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
                var column = _databaseBuilder.GetColumnBuilder(_fromRelationshipTableBuilder.TableSchema, _fromRelationshipTableBuilder.TableName,
                    referenceBuilder.ForeignKeyColumnName);
                if (!column.IsNullable())
                {
                    throw new InvalidOperationException($"Cannot set up SetNull delete behavior for relationship \"{_name}\" bacause foreign key column \"{referenceBuilder.ForeignKeyColumnName}\" is not nullable.");
                }
            }

            _onDeleteSetNull = true;
            return this;
        }

        internal RelationshipBuilder Relationship(string name)
        {
            return _databaseBuilder.Relationship(name);
        }

        internal Schema Build()
        {
            return _databaseBuilder.Build();
        }

        internal Relationship BuildRelationship()
        {
            var primaryKeyTableBuilder = _databaseBuilder.GetTableBuilder(_toRelationshipTableBuilder.TableSchema, _toRelationshipTableBuilder.TableName);
            if (primaryKeyTableBuilder == null)
            {
                throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Primary key table \"{_toRelationshipTableBuilder.TableSchema}.{_toRelationshipTableBuilder.TableName}\" is not defined.");
            }

            var foreignKeyTableBuilder = _databaseBuilder.GetTableBuilder(_fromRelationshipTableBuilder.TableSchema, _fromRelationshipTableBuilder.TableName);
            if (foreignKeyTableBuilder == null)
            {
                throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Foreign key table \"{_fromRelationshipTableBuilder.TableSchema}.{_fromRelationshipTableBuilder.TableName}\" is not defined.");
            }

            var references = _referenceBuilders.Select(r => BuildReference(r, primaryKeyTableBuilder, foreignKeyTableBuilder)).ToList();

            var primaryKeyTable = primaryKeyTableBuilder.BuildTable();
            var foreignKeyTable = foreignKeyTableBuilder.BuildTable();

            return new Relationship(_name, _onDeleteCascade, _onDeleteSetNull, primaryKeyTable, foreignKeyTable, references);

            Reference BuildReference(ReferenceBuilder referenceBuilder, TableBuilder pkTableBuilder, TableBuilder fkTableBuilder)
            {
                var primaryKeyColumn = pkTableBuilder.GetColumnBuilder(referenceBuilder.PrimaryKeyColumnName);
                if (primaryKeyColumn == null)
                {
                    throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Column \"{referenceBuilder.PrimaryKeyColumnName}\" is not defined in primary key table \"{_toRelationshipTableBuilder.TableSchema}.{_toRelationshipTableBuilder.TableName}\".");
                }

                var foreignKeyColumn = fkTableBuilder.GetColumnBuilder(referenceBuilder.ForeignKeyColumnName);
                if (foreignKeyColumn == null)
                {
                    throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Column \"{referenceBuilder.ForeignKeyColumnName}\" is not defined in foreign key table \"{_fromRelationshipTableBuilder.TableSchema}.{_fromRelationshipTableBuilder.TableName}\".");
                }

                primaryKeyColumn.CheckReference(foreignKeyColumn);

                return new Reference(primaryKeyColumn.BuildColumn(), foreignKeyColumn.BuildColumn());
            }
        }

        internal bool DefinesRelationship(string name)
        {
            return _name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }
    }
}
