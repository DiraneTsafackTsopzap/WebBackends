using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.ViewModels
{
    public class FahrerViewModel
    {
        [Required(ErrorMessage = "Der Fahrername ist erforderlich.")]
        public string FahrerName { get; set; }

        [Required(ErrorMessage = "Der Fahrervorname ist erforderlich.")]
        public string FahrerVorName { get; set; }

        [Required(ErrorMessage = "Das Alter ist erforderlich.")]
        [Range(0, int.MaxValue, ErrorMessage = "Das Alter muss eine nicht negative ganze Zahl sein.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Die E-Mail-Adresse ist erforderlich.")]
        [EmailAddress(ErrorMessage = "Ungültige E-Mail-Adresse.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Image ist erforderlich.")]
        [RegularExpression(@"^https?://.+\.(jpg|jpeg|png|gif)$", ErrorMessage = "Url Image ist nicht gültig")]
        public string Image { get; set; }
    }


}
