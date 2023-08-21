using Hospital_System.Models.DTOs;
using Hospital_System.Models.DTOs.Appointment;

namespace Hospital_System.Models.Interfaces
{
    public interface IAppointment
    {
        // Creates a new appointment.
        Task<OutAppointmentDTO> CreateAppointment(InAppoinmentDTO Appointment);

        // Retrieves a list of all appointments.
        Task<List<OutAppointmentDTO>> GetAppointments();

        // Retrieves detailed information about a specific appointment by ID.
        Task<OutAppointmentDTO> GetAppointment(int AppointmentID);

        // Updates the details of an existing appointment.
        Task<InAppoinmentDTO> UpdateAppointment(int id, InAppoinmentDTO DoctorDTO);

        // Deletes an appointment by its ID.
        Task DeleteAppointment(int id);
    }
}
