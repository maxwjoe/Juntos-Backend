using Juntos.Data;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.AspNetCore.Mvc;
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


        // Add : Adds a Club to the DB
        public async Task<Membership> Add(Membership membership)
        {
            await _context.Memberships.AddAsync(membership);
            await Save();
            return membership;
        }

        // Update : Updates a Club in the DB

        public async Task<Membership> Update(Membership membership, MembershipDto updates)
        {
            membership.Title = updates.Title;
            membership.Description = updates.Description;
            membership.AssociatedClub = updates.AssociatedClub;
            membership.BillingOption = updates.BillingOption;
            membership.Price = updates.Price;
            membership.UpdatedAt = DateTime.Now;

            await Save();
            return membership;
        }

        // Delete : Deletes a Club from the DB

        public async Task<Membership> Delete(int Id)
        {
            Membership tgtMembership = await _context.Memberships.FindAsync(Id);

            if (tgtMembership == null)
            {
                return new Membership();
            }

            _context.Memberships.Remove(tgtMembership);
            await Save();
            return tgtMembership;
        }

        // Save : Saves DB Changes

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        // GetAll : Gets all clubs from DB    
        public async Task<IEnumerable<Membership>> GetAll()
        {
            return await _context.Memberships.ToListAsync();
        }

        // GetByIdAsync : Gets Club by ID
        public async Task<Membership> GetByIdAsync(int id)
        {
            return await _context.Memberships.FirstOrDefaultAsync(i => i.Id == id);
        }

        // GetByOwnerAsync : Gets Clubs from Owner Id
        public async Task<List<Membership>> GetByClubAsync(int associatedClubId)
        {
            return await _context.Memberships.Where(i => i.AssociatedClub == associatedClubId).ToListAsync();
        }

    }
}