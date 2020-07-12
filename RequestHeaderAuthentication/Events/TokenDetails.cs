using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace ChiscoTransX.Auth.Events
{
    public class TokenDetails
    {
        /// <summary>
        ///The key retrieved from the request header
        /// </summary>
        public string TokenKey { get; }

        /// <summary>
        ///The value of the <see cref="TokenKey"/> retrieved from the header
        /// </summary>
        public string Token { get; }

        /// <summary>
        ///A value that indicate that the token has been validated and confirmed valid. If any other token is invalid, the authentication will fail.
        /// </summary>
        public bool TokenValid { get; set; }
        /// <summary>
        ///Properties supplied by the application which will be added to the <see cref="AuthenticationProperties"/>
        /// </summary>
        public Dictionary<string, string> TokenProperties { get; set; }

        public TokenDetails(string tokenKey, string token)
        {
            TokenKey = tokenKey;
            Token = token;
        }

    }
}