using Fora.Domain.Models;
using Fora.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Threading.Tasks;
using Fora.Domain.Interface.Repositories;

namespace Fora.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbContext _context;

        public UserRepository(DbContext context)
        {
            _context = context;
        }
        public async Task<UserModel> CreateUser(UserModel user)
        {
            var entity = await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == user.Email);
            if (entity != null) throw new Exception("Email already Exists");
            entity = user.ToEntity();
            _context.Set<User>().Add(entity);
            await _context.SaveChangesAsync();
            user.UserId = entity.UserId;
            return user;
        }

        public async Task DeleteUser(string id)
        {
            var entity = _context.Set<User>().Find(id);
            if (entity != null)
            {
                _context.Set<User>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public Task<string> GetPasswordHash(string email)
        {
            return _context.Set<User>()
                           .Select(u => u.PasswordHash)
                           .FirstOrDefaultAsync();
        }

        public async Task<UserModel> GetUserByEmail(string userName)
        {
            var user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == userName);
            return user.Map();
        }

        public async Task<UserModel> GetUserById(string userId)
        {
            var user = await _context.Set<User>().FindAsync(userId);
            return user.Map();
        }

        public async Task SetPassword(UserModel userModel, string passwordHash)
        {
            if (userModel == null) throw new ArgumentNullException("Invalid User Id");
            var user = await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == userModel.Email);
            if (user == null) throw new Exception("User not Found");
            user.PasswordHash = passwordHash;
            await _context.SaveChangesAsync();
        }

        public Task<UserModel> UpdateUser(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
