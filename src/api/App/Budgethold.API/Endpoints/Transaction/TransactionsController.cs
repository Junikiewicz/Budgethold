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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTransaction(int id, CancellationToken cancellationToken)
        {
            var query = new GetSingleTransactionQuery(User.GetUserId(), id);

            var result = await _mediator.Send(query, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetWalletTransactionsList([FromQuery] int walletId, CancellationToken cancellationToken)
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

        [HttpPut("{transactionId:int}")]
        public async Task<IActionResult> EditTransaction(EditTransactionRequest request, int transactionId, CancellationToken cancellationToken)
        {
            var command = new EditTransactionCommand(transactionId, User.GetUserId(), request.Name, request.Description, request.CategoryId, request.Amount, request.Date, request.WalletId);

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpDelete("{transactionId:int}")]
        public async Task<IActionResult> DeleteTransaction(int transactionId, CancellationToken cancellationToken)
        {
            var command = new DeleteTransactionCommand(User.GetUserId(), transactionId);

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }
    }
}
