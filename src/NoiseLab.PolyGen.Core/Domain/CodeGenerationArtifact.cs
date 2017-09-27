using Microsoft.CodeAnalysis.Emit;

namespace NoiseLab.PolyGen.Core.Domain
{
    public class CodeGenerationArtifact
    {
        internal CodeGenerationArtifact(EmitResult emitResult, byte[] peBytes, byte[] pdbBytes, byte[] xmlBytes)
        {
            EmitResult = emitResult;
            PeBytes = peBytes;
            PdbBytes = pdbBytes;
            XmlBytes = xmlBytes;
        }

        public EmitResult EmitResult { get; }
        public byte[] PeBytes { get; }
        public byte[] PdbBytes { get; }
        public byte[] XmlBytes { get; }
    }
}