using System;
using System.Threading.Tasks;
using Application.Activities;
using Application.Core;
using Application.Profiles;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Details = Application.Activities.Details;
using Edit = Application.Activities.Edit;

namespace API.Controllers;

public class ActivitiesController : BaseApiController
{
    [HttpGet]
    public async Task<IActionResult> GetActivities([FromQuery]ActivityParams param)
    {
        return HandlePagedResult(await Mediator.Send(new List.Query { Params = param}));
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetActivity(Guid id)
    {
        var result = await Mediator.Send(new Details.Query { Id = id });
        return HandleResult(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateActivity(Activity activity)
    {
        return HandleResult(await Mediator.Send(new Create.Command { Activity = activity }));
    }
    
    [Authorize(Policy = "IsActivityHost")]
    [HttpPut("{id}")]
    public async Task<ActionResult> EditActivity(Guid id, Activity activity)
    {
        activity.Id = id;
        return HandleResult(await Mediator.Send(new Edit.Command { Activity = activity }));
    }
    
    [Authorize(Policy = "IsActivityHost")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteActivity(Guid id)
    {
        return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
    }
    
    [HttpPost("{id}/attend")]
    public async Task<ActionResult> Attend(Guid id)
    {
        return HandleResult(await Mediator.Send(new UpdateAttendance.Command { Id = id }));
    }
}