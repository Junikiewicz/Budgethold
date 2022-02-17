using Budgethold.API.Endpoints.Transaction.Dtos;
using Budgethold.API.Extensions;
using Budgethold.Application.Commands.Transaction.AddTransaction;
using Budgethold.Application.Commands.Transaction.DeleteTransaction;
using Budgethold.Application.Commands.Transaction.EditTransaction;
using Budgethold.Application.Queries.Transaction.GetSingleTransaction;
using Budgethold.Application.Queries.Transaction.GetWalletTransactions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budgethold.API.Endpoints.Transaction
{
    [Route("api/transactions")]
    [Authorize]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransaction(int id, CancellationToken cancellationToken)
        {
            var query = new GetSingleTransactionQuery(User.GetUserId(), id);

            var result = await _mediator.Send(query, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetWalletTransactions([FromQuery] int walletId, CancellationToken cancellationToken)
        {
            var query = new GetWalletTransactionsQuery(walletId, User.GetUserId());

            var result = await _mediator.Send(query, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddTransaction(AddTransactionRequest request, CancellationToken cancellationToken)
        {
            var command = new AddTransactionCommand(User.GetUserId(), request.Name, request.Description, request.WalletId, request.CategoryId, request.Amount, request.Date);

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result, nameof(GetTransaction));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditTransaction(int id, EditTransactionRequest request, CancellationToken cancellationToken)
        {
            var command = new EditTransactionCommand(id, User.GetUserId(), request.Name, request.Description, request.CategoryId, request.Amount, request.Date, request.WalletId);

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteTransactionCommand(User.GetUserId(), id);

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }
    }
}
