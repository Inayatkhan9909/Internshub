using Microsoft.AspNetCore.Mvc;
using Internshub.Models;
using Internshub.Data;
using Microsoft.AspNetCore.Authorization;
using CloudinaryDotNet;

namespace Internshub.Controllers
{
    [Authorize (Roles ="Admin")]
    public class List_Page : Controller
    {
        private readonly RegistrationDbContext _dbContext;
        public List_Page(RegistrationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

       


        [HttpGet]
        public IActionResult Create_List()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create_List(Intern details)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("i am inside the modelstatevalid");
                _dbContext.Interns.Add(details);
                _dbContext.SaveChanges();
                Console.WriteLine(details.ToString());
                return Redirect("Create_List");

            }

            else
            {
                return View(details);
            }

        }



        [HttpGet]
        public IActionResult List()
        {
            List<Intern> interns = _dbContext.Interns.ToList();
            return View(interns);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var intern = _dbContext.Interns.Find(id);

            if (intern == null)
            {
                return NotFound();
            }
            return View(intern);
        }

        [HttpPost]
        public IActionResult Edit(Intern selected)
        {
            if(ModelState.IsValid)
            {
                var existingintern = _dbContext.Interns.Find(selected.Id);
                if (existingintern == null)
                {
                    return NotFound();
                }
                existingintern.Name = selected.Name;
                existingintern.Email = selected.Email;
                existingintern.Phone = selected.Phone;
                existingintern.Address = selected.Address;

                _dbContext.SaveChanges();
                return RedirectToAction("List");
            }
            return View(selected);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var intern = _dbContext.Interns.Find(id);
            if (intern == null)
            {
                return NotFound();
            }


            return View(intern);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {

            var intern = _dbContext.Interns.Find(id);
            if (intern == null)
            {
                return NotFound();
            }

            _dbContext.Interns.Remove(intern);
            _dbContext.SaveChanges();
            return RedirectToAction("List");
        }

    }
}
