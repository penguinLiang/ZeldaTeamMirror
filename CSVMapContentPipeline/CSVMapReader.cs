using Microsoft.Xna.Framework.Content;

namespace CSVMapContentPipeline
{
    internal class CSVMapReader : ContentTypeReader<int[][]>
    {
        protected override int[][] Read(ContentReader input, int[][] existingInstance)
        {
            var rows = input.ReadInt32();
            var cols = input.ReadInt32();
            var result = new int[rows][];

            for (var row = 0; row < rows; row++)
            {
                result[row] = new int[cols];
                for (var col = 0; col < cols; cols++)
                {
                    result[row][col] = input.ReadInt32();
                }
            }

            return result;
        }
    }
}
