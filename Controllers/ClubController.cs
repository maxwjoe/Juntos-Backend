using System.Security.Claims;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juntos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {

        private readonly IClubRepository _clubRepository;
        private readonly IAuthService _authService;
        public ClubController(IClubRepository clubRepository, IAuthService authService)
        {
            _clubRepository = clubRepository;
            _authService = authService;
        }


        // GetAllClubs : Gets all the clubs in the database
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Club>>> GetAllClubs()
        {
            var clubsDb = await _clubRepository.GetAll();

            if (clubsDb == null)
            {
                return BadRequest("Could not get clubs");
            }

            return Ok(clubsDb);
        }

        //testAuth
        [HttpGet]
        [Route("test")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<User>> TestAuth()
        {
            User user = await _authService.GetUserObjFromToken();

            return Ok(user);
        }


        // CreateNewClub : Creates a new club in the database
        [HttpPost]
        public async Task<ActionResult<Club>> CreateNewClub(ClubDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Club");
            }

            Club newClub = new Club
            {
                OwnerId = request.OwnerId,
                Title = request.Title,
                Description = request.Description,
                ClubImageUrl = request.ClubImageUrl,
            };

            Club createdClub = await _clubRepository.Create(newClub);

            return Ok(createdClub);
        }


        // UpdateExistingClub : Updates an existing club in Db
        [HttpPut]
        [Route("{clubId}")]
        public async Task<ActionResult<Club>> UpdatedExistingClub([FromBody] ClubDto updates, [FromRoute] int clubId)
        {
            Club existingClub = await _clubRepository.GetByIdAsync(clubId);

            if (updates == null)
            {
                return Ok(existingClub);
            }

            if (existingClub == null)
            {
                return BadRequest("Club does not exist");
            }

            Club updatedClub = await _clubRepository.Update(existingClub, updates);

            return Ok(updatedClub);
        }


        // DeleteExistingClub : Deletes an existing club from Db
        [HttpDelete]
        [Route("{clubId}")]
        public async Task<ActionResult<Club>> DeleteExistingClub([FromRoute] int clubId)
        {
            Club deletedClub = await _clubRepository.Delete(clubId);
            return Ok(deletedClub);
        }



    }
}