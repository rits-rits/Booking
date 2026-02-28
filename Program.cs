using System;
using System.Collections.Generic;

namespace Booking
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("=================================");
                Console.WriteLine("        BOOK YOUR FLIGHT!");
                Console.WriteLine("================================="); 
                Console.WriteLine("1. View Available Flights");
                Console.WriteLine("2. Search Flight");
                Console.WriteLine("3. Book Flight");
                Console.WriteLine("4. View Bookings");
                Console.WriteLine("5. Cancel Booking");
                Console.WriteLine("6. Exit");

                Console.WriteLine("=================================");
                Console.WriteLine();
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();

               
                switch (choice)
                {
                    case "1":
                        ViewFlights();
                        break;

                    case "2":
                        SearchFlight();
                        break;

                    case "3":
                        BookFlight();
                        break;

                    case "4":
                        ViewBookings();
                        break;

                    case "5":
                        CancelBooking();
                        break;

                    case "6":
                        Console.WriteLine("Exiting System...");
                        return;

                    default:
                        Console.WriteLine("Invalid Choice!!!");
                        Pause();
                        break;
                }

            }
        }

        static void ViewFlights()
        {
            Console.Clear();
            List<string> flights = new List<String>();
            flights.Add("1. Tokyo to South Korea");
            flights.Add("2. USA to Canada");
            flights.Add("3. Philippines to Japan");
            Console.WriteLine("=== AVAILABLE FLIGHTS ===");
            foreach (string flight in flights)
            {
                Console.WriteLine(flight);
            }

            Pause();
        }
        static void SearchFlight()
        {
            Console.Clear();
            Console.WriteLine("=== SEARCH FLIGHT ===");

            Console.Write("Enter Departure City: ");
            string from = Console.ReadLine();

            Console.Write("Enter Destination City: ");
            string to = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Searching flights from " + from + " to " + to + "...");
            Console.WriteLine("(Search results will appear here)");

            Pause();
        }

        static void BookFlight()
        {
            Console.Clear();
            Console.WriteLine("=== BOOK FLIGHT ===");

            Console.Write("Enter Flight ID: ");
            string flightID = Console.ReadLine();

            Console.Write("Enter Passenger Name: ");
            string name = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("Booking Confirmed!");
            Console.WriteLine("Passenger: " + name);
            Console.WriteLine("Flight ID: " + flightID);

            Pause();
        }

        static void ViewBookings()
        {
            Console.Clear();
            Console.WriteLine("=== VIEW BOOKINGS ===");
            Console.WriteLine("(Bookings will appear here)");

            Pause();
        }

        static void CancelBooking()
        {
            Console.Clear();
            Console.WriteLine("=== CANCEL BOOKING ===");

            Console.Write("Enter Booking ID: ");
            string bookingID = Console.ReadLine();

            Console.WriteLine("Booking Cancelled (Demo Only)");

            Pause();
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }
    }
}
