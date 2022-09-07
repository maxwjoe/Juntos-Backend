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
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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


        // CreateNewUser : Creates a new user in the database
        [HttpPost]
        public async Task<ActionResult<User>> CreateNewUser(UserDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid User");
            }

            User newUser = new User
            {
                UserName = request.UserName,
                Email = request.Email,
                Phone = request.Phone,
                Role = request.Role,
                ProfileImageUrl = request.ProfileImageUrl,
            };

            User createdUser = await _userRepository.Create(newUser);

            return Ok(createdUser);
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