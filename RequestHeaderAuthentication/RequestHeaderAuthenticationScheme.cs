using System;
using System.Threading.Tasks;
using AspNetCore.Authentication.RequestHeader.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Authentication.RequestHeader
{
    public class RequestHeaderAuthenticationScheme
    {
        private readonly RequestDelegate _next;
        private readonly string _scheme;

        public RequestHeaderAuthenticationScheme(RequestDelegate next, string scheme)
        {
            _next = next;
            _scheme = scheme ?? throw new ArgumentNullException(nameof(scheme));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var result = await httpContext.AuthenticateAsync(_scheme).ConfigureAwait(false);

            if (!result.Succeeded)
            {
                await httpContext.ReturnForbiddenError(result.Failure.Message).ConfigureAwait(false);
                return;
            }

            await _next(httpContext).ConfigureAwait(false);
        }

    }
}
