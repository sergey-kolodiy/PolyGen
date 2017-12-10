using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NoiseLab.PolyGen.Core.Domain
{
    internal sealed class EntityRelationship
    {
        internal EntityRelationship(string name, bool onDeleteCascade, bool onDeleteSetNull, Entity otherSideEntity, IEnumerable<EntityReference> references)
        {
            Name = name;
            OnDeleteCascade = onDeleteCascade;
            OnDeleteSetNull = onDeleteSetNull;
            OtherSideEntity = otherSideEntity;
            References = references.ToList();
        }

        internal string Name { get; }

        internal bool OnDeleteCascade { get; }

        internal bool OnDeleteSetNull { get; }

        internal Entity OtherSideEntity { get; }

        internal IReadOnlyCollection<EntityReference> References { get; }


        internal MemberDeclarationSyntax GenerateReferenceNavigationProperty()
        {
            return OtherSideEntity.GenerateReferenceNavigationProperty(Name);
        }

        internal MemberDeclarationSyntax GenerateCollectionNavigationProperty()
        {
            return OtherSideEntity.GenerateCollectionNavigationProperty(Name);
        }

        internal string GenerateForeignKeyPropertyNameEnumeration()
        {
            return string.Join(", ", References.Select(o => $"\"{o.OtherSideProperty.Name}\""));
        }

        internal string GetDeleteBehavior()
        {
            return OnDeleteCascade ? "DeleteBehavior.Cascade"
                : OnDeleteSetNull? "DeleteBehavior.SetNull"
                : "DeleteBehavior.Restrict";
        }
    }
}