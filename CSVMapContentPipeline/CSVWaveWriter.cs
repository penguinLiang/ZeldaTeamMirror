using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

// ReSharper disable UnusedMember.Global (used by pipeline as DLL)
namespace CSVMapContentPipeline
{
    internal class CSVWaveWriter : ContentTypeWriter<string[][]>
    {
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(string[][]).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "CSVMapContentPipeline.CSVWaveReader, CSVMapContentPipeline";
        }

        protected override void Write(ContentWriter output, string[][] value)
        {
            var rows = value.Length;
            var cols = value[0].Length;
            output.Write(rows);
            output.Write(cols);

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; cols++)
                {
                    output.Write(value[row][col]);
                }
            }
        }
    }
}
