using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Juntos.Interfaces;
using Juntos.Models;
using Juntos.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace Juntos.Services
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IConfiguration configuration, IUserRepository userRepository, IHttpContextAccessor httpContextAccesor)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccesor;
        }

        // GetClaimsFromToken : Gets all User claims from token
        private Claim[] GetClaimsFromToken()
        {
            Claim[] claims = _httpContextAccessor.HttpContext.User.Claims.ToArray();
            return claims;
        }

        private string GetClaimValueFromType(Claim[] claims, string claimType)
        {
            for (int i = 0; i < claims.Length; i++)
            {
                if (claims[i].Type == claimType)
                {
                    return claims[i].Value;
                }
            }
            return string.Empty;
        }

        public string GetUserEmailFromToken()
        {
            Claim[] claims = GetClaimsFromToken();

            string email = GetClaimValueFromType(claims, ClaimValueTypes.Email);
            return email;
        }

        public async Task<User> GetUserObjFromToken()
        {
            string email = GetUserEmailFromToken();

            User user = await _userRepository.GetByEmailAsync(email);

            return user;
        }

        // CreateToken : Generates a JWT for User
        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
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
        public async Task<AuthResponseDto> RegisterUser(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Phone = request.Phone,
                Role = request.Role,
                ProfileImageUrl = request.ProfileImageUrl,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            User createdUser = await _userRepository.Create(newUser);

            if (createdUser == null)
            {
                return new AuthResponseDto { Message = "Failed to register" };
            }

            LoginDto loginParams = new LoginDto
            {
                Email = request.Email,
                Password = request.Password,
            };

            AuthResponseDto result = await Login(loginParams);

            return result;
        }

        // Login : Handles logging in
        public async Task<AuthResponseDto> Login(LoginDto request)
        {
            User existingUser = await _userRepository.GetByEmailAsync(request.Email);

            if (existingUser == null)
            {
                return new AuthResponseDto { Message = "User not found" };
            }

            if (!VerifyPasswordHash(existingUser, request.Password))
            {
                return new AuthResponseDto { Message = "Invalid Password" };
            }

            string token = CreateToken(existingUser);

            UserDto userPayload = new UserDto
            {
                ClubId = existingUser.ClubId,
                MembershipId = existingUser.MembershipId,
                UserName = existingUser.UserName,
                Email = existingUser.Email,
                Phone = existingUser.Phone,
                Role = existingUser.Role,
                ProfileImageUrl = existingUser.ProfileImageUrl,
            };

            return new AuthResponseDto
            {
                Success = true,
                Token = token,
                currentUser = userPayload
            };
        }
    }
}