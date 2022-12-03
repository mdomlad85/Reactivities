using System.Threading.Tasks;
using Application.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProfilesController : BaseApiController
{
  
    [Authorize]
    [HttpGet("{username}")]
    public async Task<IActionResult> GetProfile(string username)
    {
        var result = await Mediator.Send(new Details.Query { Username = username });
        return HandleResult(result);
    }
  
    [Authorize(Policy = "IsProfileOwner")]
    [HttpPut("{username}")]
    public async Task<IActionResult> EditProfile(string username, [FromBody]EditProfileDto profile)
    {
        profile.Username = username;
        var result = await Mediator.Send(new Edit.Command { ProfileDto = profile});
        return HandleResult(result);
    }
}