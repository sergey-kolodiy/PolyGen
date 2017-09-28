using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.ColumnSpecifications
{
    public class JsonColumnSpecificationBuilder : ColumnSpecificationBuilderBase
    {
        public new JsonColumnSpecificationBuilder MaxLength(int maxLength)
        {
            base.MaxLength(maxLength);
            return this;
        }

        public new JsonColumnSpecificationBuilder Nullable()
        {
            base.Nullable();
            return this;
        }

        public new JsonColumnSpecificationBuilder Identity()
        {
            base.Identity();
            return this;
        }

        public new JsonColumnSpecificationBuilder Computed()
        {
            base.Computed();
            return this;
        }

        protected internal override AbstractDataType DataType { get; } = AbstractDataType.Json;

        internal JsonColumnSpecificationBuilder(ColumnBuilderBase columnBuilder)
            : base(columnBuilder)
        {
        }
    }
}