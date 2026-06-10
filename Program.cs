using System.Net.Sockets;

namespace TSDSCAP01
{
    internal class Program
    {
        static List<string> passengerNames = new List<string> { "Sara", "Fahad", "Nasser" };
        static List<string> ticketNumbers = new List<string> { "TKT-1", "TKT-2", "TKT-3" };
        static List<string> cancelledTickets = new List<string>();
        static string[] flightNumbers = { "FL-001", "FL-002", "FL-003" };
        static List<string> availableDates = new List<string> { "2026-06-10", "2026-06-11", "2026-06-12" };
        static Dictionary<string, string> bookingRecord = new Dictionary<string, string>();
        static Queue<string> checkedInQueue = new Queue<string>();
        static Stack<string> boardingStack = new Stack<string>();
        static Queue<string> waitlistQueue = new Queue<string>();


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
                cancelledTickets.Add(ticket);
                
            }
            //Check booking record in dictionary
            if (bookingRecord.ContainsKey(ticket))
            {
                Console.WriteLine("No booking found for this ticket.");
                return;
            }
            Console.WriteLine("Flights: ");
            for (int i = 0; i < flightNumbers.Length; i++)
            {
                Console.WriteLine((i + 1) + ": " + flightNumbers[i]);
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
        public static void UpdateBooking()
        {
            Console.Write("Enter Ticket ID: ");
            string ticket = Console.ReadLine();
            // Check ticket exists
            if (!ticketNumbers.Contains(ticket))
            {
                Console.WriteLine("Ticket ID does not exist");
                return;
            }
            // Check ticket is not cancelled
            if (cancelledTickets.Contains(ticket))
            {
                Console.WriteLine("This ticket has been cancelled");
                return;
            }
            //Check booking exists
            if (!bookingRecord.ContainsKey(ticket))
            {
                Console.WriteLine("No booking found for this ticket");
                return;
            }
            //Extract current booking
            string bookingInfo = bookingRecord[ticket];
            string[] parts = bookingInfo.Split('|');

            string oldFlight = parts[0];
            string oldDate = parts[1];

            //Display current booking
            Console.WriteLine("Current Booking:");
            Console.WriteLine("Flight: " + oldFlight);
            Console.WriteLine("Date: " + oldDate);

            // Menu
            Console.WriteLine("Choose update option:");
            Console.WriteLine("1. Change Flight Only");
            Console.WriteLine("2. Change Date Only");
            Console.WriteLine("3. Change Both");
            Console.WriteLine("0. Cancel Update");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 0)
            {
                Console.WriteLine("Update cancelled.");
                return;
            }
            string newFlight = oldFlight;
            string newDate = oldDate;
            // Change flight only
            if (choice == 1)
            {
                Console.WriteLine("Available Flights:");
                for (int i = 0; i < flightNumbers.Length; i++)
                {
                    Console.WriteLine((i + 1) + ": " + flightNumbers[i]);
                }

                Console.Write("Select flight: ");
                int flightChoice = int.Parse(Console.ReadLine());

                if (flightChoice < 1 || flightChoice > flightNumbers.Length)
                {
                    Console.WriteLine("Invalid flight choice");
                    return;
                }

                newFlight = flightNumbers[flightChoice - 1];
            }
            // Change date only
            else if (choice == 2)
            {
                Console.WriteLine("Available Dates:");
                for (int i = 0; i < availableDates.Count; i++)
                {
                    Console.WriteLine((i + 1) + ": " + availableDates[i]);
                }
                Console.Write("Select date: ");
                int dateChoice = int.Parse(Console.ReadLine());

                if (dateChoice < 1 || dateChoice > availableDates.Count)
                {
                    Console.WriteLine("Invalid date choice.");
                    return;
                }

                newDate = availableDates[dateChoice - 1];
            }
            // Change both
            else if (choice == 3)
            {
                Console.WriteLine("Available Flights:");
                for (int i = 0; i < flightNumbers.Length; i++)
                {
                    Console.WriteLine((i + 1) + ": " + flightNumbers[i]);
                }

                Console.Write("Select flight: ");
                int flightChoice = int.Parse(Console.ReadLine());

                if (flightChoice < 1 || flightChoice > flightNumbers.Length)
                {
                    Console.WriteLine("Invalid flight choice.");
                    return;
                }

                newFlight = flightNumbers[flightChoice - 1];

                Console.WriteLine("Available Dates:");
                for (int i = 0; i < availableDates.Count; i++)
                {
                    Console.WriteLine((i + 1) + ": " + availableDates[i]);
                }
                Console.Write("Select date: ");
                int dateChoice = int.Parse(Console.ReadLine());

                if (dateChoice < 1 || dateChoice > availableDates.Count)
                {
                    Console.WriteLine("Invalid date choice");
                    return;
                }
                newDate = availableDates[dateChoice - 1];
            }
            else
            {
                Console.WriteLine("Invalid menu option");
                return;
            }
            //Update dictionary 
            bookingRecord[ticket] = newFlight + "|" + newDate;
            //Confirmation
            Console.WriteLine("Booking Updated Successfully!");
            Console.WriteLine("OLD BOOKING:");
            Console.WriteLine("Flight: " + oldFlight);
            Console.WriteLine("Date: " + oldDate);
            Console.WriteLine("NEW BOOKING:");
            Console.WriteLine("Flight: " + newFlight);
            Console.WriteLine("Date: " + newDate);


        }
        public static void CancelTicket()
        {
            Console.Write("Enter Ticket ID: ");
            string ticket = Console.ReadLine();
            // Check ticket exists
            if (!ticketNumbers.Contains(ticket))
            {
                Console.WriteLine("Ticket ID does not exist");
                return;
            }
            // Check ticket is not cancelled
            if (cancelledTickets.Contains(ticket))
            {
                Console.WriteLine("This ticket has been cancelled");
            }
            int index = ticketNumbers.IndexOf(ticket);
            string passengerName = passengerNames[index];
            Console.WriteLine("Passenger: " + passengerName);
            if (bookingRecord.ContainsKey(ticket))
            {
                string removedBooking = bookingRecord[ticket];

                Console.WriteLine("Removed Booking: " + removedBooking);
                bookingRecord.Remove(ticket);
            }
            else
            {
                Console.WriteLine("No booking record found");
            }
            // Add ticket to cancelled list
            cancelledTickets.Add(ticket);
            // Remove from check-in queue
            bool removedFromQueue = false;
            Queue<string> tempQueue = new Queue<string>();
            while (checkedInQueue.Count > 0)
            {
                string passenger = checkedInQueue.Dequeue();
                if (passenger != passengerName)
                {
                    tempQueue.Enqueue(passenger);
                }
                else
                {
                    removedFromQueue = true;
                }
            }
            while (tempQueue.Count > 0)
            {
                checkedInQueue.Enqueue(tempQueue.Dequeue());
            }

            if (removedFromQueue)
            {
                Console.WriteLine("Passenger removed from check-in queue.");
            }
            // Remove from boarding stack
            bool removedFromStack = false;

            Stack<string> tempStack = new Stack<string>();
            Stack<string> rebuiltStack = new Stack<string>();

            while (boardingStack.Count > 0)
            {
                tempStack.Push(boardingStack.Pop());
            }

            while (tempStack.Count > 0)
            {
                string passenger = tempStack.Pop();

                if (passenger != passengerName)
                {
                    rebuiltStack.Push(passenger);
                }
                else
                {
                    removedFromStack = true;
                }
            }

            boardingStack = rebuiltStack;

            if (removedFromStack)
            {
                Console.WriteLine("Passenger removed from boarding stack");
            }
            // Cancellation summary
            Console.WriteLine("===== Cancellation Summary =====");
            Console.WriteLine("Passenger Name: " + passengerName);
            Console.WriteLine("Ticket ID: " + ticket);
            Console.WriteLine("Status: Cancelled");
        }
        public static void PassengerCheckIn()
        {
            Console.WriteLine("Choose update option:");
            Console.WriteLine("1. Check in a passenger");
            Console.WriteLine("2. View check-in queue");
            Console.WriteLine("3. Process next passenger ");
            Console.WriteLine("0. Back ");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());
            bool exit = false;
            while (exit == false)
            {
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Ticket ID: ");
                        string ticket = Console.ReadLine();
                        // Check ticket exists
                        if (!ticketNumbers.Contains(ticket))
                        {
                            Console.WriteLine("Ticket ID does not exist");
                            return;
                        }
                        // Check ticket is not cancelled
                        if (cancelledTickets.Contains(ticket))
                        {
                            Console.WriteLine("This ticket has been cancelled");
                        }
                        //Check booking record in dictionary
                        if (!bookingRecord.ContainsKey(ticket))
                        {
                            Console.WriteLine("No booking found for this ticket.");
                            return;
                        }
                        int index = ticketNumbers.IndexOf(ticket);
                        string passengerName = passengerNames[index];
                        if (checkedInQueue.Contains(passengerName))
                        {
                            Console.WriteLine("Passenger already checked in");
                        }
                        if (checkedInQueue.Count < 10)
                        {
                            checkedInQueue.Enqueue(passengerName);
                            Console.WriteLine(passengerName + " checked in successfully.");
                        }
                        else
                        {
                            waitlistQueue.Enqueue(passengerName);
                            Console.WriteLine("Check-in queue is full");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Check In Queue");

                        if (checkedInQueue.Count == 0)
                        {
                            Console.WriteLine("Queue is empty.");
                        }
                        else
                        {
                            int position = 1;
                            foreach (string passenger in checkedInQueue)
                            {
                                Console.WriteLine(position + ":" + passenger);
                                position++;
                            }
                        }
                        Console.WriteLine("Waitlist Count: " + waitlistQueue.Count);

                        break;
                    case 3:
                        Console.WriteLine("View check-in queue");
                        if (checkedInQueue.Count == 0)
                        {
                            Console.WriteLine("No passengers to process.");
                        }
                        else
                        {
                            string processedPassenger = checkedInQueue.Dequeue();

                            Console.WriteLine("Processed passenger: " + processedPassenger);
                            // Move one passenger from waitlist
                            if (waitlistQueue.Count > 0)
                            {
                                string movedPassenger = waitlistQueue.Dequeue();
                                checkedInQueue.Enqueue(movedPassenger);
                                Console.WriteLine(movedPassenger + " moved from waitlist to check-in queue");
                            }
                        }
                        break;
                    case 0:
                        Console.WriteLine("Exit");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
                Console.Write(" press any key to countinue...  ");
                Console.ReadLine();
                Console.Clear();
            }
        }
        public static void BoardPassengers()
        {

        }
        static void Main(string[] args)
        {
            bool exit = false;
            while (exit == false)
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
                        UpdateBooking();
                        break;
                    case 6:
                        CancelTicket();
                        break;
                    case 7:
                        PassengerCheckIn();
                        break;
                    case 8:
                        break;
                    case 9:
                        break;
                    case 10:
                        break;
                    case 0:
                        Console.WriteLine("Exit");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
                Console.Write(" press any key to countinue...  ");
                Console.ReadLine();
                Console.Clear();
            }
        }
    }
}
