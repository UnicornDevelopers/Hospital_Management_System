using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Appointment;

namespace Hospital_System.Models.Interfaces
{
    public interface IAppointment
    {

        Task<OutAppointmentDTO> CreateAppointment(InAppoinmentDTO Appointment);

        // GET All
        Task<List<OutAppointmentDTO>> GetAppointments();

        // GET Hotel By Id

        Task<OutAppointmentDTO> GetAppointment(int AppointmentID);

        // Update
        Task<InAppoinmentDTO> UpdateAppointment(int id, InAppoinmentDTO DoctorDTO);

        // Delete 

        Task DeleteAppointment(int id);

    }
}