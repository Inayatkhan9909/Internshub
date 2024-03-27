using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Internshub.Models;
using System.Security.Claims;
using System;

namespace Internshub.Controllers
{
	public class AccountsController : Controller
	{
      
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountsController(UserManager<ApplicationUser> userManager,
			SignInManager<ApplicationUser> signInManager)
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

                    Console.WriteLine("modelstate landi");

                    var user = new ApplicationUser {FirstName= model.FirstName, LastName=model.LastName, UserName = model.username, Email = model.email };
                    Console.WriteLine("modelstate landi");
                    var result = await _userManager.CreateAsync(user, model.password);
                    Console.WriteLine("result landi");
                    if (result.Succeeded)
                    {
                        Console.WriteLine("result succeded");
                        var role = await _userManager.AddToRoleAsync(user, "User");
                        Console.WriteLine("role landi");
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
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                Console.WriteLine($"Error: {error.Description}");
                            }
                        }
                        Console.WriteLine("succed else ki");
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
            if (ModelState.IsValid)
            {
                var login = await _signInManager.PasswordSignInAsync(model.username, model.password, false, false);

                if (login != null && login.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            else
            {
               
                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }




        [HttpGet]
        public async  Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            var userid = user.Id;
            var firstname = user.FirstName;
            var lastname = user.LastName;
            var username = user.UserName;
            var email = user.Email;
            var address = user.Address;
            var phone = user.PhoneNumber;
            var city = user.City;
            var state = user.State;
            var country = user.Country;
            var skills = user.Skills;
            var qualification = user.Qualification;
            var dob = user.DOB;
            var postalcode = user.PostalCode;

            //var firstname 
            var model = new UserProfileViewModel
            {
                UserId = userid,
                FirstName = firstname,
                LastName = lastname,
                Email = email,
                Address = address,

                UserName = username,
                City = city,
                State = state,
                Country = country,
                PostalCode = postalcode,
                Skills = skills,
                Qualification = qualification,
                DOB = dob,
                Phone = phone
               
                // You can add more properties to the model if needed
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CompleteProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            var useraddress = user.Address;
           
            var phone = user.PhoneNumber;
            var city = user.City;
            var country = user.Country;
            var skills = user.Skills;
            var qualification = user.Qualification;

            var model = new Users
            {

             
                Address = useraddress,
                Phone = phone,
                City = city,
                Country = country,
                Skills = skills,
                Qualification = qualification,

            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteProfile(Users model)
        {
            Console.WriteLine("inside complette post");
           
            if (!ModelState.IsValid)
            {
                Console.WriteLine("user var lapasa ");
                var user = await _userManager.GetUserAsync(User);
                user.Address= model.Address;
                user.PhoneNumber = model.Phone;
                user.City = model.City;
                user.Country = model.Country;
                user.Skills = model.Skills;
                user.Qualification = model.Qualification;
                user.Skills = model.Skills;
                Console.WriteLine("result lapasa var");
                var result = await _userManager.UpdateAsync(user);
                Console.WriteLine("result var landi");

                if (result.Succeeded)
                {
                    Console.WriteLine("inside result succeded");
                    return RedirectToAction("Profile");
                }
                else
                {
                    Console.WriteLine("else of result succeded");
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update user.");
                Console.WriteLine("model state else ki");
                return View();
            }
            Console.WriteLine("seda landi");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = await _userManager.GetUserAsync(User);

            var firstname = user.FirstName;
            var lastname = user.LastName;
            var email = user.Email;
            var username = user.UserName;
            var useraddress = user.Address;
            var password = user.PasswordHash;
            var  phone=          user.PhoneNumber;
            var   city=         user.City;
            var   country=         user.Country;
            var skills=       user.Skills ;
            var qualification=      user.Qualification;
         
            var model = new Users
            {

                FirstName = firstname,
                LastName = lastname,
                username = username,
                email = email,
                password = password,
                Address = useraddress,
               Phone = phone,
               City = city,
               Country = country,
               Skills = skills,
               Qualification = qualification,
                              
          };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(Users model)
        {
            Console.WriteLine("inside complette post");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("user var lapasa ");
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.username;
                    user.Email = model.email;
                    user.PasswordHash = model.password;
                    user.Address = model.Address;
                    user.PhoneNumber = model.Phone;
                    user.City = model.City;
                    user.Country = model.Country;
                    user.Skills = model.Skills;
                    user.Qualification = model.Qualification;
                    user.Skills = model.Skills;
                    Console.WriteLine("result lapasa var");
                    var result = await _userManager.UpdateAsync(user);
                    Console.WriteLine("result var landi");

                    if (result.Succeeded)
                    {
                        Console.WriteLine("inside result succeded");
                        return RedirectToAction("Profile");
                    }
                    else
                    {
                        Console.WriteLine("else of result succeded");
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update user.");
                Console.WriteLine("model state else ki");
                return View();
            }
          
            return View();
        }

        [HttpGet]
        public async  Task<IActionResult> DeleteAccount()
        {
            var user = await _userManager.GetUserAsync(User);

            var firstname = user.FirstName;
            var lastname = user.LastName;
            var email = user.Email;
            var username = user.UserName;
            var model = new Users
            {
                FirstName = firstname,
                LastName = lastname,
                username = username,
                email = email,
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(loginUser model)
        {
            var login = await _signInManager.PasswordSignInAsync(model.username, model.password, false, false);

            if (login != null && login.Succeeded)
            {
                var userToDelete = await _userManager.FindByNameAsync(model.username);

                if (userToDelete != null)
                {
                    var result = await _userManager.DeleteAsync(userToDelete);

                    if (result.Succeeded)
                    {
                       
                        await _signInManager.SignOutAsync();

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                     
                        ModelState.AddModelError(string.Empty, "Error deleting user account.");
                    }
                }
                else
                {
                    // Handle the case where the user to delete is not found
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }
            return View(model);
            

        }


    


    }
}
