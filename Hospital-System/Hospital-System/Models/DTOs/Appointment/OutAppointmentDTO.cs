﻿namespace Hospital_System.Models.DTOs
{
    public class OutAppointmentDTO
    {
        public int Id { get; set; }
        public DateTime DateOfAppointment { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string DepartmentName { get; set; }

    }
}