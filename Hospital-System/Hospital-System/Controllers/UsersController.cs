using Hospital_System.Models.DTOs.User;
using Hospital_System.Models.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Hospital_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UsersController : ControllerBase
    {
        private IUser userService;

        public UsersController(IUser service)
        {
            userService = service;
        }
        [AllowAnonymous]
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("Register")]
        public async Task<ActionResult<UserDTO>> Register(RegisterUserDTO data)
        {
            var user = await userService.Register(data, this.ModelState);

            if (ModelState.IsValid)
            {
                return user;

            }

            return BadRequest(new ValidationProblemDetails(ModelState));

        }

        [AllowAnonymous]
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("DoctorRegister")]
        public async Task<ActionResult<UserDTO>> DoctorRegister(DoctorRegistrationDTO data)
        {
            var user = await userService.RegisterDoctor(data, this.ModelState);

            if (ModelState.IsValid)
            {

                return user;
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [AllowAnonymous]
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("NurseRegister")]
        public async Task<ActionResult<UserDTO>> NurseRegister(RegisterNurseDTO data)
        {
            var user = await userService.RegisterNurse(data, this.ModelState);

            if (ModelState.IsValid)
            {

                return user;
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [AllowAnonymous]
        [Authorize(Roles = "Admin,Receptionist")]
        [HttpPost("PatientRegister")]
        public async Task<ActionResult<UserDTO>> PatientRegister(RegisterPatientDTO data)
        {
            var user = await userService.RegisterPatient(data, this.ModelState);

            if (ModelState.IsValid)
            {

                return user;
            }

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO log)
        {

            var user = await userService.Authenticate(log.UserName, log.Password);

            if (user == null)
            {
                return Unauthorized();

            }
            return user;
        }


        [Authorize(Roles = "Admin,Doctor,Nurse,Patient")]
        [HttpGet("Profile")]
        public async Task<ActionResult<UserDTO>> Profile()
        {
            return await userService.GetUser(this.User); ;
        }

    }
    }
