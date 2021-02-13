using System.ComponentModel.DataAnnotations;

namespace UsersRoles.Models
{
    public class User
    {
        #region Properties
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        
        #endregion
    }
}
