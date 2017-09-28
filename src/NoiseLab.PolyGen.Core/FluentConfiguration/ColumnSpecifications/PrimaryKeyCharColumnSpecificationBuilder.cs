using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class PrimaryKeyCharColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new PrimaryKeyCharColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new PrimaryKeyCharColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Char;

        internal PrimaryKeyCharColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
            // TODO: Is it really required to explicitly set MaxLength for System.Char property?
            MaxLength(1);
        }
    }
}