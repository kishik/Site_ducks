using System;
using System.Collections.Generic;
using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Site_ducks.Models;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.IO;
namespace Site_ducks.Controllers
{
    
    public class HomeController : Controller
    {

        [HttpGet]
        public ActionResult<string> TakeMyInform()
        {
            string text = "";
            using (StreamReader sr = new StreamReader("Users.json"))
            {
                text = sr.ReadToEnd();
            }
            var listUsers = JsonConvert.DeserializeObject<List<User>>(text);
            var key = HttpContext.Request.Cookies["User"];
            for (int i = 0; i < listUsers.Count; i++)
            {
                if (listUsers[i].Id.ToString() == key)
                {
                    return JsonConvert.SerializeObject(listUsers[i]);   
                }
            }
            return "HAHA";
        }

        [HttpGet]
        public ActionResult<string> getJSON()
        {
            using (StreamReader sr = new StreamReader("wwwroot/json/pictures.json", System.Text.Encoding.Default))
            {
                string fileData;
                fileData = sr.ReadToEnd();
                return fileData;
            }
        }





        public IActionResult Index()
        {

            if (HttpContext.Request.Cookies.Keys.Contains("User"))
            {
                string text = "";
                using (var stream = new StreamReader("Cookies.json"))
                {
                    text = stream.ReadToEnd();
                }
                var LisCook = JsonConvert.DeserializeObject<List<Cookie>>(text);
                for (int i = 0; i < LisCook.Count; i++)
                {
                    if (LisCook[i]._Cookie == HttpContext.Request.Cookies["User"])
                    {/*
                        string textUser = "";
                        using (var stream = new StreamReader("Users.json"))
                        {
                            textUser = stream.ReadToEnd();
                        }
                        var lisUser = JsonConvert.DeserializeObject<List<User>>(textUser);
                        for (int j = 0; j < lisUser.Count; j++)
                        {
                            if (HttpContext.Request.Path.ToString() == lisUser[j].Link)

                                return View(lisUser[j]);
                        }
                        */
                        return View();
                    }
                }
            }
            else
                return RedirectToAction("Login", "Account");
            return View();
        }

        public IActionResult ProfilePage()
        {

            if (HttpContext.Request.Cookies.Keys.Contains("User"))
            {
                string text = "";
                using (var stream = new StreamReader("Cookies.json"))
                {
                    text = stream.ReadToEnd();
                }
                var LisCook = JsonConvert.DeserializeObject<List<Cookie>>(text);
                for (int i = 0; i < LisCook.Count; i++)
                {
                    var aaa = HttpContext.Request.Cookies["User"];
                    if (LisCook[i]._Cookie == HttpContext.Request.Cookies["User"])
                    {
                        string textUser = "";
                        using (var stream = new StreamReader("Users.json"))
                        {
                            textUser = stream.ReadToEnd();
                        }
                        var lisUser = JsonConvert.DeserializeObject<List<User>>(textUser);
                        for (int j = 0; j < lisUser.Count; j++)
                        {
                            if (HttpContext.Request.Path.ToString().ToUpper() == lisUser[j].Link.ToUpper())

                                return View(lisUser[j]);
                        }

                        return View("ERROR");
                    }
                }
                return RedirectToAction("Login", "Account");
            }
            else
                return RedirectToAction("Login", "Account");
            
        }

        
        public IActionResult StudentsPage()
        {
            if (HttpContext.Request.Cookies.Keys.Contains("User"))
            {
                string text = "";
                using (var stream = new StreamReader("Cookies.json"))
                {
                    text = stream.ReadToEnd();
                }
                var LisCook = JsonConvert.DeserializeObject<List<Cookie>>(text);
                for (int i = 0; i < LisCook.Count; i++)
                {
                    if (LisCook[i]._Cookie == HttpContext.Request.Cookies["User"])
                    {
                        return View();
                    }
                }
            }
            else
                return RedirectToAction("Login", "Account");
            return View();
        }

        public IActionResult Departments()
        {
            if (HttpContext.Request.Cookies.Keys.Contains("User"))
            {
                string text = "";
                using (var stream = new StreamReader("Cookies.json"))
                {
                    text = stream.ReadToEnd();
                }
                var LisCook = JsonConvert.DeserializeObject<List<Cookie>>(text);
                for (int i = 0; i < LisCook.Count; i++)
                {
                    if (LisCook[i]._Cookie == HttpContext.Request.Cookies["User"])
                    {
                        return View();
                    }
                }
            }
            else
                return RedirectToAction("Login", "Account");
            return View();
        }

        [HttpPost]
        public ActionResult<string> GetNews([FromBody] NumberNewsFromJS num)
        {
            string text = "";

            using (var read = new StreamReader("News.json"))
                text = read.ReadToEnd();
            var allNews = JsonConvert.DeserializeObject<List<NewsData>>(text);
            var newsForSend = new List<NewsData>();

            for (int i = num.Number; i < Math.Min(num.Number + 7, allNews.Count); i++)
            {
                newsForSend.Add(allNews[i]);
            }
            text = JsonConvert.SerializeObject(newsForSend);
            return text;
        }








        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
