namespace NoiseLab.PolyGen.Core.FluentConfiguration.Relationships
{
    public class FromRelationshipEntityBuilder: RelationshipEntityBuilderBase
    {
        public ToRelationshipEntityBuilder To(string entityNamespace, string entityName)
        {
            return RelationshipBuilder.To(entityNamespace, entityName);
        }

        internal FromRelationshipEntityBuilder(
            RelationshipBuilder relationshipBuilder,
            string entityNamespace,
            string entityName)
            : base(relationshipBuilder, entityNamespace, entityName)
        {
        }
    }
}