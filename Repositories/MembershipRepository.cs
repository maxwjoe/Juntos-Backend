using Juntos.Data;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.EntityFrameworkCore;

namespace Juntos.Repositories
{
    public class MembershipRepository : IMembershipRepository
    {

        private readonly DataContext _context;
        public MembershipRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Membership> Create(Membership membership)
        {
            membership.CreatedAt = DateTime.Now;
            membership.UpdatedAt = DateTime.Now;

            await _context.Memberships.AddAsync(membership);
            await Save();
            return membership;

        }

        public async Task<Membership> Update(Membership membership, MembershipDto updates)
        {
            membership.Title = updates.Title;
            membership.Description = updates.Description;
            membership.Price = updates.Price;
            membership.ClubId = updates.ClubId;
            membership.BillingFrequency = updates.BillingFrequency;
            membership.UpdatedAt = DateTime.Now;

            await Save();
            return membership;
        }

        public async Task<Membership> Delete(Membership membership)
        {
            if (membership == null)
            {
                return new Membership();
            }

            _context.Memberships.Remove(membership);
            await Save();
            return membership;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<List<Membership>> GetAll(int clubId)
        {
            return await _context.Memberships.Where(i => i.ClubId == clubId).ToListAsync();
        }

        public async Task<Membership> GetByIdAsync(int membershipId)
        {
            return await _context.Memberships.FindAsync(membershipId);
        }

    }
}