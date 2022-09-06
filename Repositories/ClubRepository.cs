using Juntos.Data;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Juntos.Repositories
{
    public class ClubRepository : IClubRepository
    {

        private readonly DataContext _context;

        public ClubRepository(DataContext context)
        {
            _context = context;
        }


        // Add : Adds a Club to the DB
        public async Task<Club> Add(Club club)
        {
            await _context.Clubs.AddAsync(club);
            await Save();
            return club;
        }

        // Update : Updates a Club in the DB

        public async Task<Club> Update(Club club, ClubDto updates)
        {
            club.Title = updates.Title;
            club.Description = updates.Description;
            club.ClubImageURL = updates.ClubImageURL;
            club.OwnerId = updates.OwnerId;
            club.UpdatedAt = DateTime.Now;

            await Save();
            return club;
        }

        // Delete : Deletes a Club from the DB

        public async Task<Club> Delete(int Id)
        {
            Club tgtClub = await _context.Clubs.FindAsync(Id);

            if (tgtClub == null)
            {
                return new Club();
            }

            _context.Clubs.Remove(tgtClub);
            await Save();
            return tgtClub;
        }

        // Save : Saves DB Changes

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }

        // GetAll : Gets all clubs from DB    
        public async Task<IEnumerable<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync();
        }

        // GetByIdAsync : Gets Club by ID
        public async Task<Club> GetByIdAsync(int id)
        {
            return await _context.Clubs.FirstOrDefaultAsync(i => i.Id == id);
        }

        // GetByOwnerAsync : Gets Clubs from Owner Id
        public async Task<Club> GetByOwnerAsync(int ownerId)
        {
            return await _context.Clubs.FirstOrDefaultAsync(i => i.OwnerId == ownerId);
        }

    }
}