using Fora.Domain.Models;
using Fora.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fora.Infrastructure
{
    public static class Extensions
    {
        public static User ToEntity(this UserModel user)
        {
            if (user == null) return null;
            return new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = user.UserId,
                PasswordHash = user.PasswordHash
            };
        }

        public static UserModel Map(this User userEntity)
        {
            if (userEntity == null) return null;
            return new UserModel
            {
                Email = userEntity.Email,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                UserId = userEntity.UserId,
            };
        }
    }
}
