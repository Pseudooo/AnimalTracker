using MediatR;

namespace AnimalTrack.Services.Requests.Queries;

public interface IQuery<out T> : IRequest<T>;