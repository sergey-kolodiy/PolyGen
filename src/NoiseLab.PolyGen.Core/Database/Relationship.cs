using System.Collections.Generic;
using System.Linq;

namespace NoiseLab.PolyGen.Core.Database
{
    internal sealed class Relationship
    {
        public Relationship(string name, bool onDeleteCascade, bool onDeleteSetNull, Table primaryKeyTable, Table foreignKeyTable, IReadOnlyCollection<Reference> references)
        {
            Name = name;
            OnDeleteCascade = onDeleteCascade;
            OnDeleteSetNull = onDeleteSetNull;
            PrimaryKeyTable = primaryKeyTable;
            ForeignKeyTable = foreignKeyTable;
            References = references;
        }

        internal string Name { get; }

        internal bool OnDeleteCascade { get; }

        internal bool OnDeleteSetNull { get; }

        internal Table PrimaryKeyTable { get; }

        internal Table ForeignKeyTable { get; }

        internal IReadOnlyCollection<Reference> References { get; }

        internal void Apply()
        {
            PrimaryKeyTable.AddDependentRelationship(
                new TableRelationship(
                    Name,
                    OnDeleteCascade,
                    OnDeleteSetNull,
                    ForeignKeyTable,
                    References.Select(r => new TableReference(r.PrimaryKeyColumn, r.ForeignKeyColumn))));

            ForeignKeyTable.AddPrincipalRelationship(
                new TableRelationship(
                    Name,
                    OnDeleteCascade,
                    OnDeleteSetNull,
                    PrimaryKeyTable,
                    References.Select(r => new TableReference(r.ForeignKeyColumn, r.PrimaryKeyColumn))));
        }
    }
}