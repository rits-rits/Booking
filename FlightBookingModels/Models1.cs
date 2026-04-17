using System;

namespace FlightBookingModels
{
    public class Flight
    {
        public int FlightID { get; set; }
        public string? Route { get; set; }
    }

    public class Booking
    {
        public int BookingID { get; set; }
        public string? PassengerName { get; set; }
          public string? FlightRoute { get; set; }
        public int FlightID { get; set; }
        public int SeatNumber { get; set; }

        public string? PaymentMethod { get; set; }

        public string? Status { get; set; }
        // PENDING / CONFIRMED / CANCELLED

        public DateTime CreatedAt { get; set; }
    }
}