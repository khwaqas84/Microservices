﻿namespace AuthService.Models
{
    public class SignUpModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNumber { get; set; }

    }
}
