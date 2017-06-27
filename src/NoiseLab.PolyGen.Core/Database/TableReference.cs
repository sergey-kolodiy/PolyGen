namespace NoiseLab.PolyGen.Core.Database
{
    internal sealed class TableReference
    {
        internal TableReference(Column thisSideColumn, Column otherSideColumn)
        {
            ThisSideColumn = thisSideColumn;
            OtherSideColumn = otherSideColumn;
        }

        internal Column ThisSideColumn { get; }

        internal Column OtherSideColumn { get; }
    }
}