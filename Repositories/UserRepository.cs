using Juntos.Data;
using Juntos.Interfaces;
using Juntos.Models;
using Microsoft.EntityFrameworkCore;

namespace Juntos.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User user)
        {
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;

            await _context.Users.AddAsync(user);
            await Save();
            return user;

        }
        public async Task<User> Update(User user, UserDto updates)
        {
            user.UserName = updates.UserName;
            user.Email = updates.Email;
            user.Phone = updates.Phone;
            user.ProfileImageUrl = updates.ProfileImageUrl;
            user.UpdatedAt = DateTime.Now;

            await Save();
            return user;
        }

        public async Task<User> Delete(int userId)
        {
            User userToDelete = await GetByIdAsync(userId);

            if (userToDelete == null)
            {
                return new User();
            }

            _context.Users.Remove(userToDelete);
            await Save();
            return userToDelete;
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }




    }
}