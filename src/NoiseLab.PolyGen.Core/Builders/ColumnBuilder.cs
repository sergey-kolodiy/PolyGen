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
            var builder = new ByteColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public Int16ColumnSpecificationBuilder Int16()
        {
            var builder = new Int16ColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public Int32ColumnSpecificationBuilder Int32()
        {
            var builder = new Int32ColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public Int64ColumnSpecificationBuilder Int64()
        {
            var builder = new Int64ColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public GuidColumnSpecificationBuilder Guid()
        {
            var builder = new GuidColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public BooleanColumnSpecificationBuilder Boolean()
        {
            var boolean = new BooleanColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = boolean;
            return boolean;
        }

        public DateColumnSpecificationBuilder Date()
        {
            var builder = new DateColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public DateTimeColumnSpecificationBuilder DateTime()
        {
            var builder = new DateTimeColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public DateTimeOffsetColumnSpecificationBuilder DateTimeOffset()
        {
            var builder = new DateTimeOffsetColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public TimeOfDayColumnSpecificationBuilder TimeOfDay()
        {
            var builder = new TimeOfDayColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public TimeSpanColumnSpecificationBuilder TimeSpan()
        {
            var builder = new TimeSpanColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public CharColumnSpecificationBuilder Char()
        {
            var builder = new CharColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public SingleColumnSpecificationBuilder Single()
        {
            var builder = new SingleColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public DoubleColumnSpecificationBuilder Double()
        {
            var builder = new DoubleColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public DecimalColumnSpecificationBuilder Decimal()
        {
            var builder = new DecimalColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public StringColumnSpecificationBuilder String()
        {
            var builder = new StringColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public TextColumnSpecificationBuilder Text()
        {
            var builder = new TextColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public JsonColumnSpecificationBuilder Json()
        {
            var builder = new JsonColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public XmlColumnSpecificationBuilder Xml()
        {
            var builder = new XmlColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public BinaryColumnSpecificationBuilder Binary()
        {
            var builder = new BinaryColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public ImageColumnSpecificationBuilder Image()
        {
            var builder = new ImageColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        public RowVersionColumnSpecificationBuilder RowVersion()
        {
            var builder = new RowVersionColumnSpecificationBuilder(this);
            _columnSpecificationBuilder = builder;
            return builder;
        }

        internal ColumnBuilder Column(string name)
        {
            return _tableBuilder.Column(name);
        }

        internal TableBuilder Table(string schema, string name)
        {
            return _tableBuilder.Table(schema, name);
        }

        internal RelationshipBuilder Relationship(string name)
        {
            return _tableBuilder.Relationship(name);
        }

        internal Schema Build()
        {
            return _tableBuilder.Build();
        }

        internal Column BuildColumn()
        {
            if (_column == null)
            {
                // TODO: Validation.
                _column = new Column(
                    Ordinal,
                    Name,
                    _columnSpecificationBuilder.DataType,
                    _columnSpecificationBuilder.MaxLengthInternal,
                    _columnSpecificationBuilder.NullableInternal,
                    _columnSpecificationBuilder.PrimaryKeyInternal,
                    _columnSpecificationBuilder.IdentityInternal,
                    _columnSpecificationBuilder.ComputedInternal);
            }
            return _column;
        }

        internal bool IsNamed(string name)
        {
            return Name.Equals(name, StringComparison.OrdinalIgnoreCase);
        }

        internal bool IsComputed()
        {
            return _columnSpecificationBuilder.ComputedInternal;
        }

        internal bool IsRowVersion()
        {
            return _columnSpecificationBuilder.DataType == AbstractDataType.RowVersion;
        }

        internal bool IsNullable()
        {
            return _columnSpecificationBuilder.NullableInternal;
        }

        internal void CheckReference(ColumnBuilder foreignKeyColumn)
        {
            _columnSpecificationBuilder.CheckReference(foreignKeyColumn._columnSpecificationBuilder);
        }

        internal ColumnBuilder(TableBuilder tableBuilder, int ordinal, string name)
        {
            _tableBuilder = tableBuilder;
            Ordinal = ordinal;
            Name = name;
        }

        internal int Ordinal { get; }
        internal string Name { get; }
        private readonly TableBuilder _tableBuilder;
        private ColumnSpecificationBuilderBase _columnSpecificationBuilder;
        private Column _column;
    }
}
