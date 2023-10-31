using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using Endava.TechCourse.BankApp.Server.Controllers;
using Endava.TechCourse.BankApp.Shared;
using Endava.TechCourse.BankApp.Test.Common;
using Microsoft.AspNetCore.Mvc;

namespace Endava.TechCourse.BankApp.Test.ControllersTests
{
    public class WalletControllerTests
    {
        [Test, ApplicationData]
        public void CanCreateInstance(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(WalletController).GetConstructors());

        [Test, ApplicationData]
        public async Task ShouldGetWallets(
            [Frozen] ApplicationDbContext context,
            [Greedy] WalletController controller,
            Wallet firstWallet,
            Wallet secondWallet)
        {
            //Arrange
            context.Wallets.AddRange(firstWallet, secondWallet);
            context.SaveChanges();
            context.ChangeTracker.Clear();

            //Act
            var result = await controller.GetWallets();

            //Assert
            result.Count.Should().Be(2);
        }

        [Test, ApplicationData]
        public void ShouldReturnOkForValidId(
            [Frozen] ApplicationDbContext context,
            [Greedy] WalletController controller,
            Wallet wallet)
        {
            // Arrange
            context.Wallets.Add(wallet);
            context.SaveChanges();
            context.ChangeTracker.Clear();

            // Act
            var result = controller.GetWalletDetails(wallet.Id);

            // Assert
            Assert.IsInstanceOf<ActionResult<WalletDTO>>(result);
            var actionResult = result as ActionResult<WalletDTO>;
            Assert.IsInstanceOf<OkObjectResult>(actionResult.Result);
        }

        [Test, ApplicationData]
        public void ShouldReturnNotFoundForInvalidId(
            [Frozen] ApplicationDbContext context,
            [Greedy] WalletController controller)
        {
            // Act
            var result = controller.GetWalletDetails(Guid.NewGuid());

            // Assert
            Assert.IsInstanceOf<ActionResult<WalletDTO>>(result);
            var actionResult = result as ActionResult<WalletDTO>;
            Assert.IsInstanceOf<NotFoundResult>(actionResult.Result);
        }
    }
}