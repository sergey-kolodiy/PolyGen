using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Factories
{
    public class ReferenceFactory : FactoryBase
    {
        internal ReferenceFactory(RelationshipFactory relationshipFactory, string primaryKeyColumnName, string foreignKeyColumnName)
        {
            PrimaryKeyColumnName = primaryKeyColumnName;
            ForeignKeyColumnName = foreignKeyColumnName;
            _relationshipFactory = relationshipFactory;
        }

        internal readonly string PrimaryKeyColumnName;
        internal readonly string ForeignKeyColumnName;
        private readonly RelationshipFactory _relationshipFactory;

        public ReferenceFactory Reference(string primaryKeyColumnName, string foreignKeyColumnName)
        {
            return _relationshipFactory.Reference(primaryKeyColumnName, foreignKeyColumnName);
        }

        public RelationshipFactory Relationship(string name, string primaryKeyTableSchema, string primaryKeyTableName, string foreignKeyTableSchema, string foreignKeyTableName)
        {
            return _relationshipFactory.Relationship(name, primaryKeyTableSchema, primaryKeyTableName, foreignKeyTableSchema, foreignKeyTableName);
        }

        public DeleteBehaviorFactory OnDeleteCascade()
        {
            _relationshipFactory.OnDeleteCascade();
            return new DeleteBehaviorFactory(this);
        }

        public DeleteBehaviorFactory OnDeleteSetNull()
        {
            _relationshipFactory.OnDeleteSetNull();
            return new DeleteBehaviorFactory(this);
        }

        public TableFactory Table(string schema, string name)
        {
            return _relationshipFactory.Table(schema, name);
        }

        public Schema Build()
        {
            return _relationshipFactory.Build();
        }
    }
}