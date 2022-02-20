using System;
using System.Threading;
using System.Threading.Tasks;
using Budgethold.Application.Commands.Transaction.EditTransaction;
using Budgethold.Application.Contracts.Persistance;
using Budgethold.Domain.Common.Errors;
using Budgethold.Domain.Models;
using Moq;
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

            mockedUnitOfWork.Setup(x => x.TransactionRepository.GetTransactionOrDefaultAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(null as Domain.Models.Transaction);

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
        public async Task Handle_ExistingTransactionNotUserWallet_NotFoundErrorNoChangesToDatabase()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            mockedUnitOfWork.Setup(x => x.TransactionRepository.GetTransactionOrDefaultAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Domain.Models.Transaction("name", "description", 1, 1, 1, DateTime.Parse("2011-03-21 13:26")));
            mockedUnitOfWork.Setup(x => x.WalletsRepository.GetWalletAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Wallet("name", 1, 2));

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
        public async Task Handle_ExistingTransactionCategoryNotAssignedToUser_NotFoundErrorNoChangesToDatabase()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            mockedUnitOfWork.Setup(x => x.TransactionRepository.GetTransactionOrDefaultAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Domain.Models.Transaction());
            mockedUnitOfWork.Setup(x => x.WalletsRepository.GetWalletAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Wallet("name", 1, 1));
            mockedUnitOfWork.Setup(x => x.CategoriesRepository.GetCategoryAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category("name", 1, 1, 2));

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
        public async Task Handle_ChangeExistingTransactionAmount_SameWallet_AmountAboveZero_NoErrorNoChangesToDatabase()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            mockedUnitOfWork.Setup(x => x.TransactionRepository.GetTransactionOrDefaultAsync(1, It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Domain.Models.Transaction("name", "desc", 1, 1, 100, DateTime.Parse("2011-03-21 13:26")));
            mockedUnitOfWork.Setup(x => x.WalletsRepository.GetWalletAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Wallet("name", 1, 1));
            mockedUnitOfWork.Setup(x => x.CategoriesRepository.GetCategoryAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category("name", 1, 1, 1));
            mockedUnitOfWork.Setup(x => x.WalletsRepository.GetWalletAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(new Wallet("name", 100, 1));
            mockedUnitOfWork.Setup(x => x.WalletsRepository.GetWalletAsync(2, It.IsAny<CancellationToken>())).ReturnsAsync(new Wallet());

            mockedUnitOfWork.Setup(x => x.CategoriesRepository.GetCategoryTransactionTypeAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var commandHandler = new EditTransactionCommandHandler(mockedUnitOfWork.Object);
            var command = new EditTransactionCommand(1, 1, "name", "desc", 1, 200, DateTime.Parse("2011-03-21 13:26"), 1);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result.Error);
            mockedUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ChangeExistingTransactionAmount_SameWallet_AmountBelowZero_NoErrorNoChangesToDatabase()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            mockedUnitOfWork.Setup(x => x.TransactionRepository.GetTransactionOrDefaultAsync(1, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Domain.Models.Transaction("name", "desc", 1, 2, 100, DateTime.Parse("2011-03-21 13:26")));
            mockedUnitOfWork.Setup(x => x.WalletsRepository.GetWalletAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Wallet("name", 1, 1));
            mockedUnitOfWork.Setup(x => x.CategoriesRepository.GetCategoryAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category("name", 1, 1, 1));
            mockedUnitOfWork.Setup(x => x.WalletsRepository.GetWalletAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(new Wallet("name", 100, 1));

            mockedUnitOfWork.Setup(x => x.CategoriesRepository.GetCategoryTransactionTypeAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(1);
            mockedUnitOfWork.Setup(x => x.CategoriesRepository.GetCategoryTransactionTypeAsync(2, It.IsAny<CancellationToken>())).ReturnsAsync(2);

            var commandHandler = new EditTransactionCommandHandler(mockedUnitOfWork.Object);
            var command = new EditTransactionCommand(1, 1, "name", "desc", 2, -200, DateTime.Parse("2011-03-21 13:26"), 1);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result.Error);
            mockedUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_ChangeWalletMoneyAmount_DiffrentWallet_DiffrentWalletNotFound_NotFoundErrorNoChangesToDatabase()
        {
            // Arrange
            var mockedUnitOfWork = new Mock<IUnitOfWork>();

            mockedUnitOfWork.Setup(x => x.TransactionRepository.GetTransactionOrDefaultAsync(1, It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Domain.Models.Transaction("name", "desc", 1, 1, 100, DateTime.Parse("2011-03-21 13:26")));
            mockedUnitOfWork.Setup(x => x.WalletsRepository.GetWalletAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
               .ReturnsAsync(new Wallet("name", 1, 1));
            mockedUnitOfWork.Setup(x => x.CategoriesRepository.GetCategoryAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Category("name", 1, 1, 1));
            mockedUnitOfWork.Setup(x => x.WalletsRepository.GetWalletAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(new Wallet("name", 100, 1));
            mockedUnitOfWork.Setup(x => x.WalletsRepository.GetWalletOrDefaultAsync(2, It.IsAny<CancellationToken>())).ReturnsAsync(null as Wallet);
            mockedUnitOfWork.Setup(x => x.CategoriesRepository.GetCategoryTransactionTypeAsync(1, It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var commandHandler = new EditTransactionCommandHandler(mockedUnitOfWork.Object);
            var command = new EditTransactionCommand(1, 1, "name", "desc", 1, 200, DateTime.Parse("2011-03-21 13:26"), 2);

            // Act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result.Error);
            Assert.IsType<NotFoundError>(result.Error);
            mockedUnitOfWork.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
