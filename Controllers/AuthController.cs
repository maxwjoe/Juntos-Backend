using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juntos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserController(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        // GetAllUsers : Gets all the users in the database related to a club
        [HttpGet]
        [Route("{clubId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<User>>> GetAllUsers([FromRoute] int clubId)
        {
            var usersDb = await _userRepository.GetAll(clubId);

            if (usersDb == null)
            {
                return BadRequest("Could not get users");
            }

            return Ok(usersDb);
        }


        // Register : Creates a new user in the database
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(UserDto request)
        {

            if (request == null)
            {
                return BadRequest("Invalid User Parameters");
            }

            AuthResponseDto registerResult = await _authService.RegisterUser(request);

            return Ok(registerResult);
        }

        // Login : Logs a user in
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(UserDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid User Parameters");
            }

            AuthResponseDto loginResult = await _authService.Login(request);
            return loginResult;
        }


        // UpdateExistingUser : Updates an existing user in Db
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{userId}")]
        public async Task<ActionResult<User>> UpdateExistingUser([FromBody] UserDto updates, [FromRoute] int userId)
        {
            User existingUser = await _userRepository.GetByIdAsync(userId);

            if (existingUser == null || updates == null)
            {
                return BadRequest("Could not modify user");
            }

            User updatedUser = await _userRepository.Update(existingUser, updates);

            return Ok(updatedUser);
        }


        // DeleteExistingUser : Deletes an existing user from Db
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{userId}")]
        public async Task<ActionResult<User>> DeleteExistingUser([FromRoute] int userId)
        {
            User deletedUser = await _userRepository.Delete(userId);
            return Ok(deletedUser);
        }

    }
}