using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreMediatR.Events
{

    public abstract class Event : Message , INotification
    {
        public DateTime Timestamp { get; private set; }

        public Event()
        {
            this.Timestamp = DateTime.Now;
        }
    }
}
