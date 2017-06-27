using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
{
    public class XmlColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new XmlColumnSpecificationBuilder MaxLength(int maxLength)
        {
            base.MaxLength(maxLength);
            return this;
        }

        public new XmlColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new XmlColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new XmlColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new XmlColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Xml;

        internal XmlColumnSpecificationBuilder(ColumnBuilder columnFactory)
            : base(columnFactory)
        {
        }
    }
}