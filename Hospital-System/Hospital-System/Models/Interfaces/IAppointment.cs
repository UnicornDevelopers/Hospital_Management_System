using Hospital_System.Models.DTOs;

namespace Hospital_System.Models.Interfaces
{
    public interface IAppointment
    {

        Task<AppointmentDTO> CreateAppointment(AppointmentDTO Appointment);

        // GET All
        Task<List<AppointmentDTO>> GetAppointments();

        // GET Hotel By Id

        Task<AppointmentDTO> GetAppointment(int AppointmentID);

        // Update
        Task<AppointmentDTO> UpdateAppointment(int id, AppointmentDTO DoctorDTO);

        // Delete 

        Task DeleteAppointment(int id);

    }
}
