using System.Text.RegularExpressions;

namespace NoiseLab.PolyGen.Core.Factories
{
    public abstract class FactoryBase
    {
        // TODO: Most likely, name cannot start from number.
        protected static readonly Regex DefaultNamePattern = new Regex(@"^[a-zA-Z0-9_]*$");
    }
}
