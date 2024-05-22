using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Internshub.Models;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authorization;


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
        public async Task<IActionResult> Signup(UsersModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var user = new ApplicationUser { FirstName = model.FirstName, LastName = model.LastName, UserName = model.username, Email = model.email };
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
                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                Console.WriteLine($"Error: {error.Description}");
                            }
                        }
                        return View(model);
                    }
                }
                else
                {

                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);

                    foreach (var error in errors)
                    {
                        Console.WriteLine(error);

                    }

                    Console.WriteLine("mdoel state else ki ");

                    return View(model);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("catch ki ym");
                return View(model);

            }
        }


        [HttpGet]
        public IActionResult Login()
        {
            ModelState.Clear();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(loginUser model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var login = await _signInManager.PasswordSignInAsync(model.username, model.password, false, false);

                    if (login != null && login.Succeeded)
                    {

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid username or email");
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
                return NotFound();
            }

        }

        [Authorize(Roles = "User")]

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }

        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> CompleteProfile()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var useraddress = user.Address;

                var phone = user.PhoneNumber;
                var city = user.City;
                var country = user.Country;
                var skills = user.Skills;
                var qualification = user.Qualification;

                var model = new UsersModel
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }
        }


        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> CompleteProfile(UsersModel model)
        {
            try
            {

                if (!ModelState.IsValid)
                {

                    var user = await _userManager.GetUserAsync(User);
                    user.Address = model.Address;
                    user.PhoneNumber = model.Phone;
                    user.City = model.City;
                    user.Country = model.Country;
                    user.Skills = model.Skills;
                    user.Qualification = model.Qualification;
                    user.Skills = model.Skills;

                    var result = await _userManager.UpdateAsync(user);


                    if (result.Succeeded)
                    {
                        return RedirectToAction("Profile");
                    }
                    else
                    {
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

                    return View();
                }
                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var firstname = user.FirstName;
                var lastname = user.LastName;
                var email = user.Email;
                var username = user.UserName;
                var useraddress = user.Address;
                var password = user.PasswordHash;
                var phone = user.PhoneNumber;
                var city = user.City;
                var country = user.Country;
                var skills = user.Skills;
                var qualification = user.Qualification;

                var model = new UsersModel
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProfile(UsersModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var user = await _userManager.GetUserAsync(User);

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

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {

                        return RedirectToAction("Profile");
                    }
                    else
                    {

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }

                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
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
                Console.WriteLine(ex.Message);
                return View(model);
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> DeleteAccount()
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                var firstname = user.FirstName;
                var lastname = user.LastName;
                var email = user.Email;
                var username = user.UserName;
                var model = new UsersModel
                {
                    FirstName = firstname,
                    LastName = lastname,
                    username = username,
                    email = email,
                };
                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAccount(loginUser model)
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }


        }




    }
}
