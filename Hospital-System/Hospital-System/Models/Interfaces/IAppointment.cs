using Hospital_System.Models.DTOs;

namespace Hospital_System.Models.Interfaces
{
    public interface IAppointment
    {

        Task<OutAppointmentDTO> CreateAppointment(AppointmentDTO Appointment);

        // GET All
        Task<List<OutAppointmentDTO>> GetAppointments();

        // GET Hotel By Id

        Task<OutAppointmentDTO> GetAppointment(int AppointmentID);

        // Update
        Task<AppointmentDTO> UpdateAppointment(int id, AppointmentDTO DoctorDTO);

        // Delete 

        Task DeleteAppointment(int id);

    }
}