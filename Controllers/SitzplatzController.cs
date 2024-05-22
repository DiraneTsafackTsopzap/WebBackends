using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Models;
using webapp.Repository.BusRepository;
using webapp.Repository.SitzplatzRepository;

namespace webapp.Controllers
{
    public class SitzplatzController : Controller
    {

        private readonly IBusRepository BusRepositories;
        private readonly ISitzplatzRepository SitzPlatzRepositories;

        public SitzplatzController(IBusRepository BusRepository , ISitzplatzRepository SitzplatzRepository)
        {
            this.BusRepositories = BusRepository;
            this.SitzPlatzRepositories = SitzplatzRepository;
        }
        public IActionResult Create()
        {
            Load_Bus_To_Dropdown();
            return View();
        }

        public IActionResult List()
        {
            var ListesSieges = SitzPlatzRepositories.GetAll();
            return View(ListesSieges);
        }

        












        [HttpPost]

        public IActionResult Create(Sitzplatz sitzplatz)
        {
            if (!ModelState.IsValid)
            {
                Load_Bus_To_Dropdown();

                return View(sitzplatz);

            }

            var neuSitzplatz = new Sitzplatz()
            {
                BusId = sitzplatz.BusId,
                IsVerfugbar = true,
                Nummer = sitzplatz.Nummer
            };

            SitzPlatzRepositories.Add(neuSitzplatz);

            return RedirectToAction(nameof(List));
        }

        public IActionResult Delete(Guid id)
        {
            var obj = SitzPlatzRepositories.GetById(id);
            if (obj != null)
            {
                SitzPlatzRepositories.Delete(id);

                return Redirect("/Sitzplatz/List");
            }

            return NotFound();
        }



        private void Load_Bus_To_Dropdown()
        {
            ViewBag.ListesBussToDropdown = BusRepositories.GetAll()
                                                            .Select(bus => new SelectListItem { Value = bus.BusId.ToString(), Text = bus.BusName })
                                                            .ToList();
        }
    }
}
