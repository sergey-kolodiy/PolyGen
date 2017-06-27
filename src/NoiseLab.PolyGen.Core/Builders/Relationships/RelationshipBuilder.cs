using System;
using System.Collections.Generic;
using System.Linq;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.Relationships
{
    public class RelationshipBuilder : BuilderBase
    {
        internal RelationshipBuilder(SchemaBuilder databaseFactory, string name)
        {
            _name = name;
            _databaseFactory = databaseFactory;
        }

        private readonly string _name;
        private FromRelationshipTableBuilder _fromRelationshipTableBuilder;
        private ToRelationshipTableBuilder _toRelationshipTableBuilder;
        private bool _onDeleteCascade;
        private bool _onDeleteSetNull;
        private readonly List<ReferenceBuilder> _referenceFactories = new List<ReferenceBuilder>();
        private readonly SchemaBuilder _databaseFactory;

        public FromRelationshipTableBuilder From(string tableSchema, string tableName)
        {
            return _fromRelationshipTableBuilder = new FromRelationshipTableBuilder(this, tableSchema, tableName);
        }

        internal ToRelationshipTableBuilder To(string tableSchema, string tableName)
        {
            return _toRelationshipTableBuilder = new ToRelationshipTableBuilder(this, tableSchema, tableName);
        }

        internal ReferenceBuilder Reference(string foreignKeyColumnName, string primaryKeyColumnName)
        {
            foreignKeyColumnName.ThrowIfNullOrWhitespace(nameof(foreignKeyColumnName));
            primaryKeyColumnName.ThrowIfNullOrWhitespace(nameof(primaryKeyColumnName));

            var referenceFactory = new ReferenceBuilder(this, primaryKeyColumnName, foreignKeyColumnName);
            _referenceFactories.Add(referenceFactory);
            return referenceFactory;
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
            foreach (var referenceFactory in _referenceFactories)
            {
                var column = _databaseFactory.GetColumnFactory(_fromRelationshipTableBuilder.TableSchema, _fromRelationshipTableBuilder.TableName,
                    referenceFactory.ForeignKeyColumnName);
                if (!column.IsNullable())
                {
                    throw new InvalidOperationException($"Cannot set up SetNull delete behavior for relationship \"{_name}\" bacause foreign key column \"{referenceFactory.ForeignKeyColumnName}\" is not nullable.");
                }
            }

            _onDeleteSetNull = true;
            return this;
        }

        internal RelationshipBuilder Relationship(string name)
        {
            return _databaseFactory.Relationship(name);
        }

        internal Schema Build()
        {
            return _databaseFactory.Build();
        }

        internal Relationship BuildRelationship()
        {
            var primaryKeyTableFactory = _databaseFactory.GetTableFactory(_toRelationshipTableBuilder.TableSchema, _toRelationshipTableBuilder.TableName);
            if (primaryKeyTableFactory == null)
            {
                throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Primary key table \"{_toRelationshipTableBuilder.TableSchema}.{_toRelationshipTableBuilder.TableName}\" is not defined.");
            }

            var foreignKeyTableFactory = _databaseFactory.GetTableFactory(_fromRelationshipTableBuilder.TableSchema, _fromRelationshipTableBuilder.TableName);
            if (foreignKeyTableFactory == null)
            {
                throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Foreign key table \"{_fromRelationshipTableBuilder.TableSchema}.{_fromRelationshipTableBuilder.TableName}\" is not defined.");
            }

            var references = _referenceFactories.Select(r => BuildReference(r, primaryKeyTableFactory, foreignKeyTableFactory)).ToList();

            var primaryKeyTable = primaryKeyTableFactory.BuildTable();
            var foreignKeyTable = foreignKeyTableFactory.BuildTable();

            return new Relationship(_name, _onDeleteCascade, _onDeleteSetNull, primaryKeyTable, foreignKeyTable, references);
        }

        internal bool DefinesRelationship(string name)
        {
            return _name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }

        private Reference BuildReference(ReferenceBuilder referenceFactory, TableBuilder primaryKeyTable, TableBuilder foreignKeyTable)
        {
            var primaryKeyColumn = primaryKeyTable.GetColumnFactory(referenceFactory.PrimaryKeyColumnName);
            if (primaryKeyColumn == null)
            {
                throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Column \"{referenceFactory.PrimaryKeyColumnName}\" is not defined in primary key table \"{_toRelationshipTableBuilder.TableSchema}.{_toRelationshipTableBuilder.TableName}\".");
            }

            var foreignKeyColumn = foreignKeyTable.GetColumnFactory(referenceFactory.ForeignKeyColumnName);
            if (foreignKeyColumn == null)
            {
                throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Column \"{referenceFactory.ForeignKeyColumnName}\" is not defined in foreign key table \"{_fromRelationshipTableBuilder.TableSchema}.{_fromRelationshipTableBuilder.TableName}\".");
            }

            primaryKeyColumn.CheckReference(foreignKeyColumn);

            return new Reference(primaryKeyColumn.BuildColumn(), foreignKeyColumn.BuildColumn());
        }
    }
}
