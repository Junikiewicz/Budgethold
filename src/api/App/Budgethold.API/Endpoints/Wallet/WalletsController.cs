using Budgethold.API.Endpoints.Wallet.Dtos;
using Budgethold.API.Extensions;
using Budgethold.Application.Commands.Wallet.AddWallet;
using Budgethold.Application.Commands.Wallet.EditWallet;
using Budgethold.Application.Commands.Wallet.EditWalletOwner;
using Budgethold.Application.Queries.Wallet.GetUserWalletQuery;
using Budgethold.Application.Queries.Wallet.GetUserWallets;
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
        public async Task<IActionResult> GetUserWallet(int id, CancellationToken cancellationToken)
        {
            var query = new GetUserWalletQuery(id, User.GetUserId());

            var result = await _mediator.Send(query, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddWallet(AddWalletRequest request, CancellationToken cancellationToken)
        {
            var command = new AddWalletCommand(request.UsersId, request.Name, request.StartingValue, User.GetUserId());

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> EditWallet(EditWalletRequest request, int id, CancellationToken cancellationToken)
        {
            var command = new EditWalletCommand(id, request.Name, request.StartingValue, User.GetUserId(), request.Users);

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpPatch("newOwner/{walletId}")]
        public async Task<IActionResult> EditWalletOwner(EditWalletOwnerRequest request, int walletId, CancellationToken cancellationToken)
        {
            var command = new EditWalletOwnerCommand(request.NewOner, User.GetUserId(), walletId);

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }

    }
}
