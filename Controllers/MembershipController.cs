using System.Security.Claims;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Juntos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {

        private readonly IMembershipRepository _membershipRepository;
        private readonly IAuthService _authService;
        public MembershipController(IMembershipRepository membershipRepository, IAuthService authService)
        {
            _membershipRepository = membershipRepository;
            _authService = authService;
        }


        // GetAllMemberships : Gets all the memberships in the database belonging to a club
        [HttpGet]
        [Route("{clubId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Membership>>> GetAllMemberships([FromRoute] int clubId)
        {
            var membershipsDb = await _membershipRepository.GetAll(clubId);

            if (membershipsDb == null)
            {
                return BadRequest("Could not get memberships");
            }

            return Ok(membershipsDb);
        }


        // CreateNewMembership : Creates a new membership in the database
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Membership>> CreateNewMembership(MembershipDto request)
        {
            if (request == null)
            {
                return BadRequest("Invalid Membership");
            }

            Membership newMembership = new Membership
            {
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                ClubId = request.ClubId,
                BillingFrequency = request.BillingFrequency
            };

            Membership createdMembership = await _membershipRepository.Create(newMembership);

            return Ok(createdMembership);
        }


        // UpdateExistingMembership : Updates an existing membership in Db
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{membershipId}")]
        public async Task<ActionResult<Membership>> UpdatedExistingMembership([FromBody] MembershipDto updates, [FromRoute] int membershipId)
        {
            Membership existingMembership = await _membershipRepository.GetByIdAsync(membershipId);

            if (existingMembership == null || updates == null)
            {
                return BadRequest("Invalid Params");
            }

            Membership updatedMembership = await _membershipRepository.Update(existingMembership, updates);

            return Ok(updatedMembership);
        }


        // DeleteExistingMembership : Deletes an existing membership from Db
        [HttpDelete]
        [Route("{membershipId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Membership>> DeleteExistingMembership([FromRoute] int membershipId)
        {
            Membership membershipToDelete = await _membershipRepository.GetByIdAsync(membershipId);

            if (membershipToDelete == null)
            {
                return BadRequest("Membership does not exist");
            }

            Membership deletedMembership = await _membershipRepository.Delete(membershipToDelete);
            return Ok(deletedMembership);
        }



    }
}