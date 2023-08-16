﻿using System.ComponentModel.DataAnnotations.Schema;
using Hospital_System.Models.DTOs.Department;

namespace Hospital_System.Models.DTOs
{
    public class RoomDTO
    {

        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public bool RoomAvailability { get; set; }
        public int NumberOfBeds { get; set; }
        public int? DepartmentId { get; set; }

        //Nav
        public List<PatientDTO>? Patients { get; set; }
        public DepartmentDTO? department { get; set; }

    }
}
