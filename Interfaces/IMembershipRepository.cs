using Juntos.Models;

namespace Juntos.Interfaces
{
    public interface IMembershipRepository
    {

        // --- CRUD METHODS ---

        Task<Membership> Add(Membership membership);
        Task<Membership> Update(Membership club, MembershipDto updates);
        Task<Membership> Delete(int Id);
        Task<bool> Save();


        // --- COMPLEX METHODS ---

        Task<IEnumerable<Membership>> GetAll();
        Task<Membership> GetByIdAsync(int id);
        Task<bool> CheckExists(MembershipRef[] refs);
        Task<List<Membership>> GetByClubAsync(int associatedClubId);



    }
}