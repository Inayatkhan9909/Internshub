using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Internshub.Data;
using Internshub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net.WebSockets;

namespace Internshub.Controllers
{
    public class AdminController : Controller
    {
        private readonly EnrollDbContext _dbContextEnroll;
        private readonly AddInternshipDbContext _dbContextAddIntership;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly Cloudinary _cloudinary;

        public AdminController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            Cloudinary cloudinary , EnrollDbContext dbContext, AddInternshipDbContext dbContextAddIntership)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _cloudinary = cloudinary;
            _dbContextEnroll = dbContext;
            _dbContextAddIntership = dbContextAddIntership;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "User")]

        [HttpGet]
        public async Task<IActionResult> Enroll()
        {
            try
            { 
            var user = await _userManager.GetUserAsync(User);
            string dataValue = Request.Query["data"];
            Console.WriteLine("appliedfordata " + dataValue);
            if (user != null)
            {
                var firstname = user.FirstName;
                var lastname = user.LastName;
                var email = user.Email;
                var useraddress = user.Address;
                var phone = user.PhoneNumber;
                var city = user.City;
                var skills = user.Skills;
                var qualification = user.Qualification;
                var appliedfor = dataValue;

                var model = new EnrollResumeModel
                {
                    Name = firstname +" " +lastname,
                    Email = email,
                    Address = useraddress + " " + city,
                    Phone = phone,
                    Skills = skills,
                    Qualification = qualification,
                    Appliedfor = appliedfor
                };
                 Console.WriteLine("model "+model);
                return View(model);
                
            }
            else
            {
                Console.WriteLine("else of user");
                return View();
            }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }

        }

        [Authorize(Roles = "User")]

        [HttpPost]
        public  IActionResult Enroll(EnrollResumeModel model)
        {
            try
            {
                if(ModelState.IsValid)
                {

                    var name = model.Name;
                    var email = model.Email;
                    var address = model.Address;
                    var phone = model.Phone;
                    var qualification = model.Qualification;
                    var skills = model.Skills;
                    var appliedfor = model.Appliedfor;

                    if(model.Resumefile != null && model.Resumefile.Length > 0)
                    {
                        Console.WriteLine("resumefile " + model.Resumefile);
                        var uploadParams = new RawUploadParams
                        {
                            File = new FileDescription(model.Resumefile.FileName, model.Resumefile.OpenReadStream())
                        };
                        Console.WriteLine("uploadParams " + uploadParams);
                        var uploadResult =  _cloudinary.Upload(uploadParams);
                        var pdfUrl = "";
                        Console.WriteLine("uploadResult " + uploadResult);
                        Console.WriteLine("secureurl.seclk " + uploadResult.SecureUrl);
                        if (uploadResult != null && uploadResult.SecureUrl != null)
                        {
                            pdfUrl = uploadResult.SecureUrl.ToString();
                            Console.WriteLine("pdfurl " + pdfUrl);
                           
                        }
                        else
                        {
                            Console.WriteLine("Upload failed or SecureUri is null.");
                        }

                        var resumeEntity = new EnrollModel
                        {
                            Name = name,
                            Email = email,
                            Address = address,
                            Phone = phone,
                            Qualification = qualification,
                            Skills = skills,
                            Appliedfor = appliedfor,
                            Resumeurl = pdfUrl
                        };
                        _dbContextEnroll.Enrolls.Add(resumeEntity);
                        _dbContextEnroll.SaveChanges();

                        return RedirectToAction("Index", "Home");

                      
                    }
                    else
                    {
                        Console.WriteLine("Resumefile is null");
                        return View(model);
                    }

                }
                else
                {
                    Console.WriteLine("modelstate is not valid");
                    return View(model);
                }

                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(model);
            }
            
        }

        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult Enrolled_List()
        {
            try { 
            List<EnrollModel> enrolls = _dbContextEnroll.Enrolls.ToList();
            return View(enrolls);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }
        }


        [HttpGet]
        public async Task<IActionResult> FormStatusbyId()
        {

            try
            {
                var user = await _userManager.GetUserAsync(User);
                var email = user.Email;
                List<EnrollModel> enrolls = _dbContextEnroll.Enrolls.Where(e => e.Email == email).ToList();
                Console.WriteLine(enrolls);
                return View(enrolls);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> EditForm(int id)
        {
           try
            {
                var enrolls = await _dbContextEnroll.Enrolls.FindAsync(id);

                if (enrolls == null)
                {
                    return NotFound();
                }
                return View(enrolls);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
           
        }

        [ HttpPost]
        public async Task<IActionResult> EditForm(EnrollModel NewData)
        {
            try
            {
                var existingData = await _dbContextEnroll.Enrolls.FindAsync(NewData.Id);
                if (existingData == null)
                {
                    return NotFound();
                }
                else
                {
                    existingData.Name = NewData.Name;
                    existingData.Email = NewData.Email;
                    existingData.Address = NewData.Address;
                    existingData.Phone = NewData.Phone;
                    existingData.Qualification = NewData.Qualification;
                    existingData.Skills = NewData.Skills;
                    existingData.Appliedfor = NewData.Appliedfor;
                    _dbContextEnroll.SaveChanges();
                    return RedirectToAction("FormStatusbyId");
                }
            }
            catch 
            {
                return BadRequest();
            }

        }

        [HttpGet]
        public IActionResult DeleteForm(int id)
        {
            try
            {
                var enrolls = _dbContextEnroll.Enrolls.Find(id);
                if (enrolls == null)
                {
                    return NotFound();
                }


                return View(enrolls);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }


        [HttpPost]
        public IActionResult DeleteConfirmedForm(int id)
        {

            try
            {
                var enrolls = _dbContextEnroll.Enrolls.Find(id);
                if (enrolls == null)
                {
                    return NotFound();
                }

                _dbContextEnroll.Enrolls.Remove(enrolls);
                _dbContextEnroll.SaveChanges();
                return RedirectToAction("FormStatusbyId");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }

        [HttpGet]

        public IActionResult AddInternship()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddInternship(AddInternshipImageModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var name = model.Internship_Name;
                    var details = model.Internship_details;
                    var skills = model.Internship_Skills;
                    var duration = model.Duration;
                    var type = model.InternshipType;
                    var mode = model.InternshipMode;
                    var responsibilites = model.Responsibilites;
                    var minQualification = model.MinQualification;


                    if (model.IntershipPicFile != null && model.IntershipPicFile.Length > 0)
                    {
                        var uploadParams = new RawUploadParams
                        {
                            File = new FileDescription(model.IntershipPicFile.FileName, model.IntershipPicFile.OpenReadStream())
                        };
                        var uploadResult = _cloudinary.Upload(uploadParams);
                     
                        string imageUrl = uploadResult.SecureUrl.ToString();

                        if (uploadResult != null && uploadResult.SecureUrl != null)
                        {
                            imageUrl = uploadResult.SecureUrl.ToString();
                         
                        }
                        else
                        {
                            Console.WriteLine("Upload failed or SecureUri is null.");
                        }

                        var imageEntity = new AddInternshipModel
                        {
                            Internship_Name = name,
                            Internship_details = details,
                            Internship_Skills = skills,
                            IntershipPicUrl = imageUrl,
                            Duration = duration,
                            InternshipMode = mode,
                            Responsibilites= responsibilites,
                            MinQualification = minQualification,
                            InternshipType = type,
                        };
                        _dbContextAddIntership.Internship.Add(imageEntity);
                        _dbContextAddIntership.SaveChanges();
                        return RedirectToAction("Profile");

                    }
                    else
                    {
                        Console.WriteLine("Resumefile is null");
                        return View(model);
                    }

                }
                else
                {
                    Console.WriteLine("modelstate is not valid");
                    return RedirectToAction("profile");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View(model);
            }
        }


        [Authorize(Roles = "Admin")]
        
        [HttpGet]
        public IActionResult Internships_list()
        {
            try
            {
                List<AddInternshipModel> internships = _dbContextAddIntership.Internship.ToList();
                return View(internships);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return NotFound();
            }
        }


        [HttpGet]

        public async Task<IActionResult> EditInternship(int id)
        {
            try
            {
                var internship = await _dbContextAddIntership.Internship.FindAsync(id);

                if (internship == null)
                {
                    return NotFound();
                }
                return View(internship);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
            
        }


        [HttpPost]
        public async Task<IActionResult> EditInternship(AddInternshipModel NewData)
        {
            try
            {
                var existingData = await _dbContextAddIntership.Internship.FindAsync(NewData.Id);
                if (existingData == null)
                {
                    return NotFound();
                }
                else
                {
                    existingData.Internship_Name = NewData.Internship_Name;
                    existingData.Internship_details = NewData.Internship_details;
                    existingData.Duration = NewData.Duration;
                    existingData.Internship_Skills = NewData.Internship_Skills;
                    existingData.InternshipMode = NewData.InternshipMode;
                    existingData.InternshipType = NewData.InternshipType;
                    existingData.Responsibilites = NewData.Responsibilites;
                    existingData.MinQualification = NewData.MinQualification;
                    _dbContextAddIntership.SaveChanges();
                    return RedirectToAction("Internships_list");
                }
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpGet]
        public IActionResult DeleteInternship(int id)
        {
            try
            {
                var internship = _dbContextAddIntership.Internship.Find(id);
                if (internship == null)
                {
                    return NotFound();
                }


                return View(internship);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return BadRequest();
            }
        }


        [HttpPost]
        public IActionResult DeleteConfirmedInternship(int id)
        {

            try
            {
                var internship = _dbContextAddIntership.Internship.Find(id);
                if (internship == null)
                {
                    return NotFound();
                }

                _dbContextAddIntership.Internship.Remove(internship);
                _dbContextAddIntership.SaveChanges();
                return RedirectToAction("Internships_List");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest();
            }
        }


    }

   
}
