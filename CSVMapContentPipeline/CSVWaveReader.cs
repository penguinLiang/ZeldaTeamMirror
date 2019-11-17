using Microsoft.Xna.Framework.Content;

// ReSharper disable UnusedMember.Global (used by pipeline as DLL)
namespace CSVMapContentPipeline
{
    internal class CSVWaveReader : ContentTypeReader<string[][]>
    {
        protected override string[][] Read(ContentReader input, string[][] existingInstance)
        {
            var rows = input.ReadInt32();
            var cols = input.ReadInt32();
            var result = new string[rows][];

            for (var row = 0; row < rows; row++)
            {
                result[row] = new string[cols];
                for (var col = 0; col < cols; cols++)
                {
                    result[row][col] = input.ReadString();
                }
            }

            return result;
        }
    }
}
