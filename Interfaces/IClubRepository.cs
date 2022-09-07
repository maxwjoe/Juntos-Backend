using Juntos.Models;

namespace Juntos.Interfaces
{
    public interface IClubRepository
    {

        // --- CRUD METHODS ---
        Task<Club> Create(Club club);
        Task<Club> Update(Club club, ClubDto updates);
        Task<Club> Delete(int clubId);

        Task<bool> Save();


        // --- QUERY METHODS ---

        Task<List<Club>> GetAll();
        Task<Club> GetByIdAsync(int clubId);



    }
}