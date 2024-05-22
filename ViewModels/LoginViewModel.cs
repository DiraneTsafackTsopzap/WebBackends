using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Die E-Mail-Adresse ist erforderlich.")]
        [EmailAddress(ErrorMessage = "Ungültige E-Mail-Adresse.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Der Password ist erforderlich.")]
        public string Password { get; set; }
    }
}
