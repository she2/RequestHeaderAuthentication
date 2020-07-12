
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace RequestHeaderAuthentication.Events
{
    public class TransXAuthenticationFailedContext : ResultContext<TransXAuthOptions>
    {
        public TransXAuthenticationFailedContext(
            HttpContext context,
            AuthenticationScheme scheme,
            TransXAuthOptions options)
            : base(context, scheme, options) { }

        public Exception Exception { get; set; }
    }
}
