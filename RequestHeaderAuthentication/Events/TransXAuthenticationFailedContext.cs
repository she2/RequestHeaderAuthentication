// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;

namespace ChiscoTransX.Auth.Events
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
