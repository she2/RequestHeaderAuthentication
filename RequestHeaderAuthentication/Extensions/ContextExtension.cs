using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Authentication.RequestHeader.Extensions
{
    public static class ContextExtension
    {

        /// <summary>
        /// Writes the given message to the response body with the given status
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> </param>
        /// <param name="msg">The messge to write to the response body</param>
        /// <param name="statusCode">The <see cref="StatusCodes"/> of the response</param>
        /// <returns></returns>
        private static Task ReturnMessage(this HttpContext context, string msg, int statusCode)
        {
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(msg);
        }


        /// <summary>
        /// Writes a message to the response body with the status code set to <see cref="StatusCodes.Status403Forbidden"/>
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/></param>
        /// <param name="msg">The message to write to the response body</param>
        /// <returns></returns>
        public static Task ReturnForbiddenError(this HttpContext context, string msg) => context.ReturnMessage(msg, StatusCodes.Status403Forbidden);
    }

}
