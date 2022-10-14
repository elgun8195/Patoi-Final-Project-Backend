using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Patoi_Final_Project_Backend.Models;
using Patoi_Final_Project_Backend.ViewModels; 
using System.Threading.Tasks;
using System;
using Patoi_Final_Project_Backend.Helpers;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Microsoft.EntityFrameworkCore;
using Patoi_Final_Project_Backend.DAL;
using System.Linq;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using Patoi_Final_Project_Backend.Extensions;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;
using System.Net;

namespace Patoi_Final_Project_Backend.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager; 
        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        //123456Aa!
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser appUser = new AppUser();
            appUser.Fullname = registerVM.Fullname;
            appUser.UserName=registerVM.Username;
            appUser.Email = registerVM.Email;
            IdentityResult result = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(registerVM);
            }

            await _userManager.AddToRoleAsync(appUser, Roless.Admin.ToString());
            await _signInManager.SignInAsync(appUser, true);
            return RedirectToAction("index", "home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM, string returnurl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            AppUser dbUser = await _userManager.FindByEmailAsync(loginVM.Email);
            if (dbUser == null)
            {
                ModelState.AddModelError("", "Ya email ya da Password sehvdir");
                return View(loginVM);
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(dbUser, loginVM.Password, loginVM.RememerMe, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Your account is blocked because you write wrong password or username.You try after 5 minutes");
                    return View();
                }
                ModelState.AddModelError("", "Username or password is incorrect");
                return View();

            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your Account Is Lock Out");
                return View(loginVM);
            }

            //if (!result.Succeeded)
            //{
            //    ModelState.AddModelError("", "Ya Email ya da Password sehvdir");
            //    return View(loginVM);
            //}

            if (returnurl == null)
            {
                return RedirectToAction("index", "home");
            }
            foreach (var item in await _userManager.GetRolesAsync(dbUser))
            {
                if (item.Contains(Roless.Admin.ToString()))
                {
                    return RedirectToAction("index", "Dashboard", new { area = "Admin" });
                }
            }
            return Redirect(returnurl);

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
        public async Task CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(Roless)))
            {
                if (!await _roleManager.RoleExistsAsync(item.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
                }
            }
        }

        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest();
            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, true);
            TempData["Verified"] = true;

            return RedirectToAction("Index", "Home");
        }


        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(AccountVM account)
        {
            AppUser user = await _userManager.FindByEmailAsync(account.AppUser.Email);
            if (user == null) return BadRequest();

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token }, Request.Scheme, Request.Host.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("tu741rmps@code.edu.az", "BookShop");
            mail.To.Add(new MailAddress(user.Email));

            mail.Subject = "Reset Password";
            mail.Body = $"<a href='{link}'>Please click here to reset your password</a>";
            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.Credentials = new NetworkCredential("tu741rmps@code.edu.az", "elgun689a3");
            smtp.Send(mail);
            return RedirectToAction("index", "home");
        }




        public async Task<IActionResult> ResetPassword(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null) return BadRequest();
            AccountVM model = new AccountVM
            {
                AppUser = user,
                Token = token
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(AccountVM account)
        {
            AppUser user = await _userManager.FindByEmailAsync(account.AppUser.Email);
            AccountVM model = new AccountVM
            {
                AppUser = user,
                Token = account.Token
            };
            if (!ModelState.IsValid) return View(model);
            IdentityResult result = await _userManager.ResetPasswordAsync(user, account.Token, account.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);

            }
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Edit()
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "account");

            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            UserEditVM editedUser = new UserEditVM
            {
                Username = user.UserName,
                Email = user.Email
            };

            return View(editedUser);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserEditVM editedUser)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "account");

            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditVM eUser = new UserEditVM
            {
                Username = user.UserName,
                Email = user.Email

            };

            if (user.UserName != editedUser.Username && await _userManager.FindByNameAsync(editedUser.Username) != null)
            {
                ModelState.AddModelError("", $"{editedUser.Username} has already taken");
                return View(eUser);
            }

            if (string.IsNullOrWhiteSpace(editedUser.CurrentPassword))
            {
                user.UserName = editedUser.Username;
                user.Email = editedUser.Email; 
                await _userManager.UpdateAsync(user);
                await _signInManager.SignInAsync(user, true);
            }
            else
            {
                user.UserName = editedUser.Username;
                user.Email = editedUser.Email; 
                IdentityResult result = await _userManager.ChangePasswordAsync(user, editedUser.CurrentPassword, editedUser.Password);

                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);

                    }
                    return View(eUser);
                }
                await _signInManager.PasswordSignInAsync(user, editedUser.Password, true, true);
            }


            return RedirectToAction("index", "home");

        }

    }
}
