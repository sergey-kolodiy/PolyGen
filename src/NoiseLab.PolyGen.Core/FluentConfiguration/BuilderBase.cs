using System.Text.RegularExpressions;

namespace NoiseLab.PolyGen.Core.FluentConfiguration
{
    public abstract class BuilderBase
    {
        protected static readonly Regex DefaultNamePattern = new Regex(@"^[a-zA-Z]{1}[a-zA-Z0-9_]*$");
    }
}
