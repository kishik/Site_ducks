using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Site_ducks.Models;
using System.IO;
using Newtonsoft.Json;
using System.Web;
using Microsoft.AspNetCore.Authorization;
namespace Site_ducks.Controllers
{
    
    public class HomeController : Controller
    {



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
            return View();
        }

        public IActionResult ProfilePage()
        {
            return View();
        }
        [Authorize]
        public IActionResult Authorisation()
        {
            return Content(User.Identity.Name);
        }
        public IActionResult StudentsPage()
        {
            return View();
        }

        public IActionResult Departments()
        {
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
