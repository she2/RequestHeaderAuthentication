using ChiscoTransX.Auth.Events;
using Microsoft.AspNetCore.Authentication;

namespace ChiscoTransX.Auth
{
    public class TransXAuthOptions : AuthenticationSchemeOptions
    {
        public string Challenge { get; set; }

        public string[] HeaderKey { get; set; }

        protected new TransXAuthEvents Events
        {
            get { return (TransXAuthEvents)base.Events; }
            set { base.Events = value; }
        }
    }
}
