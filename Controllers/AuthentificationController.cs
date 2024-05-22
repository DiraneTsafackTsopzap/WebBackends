using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapp.Repository.BenuterRepository;
using webapp.ViewModels;

namespace webapp.Controllers
{
    public class AuthentificationController : Controller
    {
        private readonly IBenutzerRepository IBenutzerRepository;
        public AuthentificationController(IBenutzerRepository benutzerRepository)
        {
            this.IBenutzerRepository = benutzerRepository;
        }
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CheckCredentials(LoginViewModel loginView)
        {
            var user = IBenutzerRepository.FindByEmailAndPassword(loginView.Email, loginView.Password);

           

            if (!ModelState.IsValid)
            {
                
                return View("Login",loginView);
            }
            if (user == null)
            {
                ModelState.AddModelError("", "Benutzer wurde nicht gefunden");
                return View("Login", loginView);
            }

        
            Guid guid = new Guid(user.BenutzerId.ToString());



            HttpContext.Session.SetString("UserId_", guid.ToString());
            
            if (user.IsAdmin)
            {
                return RedirectToAction("List", "Fahrer");
            }
            else
            {
                return RedirectToAction("Reservation", "Bus");
            }


        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Items["User_"] = null;
            return Redirect("Login");
        }
    }
}
