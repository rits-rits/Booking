using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FlightBookingModels;

namespace FlightBookingDataService
{
    public class FlightJsonDataService : IFlightBookingDataService
    {
        private string flightsFile = $"{AppDomain.CurrentDomain.BaseDirectory}/Flights.json";
        private string bookingsFile = $"{AppDomain.CurrentDomain.BaseDirectory}/Bookings.json";

        private List<Flight> flights = new List<Flight>();
        private List<Booking> bookings = new List<Booking>();

        public FlightJsonDataService()
        {
            LoadFlights();
            LoadBookings();
            SeedFlights();
        }

        // ================== LOAD & SAVE ==================

        private void LoadFlights()
        {
            if (!File.Exists(flightsFile))
            {
                File.Create(flightsFile).Close();
                flights = new List<Flight>();
                return;
            }

            string json = File.ReadAllText(flightsFile);

            flights = string.IsNullOrWhiteSpace(json)
                ? new List<Flight>()
                : JsonSerializer.Deserialize<List<Flight>>(json) ?? new List<Flight>();
        }

        private void SaveFlights()
        {
            string json = JsonSerializer.Serialize(flights, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(flightsFile, json);
        }

        private void LoadBookings()
        {
            if (!File.Exists(bookingsFile))
            {
                File.Create(bookingsFile).Close();
                bookings = new List<Booking>();
                return;
            }

            string json = File.ReadAllText(bookingsFile);

            bookings = string.IsNullOrWhiteSpace(json)
                ? new List<Booking>()
                : JsonSerializer.Deserialize<List<Booking>>(json) ?? new List<Booking>();
        }

        private void SaveBookings()
        {
            string json = JsonSerializer.Serialize(bookings, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(bookingsFile, json);
        }

        // ================== SEED ==================

        private void SeedFlights()
        {
            if (flights.Count > 0) return;

            flights.AddRange(new List<Flight>
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

            });

            SaveFlights();
        }

        // ================== FLIGHTS ==================

        public List<Flight> GetFlights()
        {
            LoadFlights();
            return flights;
        }

        public Flight GetFlightById(int id)
        {
            LoadFlights();
            return flights.FirstOrDefault(f => f.FlightID == id);
        }

        // ================== BOOKINGS ==================

        public void AddBooking(Booking booking)
        {
            LoadBookings();
            bookings.Add(booking);
            SaveBookings();
        }

        public List<Booking> GetBookings()
        {
            LoadBookings();
            return bookings;
        }

        public Booking GetBookingById(int id)
        {
            LoadBookings();
            return bookings.FirstOrDefault(b => b.BookingID == id);
        }

        public void UpdateBooking(Booking updated)
        {
            LoadBookings();

            var existing = bookings.FirstOrDefault(b => b.BookingID == updated.BookingID);

            if (existing != null)
            {
                existing.Status = updated.Status;
            }

            SaveBookings();
        }

        public bool DeleteBooking(int id)
        {
            LoadBookings();

            var booking = bookings.FirstOrDefault(b => b.BookingID == id);
            if (booking == null) return false;

            booking.Status = "CANCELLED";
            SaveBookings();
            return true;
        }

        public bool IsSeatTaken(int flightId, int seatNumber)
        {
            LoadBookings();

            return bookings.Any(b =>
                b.FlightID == flightId &&
                b.SeatNumber == seatNumber &&
                b.Status != "CANCELLED");
        }
    }
}
