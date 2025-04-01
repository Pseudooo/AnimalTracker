using FluentValidation;
using MediatR;

namespace AnimalTrack.Services.Pipelines;

public class ValidationPipeline<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validationContext = new ValidationContext<TRequest>(request);
        var failures = validators.Select(x => x.Validate(validationContext))
            .SelectMany(x => x.Errors)
            .Where(x => x is not null)
            .ToList();
        
        if(failures is not [])
            throw new ValidationException(failures);

        return await next();
    }
}