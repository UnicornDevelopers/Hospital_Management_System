﻿namespace Hospital_System.Models.DTOs.User
{
    public class RegisterUserDTO
    {

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IList<string> Roles { get; set; }


    }
}
