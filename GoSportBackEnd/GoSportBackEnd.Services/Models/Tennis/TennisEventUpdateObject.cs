using System.Text.Json.Serialization;

namespace GoSportBackEnd.Services.Models.Tennis
{
    public class TennisEventUpdateObject
    {
        [JsonPropertyName("gameId")]
        public string GameId { get; set; }
        [JsonPropertyName("eventId")]
        public string EventId { get; set; }
        [JsonPropertyName("action")]
        public string Action { get; set; }
        [JsonPropertyName("newEvent")]
        public TennisEventObject NewEventObject { get; set; }
    }
}
