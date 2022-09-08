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
            club.UpdatedAt = DateTime.Now;

            await Save();
            return club;
        }

        public async Task<Club> Delete(Club club)
        {
            if (club == null)
            {
                return new Club();
            }

            _context.Clubs.Remove(club);
            await Save();
            return club;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<List<Club>> GetAll(int userId)
        {
            return await _context.Clubs.Where(i => i.OwnerId == userId).ToListAsync();
        }

        public async Task<Club> GetByIdAsync(int clubId)
        {

            return await _context.Clubs.FindAsync(clubId);
        }




    }
}