using MyWebAPI.Models;

namespace MyWebAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Register(User user);
        Task<User> GetByUsername(string username);
        Task<User> GetById(int id);
    }
}