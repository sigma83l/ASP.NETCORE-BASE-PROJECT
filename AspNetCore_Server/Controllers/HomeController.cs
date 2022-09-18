using AspNetCore_Server.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Data.SqlClient;

namespace AspNetCore_Server.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Applicationuser> _usermanager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<Applicationuser> usermanager)
        {
            _logger = logger;
            _context = context;
            _usermanager = usermanager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var time = DateTime.Now.Hour;
                var user = await _usermanager.FindByNameAsync(User.Identity.Name);
                var res = await _usermanager.IsInRoleAsync(user, "writer");
                var cred = from i in _context.SuperUsers where (i.userid == user) select i;
                var reader = from i in _context.Articles where ( 1 == 1) select i;
                if (res)
                {
                    var Articles = from i in _context.Articles where (i.writer == user) select i;
                    ViewData["articles"] = Articles;
                    ViewData["cred"] = cred;
                    ViewBag.reader = reader;
                }
            }
            return View();
        }
        public async Task<IActionResult> search(string title)
        {
            Regex re = new Regex($"[{title}]");
            var searchresult = from i in _context.Articles where (re.IsMatch(i.title)) select i;
            ViewData["searchart"] = searchresult;
            return RedirectToAction("Index", "Home", new {q=title});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}