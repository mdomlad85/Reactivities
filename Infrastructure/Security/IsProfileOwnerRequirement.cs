using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Infrastructure.Security;

public class IsProfileOwnerRequirement : IAuthorizationRequirement
{
}

public class IsProfileOwnerRequirementHandler : AuthorizationHandler<IsProfileOwnerRequirement>
{
    private readonly DataContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IsProfileOwnerRequirementHandler(DataContext dbContext, IHttpContextAccessor httpContextAccessor)
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
    }
    
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsProfileOwnerRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        if (userId == null)
            return Task.CompletedTask;

        var username = _httpContextAccessor.HttpContext?.Request.RouteValues.SingleOrDefault(x => x.Key == "username").Value?.ToString();
        
        if (string.IsNullOrEmpty(username))
            return Task.CompletedTask;
        
        var userByUsername = _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == username).Result;
        
        if (userByUsername == null)
            return Task.CompletedTask;
        
        if (userId == userByUsername.Id) 
            context.Succeed(requirement);
        
        return Task.CompletedTask;
    }
}