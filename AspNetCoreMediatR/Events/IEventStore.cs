using MediatR;
using System;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace AspNetCoreMediatR.Events
{

    public interface IEventStore
    {
        void Save<T>(T theEvent) where T : Event;
    }
}
