using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace CSVMapContentPipeline
{
    internal class CSVMapWriter : ContentTypeWriter<int[][]>
    {
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(int[][]).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "CSVMapContentPipeline.CSVMapReader, CSVMapContentPipeline";
        }

        protected override void Write(ContentWriter output, int[][] value)
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
