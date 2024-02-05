﻿namespace WebApp.Models
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        public string[] Roles { get; set; }
        public string Token { get; internal set; }
    }
}
