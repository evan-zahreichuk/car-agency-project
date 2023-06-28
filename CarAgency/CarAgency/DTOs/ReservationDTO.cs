using System;
using System.ComponentModel.DataAnnotations;

namespace CarAgency.DTOs
{
    public class ReservationDTO
    {
        [Key]
        public Guid Id { get; set; }
        public int CarNumber { get; set; }
        public string Username { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
