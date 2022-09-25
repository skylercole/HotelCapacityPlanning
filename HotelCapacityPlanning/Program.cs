using HotelCapacityPlanning;

internal class Program
{
    static void Main(string[] args)
    {
        string? jsonName = null;
        if (args.Length > 0)
        {
            jsonName = Environment.GetCommandLineArgs()[1];
        }
        else
        {
            Console.WriteLine("Please enter as command line parameter an input json file.");
            return;
        }

        Booking.PlanCapacity(jsonName);
    }    
}
