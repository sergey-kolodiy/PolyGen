namespace NoiseLab.PolyGen.Core.Builders.Relationships
{
    public class ToRelationshipTableBuilder : RelationshipTableBuilderBase
    {
        public ReferenceBuilder Reference(
            string foreignKeyColumnName,
            string primaryKeyColumnName)
        {
            return RelationshipBuilder.Reference(foreignKeyColumnName, primaryKeyColumnName);
        }

        internal ToRelationshipTableBuilder(
            RelationshipBuilder relationshipBuilder,
            string tableSchema,
            string tableName)
            : base(relationshipBuilder, tableSchema, tableName)
        {
        }
    }
}