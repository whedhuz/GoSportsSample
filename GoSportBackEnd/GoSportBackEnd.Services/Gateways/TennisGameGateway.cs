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
            _resourceName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                @"Gateways\DbMock\TennisMatches.json");
        }

        public async Task<MatchDetails> GetAsync(string id)
        {
            List<MatchDetails> matchDetailss = await GetMatchDetailssAsync();
            return matchDetailss?.SingleOrDefault(m => m.Id == id);
        }

        public async Task<MatchDetails> UpdateAsync(MatchDetails matchDetails)
        {
            List<MatchDetails> matchDetailss = await GetMatchDetailssAsync();

            var matchIndex = matchDetailss.FindIndex(m => m.Id == matchDetails.Id);
            if (matchIndex == -1)
            {
                throw new ApplicationException("Missing tennis game entry for id " + matchDetails.Id);
            }

            matchDetailss[matchIndex] = matchDetails;

            using (StreamWriter writer = new StreamWriter(_resourceName))
            {
                var matchesJsonToWrite = JsonSerializer.Serialize(matchDetailss);
                await writer.WriteAsync(matchesJsonToWrite);
            }

            return matchDetails;
        }

        private async Task<List<MatchDetails>> GetMatchDetailssAsync()
        {
            List<MatchDetails> matchDetailss;

            using (StreamReader reader = new StreamReader(_resourceName))
            {
                var matchesJson = await reader.ReadToEndAsync();
                matchDetailss = JsonSerializer.Deserialize<List<MatchDetails>>(matchesJson);
            }

            return matchDetailss;
        }
    }
}
