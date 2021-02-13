using System.Collections.Generic;
using UsersRoles.Models;

namespace UsersRoles.Interface
{
    public interface IRole
    {
        public List<Role> GetRoles();
    }
}
