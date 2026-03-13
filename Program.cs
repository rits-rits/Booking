using System;
using BookingAppServices;
using BookingModels;
namespace Booking
{
    internal class Program
    {
        static BApp app = new BApp();

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
                        return;

                    default:
                        Console.WriteLine("Invalid Choice!");
                        Pause();
                        break;
                }
            }
        }

        static void ViewFlights()
        {
            Console.Clear();
            var flights = app.GetFlights();

            Console.WriteLine("=== AVAILABLE FLIGHTS ===");

            for (int i = 0; i < flights.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {flights[i]}");
            }

            Pause();
        }

        static void SearchFlight()
        {
            Console.Clear();

            Console.Write("Enter Departure City: ");
            string from = Console.ReadLine();

            Console.Write("Enter Destination City: ");
            string to = Console.ReadLine();

            var results = app.SearchFlight(from, to);

            Console.WriteLine("\nSearch Results:");

            foreach (var flight in results)
            {
                Console.WriteLine(flight);
            }

            if (results.Count == 0)
                Console.WriteLine("No matching flights found.");

            Pause();
        }

        static void BookFlight()
        {
            Console.Clear();
            ViewFlights();

            Console.Write("Enter Flight ID: ");
            int id = Convert.ToInt32(Console.ReadLine()) - 1;

            Console.Write("Enter Passenger Name: ");
            string name = Console.ReadLine();

            app.BookFlight(id, name);

            Console.WriteLine("Booking Confirmed!");

            Pause();
        }

        static void ViewBookings()
        {
            Console.Clear();
            var bookings = app.GetBookings();

            Console.WriteLine("=== BOOKINGS ===");

            for (int i = 0; i < bookings.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {bookings[i].PassengerName} - {bookings[i].FlightRoute}");
            }

            if (bookings.Count == 0)
                Console.WriteLine("No bookings found.");

            Pause();
        }

        static void CancelBooking()
        {
            Console.Clear();
            ViewBookings();

            Console.Write("Enter Booking ID: ");
            int id = Convert.ToInt32(Console.ReadLine()) - 1;

            app.CancelBooking(id);

            Console.WriteLine("Booking Cancelled!");

            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }
}