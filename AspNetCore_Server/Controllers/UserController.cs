using AspNetCore_Server.Models;
using Microsoft.AspNetCore.Identity;
using AspNetCore_Server.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCore_Server.Controllers
{
    public class UserController : Controller
    {

        #region DI
        //private readonly ILogger<ArticleController> _logger;
        private readonly RoleManager<Applicationrole> _rolemanager;
        private readonly UserManager<Applicationuser> _userManager;
        private readonly SignInManager<Applicationuser> _signInManager;
        private ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UserController(RoleManager<Applicationrole> rolemanager, /*ILogger<ArticleController> logger,*/ UserManager<Applicationuser> userManager, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, SignInManager<Applicationuser> signInManager)
        {
            _rolemanager = rolemanager;
            //_logger = logger;
            _userManager = userManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _signInManager = signInManager;
        }
        #endregion

        #region Registration
        [HttpGet]
        public IActionResult Register()
        {
            var Roles = _rolemanager.Roles;
            ViewData["Roles"] = Roles;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(ViewUser person)
        {
            ///Register for normperson
            var Find1 = await _userManager.FindByEmailAsync(person.email);
            var Find2 = await _userManager.FindByNameAsync(person.username);
            if (Find1 != null || Find2 != null)
            {
                ModelState.AddModelError("ExistanceError", "This email have been register here already");
                return View(person);
            }
            else if (person.Password != person.Passwordconfirmation)
            {
                ModelState.AddModelError("MatchError", "Passwords do not macth");
                return View(person);
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var UserModel = new Applicationuser()
                    {
                        UserName = person.username,
                        Email = person.email,
                        PhoneNumber = person.phonenumber
                    };
                    var result = await _userManager.CreateAsync(UserModel, person.Password);
                    //var roleresult = await _userManager.AddToRoleAsync(UserModel, "Visitor");
                    if (result.Succeeded)
                    {
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(UserModel);
                        var confirmationlink = Url.Action("EmailProvider", "confEmailSender",
                            new { userId = UserModel.Id, token = token }, Request.Scheme);
                        //_logger.Log(LogLevel.Warning, confirmationlink);
                        var user = new user();
                        var Uploader = new UploadFile(_webHostEnvironment);
                        var filename = Uploader.uploadfile(person.Image);
                        user.image = Uploader.Getfileurl(filename);
                        user.bio = person.bio;
                        user.FirstName = person.FirstName;
                        user.LastName = person.LastName;
                        user.userid = UserModel;
                        _context.Add(user);
                        _context.SaveChanges();
                        await _userManager.AddToRoleAsync(UserModel, person.Role);
                        await _signInManager.PasswordSignInAsync(UserModel, person.Password, isPersistent: true, lockoutOnFailure: false);
                        if (_signInManager.IsSignedIn(User) && User.IsInRole("administrator"))
                        {
                            return View("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("ConfirmEmail", "User", new { userid = UserModel.Id, token = token });
                        }
                    }
                    return View("Error");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
        }
        public async Task<IActionResult> ConfirmEmail(string userid, string token)
        {
            if (userid == null || token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                ViewBag.TitlrError = $"The User Id {userid} is invalid";
                return RedirectToRoute("NotFound");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View();
            }
            ViewBag.TitleError = "Email can not be confirmed";
            return View("Error");
        }
#endregion

        #region Login&Logout
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, isPersistent:true, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return RedirectToAction("Login", "User", model);
                }
                else
                {
                    ModelState.AddModelError
                        (string.Empty, @"this ceridential not registered yet 
                        to register click <a>here</a>");
                    return RedirectToAction("Index", "Home");
                }
            }
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region AccountManagement
        [Authorize]
        public async Task<IActionResult> Dashboard()
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {

                return RedirectToAction("Index", "Home");
            }
        }
        #endregion

    }
}

