using System;
using System.Collections.Generic;
using System.Linq;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Factories
{
    public class RelationshipFactory : FactoryBase
    {
        internal RelationshipFactory(SchemaFactory databaseFactory, string name, string primaryKeyTableSchema, string primaryKeyTableName, string foreignKeyTableSchema, string foreignKeyTableName)
        {
            _name = name;
            _primaryKeyTableSchema = primaryKeyTableSchema;
            _primaryKeyTableName = primaryKeyTableName;
            _foreignKeyTableSchema = foreignKeyTableSchema;
            _foreignKeyTableName = foreignKeyTableName;
            _databaseFactory = databaseFactory;
        }

        private readonly string _name;
        private readonly string _primaryKeyTableSchema;
        private readonly string _primaryKeyTableName;
        private readonly string _foreignKeyTableSchema;
        private readonly string _foreignKeyTableName;
        private bool _onDeleteCascade;
        private bool _onDeleteSetNull;
        private readonly List<ReferenceFactory> _referenceFactories = new List<ReferenceFactory>();
        private readonly SchemaFactory _databaseFactory;

        public ReferenceFactory Reference(string primaryKeyColumnName, string foreignKeyColumnName)
        {
            foreignKeyColumnName.ThrowIfNullOrWhitespace(nameof(foreignKeyColumnName));
            primaryKeyColumnName.ThrowIfNullOrWhitespace(nameof(primaryKeyColumnName));

            var referenceFactory = new ReferenceFactory(this, primaryKeyColumnName, foreignKeyColumnName);
            _referenceFactories.Add(referenceFactory);
            return referenceFactory;
        }

        internal RelationshipFactory OnDeleteCascade()
        {
            if (_onDeleteSetNull)
            {
                throw new InvalidOperationException($"Cannot set up Cascade delete behavior for relationship \"{_name}\" because SetNull delete behavior has already been specified.");
            }
            _onDeleteCascade = true;
            return this;
        }

        internal RelationshipFactory OnDeleteSetNull()
        {
            if (_onDeleteCascade)
            {
                throw new InvalidOperationException($"Cannot set up SetNull delete behavior for relationship \"{_name}\" because Cascade delete behavior has already been specified.");
            }
            foreach (var referenceFactory in _referenceFactories)
            {
                var column = _databaseFactory.GetColumnFactory(_foreignKeyTableSchema, _foreignKeyTableName,
                    referenceFactory.ForeignKeyColumnName);
                if (!column.IsNullable())
                {
                    throw new InvalidOperationException($"Cannot set up SetNull delete behavior for relationship \"{_name}\" bacause foreign key column \"{referenceFactory.ForeignKeyColumnName}\" is not nullable.");
                }
            }

            _onDeleteSetNull = true;
            return this;
        }

        internal TableFactory Table(string schema, string name)
        {
            return _databaseFactory.Table(schema, name);
        }

        internal RelationshipFactory Relationship(string name, string primaryKeyTableSchema, string primaryKeyTableName, string foreignKeyTableSchema, string foreignKeyTableName)
        {
            return _databaseFactory.Relationship(name, primaryKeyTableSchema, primaryKeyTableName, foreignKeyTableSchema, foreignKeyTableName);
        }

        internal Schema Build()
        {
            return _databaseFactory.Build();
        }

        internal Relationship BuildRelationship()
        {
            var primaryKeyTableFactory = _databaseFactory.GetTableFactory(_primaryKeyTableSchema, _primaryKeyTableName);
            if (primaryKeyTableFactory == null)
            {
                throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Primary key table \"{_primaryKeyTableSchema}.{_primaryKeyTableName}\" is not defined.");
            }

            var foreignKeyTableFactory = _databaseFactory.GetTableFactory(_foreignKeyTableSchema, _foreignKeyTableName);
            if (foreignKeyTableFactory == null)
            {
                throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Foreign key table \"{_foreignKeyTableSchema}.{_foreignKeyTableName}\" is not defined.");
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

        private Reference BuildReference(ReferenceFactory referenceFactory, TableFactory primaryKeyTable, TableFactory foreignKeyTable)
        {
            var primaryKeyColumn = primaryKeyTable.GetColumnFactory(referenceFactory.PrimaryKeyColumnName);
            if (primaryKeyColumn == null)
            {
                throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Column \"{referenceFactory.PrimaryKeyColumnName}\" is not defined in primary key table \"{_primaryKeyTableSchema}.{_primaryKeyTableName}\".");
            }

            var foreignKeyColumn = foreignKeyTable.GetColumnFactory(referenceFactory.ForeignKeyColumnName);
            if (foreignKeyColumn == null)
            {
                throw new InvalidOperationException($"Invalid relationship definition: \"{_name}\". Column \"{referenceFactory.ForeignKeyColumnName}\" is not defined in foreign key table \"{_foreignKeyTableSchema}.{_foreignKeyTableName}\".");
            }

            primaryKeyColumn.CheckReference(foreignKeyColumn);

            return new Reference(primaryKeyColumn.BuildColumn(), foreignKeyColumn.BuildColumn());
        }
    }
}
