using System;

namespace CarAgency.Models
{
    public class Reservation
    {
        public CarID CarID { get; }
        public string Username { get; }
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }

        public TimeSpan Length => EndTime.Subtract(StartTime);

        public Reservation(CarID carID, string username, DateTime startTime, DateTime endTime)
        {
            CarID = carID;
            Username = username;
            StartTime = startTime;
            EndTime = endTime;
        }
    }
}
