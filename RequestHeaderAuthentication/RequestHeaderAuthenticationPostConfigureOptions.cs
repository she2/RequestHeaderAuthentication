using Microsoft.Extensions.Options;

namespace RequestHeaderAuthentication
{
    public sealed class RequestHeaderAuthenticationPostConfigureOptions : IPostConfigureOptions<RequestHeaderAuthenticationOptions>
    {
        public void PostConfigure(string name, RequestHeaderAuthenticationOptions options)
        {
            if (options.HeaderKey == null || options.HeaderKey.Length == 0)
            {
                options.HeaderKey = new[] { "X-ApiKey" };
            }

            //if (string.IsNullOrEmpty(options.Challenge))
            //{
            //    options.Challenge = TransXAuthDefault.AuthenticationSchemeOne;
            //}
        }
    }
}
