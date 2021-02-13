using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using UsersRoles.Data.DTO;
using UsersRoles.Interface;
using UsersRoles.Models;

namespace UsersRoles.Controllers
{
    public class HomeController : BaseController
    {
        #region Properties
        private readonly IUser Iuser;
        private readonly IRole Irole;

        #endregion


        #region Ctor
        public HomeController( IUser iuser, IRole role)
        {
            this.Iuser = iuser;
            this.Irole = role;
        }
        #endregion


        #region Admin Methods

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            try
            {
                var users = this.Iuser.GetUsers(base.UserId);
                var roles = this.Irole.GetRoles();
                var usersRolesDTO = new UsersRolesDTO(users, roles);
                return View(usersRolesDTO);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        [HttpPost]
        public IActionResult UpdateUser([FromForm(Name = "UserId")] int? userId, [FromForm(Name = "RoleId")] int? roleId)
        {
            try
            {
                if (userId != null && roleId != null)
                {
                    var user = this.Iuser.GetUserById(userId.Value);
                    user.RoleId = roleId;
                    this.Iuser.UpdateUser(user);
                    return Ok(new { result = "Successs" });
                }
                return BadRequest("Check validations");
            }
            catch (Exception)
            {
                return BadRequest("Error occurred");
                throw;
            }
        }

        #endregion


        #region User

        [Authorize(Roles = "User")]
        public IActionResult UserHome()
        {
            try
            {
                var user = this.Iuser.GetUserById(base.UserId);
                return View("../Account/Register", user);
            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
           
        }

        #endregion


        #region Public Methods
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

    }
}
