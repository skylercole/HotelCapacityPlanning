using Xunit;

namespace HotelCapacityPlanning.Tests
{
    public class HotelCapacityPlanningUnitTests
    {
        private const string wrongJsonMessage = "Input json file does not contain all required objects for hotel capacity planning.";

        [Fact]
        public void TestProblemExample()
        {
            const string expected = "Room ID 1: [1, 3]Room ID 2: [2]Room ID 3: []Room ID 4: [4]";

            var result = Booking.PlanCapacity("testExample.json");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void WillFailWithBadJson()
        {
            var result = Booking.PlanCapacity("bad.json");

            Assert.Equal(wrongJsonMessage, result);
        }

        [Fact]
        public void WillFailWithBadFileName()
        {
            var result = Booking.PlanCapacity("wrongName.json");

            Assert.Equal(wrongJsonMessage, result);
        }

        [Fact]
        public void Test2ndExample()
        {
            const string expected = "Room ID 1: [9]Room ID 2: [1, 3]";

            var result = Booking.PlanCapacity("2ndExample.json");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test3rdExample()
        {
            const string expected = "Room ID 1: [3, 4]Room ID 2: [1, 9]Room ID 3: [6, 8]";

            var result = Booking.PlanCapacity("3rdExample.json");

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test4thExample()
        {
            const string expected = "Room ID 1: [3, 4]Room ID 2: [1, 9]Room ID 3: [6, 8]Room ID 4: [7]Room ID 5: [10]Room ID 6: [30, 40]Room ID 7: [60, 80]Room ID 8: []Room ID 9: []";

            var result = Booking.PlanCapacity("4thExample.json");

            Assert.Equal(expected, result);
        }
    }
}