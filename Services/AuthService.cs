using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.IdentityModel.Tokens;

namespace Juntos.Services
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
        }

        // CreateToken : Generates a JWT for User
        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value
            ));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }


        // CreatePasswordHash : Hashes the password and stores hash and salt in parameters passed
        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {

            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        // VerifyPasswordhash : Verifies password matches the hashed password
        public bool VerifyPasswordHash(User user, string password)
        {

            using (var hmac = new HMACSHA512(user.PasswordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(user.PasswordHash);
            }

        }

        // RegisterUser : Handles registering a new user
        public async Task<User> RegisterUser(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Phone = request.Phone,
                Role = request.Role,
                ProfileImageUrl = request.ProfileImageUrl,
            };

            User createdUser = await _userRepository.Create(newUser);
            return createdUser;
        }

        // Login : Handles logging in
        public async Task<AuthResponseDto> Login(UserDto request)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser == null)
            {
                return new AuthResponseDto { Message = "User not found" };
            }

            if (!VerifyPasswordHash(existingUser, request.Password))
            {
                return new AuthResponseDto { Message = "Invalid Password" };
            }

            string token = CreateToken(existingUser);

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
            };
        }


    }
}