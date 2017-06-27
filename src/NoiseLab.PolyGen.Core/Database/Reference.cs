namespace NoiseLab.PolyGen.Core.Database
{
    internal sealed class Reference
    {
        internal Reference(Column primaryKeyColumn, Column foreignKeyColumn)
        {
            PrimaryKeyColumn = primaryKeyColumn;
            ForeignKeyColumn = foreignKeyColumn;
        }

        internal Column PrimaryKeyColumn { get; }

        internal Column ForeignKeyColumn { get; }
    }
}