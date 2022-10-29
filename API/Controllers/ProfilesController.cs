using System.Threading.Tasks;
using Application.Profiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class ProfilesController : BaseApiController
{
  
    [Authorize]
    [HttpGet("{username}")]
    public async Task<IActionResult> GetActivity(string username)
    {
        var result = await Mediator.Send(new Details.Query { Username = username });
        return HandleResult(result);
    }
}