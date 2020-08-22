using System;
using System.Threading.Tasks;

namespace AspNetCore.Authentication.RequestHeader.Events
{
    public class RequestHeaderAuthenticationEvents
    {
        public Func<ValidateRequestHeaderTokenContext, Task> OnValidateTransXToken { get; set; } = context => Task.CompletedTask;
        public Func<RequestHeaderAuthenticationFailedContext, Task> OnTransXAuthenticationFailed { get; set; } = context => Task.CompletedTask;


        public virtual Task ValidateToken(ValidateRequestHeaderTokenContext context) => OnValidateTransXToken(context);
        public virtual Task AuthenticationFailed(RequestHeaderAuthenticationFailedContext context) => OnTransXAuthenticationFailed(context);
    }

}
