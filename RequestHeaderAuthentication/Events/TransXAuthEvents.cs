using System;
using System.Threading.Tasks;

namespace RequestHeaderAuthentication.Events
{
    public class TransXAuthEvents
    {
        public Func<ValidateTransXTokenContext, Task> OnValidateTransXToken { get; set; } = context => Task.CompletedTask;
        public Func<TransXAuthenticationFailedContext, Task> OnTransXAuthenticationFailed { get; set; } = context => Task.CompletedTask;


        public virtual Task ValidateToken(ValidateTransXTokenContext context) => OnValidateTransXToken(context);
        public virtual Task TransXAuthenticationFailed(TransXAuthenticationFailedContext context) => OnTransXAuthenticationFailed(context);
    }

}
