﻿using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace RequestHeaderAuthentication.Events
{
    public class ValidateTransXTokenContext : ResultContext<TransXAuthOptions>
    {
        public ValidateTransXTokenContext(
            HttpContext context,
            AuthenticationScheme scheme,
            TransXAuthOptions options)
            : base(context, scheme, options) { }

        public List<TokenDetails> TokenDetails { get; set; } = new List<TokenDetails>();

        /// <summary>
        /// List of <see cref="Claim"/>s to be added to the principal 
        /// </summary>
        public List<Claim> ClaimsToSet { get; set; } = new List<Claim>();

        /// <summary>
        /// In case of failure, the error message that will be returned
        /// </summary>
        public string ErrorMessage { get; set; }

    }
}
