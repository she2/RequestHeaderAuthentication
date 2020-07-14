using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
using RequestHeaderAuthentication;

namespace RequestHeaderAuthentication
{
    public static class RequestHeaderAuthenticationExtension
    {
        /// <summary>
        /// This defaults to use <see cref="RequestHeaderDefault.AUTHENTICATION_SCHEME_ONE"/>
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>Returns <see cref="AuthenticationBuilder"/> used to configure the authentication</returns>
        public static AuthenticationBuilder AddRequestHeaderAuthentication(this AuthenticationBuilder builder)
           => builder.AddRequestHeaderAuthentication(RequestHeaderDefault.AUTHENTICATION_SCHEME_ONE, _ => { });

        /// <summary>
        /// This defaults to using <see cref="RequestHeaderDefault.AUTHENTICATION_SCHEME_ONE"/>
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configureOptions"> An <see cref="Action" /> used to configure <see cref="RequestHeaderAuthenticationOptions"/></param>
        /// <returns>Returns <see cref="AuthenticationBuilder"/> used to configure the authentication</returns>
        public static AuthenticationBuilder AddRequestHeaderAuthentication(this AuthenticationBuilder builder, Action<RequestHeaderAuthenticationOptions> configureOptions)
            => builder.AddRequestHeaderAuthentication(RequestHeaderDefault.AUTHENTICATION_SCHEME_ONE, configureOptions);

        /// <summary>
        /// Allows you to use any predefined or custom authentication scheme
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="authenticationScheme">One of the <see cref="RequestHeaderDefault"/> constants or a custom authentication schedme</param>
        /// <param name="configureOptions"> An <see cref="Action" /> used to configure <see cref="RequestHeaderAuthenticationOptions"/></param>
        /// <returns>Returns <see cref="AuthenticationBuilder"/> used to configure the authentication</returns>
        public static AuthenticationBuilder AddRequestHeaderAuthentication(this AuthenticationBuilder builder, string authenticationScheme, Action<RequestHeaderAuthenticationOptions> configureOptions)
            => builder.AddRequestHeaderAuthentication(authenticationScheme, displayName: null, configureOptions: configureOptions);

        /// <summary>
        /// Allows you to use any predefined or custom authentication scheme
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="authenticationScheme">One of the <see cref="RequestHeaderDefault"/> constants or a custom authentication schedme</param>
        /// <param name="displayName"></param>
        /// <param name="configureOptions"> An <see cref="Action" /> used to configure <see cref="RequestHeaderAuthenticationOptions"/></param>
        /// <returns>Returns <see cref="AuthenticationBuilder"/> used to configure the authentication</returns>
        public static AuthenticationBuilder AddRequestHeaderAuthentication(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<RequestHeaderAuthenticationOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<RequestHeaderAuthenticationOptions>, RequestHeaderAuthenticationPostConfigureOptions>());
            return builder.AddScheme<RequestHeaderAuthenticationOptions, RequestHeaderAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
