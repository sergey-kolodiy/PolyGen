using NoiseLab.PolyGen.Core.Domain;

namespace NoiseLab.PolyGen.Core.FluentConfiguration.Relationships
{
    public class DeleteBehaviorBuilder : BuilderBase
    {
        public RelationshipBuilder Relationship(string name)
        {
            return _referenceBuilder.Relationship(name);
        }

        public Model Build()
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