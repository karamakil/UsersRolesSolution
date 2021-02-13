using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRoles.Interface;
using UsersRoles.Models;

namespace UsersRoles.Data.Repositories
{
    public class RoleRepository : IRole
    {
        #region Properties
        private readonly DataContext context;
        #endregion

        #region Ctor
        public RoleRepository(DataContext context)
        {
            this.context = context;
        }
        #endregion

        public List<Role> GetRoles()
        {
            return this.context.Role.ToList();
        }
    }
}
