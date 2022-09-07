using Juntos.Interfaces;
using Juntos.Models;
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


        // GetAllUsers : Gets all the users in the database
        [HttpGet]
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


        // UpdateExistingUser : Updates an existing user in Db
        [HttpPut]
        [Route("{userId}")]
        public async Task<ActionResult<User>> UpdatedExistingUser([FromBody] UserDto updates, [FromRoute] int userId)
        {
            User existingUser = await _userRepository.GetByIdAsync(userId);

            if (updates == null)
            {
                return Ok(existingUser);
            }

            if (existingUser == null)
            {
                return BadRequest("User does not exist");
            }

            User updatedUser = await _userRepository.Update(existingUser, updates);

            return Ok(updatedUser);
        }


        // DeleteExistingUser : Deletes an existing user from Db
        [HttpDelete]
        [Route("{userId}")]
        public async Task<ActionResult<User>> DeleteExistingUser([FromRoute] int userId)
        {
            User deletedUser = await _userRepository.Delete(userId);
            return Ok(deletedUser);
        }



    }
}