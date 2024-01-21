using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Internshub.Models;

namespace Internshub.Controllers
{
	public class AccountsController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public AccountsController(UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		[HttpGet]
		public IActionResult Signup()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Signup(Users model)
		{
			try
			{
              
           
                if (ModelState.IsValid)
				{
                    

                    var user = new IdentityUser { UserName = model.username, Email = model.email };

                    var result = await _userManager.CreateAsync(user, model.password);

                    if (result.Succeeded)
                    {

                        var role = await _userManager.AddToRoleAsync(user, "User");
                        if (role.Succeeded)
                        {
                          
                            return RedirectToAction("Login");
                        }
                        else
                        {

                            return View(model);

                        }

                    }
                    else
                    {
                     
                        return View(model);
                    }
                }
				else
				{

					return View(model);
                }
				
               

            }
			catch (Exception ex)
			{
                Console.WriteLine(ex.ToString());
                return View(model);
					
			}

           
		}


        [HttpGet]
		public IActionResult Login()
		{
			return View();
		}

        [HttpPost]
        public async Task<IActionResult> Login(loginUser model)
        {
            var login = await _signInManager.PasswordSignInAsync(model.username, model.password,false,false);

            if (login!=null && login.Succeeded)
            {
                return RedirectToAction("SecureIndex", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
            
        }


        [HttpGet]
        public IActionResult Profile()
        {
            return View(); 
        }
    }
}
