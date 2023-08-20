using Hospital_System.Models.DTOs.User;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Hospital_System.Models.Interfaces
{
    public interface IUser
    {
        public Task<UserDTO> Register(RegisterUserDTO registerUserDTO, ModelStateDictionary modelState);

        public Task<UserDTO> RegisterDoctor(DoctorRegistrationDTO doctorRegistration, ModelStateDictionary modelState);

        public Task<UserDTO> RegisterNurse(RegisterNurseDTO nurseRegistration, ModelStateDictionary modelState);

        public Task<UserDTO> RegisterPatient(RegisterPatientDTO patientRegistration, ModelStateDictionary modelState);

        public Task<UserDTO> Authenticate(string username, string password);
        //Task<SignInResult> PasswordSignInAsync(LoginDTO signInModel);


        public Task<UserDTO> GetUser(ClaimsPrincipal principal);


    }
}
