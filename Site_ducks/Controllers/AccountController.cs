using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Site_ducks.ViewModels; // пространство имен моделей RegisterModel и LoginModel
using Site_ducks.Models; // пространство имен UserContext и класса User
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IO;
using Newtonsoft;
using Newtonsoft.Json;
namespace AuthApp.Controllers
{
    public class AccountController : Controller
    {
        
        [HttpGet]
        public IActionResult Login()
        {
            if (!HttpContext.Request.Cookies.Keys.Contains("User"))
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var textUsers = "";
                using (var stream = new StreamReader("Users.json"))
                {
                    textUsers = stream.ReadToEnd();
                }
                var Js = JsonConvert.DeserializeObject<List<User>>(textUsers);
                
                for (int i = 0; i < Js.Count; i++)
                {
                    if (model.Email == Js[i].Email && model.Password == Js[i].Password)
                    {
                        await Authenticate(model.Email); // аутентификация
                        HttpContext.Response.Cookies.Append("User", Js[i].Id.ToString());
                        string textCookies = "";
                        using (var stream = new StreamReader("Cookies.json"))
                        {
                            textCookies = stream.ReadToEnd();
                        }

                        var newCookie = new Cookie();
                        newCookie._Cookie = Js[i].Id.ToString();
                        newCookie._Id = Js[i].Id.ToString();

                        var CoockiesJson = JsonConvert.DeserializeObject<List<Cookie>>(textCookies);

                        CoockiesJson.Add(newCookie);

                        textCookies = JsonConvert.SerializeObject(CoockiesJson);

                        using (var stream = new StreamWriter("Cookies.json"))
                        {
                            stream.Write(textCookies);
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            if (!HttpContext.Request.Cookies.Keys.Contains("User"))
            {
                return View();
            }
            else
                return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                
                string textUser = "";
                using (var stream = new StreamReader("Users.json"))
                {
                    textUser = stream.ReadToEnd();
                }
                var usersJson = JsonConvert.DeserializeObject<List<User>>(textUser);
                for (int i = 0; i < usersJson.Count; i++)
                {
                    if (usersJson[i].Email == model.Email)
                    {
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                        return RedirectToAction("Login", "Account");
                    }

                }
                var newUser = new User();
                newUser.Email = model.Email;
                newUser.Password = model.Password;
                newUser.Id = usersJson[usersJson.Count - 1].Id + 1;
                newUser.Link = "/Home/ProfilePage/" + newUser.Id.ToString();
                newUser.Photo = "/images/kianu.jfif";
                newUser.Information = "No information";
                newUser.Department = "???";
                usersJson.Add(newUser);
                textUser = JsonConvert.SerializeObject(usersJson);
                using (var stream = new StreamWriter("Users.json"))
                {
                    stream.Write(textUser);
                }


                await Authenticate(model.Email); // аутентификация
                HttpContext.Response.Cookies.Append("User", newUser.Id.ToString());
                string textCookies = "";
                using (var stream = new StreamReader("Cookies.json"))
                {
                    textCookies = stream.ReadToEnd();
                }

                var newCookie = new Cookie();
                newCookie._Cookie = newUser.Id.ToString();
                newCookie._Id = newUser.Id.ToString();

                var CoockiesJson = JsonConvert.DeserializeObject<List<Cookie>>(textCookies);

                CoockiesJson.Add(newCookie);

                textCookies = JsonConvert.SerializeObject(CoockiesJson);

                using (var stream = new StreamWriter("Cookies.json"))
                {
                    stream.Write(textCookies);
                }

                return RedirectToAction("Index", "Home");
                
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            string textCookies = "";
            using (var stream = new StreamReader("Cookies.json"))
            {
                textCookies = stream.ReadToEnd();
            }
            var CoockiesJson = JsonConvert.DeserializeObject<List<Cookie>>(textCookies);
            for (int i = 0; i < CoockiesJson.Count; i++)
            {
                if (CoockiesJson[i]._Cookie == HttpContext.Request.Cookies["User"])
                {
                    CoockiesJson.RemoveAt(i);
                    //break;
                }
                    
            }
            HttpContext.Response.Cookies.Delete("User");
            return RedirectToAction("Login", "Account");
        }
    }
}