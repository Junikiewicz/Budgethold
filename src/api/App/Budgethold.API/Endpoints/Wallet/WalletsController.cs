using Budgethold.API.Endpoints.Wallet.Dtos;
using Budgethold.API.Extensions;
using Budgethold.Application.Commands.Wallet.AddWallet;
using Budgethold.Application.Commands.Wallet.EditWallet;
using Budgethold.Application.Queries.Wallet.GetUserWallets;
using Budgethold.Application.Queries.Wallet.GetWalletQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budgethold.API.Endpoints.Wallet
{
    [Route("api/wallets")]
    [Authorize]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWallet(int id, CancellationToken cancellationToken)
        {
            var query = new GetWalletQuery(id, User.GetUserId());

            var result = await _mediator.Send(query, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddWallet(AddWalletRequest request, CancellationToken cancellationToken)
        {
            var command = new AddWalletCommand(request.Name, request.StartingValue, User.GetUserId());

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result, nameof(GetWallet));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditWallet(int id, EditWalletRequest request, CancellationToken cancellationToken)
        {
            var command = new EditWalletCommand(id, request.Name, request.StartingValue, User.GetUserId());

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }
    }
}
