using Budgethold.Application.Commands.Transaction.EditTransaction;
using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common.Errors;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Budgethold.UnitTests.Core.Application.Commands.Transaction
{
    public class EditTransactionCommandHandlerTests
    {
        [Fact]
        public async Task Handle_NotExistingTransaction_NotFoundErrorNoChangesToDatabase()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            mockedUnitOfWork.Setup(x => x.TransactionRepository.GetTransactionAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(null as Domain.Models.Transaction);
            mockedUnitOfWork.Setup(x => x.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var commandHandler = new EditTransactionCommandHandler(mockedUnitOfWork.Object);
            var command = new EditTransactionCommand(1, 1, "name", "desc", 1, 1M, DateTime.Parse("2011-03-21 13:26"), 1);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result.Error);
            Assert.IsType<NotFoundError>(result.Error);
            mockedUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ExistingTransactionUserNotAssignedToWallet_NotFoundErrorNoChangesToDatabase()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            mockedUnitOfWork.Setup(x => x.TransactionRepository.GetTransactionAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Domain.Models.Transaction());
            mockedUnitOfWork.Setup(x => x.UserWalletsRepository.CheckIfUserIsAssignedToWalletAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var commandHandler = new EditTransactionCommandHandler(mockedUnitOfWork.Object);
            var command = new EditTransactionCommand(1, 1, "name", "desc", 1, 1M, DateTime.Parse("2011-03-21 13:26"), 1);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result.Error);
            Assert.IsType<NotFoundError>(result.Error);
            mockedUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}