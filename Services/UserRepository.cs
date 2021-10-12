using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tracker.Interfaces;
using Tracker.Models;

namespace Tracker.Services
{
    public class UserRepository : IUserRepository
    {

        private readonly TrackerContext _context;
        public UserRepository(TrackerContext context)
        {
            _context = context;
        }

        public User Create(User user)
        {
            _context.Add<User>(user);
            _context.SaveChanges();

            return user;
        }

        public User GetByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(e => e.Email == email);

            return user;
        }


        public User GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(e => e.Id == id);

            return user;
        }
    }
}
