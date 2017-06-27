namespace NoiseLab.PolyGen.Core.Builders.Relationships
{
    public class FromRelationshipTableBuilder: RelationshipTableBuilderBase
    {
        public ToRelationshipTableBuilder To(string tableSchema, string tableName)
        {
            return RelationshipBuilder.To(tableSchema, tableName);
        }

        internal FromRelationshipTableBuilder(
            RelationshipBuilder relationshipBuilder,
            string tableSchema,
            string tableName)
            : base(relationshipBuilder, tableSchema, tableName)
        {
        }
    }
}