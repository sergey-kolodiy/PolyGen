using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class CharColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new CharColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new CharColumnSpecificationBuilder PrimaryKey()
        {
            base.PrimaryKey();
            return this;
        }

        public new CharColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new CharColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Char;

        internal CharColumnSpecificationBuilder(ColumnBuilder columnBuilder)
            : base(columnBuilder)
        {
            // TODO: Is it really required to explicitly set MaxLength for System.Char property?
            MaxLength(1);
        }
    }
}