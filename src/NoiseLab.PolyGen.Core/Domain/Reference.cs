namespace NoiseLab.PolyGen.Core.Domain
{
    internal sealed class Reference
    {
        internal Reference(Property primaryKeyProperty, Property foreignKeyProperty)
        {
            PrimaryKeyProperty = primaryKeyProperty;
            ForeignKeyProperty = foreignKeyProperty;
        }

        internal Property PrimaryKeyProperty { get; }

        internal Property ForeignKeyProperty { get; }
    }
}