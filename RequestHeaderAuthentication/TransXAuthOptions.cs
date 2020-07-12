using Microsoft.AspNetCore.Authentication;
using RequestHeaderAuthentication.Events;

namespace RequestHeaderAuthentication
{
    public class TransXAuthOptions : AuthenticationSchemeOptions
    {
        public string Challenge { get; set; }

        public string[] HeaderKey { get; set; }

        protected new TransXAuthEvents Events
        {
            get => (TransXAuthEvents)base.Events;
            set => base.Events = value;
        }
    }
}
