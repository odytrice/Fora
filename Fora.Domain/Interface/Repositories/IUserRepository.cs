using Fora.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fora.Domain.Interface.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetUserByEmail(string userName);
        Task SetPassword(UserModel user, string passwordHash);
        Task<UserModel> UpdateUser(UserModel user);
        Task<UserModel> GetUserById(string userId);
        Task DeleteUser(string id);
        Task<UserModel> CreateUser(UserModel user);
        Task<string> GetPasswordHash(string email);
    }
}
