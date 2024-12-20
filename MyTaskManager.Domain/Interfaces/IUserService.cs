using MyTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Domain.Interfaces
{
    public interface IUserService
    {
        Task<bool> IsUsernameTakenAsync(string username);
        Task CreateUserAsync(string username, string password);
        Task<User> GetUserByUsernameAsync(string username);
    }
}
