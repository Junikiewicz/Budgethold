using Budgethold.API.Endpoints.Category.Dtos;
using Budgethold.API.Extensions;
using Budgethold.Application.Commands.Category.AddCategory;
using Budgethold.Application.Commands.Category.DeleteCategory;
using Budgethold.Application.Commands.Category.EditCategory;
using Budgethold.Application.Queries.Category.GetWalletCategories;
using Budgethold.Application.Queries.Category.GetWalletCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budgethold.API.Endpoints.Category
{
    [Route("api/categories")]
    [Authorize]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetWalletCategories(int walletId, CancellationToken cancellationToken)
        {
            var command = new GetWalletCategoriesQuery(User.GetUserId(), walletId);

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingleCategory(int id, CancellationToken cancellationToken)
        {
            var command = new GetWalletCategoryQuery(id, User.GetUserId());

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryRequest request, CancellationToken cancellationToken)
        {
            var command = new AddCategoryCommand(User.GetUserId(), request.Name, request.ParentCategoryId, request.TransactionTypeId, request.WalletId);

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result, nameof(GetSingleCategory));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, EditCategoryRequest request, CancellationToken cancellationToken)
        {
            var command = new EditCategoryCommand(id, User.GetUserId(), request.Name, request.ParentCategoryId);

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            var command = new DeleteCategoryCommand(id, User.GetUserId());

            var result = await _mediator.Send(command, cancellationToken);

            return this.GetResponseFromResult(result);
        }
    }
}
