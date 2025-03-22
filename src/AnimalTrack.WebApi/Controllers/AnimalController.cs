using AnimalTrack.ClientModels;
using AnimalTrack.Services.Requests.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimalTrack.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class AnimalController(IMediator mediator) : ControllerBase
{
    [HttpPost("", Name = nameof(CreateAnimal))]
    public async Task<ActionResult<AnimalModel>> CreateAnimal(string name, CancellationToken cancellationToken)
    {
        var command = new CreateAnimalCommand(name);
        var result = await mediator.Send(command, cancellationToken);
        return result;
    }
}