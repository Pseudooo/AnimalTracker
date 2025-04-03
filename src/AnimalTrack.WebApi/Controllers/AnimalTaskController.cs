using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.Services.Requests.Commands;
using AnimalTrack.Services.Requests.Queries;
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
        [FromBody] string name,
        CancellationToken cancellationToken)
    {
        var command = new CreateAnimalTaskCommand(animalId, name);
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
        [FromBody] string name,
        CancellationToken cancellationToken)
    {
        var command = new UpdateAnimalTaskCommand(taskId, name);
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