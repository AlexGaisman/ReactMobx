using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracker.Models;

namespace Tracker.Interfaces
{
    public interface IUserRepository
    {
        User Create(User user);
        User GetByEmail(string email);

        User GetById(int id);
        User UpdateRole(int id, string role);
    }
}
