using System.IO;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace CSVMapContentPipeline
{
    [ContentImporter(".csv", DefaultProcessor = "PassThroughProcessor", DisplayName = "CSV Map Importer - ZeldaTeam3")]
    public class CSVMapImporter : ContentImporter<int[,]>
    {
        public const int Columns = 16;
        public const int Rows = 11;

        public override int[,] Import(string filename, ContentImporterContext context)
        {
            var result = new int[Rows, Columns];
            context.Logger.LogMessage("Importing CSV map: {0}", filename);

            var fileStream = new StreamReader(filename);
            for (var row = 0; row < Rows; row++)
            {
                var colStrings = fileStream.ReadLine()?.Split(',');
                if (colStrings == null)
                {
                    throw new InvalidContentException($"CSV map file {filename} does not contain ${Rows} rows");
                }

                if (colStrings.Length != Columns)
                {
                    throw new InvalidContentException($"CSV map file {filename}, row ${row} does not contain ${Columns} columns");
                }

                for (var col = 0; col < result.GetLength(1); col++)
                {
                    result[row, col] = int.Parse(colStrings[col]);
                }
            }
            fileStream.Close();

            return result;
        }
    }
}
