using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

// ReSharper disable UnusedMember.Global (used by pipeline as DLL)
namespace CSVMapContentPipeline
{
    [ContentImporter(".csv", DefaultProcessor = "PassThroughProcessor", DisplayName = "CSV Map Importer - ZeldaTeam3")]
    public class CSVMapImporter : ContentImporter<int[][]>
    {
        public override int[][] Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing CSV map: {0}", filename);

            var fileStream = new StreamReader(filename);
            var rowStrings = fileStream.ReadToEnd().Trim().Split('\n');
            var result = new int[rowStrings.Length][];
            fileStream.Close();

            for (var row = 0; row < rowStrings.Length; row++)
            {
                var colStrings = rowStrings[row].Split(',');
                result[row] = new int[colStrings.Length];

                for (var col = 0; col < colStrings.Length; col++)
                {
                    result[row][col] = int.Parse(colStrings[col].Trim());
                }
            }

            return result;
        }
    }
}
