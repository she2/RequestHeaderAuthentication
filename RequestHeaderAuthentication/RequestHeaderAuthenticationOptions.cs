using Microsoft.AspNetCore.Authentication;
using RequestHeaderAuthentication.Events;

namespace RequestHeaderAuthentication
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
