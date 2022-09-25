using Newtonsoft.Json;

namespace HotelCapacityPlanning
{
    public class JsonData
    {
        [JsonProperty("roomTypes")]
        public IEnumerable<int>? RoomTypes { get; set; }

        [JsonProperty("rooms")]
        public IEnumerable<Room>? Rooms { get; set; }

        [JsonProperty("reservations")]
        public IEnumerable<Reservation>? Reservations { get; set; }
    }
}
