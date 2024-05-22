

using Microsoft.AspNetCore.Builder;
using webapp;

namespace Culture
{
    public static class CustomAuthorisationExtention
    {
        public static IApplicationBuilder UseCustomAuthorization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomAuthorisation>();
        }
    }
}
