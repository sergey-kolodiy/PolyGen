using NoiseLab.Common.Extensions;

namespace NoiseLab.PolyGen.Core.Builders.Relationships
{
    public abstract class RelationshipTableBuilderBase
    {
        protected readonly RelationshipBuilder RelationshipBuilder;
        internal readonly string TableSchema;
        internal readonly string TableName;

        protected internal RelationshipTableBuilderBase(RelationshipBuilder relationshipBuilder, string tableSchema, string tableName)
        {
            tableSchema.ThrowIfNullOrWhitespace(nameof(tableSchema));
            tableName.ThrowIfNullOrWhitespace(nameof(tableName));

            RelationshipBuilder = relationshipBuilder;
            TableSchema = tableSchema;
            TableName = tableName;
        }
    }
}