using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.Services.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimalTrack.WebApi.Controllers;

[ApiController]
[Route("animal")]
public class AnimalTaskController(IMediator mediator) : ControllerBase
{
    [HttpGet("{animalId}/tasks", Name = nameof(GetAnimalTasks))]
    public async Task<ActionResult<List<AnimalTaskModel>>> GetAnimalTasks(
        int animalId,
        CancellationToken cancellationToken)
    {
        var query = new GetAnimalTasksQuery(animalId);
        var results = await mediator.Send(query, cancellationToken);
        return results;
    }
}