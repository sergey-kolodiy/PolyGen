using System.Collections.Generic;
using System.Linq;

namespace NoiseLab.PolyGen.Core.Domain
{
    internal sealed class Relationship
    {
        public Relationship(string name, bool onDeleteCascade, bool onDeleteSetNull, Entity primaryKeyEntity, Entity foreignKeyEntity, IReadOnlyCollection<Reference> references)
        {
            Name = name;
            OnDeleteCascade = onDeleteCascade;
            OnDeleteSetNull = onDeleteSetNull;
            PrimaryKeyEntity = primaryKeyEntity;
            ForeignKeyEntity = foreignKeyEntity;
            References = references;
        }

        internal string Name { get; }

        internal bool OnDeleteCascade { get; }

        internal bool OnDeleteSetNull { get; }

        internal Entity PrimaryKeyEntity { get; }

        internal Entity ForeignKeyEntity { get; }

        internal IReadOnlyCollection<Reference> References { get; }

        internal void Apply()
        {
            PrimaryKeyEntity.AddDependentRelationship(
                new EntityRelationship(
                    Name,
                    OnDeleteCascade,
                    OnDeleteSetNull,
                    ForeignKeyEntity,
                    References.Select(r => new EntityReference(r.PrimaryKeyProperty, r.ForeignKeyProperty))));

            ForeignKeyEntity.AddPrincipalRelationship(
                new EntityRelationship(
                    Name,
                    OnDeleteCascade,
                    OnDeleteSetNull,
                    PrimaryKeyEntity,
                    References.Select(r => new EntityReference(r.ForeignKeyProperty, r.PrimaryKeyProperty))));
        }
    }
}