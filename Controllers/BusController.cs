using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapp.Models;
using webapp.Repository.BenuterRepository;
using webapp.Repository.BusRepository;
using webapp.Repository.FahrerRepository;
using webapp.Repository.ReservationRepository;
using webapp.ViewModels;

namespace webapp.Controllers
{
    public class BusController : Controller
    {
        private readonly IFahrerRepository FahrerRepository;
        private readonly IBusRepository BusRepository;
        private readonly IReservationRepository ReservationRepository;
        public BusController(IFahrerRepository FahrerRepositories ,
                             IBusRepository BusRepositories , 
                             IReservationRepository ReservationRepositories )
        {
            this.FahrerRepository = FahrerRepositories;
            this.BusRepository = BusRepositories;
            this.ReservationRepository = ReservationRepositories;

        }
        public IActionResult Create() 
        {

            Load_Fahrer_toDropdown();
            return View();
        }

        public IActionResult List()
        {

            var ListesBus = BusRepository.GetAll();
            return View(ListesBus);
        }

        public IActionResult Reservation()
        {
            var ListesSieges = BusRepository.GetAll();
            return View(ListesSieges);
        }


        [HttpGet]
        public IActionResult ListeReservation()
        {
            var user = HttpContext.Session.GetString("UserId_");
            Guid Guid_User_Id = new Guid(user.ToString());

            var ListesReservation = ReservationRepository.HoleReservierungenNachBenutzerId(Guid_User_Id);
            return View(ListesReservation);
        }

        [HttpPost]
        public IActionResult Create([FromForm] Bus meinbus)
        {

            if (!ModelState.IsValid)
            {
                Load_Fahrer_toDropdown();
                return View(meinbus);
            }


            BusRepository.Save(meinbus);

            return RedirectToAction(nameof(List));
        }



        [HttpGet]
        public IActionResult Details(Guid id, DateTime selectedDate)
        {

            HttpContext.Session.SetString("SelectedDate", selectedDate.ToString());



            ViewBag.SelectedBusId = id;
            ViewBag.SelectedDate = selectedDate;

            var VerfugbareSitzplatze = ReservationRepository.VerfügbarePlätzeabrufen(id, selectedDate);


            ViewBag.BusId = id;

            if (VerfugbareSitzplatze == null)
            {
                return NotFound();
            }


            return View(VerfugbareSitzplatze);

        }




       

        [HttpPost]
        public IActionResult Details(List<Guid> ListesSelectedSeats,Guid busId)
        {
            var userIdString = HttpContext.Session.GetString("UserId_");

            DateTime mySelectedDate = DateTime.Now; 
            

            if (HttpContext.Session.TryGetValue("SelectedDate", out var selectedDateBytes))
            {
                var selectedDate = DateTime.Parse(Encoding.UTF8.GetString(selectedDateBytes));

            
                mySelectedDate = selectedDate; 

               
            }
            else
            {
                // selektiertes datum wurde nicht im Session gefunden
            }

            var ReservationVM = new ReservationViewModel()
            {
                UserId = userIdString,
                SelectiertenSitzplatzen = ListesSelectedSeats,
                ReservationDate = mySelectedDate,
                BusId = busId
            };

       
            this.ReservationRepository.ErstellungReservierung(ReservationVM);
            return RedirectToAction(nameof(ListeReservation));


       
        }









        private void Load_Fahrer_toDropdown()
        {
            ViewBag.FahrerListes = FahrerRepository.GetAll();

           
        }

        public IActionResult Delete(Guid id)
        {
            var obj = BusRepository.ById(id);
            if (obj == null)
            {

                return NotFound();
            }

            BusRepository.Delete(id);

            return RedirectToAction(nameof(List));


        }





       



        public IActionResult Deleting(Guid id)
        {
            var user = HttpContext.Session.GetString("UserId_");
            Guid Guid_User_Id = new Guid(user.ToString());

            var obj = ReservationRepository.ById(id);
            if (obj != null)
            {
                ReservationRepository.DeleteReservation(id, Guid_User_Id);

                return RedirectToAction(nameof(List));
            }

            return RedirectToAction(nameof(List));


        }


        public IActionResult PageNotFound()
        {

            return View();


        }
    }
}
