using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NoiseLab.PolyGen.Core.Domain
{
    internal sealed class TableRelationship
    {
        internal TableRelationship(string name, bool onDeleteCascade, bool onDeleteSetNull, Table otherSideTable, IEnumerable<TableReference> references)
        {
            Name = name;
            OnDeleteCascade = onDeleteCascade;
            OnDeleteSetNull = onDeleteSetNull;
            OtherSideTable = otherSideTable;
            References = references.ToList();
        }

        internal string Name { get; }

        internal bool OnDeleteCascade { get; }

        internal bool OnDeleteSetNull { get; }

        internal Table OtherSideTable { get; }

        internal IReadOnlyCollection<TableReference> References { get; }


        internal MemberDeclarationSyntax GenerateReferenceNavigationProperty()
        {
            return OtherSideTable.GenerateReferenceNavigationProperty(Name);
        }

        internal MemberDeclarationSyntax GenerateCollectionNavigationProperty()
        {
            return OtherSideTable.GenerateCollectionNavigationProperty(Name);
        }

        internal string GenerateForeignKeyPropertyNameEnumeration()
        {
            return string.Join(", ", References.Select(o => $"\"{o.OtherSideColumn.Name}\""));
        }

        internal string GetDeleteBehavior()
        {
            return OnDeleteCascade ? "DeleteBehavior.Cascade"
                : OnDeleteSetNull? "DeleteBehavior.SetNull"
                : "DeleteBehavior.Restrict";
        }
    }
}