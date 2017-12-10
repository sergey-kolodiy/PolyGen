namespace NoiseLab.PolyGen.Core.FluentConfiguration.Relationships
{
    public class ToRelationshipEntityBuilder : RelationshipEntityBuilderBase
    {
        public ReferenceBuilder Reference(
            string foreignKeyPropertyName,
            string primaryKeyPropertyName)
        {
            return RelationshipBuilder.Reference(foreignKeyPropertyName, primaryKeyPropertyName);
        }

        internal ToRelationshipEntityBuilder(
            RelationshipBuilder relationshipBuilder,
            string entityNamespace,
            string entityName)
            : base(relationshipBuilder, entityNamespace, entityName)
        {
        }
    }
}