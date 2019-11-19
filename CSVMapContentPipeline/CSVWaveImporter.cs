using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

// ReSharper disable UnusedMember.Global (used by pipeline as DLL)
namespace CSVMapContentPipeline
{
    [ContentImporter(".csv", DefaultProcessor = "PassThroughProcessor", DisplayName = "CSV Wave Importer for Survival Mode - ZeldaTeam3")]
    public class CSVWaveImporter : ContentImporter<string[][]>
    {
        public override string[][] Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing CSV wave: {0}", filename);

            var fileStream = new StreamReader(filename);
            var rowStrings = fileStream.ReadToEnd().Trim().Split('\n');
            var result = new string[rowStrings.Length][];
            fileStream.Close();

            for (var row = 0; row < rowStrings.Length; row++)
            {
                var colStrings = rowStrings[row].Split(',');
                result[row] = new string[colStrings.Length];

                for (var col = 0; col < colStrings.Length; col++)
                {
                    result[row][col] = colStrings[col].Trim();
                }
            }

            return result;
        }
    }
}
