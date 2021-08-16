using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using api.Models;
using api.Models.Auth;


namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;


        public AuthController(UserManager<User> _userManager, SignInManager<User> _signInManager)
        {
            this.userManager = _userManager;
            this.signInManager = _signInManager;
        }

        [HttpPost("[action]")]
        public async Task<IdentityResult> CreateUser(RegisterModel registerModel)
        {
            var user = new User()
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
                FullName = registerModel.Name,
                University = registerModel.University,
                Study = registerModel.Study,
                Degree = registerModel.Degree
            };

            var result = await userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
            {
                //here we tie the new user to the role
                await userManager.AddToRoleAsync(user, "User");
            }
            return result;
        }

        [HttpPost("[action]")]
        public async Task<Microsoft.AspNetCore.Identity.SignInResult> SignIn(LoginModel loginModel)
        {
            var result = await signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, true, false);
            return result;
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "User")]
        public async Task SignOut()
        {
            await signInManager.SignOutAsync();
        }
    }
}