﻿using AuthService.Database;
using AuthService.Database.Entities;
using AuthService.Models;
using AuthService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthService.Services.Implementation
{
    public class AuthserviceRepository : IAuthserviceRepository
    {
        AppDbContext _db;
        IConfiguration _config;
        public AuthserviceRepository(AppDbContext db,IConfiguration config)
        {
            _db = db;
            _config = config;
        }
        public bool CreateUser(SignUpModel user)
        {
            try
            {
                user.Password=BCrypt.Net.BCrypt.HashPassword(user.Password);
                User model = new User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    PhoneNumber = user.PhoneNumber
                };
                _db.Users.Add(model);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
           
        }
        private string GenerateJSONWebToken(UserModel user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                             new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                             new Claim(JwtRegisteredClaimNames.Email, user.Email),
                             new Claim("Roles", string.Join(",",user.Roles)),
                             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                             };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                                            _config["Jwt:Audience"],
                                            claims,
                                            expires: DateTime.UtcNow.AddMinutes(60), //token expiry minutes
                                            signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserModel ValidateUser(string Email, string password)
        {
            User user = _db.Users.Include(u => u.Roles).Where(x=>x.Email == Email).FirstOrDefault();
            if (user != null)
            {   
                bool isVerified=BCrypt.Net.BCrypt.Verify(password, user.Password);
                if (isVerified)
                {
                    UserModel model = new UserModel()
                    {
                        Name = user.Name,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        Id = user.Id,
                        Roles=user.Roles.Select(r=>r.Name).ToArray()
                    };
                    model.Token = GenerateJSONWebToken(model);
                    return model;
                }
            }
            return null;
        }
    }
}
