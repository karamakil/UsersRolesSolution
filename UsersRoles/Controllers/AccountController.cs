using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using UsersRoles.Helpers;
using UsersRoles.Interface;
using UsersRoles.Models;

namespace UsersRoles.Controllers
{
    public class AccountController : BaseController
    {
        #region Properties

        private readonly IUser IUser;
        #endregion

        #region ctor
        public AccountController(IUser iuser)
        {
            this.IUser = iuser;
        }

        #endregion

        #region Login

        [AllowAnonymous]
        public IActionResult Login()
        {
            ViewBag.Result = string.Empty;
            return View();
        }

        private Task LoginClaims(User user)
        {
            var claims = new List<Claim> {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName, ClaimValueTypes.String),
                    new Claim(ClaimTypes.Sid, user.Id.ToString(), ClaimValueTypes.Integer64),
                    new Claim(ClaimTypes.Role, ((UserRoles)user.RoleId).ToString(), ClaimValueTypes.String),
                    new Claim(ClaimTypes.GroupSid, user.RoleId.ToString(), ClaimValueTypes.Integer32)
                };
            var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);
            return HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal,
                    new AuthenticationProperties
                    {
                        AllowRefresh = true
                    });
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var retVal = this.IUser.Find(user);
                    if (retVal == null)
                    {
                        ViewBag.Result = "Invalid Credentials";
                        return View();
                    }
                    if (retVal.RoleId == null)
                    {
                        ViewBag.Result = "Please wait until your role is updated by admin";
                        return View();
                    }
                    this.LoginClaims(retVal);
                    switch (retVal.RoleId)
                    {
                        case (int)UserRoles.User:
                            ViewBag.IsAuth = true;
                            return RedirectToAction(nameof(HomeController.UserHome), "Home");

                        case (int)UserRoles.Admin:
                            return RedirectToAction(nameof(HomeController.Index), "Home");

                        default:
                            return View();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
                throw;
            }
            return View();
        }

        #endregion

        #region Logout
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(AccountController.Login), "Account");
        }
        #endregion

        #region Register

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.UserName = user.UserName.ToLower();
                    user.Password = RijndaelCryptographyUtilities.Encrypt(user.Password);
                    if (!User.Identity.IsAuthenticated)
                    {
                        if (this.IUser.UserExists(user.UserName))
                        {
                            ViewBag.Result = "User already exists";
                            return View();
                        }
                        this.IUser.Insert(user);
                        TempData["IsRegistered"] = "Registration completed";
                        return RedirectToAction(nameof(AccountController.Login), "Account");
                    }
                    else
                    {
                        user.Id = base.UserId;
                        user.RoleId = base.RoleId;
                        this.IUser.UpdateUser(user);
                        ViewBag.Result = "Update is done successfully";
                        return View();
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
                throw;
            }
            return View();
        }

        #endregion

    }
}
