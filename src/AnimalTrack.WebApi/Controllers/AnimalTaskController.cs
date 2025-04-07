using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.Services.Requests.Commands;
using AnimalTrack.Services.Requests.Queries;
using AnimalTrack.WebApi.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimalTrack.WebApi.Controllers;

[ApiController]
[Route("animal")]
public class AnimalTaskController(IMediator mediator) : ControllerBase
{
    [HttpPost("{animalId}/tasks")]
    public async Task<ActionResult<AnimalTaskModel>> CreateAnimalTask(
        int animalId,
        [FromBody] AnimalTaskRequestBody body,
        CancellationToken cancellationToken)
    {
        var command = new CreateAnimalTaskCommand(animalId, body.Name, body.Frequency, body.ScheduledFor);
        var result = await mediator.Send(command, cancellationToken);
        return result;
    }
    
    [HttpGet("{animalId}/tasks", Name = nameof(GetAnimalTasks))]
    public async Task<ActionResult<List<AnimalTaskModel>>> GetAnimalTasks(
        int animalId,
        CancellationToken cancellationToken)
    {
        var query = new GetAnimalTasksQuery(animalId);
        var results = await mediator.Send(query, cancellationToken);
        return results;
    }

    [HttpPut("tasks/{taskId}", Name = nameof(UpdateAnimalTask))]
    public async Task<IActionResult> UpdateAnimalTask(
        int taskId,
        [FromBody] AnimalTaskRequestBody body,
        CancellationToken cancellationToken)
    {
        var command = new UpdateAnimalTaskCommand(taskId, body.Name, body.Frequency, body.ScheduledFor);
        var result = await mediator.Send(command, cancellationToken);
        if (result)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }
}