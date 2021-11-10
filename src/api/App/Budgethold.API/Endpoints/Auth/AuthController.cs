using Budgethold.API.Endpoints.Auth.Dtos;
using Budgethold.API.Extensions;
using Budgethold.Security.Commands.SignIn;
using Budgethold.Security.Commands.SignOut;
using Budgethold.Security.Commands.SignUp;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budgethold.API.Endpoints.Auth
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(SignUpRequest request, CancellationToken cancellationToken)
        {
            var command = new SignUpCommand(request.Email, request.Password);

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn(SignInRequest request, CancellationToken cancellationToken)
        {
            var command = new SignInCommand(request.Email, request.Password);

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [Authorize]
        [HttpPost("sign-out")]
        public async Task<IActionResult> SignOut(CancellationToken cancellationToken)
        {
            var command = new SignOutCommand();

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }
    }
}
