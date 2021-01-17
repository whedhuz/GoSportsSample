using System.Collections.Generic;

namespace GoSportBackEnd.Services.Models.Tennis
{
    public class MatchDetails
    {
        public string Id { get; set; }

        // 0 = game not started, server not selected
        // 1 = player 1
        // 2 = player 2
        public int ServingPlayer { get; set; } = 0;
        public List<SetScore> SetScores { get; set; } = new List<SetScore>();
        public CurrentGameScore CurrentGameScore { get; set; } = new CurrentGameScore();

        // To be added: Player Details, teams, game format (number of set to be played) etc
    }

    public class SetScore
    {
        public int PlayerOneScore { get; set; } = 0;
        public int PlayerTwoScore { get; set; } = 0;
    }

    public class CurrentGameScore
    {
        public string PlayerOneScore { get; set; } = "0";
        public string PlayerTwoScore { get; set; } = "0";
    }
}
