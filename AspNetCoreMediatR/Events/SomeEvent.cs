using MediatR;

namespace AspNetCoreMediatR
{
    public class SomeEvent : INotification
    {
        public SomeEvent(string message)
        {
            this.Message = message;
        }

        public string Message { get; }
    }
}
