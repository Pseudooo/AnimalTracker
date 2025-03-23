using AnimalTrack.ClientModels;
using AnimalTrack.Services.Requests.Commands;
using AnimalTrack.Services.Requests.Queries;
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

    [HttpGet("{id}", Name = nameof(GetAnimalById))]
    public async Task<ActionResult<AnimalModel>> GetAnimalById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetAnimalByIdQuery(id);
        var result = await mediator.Send(query, cancellationToken);
        if(result is null)
            return NotFound();
        
        return result;
    }

    [HttpGet("", Name = nameof(GetAnimalPage))]
    public async Task<ActionResult<List<AnimalModel>>> GetAnimalPage(
        int pageNumber = 1,
        int pageSize = 30,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAnimalPageQuery(pageNumber, pageSize);
        var result = await mediator.Send(query, cancellationToken);
        return result;
    }
 }