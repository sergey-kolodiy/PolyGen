using NoiseLab.Common.Extensions;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.Relationships
{
    public abstract class RelationshipEntityBuilderBase
    {
        protected readonly RelationshipBuilder RelationshipBuilder;
        internal readonly string EntityNamespace;
        internal readonly string EntityName;

        protected internal RelationshipEntityBuilderBase(RelationshipBuilder relationshipBuilder, string entityNamespace, string entityName)
        {
            entityNamespace.ThrowIfNullOrWhitespace(nameof(entityNamespace));
            entityName.ThrowIfNullOrWhitespace(nameof(entityName));

            RelationshipBuilder = relationshipBuilder;
            EntityNamespace = entityNamespace;
            EntityName = entityName;
        }
    }
}