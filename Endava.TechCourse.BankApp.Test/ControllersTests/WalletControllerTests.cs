using Endava.TechCourse.BankApp.Server.Controllers;
using Endava.TechCourse.BankApp.Test.Common;

namespace Endava.TechCourse.BankApp.Test.ControllersTests
{
    public class WalletControllerTests
    {
        [Test, ApplicationData]
        public void CanCreateInstance(GuardClauseAssertion assertion)
            => assertion.Verify(typeof(WalletController).GetConstructors());
    }
}