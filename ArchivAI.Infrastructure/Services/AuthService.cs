using ArchivAI.Application.DTOs;
using ArchivAI.Application.Interfaces;
using ArchivAI.Core.Entities;
using ArchivAI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace ArchivAI.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ArchivAIDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ArchivAIDbContext context , IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<AuthResponseDTO> LoginAsync(LoginDTO loginDTO)
        {
            var user = await _context.AppUsers.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDTO.Password , user.PasswordHash))
            {
                throw new Exception("Invalid Email or Password");
            }
            return GenerateToken(user);
        }

        public async Task<AuthResponseDTO> RegisterAsync(RegisterDTO registerDTO)
        {
            var exists = await _context.AppUsers.AnyAsync(u=>u.Email == registerDTO.Email);
            if (exists) {
                throw new Exception("User Already Exists");
            }
            var NewUser = new AppUser()
            {
                FullName = registerDTO.FullName,
                Email = registerDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password) 
            };
            _context.AppUsers.Add(NewUser);
            await _context.SaveChangesAsync();

            return GenerateToken(NewUser);
        }

        private AuthResponseDTO GenerateToken(AppUser newUser)
        {
            var key = new SymmetricSecurityKey(UTF8Encoding.UTF8.GetBytes(_configuration["JWTSettings:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(double.Parse(_configuration["JWTSettings:DurationInDays"]!));
            var claims = new[]
            {
                new System.Security.Claims.Claim("id", newUser.Id.ToString()),
                new System.Security.Claims.Claim("email", newUser.Email),
                new System.Security.Claims.Claim("fullName", newUser.FullName)
            };
            var token = new JwtSecurityToken(
                _configuration["JWTSettings:Issuer"],
                _configuration["JWTSettings:Audience"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new AuthResponseDTO
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                 ExpiresAt = expires,
                  Email = newUser.Email,
                  FullName = newUser.FullName,
            };
        }
    }
}
