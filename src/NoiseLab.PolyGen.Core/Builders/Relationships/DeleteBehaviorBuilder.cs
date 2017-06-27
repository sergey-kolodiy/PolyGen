using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.Relationships
{
    public class DeleteBehaviorBuilder : BuilderBase
    {
        public RelationshipBuilder Relationship(string name)
        {
            return _referenceFactory.Relationship(name);
        }

        public Schema Build()
        {
            return _referenceFactory.Build();
        }

        internal DeleteBehaviorBuilder(ReferenceBuilder referenceFactory)
        {
            _referenceFactory = referenceFactory;
        }

        private readonly ReferenceBuilder _referenceFactory;
    }
}