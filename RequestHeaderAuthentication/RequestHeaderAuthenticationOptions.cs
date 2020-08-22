using AspNetCore.Authentication.RequestHeader.Events;
using Microsoft.AspNetCore.Authentication;

namespace AspNetCore.Authentication.RequestHeader
{
    public class RequestHeaderAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string Challenge { get; set; }

        public string[] HeaderKey { get; set; }

        protected new RequestHeaderAuthenticationEvents Events
        {
            get => (RequestHeaderAuthenticationEvents)base.Events;
            set => base.Events = value;
        }
    }
}
