using Juntos.Data;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Juntos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClubController : ControllerBase
    {
        private readonly IClubRepository _clubRepository;

        public ClubController(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }


        [HttpGet]
        public async Task<ActionResult<List<Club>>> GetAllClubs()
        {
            var clubsDb = await _clubRepository.GetAll();
            return Ok(clubsDb);
        }

        [HttpGet]
        [Route("{ownerId}")]
        public async Task<ActionResult<Club>> GetClubByOwner([FromRoute] int ownerId)
        {
            var clubsDb = await _clubRepository.GetByOwnerAsync(ownerId);

            if (clubsDb == null)
            {
                return BadRequest("Could not find a club");
            }

            return Ok(clubsDb);
        }


        [HttpPost]
        public async Task<ActionResult<Club>> CreateClub(ClubDto request)
        {
            var newClub = new Club
            {
                Title = request.Title,
                Description = request.Description,
                OwnerId = request.OwnerId,
                ClubImageURL = request.ClubImageURL,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            Club createdClub = await _clubRepository.Add(newClub);

            if (createdClub == null)
            {
                return BadRequest("Failed to create club");
            }

            return Ok(createdClub);
        }


        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Club>> UpdateClub([FromBody] ClubDto updatedClub, [FromRoute] int id)
        {
            Club existingClub = await _clubRepository.GetByIdAsync(id);

            if (existingClub == null)
            {
                return BadRequest("Club not found");
            }

            Club result = await _clubRepository.Update(existingClub, updatedClub);
            return result;
        }


        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Club>> DeleteClub(int id)
        {
            Club deletedClub = await _clubRepository.Delete(id);
            return Ok(deletedClub);
        }

    }
}