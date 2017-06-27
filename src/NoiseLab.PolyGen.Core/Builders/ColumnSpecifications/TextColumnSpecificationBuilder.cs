using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.ColumnSpecifications
{
    public class TextColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new TextColumnSpecificationBuilder MaxLength(int maxLength)
        {
            base.MaxLength(maxLength);
            return this;
        }

        public new TextColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new TextColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new TextColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new TextColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Text;

        internal TextColumnSpecificationBuilder(ColumnBuilder columnFactory)
            : base(columnFactory)
        {
        }
    }
}