using System.Collections.Generic;
using FlightBookingModels;

namespace FlightBookingDataService
{
    public class DataService
    {
        private IFlightBookingDataService _dataService;

        public DataService(IFlightBookingDataService dataService)
        {
            _dataService = dataService;
        }

        // ================= FLIGHTS =================

        public List<Flight> GetFlights()
        {
            return _dataService.GetFlights();
        }

        public Flight GetFlightById(int id)
        {
            return _dataService.GetFlightById(id);
        }

        // ================= BOOKINGS =================

        public void AddBooking(Booking booking)
        {
            _dataService.AddBooking(booking);
        }

        public List<Booking> GetBookings()
        {
            return _dataService.GetBookings();
        }

        public Booking GetBookingById(int id)
        {
            return _dataService.GetBookingById(id);
        }

        public bool DeleteBooking(int id)
        {
            return _dataService.DeleteBooking(id);
        }

        public bool IsSeatTaken(int flightId, int seatNumber)
        {
            return _dataService.IsSeatTaken(flightId, seatNumber);
        }

        public void UpdateBooking(Booking booking)
        {
            _dataService.UpdateBooking(booking);
        }
    }
}