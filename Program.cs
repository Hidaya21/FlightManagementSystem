namespace TSDSCAP01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("================================= ");
            Console.WriteLine("SKY WINGS FLIGHT MANAGEMENT SYSTEM");
            Console.WriteLine("================================= ");
            Console.WriteLine("1. Register New Passenger ");
            Console.WriteLine("2. View All Passengers ");
            Console.WriteLine("3. Book a Flight Ticke   ");
            Console.WriteLine("4. View Booking Details ");
            Console.WriteLine("5. Update a Booking   ");
            Console.WriteLine("6. Cancel a Ticket ");
            Console.WriteLine("7. Passenger Check-In");
            Console.WriteLine("8. Board Passengers (Boarding Stack) ");
            Console.WriteLine("9. Generate Flight Manifest ");
            Console.WriteLine("10.Manage Waitlist & Seat Assignment");
            Console.WriteLine("0. Exit");
            Console.WriteLine("================================= ");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    List<string> passengerNames = new List<string>();
                    List<string> ticketNumbers = new List<string>();
                    Console.Write("Enter passenger name: ");
                    string name = Console.ReadLine();
                    //check if name is empty
                    if (name.Trim() != "")
                    {
                        Console.WriteLine("Error passenger name cannot be empty");
                    }
                    else
                    {
                        Console.WriteLine("Passenger added seccussfully");
                    }
                    foreach(string passenger in passengerNames)
                    {
                        if(passenger.ToLower() == name.ToLower())
                        {
                            Console.WriteLine("Passenger added seccussfully");
                        }
                    }

                        break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                case 7:
                    break;
                case 8:
                    break;
                case 9:
                    break;
                case 10:
                    break;
                case 0:
                    break;


            }
         }
    }
}
