using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using UsersRoles.Helpers;
using UsersRoles.Interface;
using UsersRoles.Models;

namespace UsersRoles.Data.Repositories
{
    public class UserRepository : IUser
    {
        #region Properties
        private readonly DataContext context;
        #endregion

        #region ctor
        public UserRepository(DataContext dataContext)
        {
            this.context = dataContext;
        }
        #endregion

        #region Public Methods

        public User Find(User user)
        {
            var encPass = RijndaelCryptographyUtilities.Encrypt(user.Password);
            return this.context.Users
                .FirstOrDefault(u => u.UserName == user.UserName && u.Password == encPass);
        }

        public User Register(User user)
        {
            throw new NotImplementedException();
        }

        public bool UserExists(string userName)
        {
            return this.context.Users.Any(x => x.UserName == userName.ToLower());
        }

        public void Insert(User user)
        {
            this.context.Add(user);
            this.context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            this.context.Entry(user).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public User GetUserById(int id)
        {
            return this.context.Users.FirstOrDefault(x => x.Id == id);
        }

        public List<User> GetUsers(int id)
        {
            return this.context.Users.Where(x => x.Id != id).ToList();
        }

        #endregion
    }
}
