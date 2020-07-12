using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace RequestHeaderAuthentication
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseHeaderAuth(this IApplicationBuilder builder, string scheme)
            => builder.UseMiddleware<RequestHeaderAuthenticationScheme>(scheme);

    }
}
