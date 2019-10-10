using System;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace CSVMapContentPipeline
{
    class CSVMapWriter : ContentTypeWriter<int[,]>
    {
        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return typeof(int[,]).AssemblyQualifiedName;
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "CSVMapContentPipeline.CSVMapReader, CSVMapContentPipeline";
        }

        protected override void Write(ContentWriter output, int[,] value)
        {
            var rows = value.GetLength(0);
            var cols = value.GetLength(1);
            output.Write(rows);
            output.Write(cols);

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; cols++)
                {
                    output.Write(value[row, col]);
                }
            }
        }
    }
}
