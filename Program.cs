using FlightBookingAppService;
using FlightBookingModels;
using System;

namespace Booking
{
    internal class Program
    {
        static AppService appService = new AppService();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=================================");
                Console.WriteLine("        BOOK YOUR FLIGHT!");
                Console.WriteLine("=================================");
                Console.WriteLine("1. View Flights");
                Console.WriteLine("2. Search Flight");
                Console.WriteLine("3. Book Flight");
                Console.WriteLine("4. View Bookings");
                Console.WriteLine("5. Pay Booking");
                Console.WriteLine("6. Cancel Booking");
                Console.WriteLine("7. Exit");
                Console.Write("Select option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": ViewFlights(); break;
                    case "2": SearchFlight(); break;
                    case "3": BookFlight(); break;
                    case "4": ViewBookings(); break;
                    case "5": PayBooking(); break;
                    case "6": CancelBooking(); break;
                    case "7": return;
                }
            }
        }

        static void ViewFlights()
        {
            Console.Clear();
            var flights = appService.GetFlights();

            Console.WriteLine("=== FLIGHTS ===");

            foreach (var f in flights)
            {
                Console.WriteLine($"{f.FlightID}. {f.Route}");
            }

            Pause();
        }

        static void SearchFlight()
        {
            Console.Clear();
            ViewFlights();
            Console.Write("From: ");
            string from = Console.ReadLine();

            Console.Write("To: ");
            string to = Console.ReadLine();

            var results = appService.SearchFlights(from, to);

            Console.WriteLine("\nResults:");
            if (results.Count == 0)
            {
                Console.WriteLine("No flights found.");
            }
            else
            {
                foreach (var f in results)
                {
                    Console.WriteLine($"{f.FlightID}. {f.Route}");
                }
            }

            Pause();
        }

        static void BookFlight()
        {
            Console.Clear();
            ViewFlights();

            Console.Write("Enter Flight ID: ");
            int flightId;
            while (!int.TryParse(Console.ReadLine(), out flightId))
            {
                Console.Write("Invalid input: ");
            }

            Console.Write("Passenger Name: ");
            string name = Console.ReadLine();

            Console.Write("Seat Number: ");
            int seat;
            while (!int.TryParse(Console.ReadLine(), out seat))
            {
                Console.Write("Invalid seat: ");
            }

            Console.WriteLine("Payment Method:");
            Console.WriteLine("1. Cash");
            Console.WriteLine("2. Card");
            Console.Write("Choose: ");

            string payment = Console.ReadLine() == "1" ? "Cash" : "Card";

            bool success = appService.BookFlight(flightId, name, seat, payment);

            Console.WriteLine(success
                ? "Booking is PENDING. Complete payment within 10 minutes."
                : "Booking failed (invalid flight or seat taken)");

            Pause();
        }
        static void ViewBookings()
        {
            Console.Clear();
            var bookings = appService.GetBookings();

            Console.WriteLine("=== BOOKINGS ===");

            if (bookings.Count == 0)
            {
                Console.WriteLine("No bookings.");
            }
            else
            {
                foreach (var b in bookings)
                {
                    Console.WriteLine(
                        $"{b.BookingID}. {b.PassengerName} | Flight {b.FlightID} | Seat {b.SeatNumber} | {b.PaymentMethod} | {b.Status}"
                    );
                }
            }

            Pause();
        }
        static void PayBooking()
        {
            Console.Clear();
            ViewBookings();

            Console.Write("Enter Booking ID to pay: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid input: ");
            }

            bool success = appService.ConfirmPayment(id);

            Console.WriteLine(success
                ? "Payment successful! Booking CONFIRMED."
                : "Payment failed (booking not found or already processed)");

            Pause();
        }
        static void CancelBooking()
        {
            Console.Clear();
            ViewBookings();

            Console.Write("Enter Booking ID: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Write("Invalid input: ");
            }

            bool success = appService.CancelBooking(id);

            Console.WriteLine(success ? "Cancelled!" : "Booking not found.");
            Pause();
        }

        static void Pause()
        {
            Console.WriteLine("\nPress any key...");
            Console.ReadKey();
        }
    }
}