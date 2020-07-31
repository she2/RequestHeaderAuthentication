﻿
using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace RequestHeaderAuthentication.Events
{
    public sealed class RequestHeaderAuthenticationFailedContext : ResultContext<RequestHeaderAuthenticationOptions>
    {
        public RequestHeaderAuthenticationFailedContext(
            HttpContext context,
            AuthenticationScheme scheme,
            RequestHeaderAuthenticationOptions options)
            : base(context, scheme, options) { }

        public Exception Exception { get; set; }
    }
}
