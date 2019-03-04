using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreMediatR.Command
{
    public class LoginCommand : IRequest<LoginCommandResult>
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult>
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;

        public LoginCommandHandler(SignInManager<ApplicationUser> signInManager, ILogger logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, request.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return new LoginCommandResult() { IsSuccess = true };
            }
            if (result.RequiresTwoFactor)
            {
                return new LoginCommandResult() { Need2FA = true };
            }
            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return new LoginCommandResult() { IsLockout = true };
            }
            else
            {
                return new LoginCommandResult() { IsSuccess = false };
            }
        }
    }
}
