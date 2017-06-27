using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Factories
{
    public class DeleteBehaviorFactory : FactoryBase
    {
        public RelationshipFactory Relationship(string name, string primaryKeyTableSchema, string primaryKeyTableName, string foreignKeyTableSchema, string foreignKeyTableName)
        {
            return _referenceFactory.Relationship(name, primaryKeyTableSchema, primaryKeyTableName, foreignKeyTableSchema, foreignKeyTableName);
        }

        public Schema Build()
        {
            return _referenceFactory.Build();
        }

        internal DeleteBehaviorFactory(ReferenceFactory referenceFactory)
        {
            _referenceFactory = referenceFactory;
        }

        private readonly ReferenceFactory _referenceFactory;
    }
}