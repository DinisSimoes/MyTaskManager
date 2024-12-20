using MyTaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManager.Domain.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
