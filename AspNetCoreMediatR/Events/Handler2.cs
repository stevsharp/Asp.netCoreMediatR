using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AspNetCoreMediatR
{

    public class Handler2 : INotificationHandler<SomeEvent>
    {
        private readonly ILogger<Handler1> _logger;

        public Handler2(ILogger<Handler1> logger)
        {
            _logger = logger;
        }

        public Task Handle(SomeEvent notification, CancellationToken cancellationToken)
        {
            return Task.Run(() => {
                _logger.LogWarning($"Handled: {notification.Message}");
            });

        }
    }
}
