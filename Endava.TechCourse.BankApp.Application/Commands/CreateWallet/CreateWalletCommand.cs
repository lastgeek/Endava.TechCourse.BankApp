using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.CreateWallet
{
    public class CreateWalletCommand : IRequest
    {
        public string Code { get; set; }
        public string TypeId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public string UserId { get; set; }
    }
}