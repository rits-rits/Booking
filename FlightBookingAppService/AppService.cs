using System.Collections.Generic;
using FlightBookingDataService;
using FlightBookingModels;

namespace FlightBookingAppService
{
    public class AppService
    {
        DataService dataService = new DataService(new FlightDBDataService());
        int bookingCounter = 1;

        public List<Flight> GetFlights()
        {
            return dataService.GetFlights();
        }

        public List<Flight> SearchFlights(string from, string to)
        {
            var results = new List<Flight>();

            foreach (var flight in dataService.GetFlights())
            {
                string route = flight.Route.ToLower();

                if (route.Contains(from.ToLower()) && route.Contains(to.ToLower()))
                {
                    results.Add(flight);
                }
            }

            return results;
        }

        public bool BookFlight(int flightId, string name, int seatNumber, string paymentMethod)
        {
            var flight = dataService.GetFlightById(flightId);

            if (flight == null)
                return false;

            if (dataService.IsSeatTaken(flightId, seatNumber))
                return false;

            Booking booking = new Booking
            {
                BookingID = bookingCounter++,
                PassengerName = name,
                FlightID = flightId,
                SeatNumber = seatNumber,
                PaymentMethod = paymentMethod,
                Status = "PENDING",
                CreatedAt = DateTime.Now
            };

            dataService.AddBooking(booking);
            return true;
        }
        public List<Booking> GetBookings()
        {
            AutoCancelExpired();
            return dataService.GetBookings();
        }

        public bool CancelBooking(int bookingId)
        {
            return dataService.DeleteBooking(bookingId);
        }
        public bool ConfirmPayment(int bookingId)
        {
            var booking = dataService.GetBookingById(bookingId);

            if (booking == null || booking.Status != "PENDING")
                return false;

            booking.Status = "CONFIRMED";

            dataService.UpdateBooking(booking);
            return true;
        }
        public void AutoCancelExpired()
        {
            foreach (var b in dataService.GetBookings())
            {
                if (b.Status == "PENDING")
                {
                    if ((DateTime.Now - b.CreatedAt).TotalMinutes >= 10)
                    {
                        b.Status = "CANCELLED";
                    }
                }
            }
        }
    }
}