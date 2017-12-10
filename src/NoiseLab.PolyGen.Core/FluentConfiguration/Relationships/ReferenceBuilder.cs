using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.Relationships
{
    public class ReferenceBuilder : BuilderBase
    {
        public ReferenceBuilder Reference(string foreignKeyPropertyName, string primaryKeyPropertyName)
        {
            return _relationshipBuilder.Reference(foreignKeyPropertyName, primaryKeyPropertyName);
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

        public Model Build()
        {
            return _relationshipBuilder.Build();
        }

        internal ReferenceBuilder(RelationshipBuilder relationshipBuilder, string primaryKeyPropertyName, string foreignKeyPropertyName)
        {
            PrimaryKeyPropertyName = primaryKeyPropertyName;
            ForeignKeyPropertyName = foreignKeyPropertyName;
            _relationshipBuilder = relationshipBuilder;
        }

        internal readonly string PrimaryKeyPropertyName;
        internal readonly string ForeignKeyPropertyName;
        private readonly RelationshipBuilder _relationshipBuilder;
    }
}