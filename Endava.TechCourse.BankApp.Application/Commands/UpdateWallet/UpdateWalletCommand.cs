using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.UpdateWallet
{
    public class UpdateWalletCommand : IRequest
    {
        public Guid WalletId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
}