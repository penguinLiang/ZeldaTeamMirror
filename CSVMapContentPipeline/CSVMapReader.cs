using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace CSVMapContentPipeline
{
    class CSVMapReader : ContentTypeReader<int[,]>
    {
        protected override int[,] Read(ContentReader input, int[,] existingInstance)
        {
            var rows = input.ReadInt32();
            var cols = input.ReadInt32();
            var result = new int[rows, cols];

            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; cols++)
                {
                    result[row, col] = input.ReadInt32();
                }
            }

            return result;
        }
    }
}
