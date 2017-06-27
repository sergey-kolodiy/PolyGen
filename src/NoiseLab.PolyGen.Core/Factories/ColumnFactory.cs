using System;
using NoiseLab.Common.Extensions;
using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Factories
{
    public class ColumnFactory : FactoryBase
    {
        public ColumnFactory MaxLength(int maxLength)
        {
            if (!_dataType.SupportsMaxLengthRestriction)
            {
                throw new InvalidOperationException(
                    $"Column \"{_name}\" with data type \"{_dataType.Name}\" does not support maximum length restriction.");
            }
            maxLength.ThrowIfLessThan(1, nameof(maxLength));

            _maxLength = maxLength;
            return this;
        }

        public ColumnFactory Nullable()
        {
            if (_primaryKey)
            {
                throw new InvalidOperationException($"Column \"{_name}\" cannot be nullable because it is a part of the primary key.");
            }
            if (_identity)
            {
                throw new InvalidOperationException($"Column \"{_name}\" cannot be nullable because it is an identity column.");
            }

            _nullable = true;
            return this;
        }

        public ColumnFactory PrimaryKey()
        {
            if (_nullable)
            {
                throw new InvalidOperationException($"Nullable column \"{_name}\" cannot be set as a part of the primary key.");
            }

            _primaryKey = true;
            return this;
        }

        public ColumnFactory Identity()
        {
            if (!_dataType.SupportsIdentitySpecification)
            {
                throw new InvalidOperationException(
                    $"Column \"{_name}\" with data type \"{_dataType.Name}\" does not support identity specification.");
            }
            if (_nullable)
            {
                throw new InvalidOperationException($"Nullable column \"{_name}\" cannot be set as an identity column.");
            }
            if (_computed)
            {
                throw new InvalidOperationException($"Computed column \"{_name}\" cannot be set as an identity column.");
            }
            if (_dataType == AbstractDataType.RowVersion)
            {
                throw new InvalidOperationException($"Row version column \"{_name}\" cannot be set as an identity column.");
            }

            _identity = true;
            return this;
        }

        public ColumnFactory Computed()
        {
            if (_identity)
            {
                throw new InvalidOperationException($"Identity column \"{_name}\" cannot be set as a computed column.");
            }

            _computed = true;
            return this;
        }

        public ColumnFactory Column(string name, AbstractDataType abstractDataType)
        {
            return _tableFactory.Column(name, abstractDataType);
        }

        public TableFactory Table(string schema, string name)
        {
            return _tableFactory.Table(schema, name);
        }

        public RelationshipFactory Relationship(string name, string primaryKeyTableSchema, string primaryKeyTableName, string foreignKeyTableSchema, string foreignKeyTableName)
        {
            return _tableFactory.Relationship(name, primaryKeyTableSchema, primaryKeyTableName, foreignKeyTableSchema, foreignKeyTableName);
        }

        public Schema Build()
        {
            return _tableFactory.Build();
        }

        internal ColumnFactory(TableFactory tableFactory, int ordinal, string name, AbstractDataType dataType)
        {
            _tableFactory = tableFactory;
            _ordinal = ordinal;
            _name = name;
            _dataType = dataType;
        }

        private readonly TableFactory _tableFactory;
        private readonly int _ordinal;
        private readonly string _name;
        private readonly AbstractDataType _dataType;
        private int? _maxLength;
        private bool _nullable;
        private bool _primaryKey;
        private bool _identity;
        private bool _computed;
        private Column _column;

        internal Column BuildColumn()
        {
            if (_column == null)
            {
                // TODO: Validation.
                _column = new Column(_ordinal, _name, _dataType, _maxLength, _nullable, _primaryKey, _identity, _computed);
            }
            return _column;
        }

        internal bool IsNamed(string name)
        {
            return _name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }

        internal bool IsComputed()
        {
            return _computed;
        }

        internal bool IsRowVersion()
        {
            return _dataType == AbstractDataType.RowVersion;
        }

        internal bool IsNullable()
        {
            return _nullable;
        }

        internal void CheckReference(ColumnFactory foreignKeyColumn)
        {
            if (_dataType != foreignKeyColumn._dataType)
            {
                throw new InvalidOperationException($"Primary key column \"{_name}\" cannot reference foreign key column \"{foreignKeyColumn._name}\". Data type mismatch: {_dataType.Name} and {foreignKeyColumn._dataType.Name}.");
            }
            // TODO: Add other validations here.
        }
    }
}
