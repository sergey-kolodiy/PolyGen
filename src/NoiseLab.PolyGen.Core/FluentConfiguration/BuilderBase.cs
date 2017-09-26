using System.Text.RegularExpressions;

namespace NoiseLab.PolyGen.Core.FluentConfiguration
{
    public abstract class BuilderBase
    {
        // TODO: Most likely, name cannot start from number.
        protected static readonly Regex DefaultNamePattern = new Regex(@"^[a-zA-Z0-9_]*$");
    }
}
