using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using GoSportBackEnd.Services.Gateways.Interfaces;
using GoSportBackEnd.Services.Models.Tennis;

namespace GoSportBackEnd.Services.Gateways
{
    // Replicating DB operation
    public class TennisGameGateway : ITennisGameGateway
    {
        // Make it testable
        protected string _resourceName;

        public TennisGameGateway()
        {
            _resourceName = $"{typeof(TennisGameGateway).Namespace}.DbMock.TennisMatches.json";
        }

        public async Task<MatchDetails> GetAsync(string id)
        {
            List<MatchDetails> matchDetailss;

            using (Stream stream = GetFileStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var matchesJson = await reader.ReadToEndAsync();
                matchDetailss = JsonSerializer.Deserialize<List<MatchDetails>>(matchesJson);
            }

            return matchDetailss?.SingleOrDefault(m => m.Id == id);
        }

        public async Task<MatchDetails> UpdateAsync(MatchDetails matchDetails)
        {
            List<MatchDetails> matchDetailss;
            using (Stream stream = GetFileStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var matchesJson = await reader.ReadToEndAsync();
                matchDetailss = JsonSerializer.Deserialize<List<MatchDetails>>(matchesJson);
            }

            var retrievedMatchDetails = matchDetailss?.SingleOrDefault(m => m.Id == matchDetails.Id);
            if (retrievedMatchDetails == null)
            {
                throw new ApplicationException("Missing tennis game entry for id " + matchDetails.Id);
            }

            retrievedMatchDetails = matchDetails;

            using (Stream stream = GetFileStream())
            using (StreamWriter writer = new StreamWriter(stream))
            {
                var matchesJsonToWrite = JsonSerializer.Serialize(matchDetailss);
                await writer.WriteAsync(matchesJsonToWrite);
            }

            return retrievedMatchDetails;
        }

        private Stream GetFileStream()
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetManifestResourceStream(_resourceName);
        }
    }
}
