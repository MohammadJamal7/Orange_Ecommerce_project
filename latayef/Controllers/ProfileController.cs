using Ecommerce_Project.ViewModels;
using latayef.Data;
using latayef.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Printing;

namespace Ecommerce_Project.Controllers
{
	public class ProfileController : Controller
	{

		private readonly ApplicationContext _context;
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		public ProfileController(ApplicationContext context, UserManager<User> userManager, SignInManager<User> signInManager)
		{
			_context = context;
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpPost]
		public async Task<IActionResult> UpdateInfo(ProfileViewModle model)
		{
			// Fetch the currently logged-in user
			User user = await _userManager.GetUserAsync(User);

			if (user == null)
			{
				return RedirectToAction("Login", "Account");
			}

			// Validate ModelState
			if (!ModelState.IsValid)
			{
                return RedirectToAction("Login", "Account");
            }

			// Update the user's personal information
			user.Name = model.Name;
			user.PhoneNumber = model.PhoneNumber;
			user.City = model.City;
			user.Address = model.Address;
			user.State = model.State;

			// Update email address if it has changed
			if (user.Email != model.Email)
			{
				var setEmailResult = await _userManager.SetEmailAsync(user, model.Email);
				if (!setEmailResult.Succeeded)
				{
					foreach (var error in setEmailResult.Errors)
					{
						ModelState.AddModelError("", error.Description);
					}
                    return RedirectToAction("Profile", "Pages");
                }
			}

			// Change password if provided
			//if (!string.IsNullOrEmpty(model.Pasword))
			//{
			//	var removePasswordResult = await _userManager.RemovePasswordAsync(user);
			//	if (removePasswordResult.Succeeded)
			//	{
			//		var addPasswordResult = await _userManager.AddPasswordAsync(user, model.Pasword);
			//		if (!addPasswordResult.Succeeded)
			//		{
			//			foreach (var error in addPasswordResult.Errors)
			//			{
			//				ModelState.AddModelError("", error.Description);
			//			}
			//			return RedirectToAction("Profile", "Pages");
			//		}
			//	}
			//	else
			//	{
			//		foreach (var error in removePasswordResult.Errors)
			//		{
			//			ModelState.AddModelError("", error.Description);
			//		}
			//		return  RedirectToAction("Profile", "Pages");
			//	}
			//}

			// Save changes to the user
			var updateResult = await _userManager.UpdateAsync(user);
			if (!updateResult.Succeeded)
			{
				foreach (var error in updateResult.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
				return  RedirectToAction("Profile", "Pages");
			}

			// Force update security stamp to ensure new claims are generated
			await _userManager.UpdateSecurityStampAsync(user);

			// Sign out the user to refresh authentication claims
			await _signInManager.SignOutAsync();

			// Re-sign in the user
			await _signInManager.SignInAsync(user, isPersistent: false);

			TempData["SuccessMessage"] = "Your information has been updated successfully.";
			return RedirectToAction("Profile", "Pages");
		}


	}
}
