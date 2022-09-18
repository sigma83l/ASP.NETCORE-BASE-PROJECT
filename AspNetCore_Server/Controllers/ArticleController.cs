using AspNetCore_Server.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_Server.Controllers
{
    public class ArticleController : Controller
    {
        private readonly UserManager<Applicationuser> _usermanager;
        private readonly RoleManager<Applicationrole> _rolemanager;
        private readonly ApplicationDbContext _context;
        public ArticleController(UserManager<Applicationuser> usermanager, RoleManager<Applicationrole> rolemanager, ApplicationDbContext context)
        {
            _usermanager = usermanager;
            _rolemanager = rolemanager;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError(string.Empty, "you should be loged in first to add articles");
                return RedirectToAction("Index", "Home");
            }

            var user = await _usermanager.FindByNameAsync(User.Identity.Name);
            var IsInRole = await _usermanager.IsInRoleAsync(user, "writer");
            if (!IsInRole)
            {
                ModelState.AddModelError(string.Empty, "access denid");
                ModelState.AddModelError(string.Empty, "you dont have writers access");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(Article article)
        {
            //if (!ModelState.IsValid)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
                var user = await _usermanager.FindByNameAsync(User.Identity.Name);
                Article arti = new Article()
                {
                    title = article.title,
                    content = article.content,
                    text = article.text,
                    writer = user
                };
                _context.Add(arti);
                _context.SaveChanges();
                return RedirectToAction("Dashboard","User");
        //    }
                
        }

    }
}
