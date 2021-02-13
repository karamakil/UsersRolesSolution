using System.Collections.Generic;
using System.Threading.Tasks;
using UsersRoles.Models;

namespace UsersRoles.Interface
{
    public interface IUser
    {
        public User Find(User user);
        public User Register(User user);
        public bool UserExists(string userName);
        public void Insert(User user);
        public void UpdateUser(User user);
        public User GetUserById(int id);
        public List<User> GetUsers(int id);
    }
}
