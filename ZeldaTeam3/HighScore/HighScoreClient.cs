using System;
using System.IO;
using System.Net;
using System.Text;

namespace Zelda.HighScore
{
    public class HighScoreClient
    {
        private const string Url = "https://zelda-high-score.herokuapp.com/";
        private const int Timeout = 2500;

        private static PlayerScore[] ResponseScores(WebResponse response)
        {
            var text = "";
            if (response.ContentLength > 0)
            {
                var stream = response.GetResponseStream();
                var reader = new StreamReader(stream ?? throw new Exception("Invalid stream"));
                text = reader.ReadToEnd();
                reader.Dispose();
                stream.Dispose();
            }
            if (((HttpWebResponse)response).StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(text);
            }
            response.Close();

            var lines = text.Split('\n');
            var result = new PlayerScore[lines.Length - 1];
            for (var i = 0; i < lines.Length - 1; i++)
            {
                var line = lines[i].Split(',');
                result[i] = new PlayerScore { Initials = line[0], Score = int.Parse(line[1]) };
            }

            return result;
        }

        public static PlayerScore[] Scores()
        {
            var apiClient = WebRequest.CreateHttp(Url);
            apiClient.Timeout = Timeout;
            apiClient.Method = "GET";
            apiClient.KeepAlive = false;

            return ResponseScores(apiClient.GetResponse());
        }

        public static PlayerScore[] Submit(PlayerScore score)
        {
            var apiClient = WebRequest.CreateHttp(Url);
            apiClient.Timeout = Timeout;
            apiClient.Method = "POST";
            apiClient.KeepAlive = false;

            var body = score.Initials + "," + score.Score;
            var bodyBytes = Encoding.ASCII.GetBytes(body);

            apiClient.ContentLength = bodyBytes.Length;

            var outStream = apiClient.GetRequestStream();
            outStream.Write(bodyBytes, 0, bodyBytes.Length);
            outStream.Close();

            return ResponseScores(apiClient.GetResponse());
        }
    }
}
