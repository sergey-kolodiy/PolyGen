using NoiseLab.PolyGen.Core.Database;

namespace NoiseLab.PolyGen.Core.Builders.Relationships
{
    public class DeleteBehaviorBuilder : BuilderBase
    {
        public RelationshipBuilder Relationship(string name)
        {
            return _referenceBuilder.Relationship(name);
        }

        public Schema Build()
        {
            return _referenceBuilder.Build();
        }

        internal DeleteBehaviorBuilder(ReferenceBuilder referenceBuilder)
        {
            _referenceBuilder = referenceBuilder;
        }

        private readonly ReferenceBuilder _referenceBuilder;
    }
}