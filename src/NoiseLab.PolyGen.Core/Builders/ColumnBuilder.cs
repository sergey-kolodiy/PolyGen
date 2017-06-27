using System;
using NoiseLab.PolyGen.Core.Builders.ColumnSpecifications;
using NoiseLab.PolyGen.Core.Builders.Relationships;
using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders
{
    public class ColumnBuilder : BuilderBase
    {
        public ByteColumnSpecificationBuilder Byte()
        {
            var factory = new ByteColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public Int16ColumnSpecificationBuilder Int16()
        {
            var factory = new Int16ColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public Int32ColumnSpecificationBuilder Int32()
        {
            var factory = new Int32ColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public Int64ColumnSpecificationBuilder Int64()
        {
            var factory = new Int64ColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public GuidColumnSpecificationBuilder Guid()
        {
            var factory = new GuidColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public BooleanColumnSpecificationBuilder Boolean()
        {
            var factory = new BooleanColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public DateColumnSpecificationBuilder Date()
        {
            var factory = new DateColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public DateTimeColumnSpecificationBuilder DateTime()
        {
            var factory = new DateTimeColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public DateTimeOffsetColumnSpecificationBuilder DateTimeOffset()
        {
            var factory = new DateTimeOffsetColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public TimeOfDayColumnSpecificationBuilder TimeOfDay()
        {
            var factory = new TimeOfDayColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public TimeSpanColumnSpecificationBuilder TimeSpan()
        {
            var factory = new TimeSpanColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public CharColumnSpecificationBuilder Char()
        {
            var factory = new CharColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public SingleColumnSpecificationBuilder Single()
        {
            var factory = new SingleColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public DoubleColumnSpecificationBuilder Double()
        {
            var factory = new DoubleColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public DecimalColumnSpecificationBuilder Decimal()
        {
            var factory = new DecimalColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public StringColumnSpecificationBuilder String()
        {
            var factory = new StringColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public TextColumnSpecificationBuilder Text()
        {
            var factory = new TextColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public JsonColumnSpecificationBuilder Json()
        {
            var factory = new JsonColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public XmlColumnSpecificationBuilder Xml()
        {
            var factory = new XmlColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public BinaryColumnSpecificationBuilder Binary()
        {
            var factory = new BinaryColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public ImageColumnSpecificationBuilder Image()
        {
            var factory = new ImageColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        public RowVersionColumnSpecificationBuilder RowVersion()
        {
            var factory = new RowVersionColumnSpecificationBuilder(this);
            _columnSpecificationFactory = factory;
            return factory;
        }

        internal ColumnBuilder Column(string name)
        {
            return _tableFactory.Column(name);
        }

        internal TableBuilder Table(string schema, string name)
        {
            return _tableFactory.Table(schema, name);
        }

        internal RelationshipBuilder Relationship(string name)
        {
            return _tableFactory.Relationship(name);
        }

        internal Schema Build()
        {
            return _tableFactory.Build();
        }

        internal Column BuildColumn()
        {
            if (_column == null)
            {
                // TODO: Validation.
                _column = new Column(
                    Ordinal,
                    Name,
                    _columnSpecificationFactory.DataType,
                    _columnSpecificationFactory.MaxLengthInternal,
                    _columnSpecificationFactory.NullableInternal,
                    _columnSpecificationFactory.PrimaryKeyInternal,
                    _columnSpecificationFactory.IdentityInternal,
                    _columnSpecificationFactory.ComputedInternal);
            }
            return _column;
        }

        internal bool IsNamed(string name)
        {
            return Name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }

        internal bool IsComputed()
        {
            return _columnSpecificationFactory.ComputedInternal;
        }

        internal bool IsRowVersion()
        {
            return _columnSpecificationFactory.DataType == AbstractDataType.RowVersion;
        }

        internal bool IsNullable()
        {
            return _columnSpecificationFactory.NullableInternal;
        }

        internal void CheckReference(ColumnBuilder foreignKeyColumn)
        {
            _columnSpecificationFactory.CheckReference(foreignKeyColumn._columnSpecificationFactory);
        }

        internal ColumnBuilder(TableBuilder tableFactory, int ordinal, string name)
        {
            _tableFactory = tableFactory;
            Ordinal = ordinal;
            Name = name;
        }

        internal int Ordinal { get; }
        internal string Name { get; }
        private readonly TableBuilder _tableFactory;
        private ColumnSpecificationBuilderBase _columnSpecificationFactory;
        private Column _column;
    }
}
