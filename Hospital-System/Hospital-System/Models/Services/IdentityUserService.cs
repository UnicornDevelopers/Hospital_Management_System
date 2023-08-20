using Hospital_System.Data;
using Hospital_System.Models.DTOs.User;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hospital_System.Models.Services
{
    public class IdentityUserService : IUser
    {
        private readonly HospitalDbContext _context;
        private UserManager<ApplicationUser> userManager;
        private  SignInManager<ApplicationUser> _signInManager;
        private JwtTokenService tokenService;
        public IdentityUserService(HospitalDbContext context, UserManager<ApplicationUser> manager, JwtTokenService tokenService, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            userManager = manager;
            this.tokenService = tokenService;
            signInManager = _signInManager;

        }




        public async Task<UserDTO> Register(RegisterUserDTO data, ModelStateDictionary modelState)
        {
            //throw new NotImplementedException();
            var user = new ApplicationUser()
            {
                UserName = data.UserName,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber,

            };
            var result = await userManager.CreateAsync(user, data.Password);
            if (result.Succeeded)
            {
                // Becuase we are an actual user, let's add them to their role
                await userManager.AddToRolesAsync(user, data.Roles);
                return new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5)),
                };
            }
            // Put errors into modelState
            // Ternary:   var foo = conditionIsTrue ? goodthing : bad;
            foreach (var error in result.Errors)
            {
                var errorKey =
                  error.Code.Contains("Password") ? nameof(data.Password) :
                  error.Code.Contains("Email") ? nameof(data.Email) :
                  error.Code.Contains("UserName") ? nameof(data.UserName) :
                  "";
                modelState.AddModelError(errorKey, error.Description);
            }
            return null;
        }


        //public async Task<SignInResult> PasswordSignInAsync(LoginDTO signInModel)
        //{

        //    var result = await _signInManager.PasswordSignInAsync(signInModel.UserName, signInModel.Password, signInModel.RememberMe, false);
        //    return result;

        //}

        public async Task<UserDTO> RegisterDoctor(DoctorRegistrationDTO doctorRegistration, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser()
            {
                UserName = doctorRegistration.UserName,
                Email = doctorRegistration.Email,
                PhoneNumber = doctorRegistration.PhoneNumber,


            };


            var result = await userManager.CreateAsync(user, doctorRegistration.Password);

            foreach (var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(doctorRegistration.Password) :
                               error.Code.Contains("Email") ? nameof(doctorRegistration.Email) :
                               error.Code.Contains("Username") ? nameof(doctorRegistration.UserName) :
                               "";

                modelState.AddModelError(errorKey, error.Description);
            }

            if (result.Succeeded)
            {

                {
                    var newDoctor = new Doctor
                    {
                        UserId = user.Id, // Associate with user ID
                        FirstName = doctorRegistration.FirstName,
                        LastName = doctorRegistration.LastName,
                        ContactNumber = doctorRegistration.PhoneNumber,
                        DepartmentId = doctorRegistration.DepartmentId,
                        Gender = doctorRegistration.Gender,
                        Speciality = doctorRegistration.Speciality,
                       
                    };

                    _context.Doctors.Add(newDoctor);
                    await _context.SaveChangesAsync();

                }
            }
            await userManager.AddToRolesAsync(user, doctorRegistration.Roles);


            return new UserDTO()
            {
                Id = user.Id,
                UserName = user.UserName,
                Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5)),
                Roles = await userManager.GetRolesAsync(user),
            };



        }




        public async Task<UserDTO> RegisterNurse(RegisterNurseDTO nurseRegistration, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser()
            {
                UserName = nurseRegistration.UserName,
                Email = nurseRegistration.Email,
                PhoneNumber = nurseRegistration.PhoneNumber,
                //Roles = nurseRegistration.Roles

            };


            var result = await userManager.CreateAsync(user, nurseRegistration.Password);

            foreach (var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(nurseRegistration.Password) :
                               error.Code.Contains("Email") ? nameof(nurseRegistration.Email) :
                               error.Code.Contains("Username") ? nameof(nurseRegistration.UserName) :
                               "";

                modelState.AddModelError(errorKey, error.Description);
            }

            if (result.Succeeded)
            {

                var newNurse = new Nurse
                {
                    UserId = user.Id, // Associate with user ID
                    FirstName = nurseRegistration.FirstName,
                    LastName = nurseRegistration.LastName,
                    ContactNumber = nurseRegistration.PhoneNumber,
                    DepartmentId = nurseRegistration.DepartmentId,
                    Gender = nurseRegistration.Gender

                    // Other doctor properties
                };
                _context.Nurses.Add(newNurse);
                await _context.SaveChangesAsync();

            }

            await userManager.AddToRolesAsync(user, nurseRegistration.Roles);


            return new UserDTO()
            {
                Id = user.Id,
                UserName = user.UserName,
                Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5)),
                Roles = await userManager.GetRolesAsync(user),
            };



        }

        public async Task<UserDTO> RegisterPatient(RegisterPatientDTO patientRegistration, ModelStateDictionary modelState)
        {
            var user = new ApplicationUser()
            {
                UserName = patientRegistration.UserName,
                Email = patientRegistration.Email,
                PhoneNumber = patientRegistration.PhoneNumber,
                Roles = patientRegistration.Roles

            };


            var result = await userManager.CreateAsync(user, patientRegistration.Password);

            foreach (var error in result.Errors)
            {
                var errorKey = error.Code.Contains("Password") ? nameof(patientRegistration.Password) :
                               error.Code.Contains("Email") ? nameof(patientRegistration.Email) :
                               error.Code.Contains("Username") ? nameof(patientRegistration.UserName) :
                               "";

                modelState.AddModelError(errorKey, error.Description);
            }

            if (result.Succeeded)
            {

                var newPatient = new Patient
                {
                    UserId = user.Id, // Associate with user ID
                    FirstName = patientRegistration.FirstName,
                    LastName = patientRegistration.LastName,
                    ContactNumber = patientRegistration.PhoneNumber,
                    Gender = patientRegistration.Gender,
                    RoomId = patientRegistration.RoomId,
                    DoB = patientRegistration.DoB,
                    Address = patientRegistration.Address,
                    
                    // Other doctor properties
                };
                _context.Patients.Add(newPatient);
                await _context.SaveChangesAsync();

            }
            await userManager.AddToRolesAsync(user, patientRegistration.Roles);


            return new UserDTO()
            {
                Id = user.Id,
                UserName = user.UserName,
                Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5)),
                Roles = await userManager.GetRolesAsync(user),
            };
        }

        public async Task<UserDTO> Authenticate(string username, string password)
        {
            var user = await userManager.FindByNameAsync(username);


            if (await userManager.CheckPasswordAsync(user, password))
            {
                return new UserDTO
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5)),
                    Roles = await userManager.GetRolesAsync(user),

                };
            }

            return null;

        }



        public async Task<UserDTO> GetUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);

            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Token = await tokenService.GetToken(user, System.TimeSpan.FromMinutes(5)),
                Roles= await userManager.GetRolesAsync(user),
                
            };
        }

      

    }
}
