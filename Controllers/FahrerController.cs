using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.ViewModels;
using webapp.Models;
using webapp.Repository.FahrerRepository;

namespace webapp.Controllers
{
    public class FahrerController : Controller
    {
        private readonly IFahrerRepository IFahrerRepository;
        public FahrerController(IFahrerRepository FahrerRepositories)
        {
            this.IFahrerRepository = FahrerRepositories;
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(FahrerViewModel Neuer_Fahrer)
        {
            if (!ModelState.IsValid)
            {
                return View(Neuer_Fahrer);
            }
            


            IFahrerRepository.Save(Neuer_Fahrer);
            return RedirectToAction(nameof(List));
        }




        public IActionResult List()
        {
            var FahrerListes =  IFahrerRepository.GetAll();

            return View(FahrerListes);
        }

        public IActionResult Delete(Guid id)
        {
            var obj = IFahrerRepository.ById(id);

            if (obj == null)
            {

                return NotFound();
            }

            IFahrerRepository.Delete(id);

            return RedirectToAction(nameof(List));


        }



        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var fahrer = IFahrerRepository.ById(id);
            if (fahrer == null)
            {
                return View("NotFound");
            }

           
            var viewModel = new FahrerViewModel
            {
                FahrerName = fahrer.FahrerName,
                FahrerVorName = fahrer.FahrerName,
                Age = fahrer.Age,
                Image = fahrer.Image,
                Email = fahrer.Email,
                
            };



            return View(viewModel);


        }

        [HttpPost]
        public IActionResult Edit(Guid id, FahrerViewModel fahrer)
        {
            if (!ModelState.IsValid)
            {
                return View(fahrer);
            }
            IFahrerRepository.Update(id, fahrer);
            return RedirectToAction(nameof(List));
        }


    }
}
