using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace ChiscoTransX.Auth.Events
{
    public static class TransXAuthExtension
    {
        /// <summary>
        /// This defaults to using <see cref="TransXAuthDefault.AUTHENTICATION_SCHEME_ONE"/>
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>Returns <see cref="AuthenticationBuilder"/> used to configure the authentication</returns>
        public static AuthenticationBuilder AddTransXAuth(this AuthenticationBuilder builder)
           => builder.AddTransXAuth(TransXAuthDefault.AUTHENTICATION_SCHEME_ONE, _ => { });

        /// <summary>
        /// This defaults to using <see cref="TransXAuthDefault.AUTHENTICATION_SCHEME_ONE"/>
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configureOptions"> An <see cref="Action" /> used to configure <see cref="TransXAuthOptions"/></param>
        /// <returns>Returns <see cref="AuthenticationBuilder"/> used to configure the authentication</returns>
        public static AuthenticationBuilder AddTransXAuth(this AuthenticationBuilder builder, Action<TransXAuthOptions> configureOptions)
            => builder.AddTransXAuth(TransXAuthDefault.AUTHENTICATION_SCHEME_ONE, configureOptions);

        /// <summary>
        /// Allows you to use any predefined or custom authentication scheme
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="authenticationScheme">One of the <see cref="TransXAuthDefault"/> constants or a custom authentication schedme</param>
        /// <param name="configureOptions"> An <see cref="Action" /> used to configure <see cref="TransXAuthOptions"/></param>
        /// <returns>Returns <see cref="AuthenticationBuilder"/> used to configure the authentication</returns>
        public static AuthenticationBuilder AddTransXAuth(this AuthenticationBuilder builder, string authenticationScheme, Action<TransXAuthOptions> configureOptions)
            => builder.AddTransXAuth(authenticationScheme, displayName: null, configureOptions: configureOptions);

        /// <summary>
        /// Allows you to use any predefined or custom authentication scheme
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="authenticationScheme">One of the <see cref="TransXAuthDefault"/> constants or a custom authentication schedme</param>
        /// <param name="displayName"></param>
        /// <param name="configureOptions"> An <see cref="Action" /> used to configure <see cref="TransXAuthOptions"/></param>
        /// <returns>Returns <see cref="AuthenticationBuilder"/> used to configure the authentication</returns>
        public static AuthenticationBuilder AddTransXAuth(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<TransXAuthOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<TransXAuthOptions>, TransXAuthPostConfigureOptions>());
            return builder.AddScheme<TransXAuthOptions, TransXAuthHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
