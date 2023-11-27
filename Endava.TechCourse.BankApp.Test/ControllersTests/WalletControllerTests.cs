using Endava.TechCourse.BankApp.Application.Commands.UpdateWallet;
using Endava.TechCourse.BankApp.Application.Queries.GetWallets;
using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Server.Controllers;
using Endava.TechCourse.BankApp.Shared;
using Endava.TechCourse.BankApp.Test.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Endava.TechCourse.BankApp.Test.ControllersTests
{
    [TestFixture]
    public class WalletControllerTests
    {
        [Test, ApplicationData]
        public void CanCreateInstance(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(WalletController).GetConstructors());

        [Test, ApplicationData]
        public async Task ShouldGetWallets(
            Mock<IMediator> mediatorMock,
            [Greedy] WalletController controller,
            Guid userId,
            List<Wallet> wallets)
        {
            // Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetWalletsQuery>(), default)).ReturnsAsync(wallets);

            // Act
            var okActionResult = await controller.GetWallets(userId);

            // Assert
            Assert.NotNull(okActionResult);
            Assert.IsInstanceOf<OkObjectResult>(okActionResult.Result);
        }

        [Test, ApplicationData]
        public async Task ShouldGetWalletById(
            [Frozen] Mock<IMediator> mediatorMock,
            [Greedy] WalletController controller,
            Guid walletId,
            Wallet wallet)
        {
            // Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<GetWalletByIdQuery>(), default)).ReturnsAsync(wallet);

            // Act
            var actionResult = await controller.GetWalletById(walletId);

            // Assert
            Assert.NotNull(actionResult);
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
        }

        [Test, ApplicationData]
        public async Task ShouldUpdateWallet(
            [Frozen] Mock<IMediator> mediatorMock,
            [Greedy] WalletController controller,
            string walletCode,
            UpdateWalletDTO updateWalletDTO)
        {
            // Arrange
            mediatorMock.Setup(m => m.Send(It.IsAny<UpdateWalletCommand>(), default)).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await controller.UpdateWallet(walletCode, updateWalletDTO);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<OkResult>(result);
        }
    }
}