using Newtonsoft.Json;

namespace HotelCapacityPlanning
{
    public class Reservation
    {
        [JsonProperty("reservationId")]
        public int ReservationId { get; set; }

        [JsonProperty("roomType")]
        public int RoomType { get; set; }

        [JsonProperty("startDate")]
        public DateTime StartDate { get; set; }

        [JsonProperty("endDate")]
        public DateTime EndDate { get; set; }

        public double GetDays()
        {
            return (EndDate - StartDate).TotalDays;
        }
    }
}
