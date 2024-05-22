using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using webapp.Models;
using webapp.Repository.BenuterRepository;

namespace webapp
{
    public class CustomAuthorisation
    {
        private RequestDelegate next;

        public CustomAuthorisation(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context, IBenutzerRepository repository)
        {
            if (!context.Request.Path.StartsWithSegments("/Authentification"))
            {

                var userIdString = context.Session.GetString("UserId_");

                if (!string.IsNullOrEmpty(userIdString))
                {
                    Guid userIdAsGuid = new Guid(userIdString);
                    var user = repository.ById(userIdAsGuid);

                    if (user.IsAdmin)
                    {
                        // User ist Admin und kann leider nicht reservierung machen

                        SetUserContextItems(context, user);
                        // User 
                        if ((context.Request.Path.StartsWithSegments("/Bus/Details")))
                        {
                            context.Response.Redirect("/Bus/PageNotFound"); // Redirection zur Dieser Seite 
                            return;
                        }
                    }
                    else
                    {
                        // user ist loggedin und ist kein Admin
                        SetUserContextItems(context, user);

                        // User kann auf alle diese Seiten zugreifen
                        if (!(context.Request.Path.StartsWithSegments("/Bus/Reservation") || (context.Request.Path.StartsWithSegments("/Bus/Deleting") ||
                              context.Request.Path.StartsWithSegments("/Bus/ListeReservation") ||
                              context.Request.Path.StartsWithSegments("/Bus/Details"))))
                        {
                            context.Response.Redirect("/Bus/Reservation"); // Redirection zur dieser Seite. 
                            return;
                        }
                    }
                }
                else
                {
                    // L'utilisateur n'est pas authentifié, rediriger vers la page de login ou d'inscription
                    if (context.Request.Path != "/Authentification/Login" && context.Request.Path != "/Register/Create")
                    {
                        context.Response.Redirect("/Authentification/Login");
                        return;
                    }
                }
            }

            await next(context);
        }

        private void SetUserContextItems(HttpContext context, Benutzer user)
        {
            context.Items["User_"] = user;
            context.Items["IsAdmin_"] = user.IsAdmin;
            context.Items["Firstname_"] = user.Firstname;
        }

    }


}
