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


        // GetAllClubs : Gets all the clubs in the database belonging to user
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Club>>> GetAllClubs()
        {
            User reqUser = await _authService.GetUserObjFromToken();

            if (reqUser == null)
            {
                return Unauthorized("You do not have the correct credentials");
            }

            var clubsDb = await _clubRepository.GetAll(reqUser.Id);

            if (clubsDb == null)
            {
                return BadRequest("Could not get clubs");
            }

            return Ok(clubsDb);
        }


        // CreateNewClub : Creates a new club in the database
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Club>> CreateNewClub(ClubDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Club");
            }

            User reqUser = await _authService.GetUserObjFromToken();

            if (reqUser == null)
            {
                return Unauthorized("You do not have the correct credentials");
            }

            Club newClub = new Club
            {
                OwnerId = reqUser.Id,
                Title = request.Title,
                Description = request.Description,
                ClubImageUrl = request.ClubImageUrl,
            };

            Club createdClub = await _clubRepository.Create(newClub);

            return Ok(createdClub);
        }


        // UpdateExistingClub : Updates an existing club in Db
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{clubId}")]
        public async Task<ActionResult<Club>> UpdatedExistingClub([FromBody] ClubDto updates, [FromRoute] int clubId)
        {
            Club existingClub = await _clubRepository.GetByIdAsync(clubId);

            if (existingClub == null || updates == null)
            {
                return BadRequest("Invalid Params");
            }

            Club updatedClub = await _clubRepository.Update(existingClub, updates);

            return Ok(updatedClub);
        }


        // DeleteExistingClub : Deletes an existing club from Db
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{clubId}")]
        public async Task<ActionResult<Club>> DeleteExistingClub([FromRoute] int clubId)
        {
            Club clubToDelete = await _clubRepository.GetByIdAsync(clubId);

            if (clubToDelete == null)
            {
                return BadRequest("Failed to delete club");
            }

            Club deletedClub = await _clubRepository.Delete(clubToDelete);
            return Ok(deletedClub);
        }



    }
}