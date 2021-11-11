using Budgethold.API.Endpoints.Wallet.Dtos;
using Budgethold.API.Extensions;
using Budgethold.Application.Commands.Wallet.AddWalletCommand;
using Budgethold.Application.Queries.Wallet.GetUserWalletsQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Budgethold.API.Endpoints.Wallet
{
    [Route("api/wallets")]
    [ApiController]
    public class WalletsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserWallets(CancellationToken cancellationToken)
        {
            var query = new GetUserWalletsQuery(User.GetUserId());

            var result = await _mediator.Send(query, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddWallet(AddWalletRequest request, CancellationToken cancellationToken)
        {
            var command = new AddWalletCommand(User.GetUserId(), request.Name, request.StartingValue);

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }
    }
}
