using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Technova_ecom.Models.Entities;

namespace Technova_ecom.Controllers
{
    public class AuthController : Controller
    {
        private DatabaseContext _db;
        public AuthController(DatabaseContext db) {
            _db = db;
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                if(_db.Users.Any(u => u.Username == user.Username))
                {
                    ViewBag.ErrorMessage = "User Already Exists! Please try another username";
                    return View(user);
                }
                user.HashedPassword = BCrypt.Net.BCrypt.HashPassword(user.HashedPassword);
                _db.Users.Add(user);
                await _db.SaveChangesAsync();
                return RedirectToAction("Login", "Auth");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {
                if (_db.Users.Any(u => u.Username == user.Username))
                {
                    var loggedInUser = await _db.Users.FirstOrDefaultAsync(u => u.Username.Equals(user.Username));

                    if(BCrypt.Net.BCrypt.Verify(user.HashedPassword, loggedInUser.HashedPassword))
                    {
                        return RedirectToAction("Index", "Home");

                    }
                    else
                        ViewBag.ErrorMessage = "Incorrect Password";    
                }
                else
                {
                    ViewBag.ErrorMessage = "Incorrect Username";
                    return View(user);
                }
                
            }

            return View(user);
        }
    }
}
