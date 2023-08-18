using Hospital_System.Models.DTOs.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Hospital_System.Models.Interfaces
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterUserDTO registerUserDTO, ModelStateDictionary modelState);

        public Task<UserDTO> RegisterDoctor(DoctorRegistrationDTO doctorRegistration, ModelStateDictionary modelState);

        public Task<UserDTO> RegisterNurse(RegisterNurseDTO nurseRegistration, ModelStateDictionary modelState);

        public Task<UserDTO> RegisterPatient(RegisterPatientDTO patientRegistration, ModelStateDictionary modelState);

        public Task<UserDTO> Authenticate(string username, string password);


        public Task<UserDTO> GetUser(ClaimsPrincipal principal);


    }
}
