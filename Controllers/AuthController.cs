using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

        public IActionResult Logout()
        {
       
            Response.Cookies.Delete("jwt_token");
            return RedirectToAction("Login", "Auth");
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
                        var token = GenerateToken(loggedInUser);
                        Response.Cookies.Append("jwt_token", token, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict
                        });
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

        private string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role ?? "Public")
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF32.GetBytes(
                    "class-work-5E"
                ));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                    issuer: "https://localhost:7084/",
                    audience: "https://localhost:7084/",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
