using System.Collections.Generic;
using FlightBookingModels;

namespace FlightBookingDataService
{
    public interface IFlightBookingDataService
    {
        List<Flight> GetFlights();
        Flight GetFlightById(int id);

        void AddBooking(Booking booking);
        List<Booking> GetBookings();
        Booking GetBookingById(int id);
        void UpdateBooking(Booking booking);

        bool DeleteBooking(int id);
        bool IsSeatTaken(int flightId, int seatNumber);
    }
}