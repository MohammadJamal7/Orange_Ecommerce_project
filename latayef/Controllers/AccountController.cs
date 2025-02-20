using Ecommerce_Project.ViewModels;
using latayef.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce_Project.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager; // Manages user-related actions
        private readonly SignInManager<User> _signInManager; // Manages sign-in and sign-out actions
        private readonly RoleManager<IdentityRole> _roleManager; // Manages roles

        // Constructor injection of UserManager, SignInManager, and RoleManager
        public AccountController(UserManager<User> userManager,
                                 SignInManager<User> signInManager,
                                 RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                // Check if a user with the same email already exists
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "This email address is already in use.");
                    return View(model); // Return the view with validation error
                }

                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    PhoneNumber = model.Phone,
                    State = model.State,
                    City = model.City,
                    Address = model.Adress,

                };

                // Attempt to create the user with the provided password
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Customer"); // Assign default role
                    await _signInManager.SignInAsync(user, isPersistent: false); // Sign in the user
                    return RedirectToAction("Index", "Pages"); // Redirect to main page on success
                }

                // Add errors to ModelState if user creation failed
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                // Handle model validation errors
                ModelState.AddModelError(string.Empty, "Please correct the form errors and try again.");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModelView model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to sign in with the provided credentials
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Pages"); // Redirect on successful login
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Your account is locked. Please try again later.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt. Please check your email and password.");
                }
            }
            else
            {
                // Display error if model validation fails
                ModelState.AddModelError(string.Empty, "Please enter valid credentials.");
            }

            return View(model); // Redisplay form with errors if login fails
        }
    }
}
