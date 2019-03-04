using AspNetCoreMediatR.SignalR;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace AspNetCoreMediatR.MassTransit
{
    public class SendMessageConsumer : IConsumer<Message>
    {
        public async Task Consume(ConsumeContext<Message> context)
        {
            Console.WriteLine($"Receive message value: {context.Message.Value}");

            var server = new SignalRServer();

            await server.Send(context.Message.Value);
        }
    }
}
