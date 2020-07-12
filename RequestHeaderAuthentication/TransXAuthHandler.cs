using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using RequestHeaderAuthentication.Events;
using RequestHeaderAuthentication.Extensions;

namespace RequestHeaderAuthentication
{
    public sealed class TransXAuthHandler : AuthenticationHandler<TransXAuthOptions>
    {
        public TransXAuthHandler(IOptionsMonitor<TransXAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, IDataProtectionProvider dataProtection, ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }


        protected new TransXAuthEvents Events
        {
            get => (TransXAuthEvents)base.Events;
            set => base.Events = value;
        }

        protected override Task<object> CreateEventsAsync() => Task.FromResult<object>(new TransXAuthEvents());

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                // initialize the context
                var validateTokenContext = new ValidateTransXTokenContext(Context, Scheme, Options);

                // add the headers to the context for validation
                foreach (var headerKey in Options.HeaderKey)
                {
                    var tokenDetails = new TokenDetails(headerKey, Request.Headers[headerKey]);

                    //if (!string.IsNullOrEmpty(tokenDetails.Token))
                    //{
                    validateTokenContext.TokenDetails.Add(tokenDetails);
                    //}

                }

                // There must be  at least one header
                if (validateTokenContext.TokenDetails.All(s => string.IsNullOrEmpty(s.Token)))
                    return AuthenticateResult.Fail("No validation key present in the request header.");

                // Validates the headers using the event handler provided by the dev
                await Events.ValidateToken(validateTokenContext).ConfigureAwait(false);

                if (validateTokenContext.Result != null)
                    return validateTokenContext.Result;

                if (validateTokenContext.TokenDetails.Any(s => !s.TokenValid))
                    return AuthenticateResult.Fail(validateTokenContext.ErrorMessage ?? "Request Header validation fails.");

                // set claims if required
                if (validateTokenContext.ClaimsToSet.Count > 0)
                {
                    validateTokenContext.Principal = new ClaimsPrincipal(new ClaimsIdentity(validateTokenContext.ClaimsToSet, Options.ClaimsIssuer));
                    Context.User = validateTokenContext.Principal;
                }

                // set the token properties
                foreach (var tokenDetail in validateTokenContext.TokenDetails)
                {
                    if (tokenDetail.TokenProperties == null) continue;
                    foreach (var tokenProperties in tokenDetail.TokenProperties)
                    {
                        validateTokenContext.Properties.StoreTokens(new[]
                        {
                            new AuthenticationToken { Name = tokenProperties.Key, Value = tokenProperties.Value }
                        });
                    }
                }

                validateTokenContext.Success();
                Logger.TokenValidationSucceeded();
                return validateTokenContext.Result;
            }
            catch (Exception ex)
            {

                Logger.ErrorProcessingMessage(ex);

                var authenticationFailedContext = new TransXAuthenticationFailedContext(Context, Scheme, Options)
                {
                    Exception = ex
                };

                await Events.TransXAuthenticationFailed(authenticationFailedContext).ConfigureAwait(false);
                if (authenticationFailedContext.Result != null)
                {
                    return authenticationFailedContext.Result;
                }

                throw;
            }

        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            var authResult = await HandleAuthenticateOnceSafeAsync().ConfigureAwait(false);

            Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            Response.Headers.Append(HeaderNames.WWWAuthenticate, Options.Challenge);
            await Response.WriteAsync(authResult?.Failure.Message).ConfigureAwait(false);
        }

        protected override Task HandleForbiddenAsync(AuthenticationProperties properties) => base.HandleForbiddenAsync(properties);
    }
}
