using Juntos.Data;
using Juntos.Interfaces;
using Juntos.Models;
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

        public async Task<Club> Create(Club club)
        {
            club.CreatedAt = DateTime.Now;
            club.UpdatedAt = DateTime.Now;

            await _context.Clubs.AddAsync(club);
            await Save();
            return club;

        }
        public async Task<Club> Update(Club club, ClubDto updates)
        {
            club.Title = updates.Title;
            club.Description = updates.Description;
            club.ClubImageUrl = updates.ClubImageUrl;
            club.OwnerId = updates.OwnerId;
            club.UpdatedAt = DateTime.Now;

            await Save();
            return club;
        }

        public async Task<Club> Delete(int clubId)
        {
            Club clubToDelete = await GetByIdAsync(clubId);

            if (clubToDelete == null)
            {
                return new Club();
            }

            _context.Clubs.Remove(clubToDelete);
            await Save();
            return clubToDelete;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<List<Club>> GetAll()
        {
            return await _context.Clubs.ToListAsync();
        }

        public async Task<Club> GetByIdAsync(int clubId)
        {
            return await _context.Clubs.FindAsync(clubId);
        }




    }
}