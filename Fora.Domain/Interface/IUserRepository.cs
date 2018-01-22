using Fora.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fora.Domain.Interface
{
    public interface IUserRepository
    {
        UserModel GetUser(string userId);
    }
}
