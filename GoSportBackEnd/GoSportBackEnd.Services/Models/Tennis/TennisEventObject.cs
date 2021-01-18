using System.Text.Json.Serialization;

namespace GoSportBackEnd.Services.Models.Tennis
{
    public class TennisEventObject
    {
        [JsonPropertyName("gameId")]
        public string GameId { get; set; }
    }
}
