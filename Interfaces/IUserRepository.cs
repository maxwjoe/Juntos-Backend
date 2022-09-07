using Juntos.Models;

namespace Juntos.Interfaces
{
    public interface IUserRepository
    {

        // --- CRUD METHODS ---
        Task<User> Create(User user);
        Task<User> Update(User user, UserDto updates);
        Task<User> Delete(int userId);

        Task<bool> Save();


        // --- QUERY METHODS ---

        Task<List<User>> GetAll();
        Task<User> GetByIdAsync(int userId);
        Task<User> GetByEmailAsync(string userEmail);



    }
}