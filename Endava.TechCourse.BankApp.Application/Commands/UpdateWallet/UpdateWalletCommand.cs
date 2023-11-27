using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.UpdateWallet
{
    public class UpdateWalletCommand : IRequest
    {
        public string WalletCode { get; set; }
        public decimal UpdateAmount { get; set; }
        public Guid Currency { get; set; }
    }
}