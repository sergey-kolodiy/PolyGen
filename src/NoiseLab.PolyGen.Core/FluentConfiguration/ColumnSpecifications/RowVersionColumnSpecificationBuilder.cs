using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class RowVersionColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new RowVersionColumnSpecificationBuilder MaxLength(int maxLength)
        {
            base.MaxLength(maxLength);
            return this;
        }

        public new RowVersionColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new RowVersionColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new RowVersionColumnSpecificationBuilder Computed()
        {
            // TODO: Does it make sense to set RowVersion column as Computed?
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.RowVersion;

        internal RowVersionColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}
