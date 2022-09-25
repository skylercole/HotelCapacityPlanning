using Newtonsoft.Json;

namespace HotelCapacityPlanning
{
    public class Room
    {
        public Room()
        {
            Reservations = new List<Reservation>();
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        
        [JsonProperty("roomType")]
        public int RoomType { get; set; }

        public List<Reservation> Reservations { get; set; }
    }
}
