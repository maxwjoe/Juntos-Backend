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

        //TODO: Delete this route
        // GetAllUsers : Gets all the users in the database
        [HttpGet]
        [Authorize(Roles = "Developer")]
        public async Task<ActionResult<List<User>>> GetAllUsers()
        {
            var usersDb = await _userRepository.GetAll();

            if (usersDb == null)
            {
                return BadRequest("Could not get users");
            }

            return Ok(usersDb);
        }


        // Register : Creates a new user in the database
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {

            if (request == null)
            {
                return BadRequest("Invalid User Parameters");
            }

            User createdUser = await _authService.RegisterUser(request);

            return Ok(createdUser);
        }

        // Login : Logs a user in
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login(UserDto request)
        {
            var response = await _authService.Login(request);

            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        //TODO: Beyond MVP -> Make sure only admin from a certain club can modify user
        // UpdateExistingUser : Updates an existing user in Db
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{userId}")]
        public async Task<ActionResult<User>> UpdatedExistingUser([FromBody] UserDto updates, [FromRoute] int userId)
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