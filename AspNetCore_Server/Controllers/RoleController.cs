using AspNetCore_Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore_Server.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<Applicationrole> _rolemanager;
        public RoleController(RoleManager<Applicationrole> rolemanager)
        {
            _rolemanager = rolemanager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(Applicationrole Role)
        {
            var exist = _rolemanager.FindByNameAsync(Role.Name);
            if (exist != null)
            {
                ModelState.AddModelError("", "there is a role with same specific registered!");
                return RedirectToAction("Index", "Role", Role);
            }
            await _rolemanager.CreateAsync(Role);
            return RedirectToAction("ViewRoles", "Role");
        }
        [HttpGet]
        public async Task<IActionResult> ViewRoles()
        {
            var Roles = _rolemanager.Roles;
            ViewData["Roles"] = Roles;
            return View();
        }
    }
}
