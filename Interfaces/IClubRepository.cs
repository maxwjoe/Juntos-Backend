using Juntos.Models;

namespace Juntos.Interfaces
{
    public interface IClubRepository
    {

        // --- CRUD METHODS ---

        Task<Club> Add(Club club);
        Task<Club> Update(Club club, ClubDto updates);
        Task<Club> Delete(int Id);
        Task<bool> Save();


        // --- COMPLEX METHODS ---

        Task<IEnumerable<Club>> GetAll();
        Task<Club> GetByIdAsync(int id);
        Task<Club> GetByOwnerAsync(int ownerId);


    }
}