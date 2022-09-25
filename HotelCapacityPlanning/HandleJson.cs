using Newtonsoft.Json;

namespace HotelCapacityPlanning
{
    public static class HandleJson
    {
        public static JsonData? LoadJson(string jsonFile)
        {
            try
            {
                using var r = new StreamReader(jsonFile);
                string json = r.ReadToEnd();
                var jsonData = JsonConvert.DeserializeObject<JsonData>(json);
                return jsonData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while loading json: {ex.Message}");
                return null;
            }
        }
    }
}
