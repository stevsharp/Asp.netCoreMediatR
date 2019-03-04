using MediatR;
using System;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace AspNetCoreMediatR.Events
{

    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
