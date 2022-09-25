namespace HotelCapacityPlanning
{
    public static class Booking
    {
        // Keep the JSON structure statically, so we don't have to pass it recursively each time.
        public static JsonData? InputJson = null;

        /// <summary>
        /// Maximise the availability of each room type
        /// </summary>
        /// <param name="jsonName">json file name</param>
        /// <returns>the output, useful for testing against</returns>
        public static string PlanCapacity(string? jsonName)
        {
            InputJson = HandleJson.LoadJson(jsonName ?? "testjson.json");
            var resultOutput = string.Empty;

            // Some sanity checks
            if (InputJson == null || InputJson.RoomTypes == null || InputJson.Rooms == null || InputJson.Reservations == null)
            {
                var message = "Input json file does not contain all required objects for hotel capacity planning.";
                Console.WriteLine(message);
                return message;
            }

            var reservations = new List<Reservation>(InputJson.Reservations);

            // Optimize rooms one by one
            foreach (var room in InputJson.Rooms)
            {
                // Choose only reservation ids compatible with the room.
                var reservationIdList = reservations.Where(x => x.RoomType == room.RoomType).Select(x => x.ReservationId).ToList();
                var result = Booking.RecursiveBooking(room.Id, reservationIdList, new List<Reservation>(), new List<Reservation>());

                // Useful output for checking booked capacity per room.
                //result.ForEach(i => Console.WriteLine($"{i.StartDate} to {i.EndDate}"));

                // Output the result after each room is optimized
                string output = $"Room ID {room.Id}: [";
                var c = 0;
                foreach (var reservation in result)
                {
                    if (c > 0)
                        output += ", ";

                    output += reservation.ReservationId;
                    c++;
                }
                output += "]";
                Console.WriteLine(output);

                // Remove the chosen set of reservations from the pool of reservations for the next room in the list.
                foreach (var reservation in result)
                    reservations.RemoveAll(x => x.ReservationId == reservation.ReservationId);

                resultOutput += output;
            }

            return resultOutput;
        }

        /// <summary>
        /// A backtracking recursive solution to the stated hotel capacity planning problem.
        /// It will recursively exhaust all reservations for a room and choose the highest capacity set of reservations for a room.
        /// </summary>
        /// <param name="roomId">room id</param>
        /// <param name="reservationIdList">list of available reservation ids</param>
        /// <param name="reservationsMadeSoFar">list of reservations made so far for this room in the current recursive run</param>
        /// <param name="reservationsOptimal">list of the best possible reservation set for this room</param>
        /// <returns>the best possible reservation solution for the room id</returns>
        public static List<Reservation> RecursiveBooking(int roomId, List<int> reservationIdList, List<Reservation> reservationsMadeSoFar, List<Reservation> reservationsOptimal)
        {
            foreach (var reservationId in reservationIdList)
            {
                var room = InputJson.Rooms.Single(x => x.Id == roomId);
                var reservation = InputJson.Reservations.Single(x => x.ReservationId == reservationId);

                var reservationsForCurrentRecursion = new List<Reservation>(reservationsMadeSoFar);

                if (room.RoomType == reservation.RoomType && // Only the same room type allowed
                    !reservationsForCurrentRecursion.Any(x => x.StartDate < reservation.EndDate && reservation.StartDate < x.EndDate)) // new reservation shouldn't collide with existing reservations
                {
                    reservationsForCurrentRecursion.Add(reservation);
                }

                var newReservationIdList = new List<int>(reservationIdList);
                newReservationIdList.Remove(reservationId);

                if (newReservationIdList.Count == 0)
                {
                    var totalDaysFromCurrentReservations = reservationsForCurrentRecursion.Sum(x => x.GetDays());
                    var totalDaysFromBestReservations = reservationsOptimal.Sum(x => x.GetDays());

                    if (totalDaysFromCurrentReservations > totalDaysFromBestReservations)
                    {
                        reservationsOptimal = reservationsForCurrentRecursion;
                    }

                    return reservationsOptimal;
                }

                reservationsOptimal = RecursiveBooking(roomId, newReservationIdList, reservationsForCurrentRecursion, reservationsOptimal);
            }

            return reservationsOptimal;
        }
    }
}
