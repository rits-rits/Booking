using System.Collections.Generic;
using FlightBookingModels;

namespace FlightBookingDataService
{
    public class DataService
    {
        private List<Flight> flights = new List<Flight>()
        {
            new Flight { FlightID = 1, Route = "Manila to Seoul" },
            new Flight { FlightID = 2, Route = "Manila to Ottawa" },
            new Flight { FlightID = 3, Route = "Manila to Bern" },
            new Flight { FlightID = 4, Route = "Manila to Tokyo" },
            new Flight { FlightID = 5, Route = "Manila to Bangkok" },
            new Flight { FlightID = 6, Route = "Manila to Beijing" },
            new Flight { FlightID = 7, Route = "Manila to Taipei" },
            new Flight { FlightID = 8, Route = "Manila to Rome" },
            new Flight { FlightID = 9, Route = "Manila to Washington" },
            new Flight { FlightID = 10, Route = "Manila to London" }
        };

        private List<Booking> bookings = new List<Booking>();

        // FLIGHTS
        public List<Flight> GetFlights()
        {
            return flights;
        }

        public Flight GetFlightById(int id)
        {
            foreach (var f in flights)
            {
                if (f.FlightID == id)
                    return f;
            }
            return null;
        }

        // BOOKINGS
        public void AddBooking(Booking booking)
        {
            bookings.Add(booking);
        }

        public List<Booking> GetBookings()
        {
            return bookings;
        }

        public Booking GetBookingById(int id)
        {
            foreach (var b in bookings)
            {
                if (b.BookingID == id)
                    return b;
            }
            return null;
        }

        public bool DeleteBooking(int id)
        {
            var booking = GetBookingById(id);

            if (booking == null)
                return false;

            booking.Status = "CANCELLED";
            return true;
        }
        public bool IsSeatTaken(int flightId, int seatNumber)
        {
            foreach (var b in bookings)
            {
                if (b.FlightID == flightId &&
                    b.SeatNumber == seatNumber &&
                    b.Status != "CANCELLED")
                {
                    return true;
                }
            }
            return false;
        }
    }
}