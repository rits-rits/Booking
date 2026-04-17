using Microsoft.Data.SqlClient;
using FlightBookingModels;
using System;
using System.Collections.Generic;

namespace FlightBookingDataService
{
    public class FlightDBDataService : IFlightBookingDataService
    {
        private string connectionString =
            "Data Source=localhost\\SQLEXPRESS;Initial Catalog=FlightBookingMgmt;Integrated Security=True;TrustServerCertificate=True;";

        private SqlConnection sqlConnection;

        public FlightDBDataService()
        {
            sqlConnection = new SqlConnection(connectionString);
            SeedFlights();
        }

        // ================== SEED ==================
        private void SeedFlights()
        {
            if (GetFlights().Count > 0) return;

            AddFlight(new Flight { FlightID = 1, Route = "Manila to Seoul" });
            AddFlight(new Flight { FlightID = 2, Route = "Manila to Tokyo" });
            AddFlight(new Flight { FlightID = 3, Route = "Manila to Bangkok" });
            AddFlight(new Flight { FlightID = 3, Route = "Manila to Ottawa" });
            AddFlight(new Flight { FlightID = 3, Route = "Manila to Bern" });
            AddFlight(new Flight { FlightID = 3, Route = "Manila to Beijing" });
            AddFlight(new Flight { FlightID = 3, Route = "Manila to Taipei" });
            AddFlight(new Flight { FlightID = 3, Route = "Manila to Rome" });
            AddFlight(new Flight { FlightID = 3, Route = "Manila to Washington" });
            AddFlight(new Flight { FlightID = 3, Route = "Manila to London" });
        }

        private void AddFlight(Flight flight)
        {
            string query = "INSERT INTO Flights VALUES (@id, @route)";

            var cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@id", flight.FlightID);
            cmd.Parameters.AddWithValue("@route", flight.Route);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        // ================== FLIGHTS ==================

        public List<Flight> GetFlights()
        {
            var flights = new List<Flight>();

            string query = "SELECT * FROM Flights";
            var cmd = new SqlCommand(query, sqlConnection);

            sqlConnection.Open();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                flights.Add(new Flight
                {
                    FlightID = Convert.ToInt32(reader["FlightID"]),
                    Route = reader["Route"].ToString()
                });
            }

            sqlConnection.Close();
            return flights;
        }

        public Flight GetFlightById(int id)
        {
            string query = "SELECT * FROM Flights WHERE FlightID = @id";

            var cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@id", id);

            sqlConnection.Open();
            var reader = cmd.ExecuteReader();

            Flight flight = null;

            if (reader.Read())
            {
                flight = new Flight
                {
                    FlightID = Convert.ToInt32(reader["FlightID"]),
                    Route = reader["Route"].ToString()
                };
            }

            sqlConnection.Close();
            return flight;
        }

        // ================== BOOKINGS ==================

        public void AddBooking(Booking booking)
        {
            string query = @"INSERT INTO Bookings (PassengerName, FlightID, SeatNumber, PaymentMethod, Status, CreatedAt) 
            VALUES (@name, @flightId, @seat, @payment, @status, @created)";

            var cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@id", booking.BookingID);
            cmd.Parameters.AddWithValue("@name", booking.PassengerName);
            cmd.Parameters.AddWithValue("@flightId", booking.FlightID);
            cmd.Parameters.AddWithValue("@seat", booking.SeatNumber);
            cmd.Parameters.AddWithValue("@payment", booking.PaymentMethod);
            cmd.Parameters.AddWithValue("@status", booking.Status);
            cmd.Parameters.AddWithValue("@created", booking.CreatedAt);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public List<Booking> GetBookings()
        {
            var bookings = new List<Booking>();

            string query = "SELECT * FROM Bookings";
            var cmd = new SqlCommand(query, sqlConnection);

            sqlConnection.Open();
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                bookings.Add(new Booking
                {
                    BookingID = Convert.ToInt32(reader["BookingID"]),
                    PassengerName = reader["PassengerName"].ToString(),
                    FlightID = Convert.ToInt32(reader["FlightID"]),
                    SeatNumber = Convert.ToInt32(reader["SeatNumber"]),
                    PaymentMethod = reader["PaymentMethod"].ToString(),
                    Status = reader["Status"].ToString(),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                });
            }

            sqlConnection.Close();
            return bookings;
        }

        public Booking GetBookingById(int id)
        {
            string query = "SELECT * FROM Bookings WHERE BookingID = @id";

            var cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@id", id);

            sqlConnection.Open();
            var reader = cmd.ExecuteReader();

            Booking booking = null;

            if (reader.Read())
            {
                booking = new Booking
                {
                    BookingID = Convert.ToInt32(reader["BookingID"]),
                    PassengerName = reader["PassengerName"].ToString(),
                    FlightID = Convert.ToInt32(reader["FlightID"]),
                    SeatNumber = Convert.ToInt32(reader["SeatNumber"]),
                    PaymentMethod = reader["PaymentMethod"].ToString(),
                    Status = reader["Status"].ToString(),
                    CreatedAt = Convert.ToDateTime(reader["CreatedAt"])
                };
            }

            sqlConnection.Close();
            return booking;
        }

        public bool DeleteBooking(int id)
        {
            string query = "UPDATE Bookings SET Status = 'CANCELLED' WHERE BookingID = @id";

            var cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@id", id);

            sqlConnection.Open();
            int rows = cmd.ExecuteNonQuery();
            sqlConnection.Close();

            return rows > 0;
        }

        public bool IsSeatTaken(int flightId, int seatNumber)
        {
            string query = @"SELECT COUNT(*) FROM Bookings 
                             WHERE FlightID = @flightId 
                             AND SeatNumber = @seat 
                             AND Status != 'CANCELLED'";

            var cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@flightId", flightId);
            cmd.Parameters.AddWithValue("@seat", seatNumber);

            sqlConnection.Open();
            int count = (int)cmd.ExecuteScalar();
            sqlConnection.Close();

            return count > 0;
        }

        public void UpdateBooking(Booking booking)
        {
            string query = @"UPDATE Bookings 
                             SET Status = @status 
                             WHERE BookingID = @id";

            var cmd = new SqlCommand(query, sqlConnection);
            cmd.Parameters.AddWithValue("@status", booking.Status);
            cmd.Parameters.AddWithValue("@id", booking.BookingID);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();
        }
    }
}
