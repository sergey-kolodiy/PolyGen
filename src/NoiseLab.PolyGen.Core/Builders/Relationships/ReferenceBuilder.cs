using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.Relationships
{
    public class ReferenceBuilder : BuilderBase
    {
        public ReferenceBuilder Reference(string foreignKeyColumnName, string primaryKeyColumnName)
        {
            return _relationshipBuilder.Reference(foreignKeyColumnName, primaryKeyColumnName);
        }

        public RelationshipBuilder Relationship(string name)
        {
            return _relationshipBuilder.Relationship(name);
        }

        public DeleteBehaviorBuilder OnDeleteCascade()
        {
            _relationshipBuilder.OnDeleteCascade();
            return new DeleteBehaviorBuilder(this);
        }

        public DeleteBehaviorBuilder OnDeleteSetNull()
        {
            _relationshipBuilder.OnDeleteSetNull();
            return new DeleteBehaviorBuilder(this);
        }

        public Schema Build()
        {
            return _relationshipBuilder.Build();
        }

        internal ReferenceBuilder(RelationshipBuilder relationshipBuilder, string primaryKeyColumnName, string foreignKeyColumnName)
        {
            PrimaryKeyColumnName = primaryKeyColumnName;
            ForeignKeyColumnName = foreignKeyColumnName;
            _relationshipBuilder = relationshipBuilder;
        }

        internal readonly string PrimaryKeyColumnName;
        internal readonly string ForeignKeyColumnName;
        private readonly RelationshipBuilder _relationshipBuilder;
    }
}