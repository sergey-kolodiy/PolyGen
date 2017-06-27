using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.Relationships
{
    public class ReferenceBuilder : BuilderBase
    {
        internal ReferenceBuilder(RelationshipBuilder relationshipFactory, string primaryKeyColumnName, string foreignKeyColumnName)
        {
            PrimaryKeyColumnName = primaryKeyColumnName;
            ForeignKeyColumnName = foreignKeyColumnName;
            _relationshipFactory = relationshipFactory;
        }

        internal readonly string PrimaryKeyColumnName;
        internal readonly string ForeignKeyColumnName;
        private readonly RelationshipBuilder _relationshipFactory;

        public ReferenceBuilder Reference(string foreignKeyColumnName, string primaryKeyColumnName)
        {
            return _relationshipFactory.Reference(foreignKeyColumnName, primaryKeyColumnName);
        }

        public RelationshipBuilder Relationship(string name)
        {
            return _relationshipFactory.Relationship(name);
        }

        public DeleteBehaviorBuilder OnDeleteCascade()
        {
            _relationshipFactory.OnDeleteCascade();
            return new DeleteBehaviorBuilder(this);
        }

        public DeleteBehaviorBuilder OnDeleteSetNull()
        {
            _relationshipFactory.OnDeleteSetNull();
            return new DeleteBehaviorBuilder(this);
        }

        public Schema Build()
        {
            return _relationshipFactory.Build();
        }
    }
}