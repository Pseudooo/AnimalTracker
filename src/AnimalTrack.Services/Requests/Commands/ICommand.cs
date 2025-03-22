using MediatR;

namespace AnimalTrack.Services.Requests.Commands;

public interface ICommand<out T> : IRequest<T>;