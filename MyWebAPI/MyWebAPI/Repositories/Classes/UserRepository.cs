using MyWebAPI.Models;
using MyWebAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebAPI.Repositories.Classes
{
    public class UserRepository : IUserRepository
    {
        #region Private Properties
        private readonly EmployeeDBContext _context;
        #endregion

        #region Constructor
        public UserRepository(EmployeeDBContext context)
        {
            _context = context;
        }
        #endregion

        #region Public Methods
        public async Task<User> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> Register(User user)
        {
            _context.Users.Add(user);
            user.UserId = await _context.SaveChangesAsync();

            return user;
        }
        #endregion
    }
}