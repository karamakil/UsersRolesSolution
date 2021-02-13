﻿using System.Collections.Generic;

namespace UsersRoles.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
