using MediatR;
using System;
using FluentValidation.Results;
using System.Threading.Tasks;

namespace AspNetCoreMediatR.Events
{
    public abstract class Command : Message
    {
        public DateTime Timestamp { get; protected set; }

        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            this.Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }


    public class CommandHandler
    {

    }

    public class CustomerCommandHandler : CommandHandler
    {

    }
}
