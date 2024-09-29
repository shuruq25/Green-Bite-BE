using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update;
using src.Database;
using src.Entity;
using src.Utils;

namespace src.Repository
{
    public class UserRepository
    {
        protected DbSet<User> _user;
        protected DatabaseContext _databaseContext;

        public UserRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _user = databaseContext.Set<User>();
        }

        public async Task<User> CreateOneAsync(User newUser)
        {
            await _user.AddAsync(newUser);
            await _databaseContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<List<User>> GetAllAsync(PaginationOptions paginationOptions)
        {
            var result = _user.Where(u => u.Name.ToLower().Contains(paginationOptions.Search));
            return await result
                .Skip(paginationOptions.Offset)
                .Take(paginationOptions.Limit)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _user.FindAsync(id);
        }

        public async Task<bool> DeleteOneAsync(User user)
        {
            _user.Remove(user);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOneAsync(User updateUser)
        {
            _user.Update(updateUser);
            await _databaseContext.SaveChangesAsync();
            return true;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _user.FirstOrDefaultAsync(u => u.EmailAddress == email);
        }
    }
}
