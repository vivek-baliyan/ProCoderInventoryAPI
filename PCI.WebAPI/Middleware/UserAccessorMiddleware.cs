using PCI.Application.Services.Interfaces;
using System.Security.Claims;

namespace PCI.WebAPI.Middleware
{
    public class UserAccessorMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, IUserAccessorService userAccessor)
        {
            var userId = context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {
                userAccessor.SetCurrentUserId(userId);
            }
            await _next(context);
        }
    }
}
