using Juntos.Data;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Juntos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {

        private readonly IMembershipRepository _membershipRepository;

        public MembershipController(IMembershipRepository membershipRepository)
        {
            _membershipRepository = membershipRepository;
        }

        [HttpGet]
        public async Task<ActionResult<Membership>> GetAllMemberships()
        {
            var membershipsDb = await _membershipRepository.GetAll();
            return Ok(membershipsDb);
        }

        [HttpGet]
        [Route("{clubId}")]
        public async Task<ActionResult<List<Membership>>> GetMembershipsByClub([FromRoute] int clubId)
        {
            var membershipsDb = await _membershipRepository.GetByClubAsync(clubId);

            if (membershipsDb == null)
            {
                return BadRequest("No Memberships found");
            }

            return Ok(membershipsDb);
        }

        [HttpPost]
        public async Task<ActionResult<Membership>> CreateMembership([FromBody] MembershipDto request)
        {
            var newMembership = new Membership
            {
                Title = request.Title,
                Description = request.Description,
                AssociatedClub = request.AssociatedClub,
                BillingOption = request.BillingOption,
                Price = request.Price,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            Membership createdMembership = await _membershipRepository.Add(newMembership);

            if (createdMembership == null)
            {
                return BadRequest("Failed to create membership");
            }

            return Ok(createdMembership);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult<Membership>> UpdateMembership([FromBody] MembershipDto updates, [FromRoute] int id)
        {
            Membership existingMembership = await _membershipRepository.GetByIdAsync(id);

            if (existingMembership == null)
            {
                return BadRequest("Could not find membership");
            }

            Membership result = await _membershipRepository.Update(existingMembership, updates);
            return result;
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult<Membership>> DeleteMembership([FromRoute] int id)
        {
            Membership deletedMembership = await _membershipRepository.Delete(id);
            return deletedMembership;
        }

    }
}