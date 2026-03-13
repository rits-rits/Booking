using System.Collections.Generic;
using BookingDataServices;
using BookingModels;
namespace BookingAppServices
{
    public class BApp
    {
        


            BData data = new BData();

            public List<string> GetFlights()
            {
                return data.flights;
            }

            public List<BookingModel> GetBookings()
            {
                return data.bookings;
            }

            public void BookFlight(int flightIndex, string passengerName)
            {
                BookingModel booking = new BookingModel();
                booking.PassengerName = passengerName;
                booking.FlightRoute = data.flights[flightIndex];

                data.bookings.Add(booking);
            }

            public void CancelBooking(int bookingIndex)
            {
                data.bookings.RemoveAt(bookingIndex);
            }

            public List<string> SearchFlight(string from, string to)
            {
                List<string> results = new List<string>();

                foreach (var flight in data.flights)
                {
                    if (flight.ToLower().Contains(from.ToLower()) &&
                        flight.ToLower().Contains(to.ToLower()))
                    {
                        results.Add(flight);
                    }
                }

                return results;
            }
        }
    }
}
}
