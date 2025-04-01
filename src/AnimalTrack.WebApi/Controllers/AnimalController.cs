using AnimalTrack.ClientModels;
using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.Services.Exceptions;
using AnimalTrack.Services.Requests.Commands;
using AnimalTrack.Services.Requests.Queries;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AnimalTrack.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
[ApiVersion("1.0")]
public class AnimalController(IMediator mediator) : ControllerBase
{
    [HttpPost("", Name = nameof(CreateAnimal))]
    public async Task<ActionResult<AnimalModel>> CreateAnimal(
        [FromBody] string name,
        CancellationToken cancellationToken)
    {
        var command = new CreateAnimalCommand(name);
        var result = await mediator.Send(command, cancellationToken);
        return result;
    }

    [HttpPost("{animalId}/notes", Name = nameof(CreateAnimalNote))]
    public async Task<ActionResult<AnimalNoteModel>> CreateAnimalNote(
        int animalId,
        [FromBody] string note,
        CancellationToken cancellationToken)
    {
        try
        {
            var command = new CreateAnimalNoteCommand(animalId, note);
            var result = await mediator.Send(command, cancellationToken);
            return result;
        }
        catch (RequestValidationException ex)
        {
            return BadRequest(ex.Failures);
        }
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

    [HttpGet("{animalId}/notes", Name = nameof(GetAnimalNotes))]
    public async Task<ActionResult<List<AnimalNoteModel>>> GetAnimalNotes(
        int animalId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAnimalNotesQuery(animalId);
        var result = await mediator.Send(query, cancellationToken);
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnimal(
        int id, 
        [FromBody] string name,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateAnimalCommand(id, name);
        var success = await mediator.Send(command, cancellationToken);
        if (success)
        {
            return NoContent();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpDelete("note/{id}", Name = nameof(DeleteAnimalNote))]
    public async Task<IActionResult> DeleteAnimalNote(int id, CancellationToken cancellationToken = default)
    {
        var command = new DeleteAnimalNoteCommand(id);
        var success = await mediator.Send(command, cancellationToken);
        if(!success)
            return NotFound();

        return NoContent();
    }
 }