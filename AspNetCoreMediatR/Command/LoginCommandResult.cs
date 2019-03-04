namespace AspNetCoreMediatR.Command
{
    public class LoginCommandResult
    {
        public bool IsSuccess { get; set; }

        public bool Need2FA { get; set; }

        public bool IsLockout { get; set; }
    }
}