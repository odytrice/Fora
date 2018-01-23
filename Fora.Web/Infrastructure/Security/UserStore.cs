using Fora.Domain.Interface.Repositories;
using Fora.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fora.Web.Infrastructure.Security
{
    public class UserStore : IUserStore<UserModel>, IUserPasswordStore<UserModel>
    {
        private IUserRepository _repo;

        public UserStore(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IdentityResult> CreateAsync(UserModel user, CancellationToken cancellationToken)
        {
            var operation = await Operation.Run(async () =>
            {
                var newUser = await _repo.CreateUser(user);
                user.UserId = newUser.UserId;
            });

            return operation.ToIdentity();
        }

        public async Task<IdentityResult> DeleteAsync(UserModel user, CancellationToken cancellationToken)
        {
            var operation = await Operation.Run(async () =>
            {
                await _repo.DeleteUser(user.UserId);
            });
            return operation.ToIdentity();
        }

        public void Dispose()
        {
            // Do Nothing
        }

        public Task<UserModel> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _repo.GetUserById(userId);
        }

        public Task<UserModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _repo.GetUserByEmail(normalizedUserName);
        }

        public Task<string> GetNormalizedUserNameAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<string> GetPasswordHashAsync(UserModel user, CancellationToken cancellationToken)
        {
            return _repo.GetPasswordHash(user.Email);
        }

        public async Task<string> GetUserIdAsync(UserModel model, CancellationToken cancellationToken)
        {
            var user = await _repo.GetUserByEmail(model.Email);
            return user?.UserId;
        }

        public Task<string> GetUserNameAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> HasPasswordAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public Task SetNormalizedUserNameAsync(UserModel user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email = normalizedName);
        }

        public Task SetPasswordHashAsync(UserModel user, string passwordHash, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash = passwordHash);
        }

        public Task SetUserNameAsync(UserModel user, string userName, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        public async Task<IdentityResult> UpdateAsync(UserModel user, CancellationToken cancellationToken)
        {
            var operation = await Operation.Run(async () =>
            {
                return await _repo.UpdateUser(user);
            });

            return operation.ToIdentity();
        }
    }
}
