using Juntos.Models;

namespace Juntos.Interfaces
{
    public interface IMembershipRepository
    {
        // --- CRUD METHODS ---
        Task<Membership> Create(Membership membership);
        Task<Membership> Update(Membership membership, MembershipDto updates);
        Task<Membership> Delete(Membership membership);

        Task<bool> Save();


        // --- QUERY METHODS ---

        Task<List<Membership>> GetAll(int clubId);
        Task<Membership> GetByIdAsync(int membershipId);
    }
}