using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace UsersRoles.Controllers
{
    public class BaseController : Controller
    {
        #region Properties
        public int UserId { get { return Convert.ToInt32(User.Claims.First(t => t.Type == ClaimTypes.Sid).Value); } }
        public int RoleId { get { return Convert.ToInt32(User.Claims.First(t => t.Type == ClaimTypes.GroupSid).Value); } }

        #endregion

    }
}
