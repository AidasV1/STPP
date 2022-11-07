using Microsoft.AspNetCore.Authorization;
//using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using STPP_Project.Auth.Model;
using System.Security.Claims;

namespace STPP_Project.Auth
{
    public class ResourceOwnerAuthorizationHandler : AuthorizationHandler<ResourceOwnerRequirement, IUserOwnedResource>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOwnerRequirement requirement,
        IUserOwnedResource resource)
        {
            if (context.User.IsInRole(ProjectRoles.Admin) ||
                context.User.FindFirstValue(JwtRegisteredClaimNames.Sub) == resource.UserId)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public record ResourceOwnerRequirement : IAuthorizationRequirement;
}
