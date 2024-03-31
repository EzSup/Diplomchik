using Microsoft.Extensions.Configuration;
using static Restaurant.Core.DTOs.Responses.CustomResponses;
using Restaurant.Core.DTOs.SmallDtos;
using Restaurant.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restaurant.Core.Models;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Functions.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Data;

namespace Restaurant.Core.Repositories
{
    public class Account : IAccount
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly IPasswordHasher _passwordHasher;

        public Account(RestaurantDbContext dbContext, IConfiguration config, IPasswordHasher passwordHasher)
        {
            _dbContext = dbContext;
            _config = config;
            _passwordHasher = passwordHasher;
        }

        public async Task<RegistrationResponse> RegisterAsync(RegisterDto model)
        {
            var findUser = await GetUser(model.Email);
            if (findUser != null)
            {
                return new RegistrationResponse(false, "User already exist");
            }

            _dbContext.Users.Add(
                new User()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = _passwordHasher.HashPassword(model.Password)
                });

            await _dbContext.SaveChangesAsync();
            return new RegistrationResponse(true, "Success");
        }

        public async Task<LoginResponse> LoginAsync(LoginDto model)
        {
            var findUser = await GetUser(model.Email);
            if (findUser == null) return new LoginResponse(false, "User not found");

            if (!_passwordHasher.Verify(model.Password, findUser.Password))
            {
                return new LoginResponse(false, "Email/Password not valid");
            }

            string jwtToken = GenerateToken(findUser);
            return new LoginResponse(true, "Login successfully", jwtToken);
        }

        private async Task<User> GetUser(string email)
            => await _dbContext.Users.FirstOrDefaultAsync(e => e.Email == email);

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var userClaims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!)
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"]!,
                audience: _config["Jwt:Audience"]!,
                claims: userClaims,
                expires: DateTime.Now.AddHours(int.Parse(_config["Jwt:ExpiresHours"]!)),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
