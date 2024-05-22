
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Models;
using webapp.Repository.BenuterRepository;

namespace webapp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IBenutzerRepository BenutzerRepository;
       

        public RegisterController(IBenutzerRepository BenutzerRepositories)
        {
            this.BenutzerRepository = BenutzerRepositories;

            
        }
        public IActionResult Create()
        {
            return View();

        }

        public IActionResult List()
        {
            var BenutzerListes = BenutzerRepository.GetAll();
            return View(BenutzerListes);
        }





        [HttpPost]
        public IActionResult Create([FromForm] Benutzer NeuBenutzer)
        {
            if (!ModelState.IsValid)
            {
                return View(NeuBenutzer);
            }


         
            BenutzerRepository.Save(NeuBenutzer);
           


            return Redirect("List");

        }

        

        public IActionResult Edit(Guid id)
        {
            var obj = BenutzerRepository.ById(id);


            if (obj == null)
            {
                return NotFound();
            }
            else
                return View(obj);

        }

        [HttpPost]
        public IActionResult EditBenutzer(Benutzer NeuBenutzer)
        {
            if (!ModelState.IsValid)
            {
                
                return View(NeuBenutzer);
            }

            BenutzerRepository.Update(NeuBenutzer);
            

            return Redirect("List");


        }



        //----------------------------------------------------------------------------------- Das Löschen eines Benutzers von Db
        public IActionResult Delete(Guid id)
        {
            var obj = BenutzerRepository.ById(id);
            if (obj != null)
            {
                BenutzerRepository.Delete(id);
                
                return Redirect("/Register/List");
            }

            return NotFound();
        }


    }
}
