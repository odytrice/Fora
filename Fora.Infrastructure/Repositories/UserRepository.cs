using Fora.Domain.Interface;
using Fora.Domain.Models;
using Fora.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fora.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private DbContext _context;

        public UserRepository(DbContext context)
        {
            _context = context;
        }
        public UserModel GetUser(string userId)
        {
            var query = from user in _context.Set<User>()
                        where user.UserId == userId
                        select user;
            var entity = query.FirstOrDefault();

            if (entity == null) return null;

            return new UserModel().Assign(entity);
        }
    }
}
