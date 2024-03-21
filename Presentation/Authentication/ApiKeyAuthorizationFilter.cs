using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Presentation.Authentication
{
	internal sealed class ApiKeyAuthorizeFilter : IAsyncAuthorizationFilter
	{
        private readonly IUserService _userService;
        public ApiKeyAuthorizeFilter(IUserService userService)
        {
            _userService = userService;
        }

        private const string ApiKeyHeaderName = "X-Api-Key";

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (context.ActionDescriptor.EndpointMetadata.Any(em => em.GetType() == typeof(AllowAnonymousAttribute)))
            {
                // Allow the request to proceed without further authorization checks
                return;
            }

            if (! await IsApiKeyValid(context.HttpContext))
            {
                context.Result = new UnauthorizedResult();
            }    
        }

        private async Task<bool> IsApiKeyValid(HttpContext context)
        {
            string? apiKey = context.Request.Headers[ApiKeyHeaderName];

            if (string.IsNullOrWhiteSpace(apiKey))
            {
                return false;
            }
            string? actualApiKey = await _userService.GetUserApiKey(apiKey);

            return apiKey == actualApiKey;
        }
    }
}

