using Microsoft.AspNetCore.Builder;

namespace AspNetCore.Authentication.RequestHeader
{
    public static class MiddlewareExtension
    {
        public static IApplicationBuilder UseRequestHeaderAuthentication(this IApplicationBuilder builder, string scheme)
            => builder.UseMiddleware<RequestHeaderAuthenticationScheme>(scheme);

    }
}
