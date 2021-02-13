using System.Collections.Generic;
using UsersRoles.Models;

namespace UsersRoles.Data.DTO
{
    public class UsersRolesDTO
    {
        #region Properties

        public List<User> Users { get; set; }
        public List<Role> Roles { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        #endregion

        #region Ctor
        public UsersRolesDTO(List<User> users, List<Role> roles)
        {
            this.Users = users;
            this.Roles = roles;
        }
        #endregion
    }
}
