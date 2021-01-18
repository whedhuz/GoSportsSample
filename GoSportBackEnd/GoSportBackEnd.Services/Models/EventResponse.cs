using System;

namespace GoSportBackEnd.Services.Models
{
    public class EventResponse
    {
    }

    public class SuccessResponse : EventResponse
    {
        public Guid Id { get; set; }
        public object ResponseObj { get; set; }
    }

    public class ErrorResponse : EventResponse
    {
        public string ErrorMsg { get; set; }
    }
}
