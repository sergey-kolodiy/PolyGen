namespace NoiseLab.PolyGen.Core.Domain
{
    internal sealed class EntityReference
    {
        internal EntityReference(Property thisSideProperty, Property otherSideProperty)
        {
            ThisSideProperty = thisSideProperty;
            OtherSideProperty = otherSideProperty;
        }

        internal Property ThisSideProperty { get; }

        internal Property OtherSideProperty { get; }
    }
}