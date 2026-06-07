using System.Net.Sockets;

namespace TSDSCAP01
{
    internal class Program
    {
        static List<string> passengerNames = new List<string> { "Sara", "Fahad", "Nasser" };
        static List<string> ticketNumbers = new List<string> { "TKT-1", "TKT-2", "TKT-3" };
        static List<string> cancelledTickets = new List<string>();
        static string[] flightNumbers = { "FL-001", "FL-002", "FL-003" };
        static List<string> availableDates = new List<string> {"2026-06-10","2026-06-11","2026-06-12" };
        static Dictionary<string, string> bookingRecord = new Dictionary<string, string>();

        

        public static void RegisterNewPassenger()
        {
            // Ask user to enter passenger name
            Console.Write("Enter passenger name: ");
            string name = Console.ReadLine();
            //check if name is empty
            if (name.Trim() == "")
            {
                Console.WriteLine("Error passenger name cannot be empty");
                return;

            }
            else
            {
                // Loop through existing passengers to check duplicates
                foreach (string passenger in passengerNames)
                {
                    // Compare names ignoring case (Sara = sara)
                    if (passenger.ToLower() == name.ToLower())
                    {
                        Console.WriteLine("Error passenger already exit");
                        return;
                    }
                }

            }
            // Generate ticket ID based on current number of passengers
            string ticketId = "TKT-" + (passengerNames.Count + 1);
            // Add passenger name to list
            passengerNames.Add(name);
            // Add ticket ID to ticket list
            ticketNumbers.Add(ticketId);
            //Display a success confirmation
            Console.WriteLine("Passenger registered successfully!");
            Console.WriteLine("Passenger Name: " + name);
            Console.WriteLine("Ticket ID: " + ticketId);
        }
        public static void ViewAllPassengers()
        {
            //Check if passengerNames is empty
            if (passengerNames.Count == 0)
            {
                Console.WriteLine("No passengers registered yet");
            }
            //Display a formatted table header
            Console.WriteLine("No. | Passenger Name | Ticket ID | Status");
            Console.WriteLine("-------------------------------------------");
            //Loop using for important requirement
            for (int i = 0; i < passengerNames.Count; i++)
            {
                string name = passengerNames[i];
                string ticket = ticketNumbers[i];
                // Check cancellation status
                string status;
                if (cancelledTickets.Contains(ticket))
                {
                    status = "CANCELLED";
                }
                else
                {
                    status = "Active";
                }
                //display row
                Console.WriteLine((i + 1) + " | " + name + " | " + ticket + " | " + status);
            }
            //Total count
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Total Passengers: " + passengerNames.Count);
        }
        public static void BookFlightTicke()
        {
            Console.Write("Enter your ticket: ");
            string ticket = Console.ReadLine();
            //Validate ticket exists in ticketNumbers
            if (!ticketNumbers.Contains(ticket))
            {
                Console.WriteLine("Ticket does not exist ");
                return;
            }
            //Check if ticket is cancelled
            if (cancelledTickets.Contains(ticket))
            {
                Console.WriteLine("This ticket is cancelled");
                return;
            }
            //Check booking record in dictionary
            if (!bookingRecord.ContainsKey(ticket))
            {
                Console.WriteLine("No booking found for this ticket.");
                return;
            }
            Console.WriteLine("Flights: ");
            for (int i = 0; i < flightNumbers.Length; i++)
            {
                Console.WriteLine(i + ": " + flightNumbers[i]);
            }
            // Ask user to select a flight
            Console.Write("Select Flight: ");
            int flightChoice = int.Parse(Console.ReadLine());
            // Validate flight selection
            if (flightChoice < 1 || flightChoice > flightNumbers.Length)
            {
                Console.WriteLine("Invalid flight choice");
                return;
            }
            // Get selected flight
            string selectedFlight = flightNumbers[flightChoice - 1];
            // Display all available dates with index numbers
            Console.WriteLine("Available Dates:");
            for (int i = 0; i < availableDates.Count; i++)
            {
                Console.WriteLine((i + 1) + ". " + availableDates[i]);
            }
            // Ask user to select a date
            Console.Write("Select Date: ");
            int dateChoice = int.Parse(Console.ReadLine());
            // Validate date selection
            if (dateChoice < 1 || dateChoice > availableDates.Count)
            {
                Console.WriteLine("Invalid date choice.");
                return;
            }
            // Get selected date
            string selectedDate = availableDates[dateChoice - 1];
            // Add or update booking in dictionary 
            bookingRecord.Add(ticket, selectedFlight + "|" + selectedDate);
            // Find passenger index in list
            int passengerIndex = ticketNumbers.IndexOf(ticket);
            // Show confirmation message
            Console.WriteLine("Booking Confirmed!");
            Console.WriteLine("Passenger Name: " + passengerNames[passengerIndex]);
            Console.WriteLine("Ticket ID: " + ticket);
            Console.WriteLine("Flight: " + selectedFlight);
            Console.WriteLine("Date: " + selectedDate);

        }

        public static void ViewBookingDetails()
        {
            Console.Write("Enter your ticket: ");
            string ticketID = Console.ReadLine();
            //Validate ticket exists in ticketNumbers
            if (!ticketNumbers.Contains(ticketID))
            {
                Console.WriteLine("Ticket does not exist ");
                return;
            }
            //Get passenger name using index
            int index = ticketNumbers.IndexOf(ticketID);
            string passengerName = passengerNames[index];

            //Check if ticket is cancelled
            if (cancelledTickets.Contains(ticketID))
            {
                Console.WriteLine("This ticket is cancelled");
                return;

            }
            //Check booking record in dictionary
            if (!bookingRecord.ContainsKey(ticketID))
            {
                Console.WriteLine("No booking found for this ticket.");
                return;
            }
            // Retrieve and split booking details
            string bookingInfo = bookingRecord[ticketID];
            string[] parts = bookingInfo.Split('|');

            string flightNumber = parts[0];
            string date = parts[1];

            //Display booking summary
            Console.WriteLine("===== BOOKING SUMMARY =====");
            Console.WriteLine("Passenger Name : " + passengerName);
            Console.WriteLine("Ticket ID      : " + ticketID);
            Console.WriteLine("Flight Number  : " + flightNumber);
            Console.WriteLine("Date           : " + date);
        }
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
                    RegisterNewPassenger();
                    break;
                case 2:
                    ViewAllPassengers();
                    break;
                case 3:
                    BookFlightTicke();
                    break;
                case 4:
                    ViewBookingDetails();

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
