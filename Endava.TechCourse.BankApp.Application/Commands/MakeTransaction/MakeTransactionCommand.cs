using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.MakeTransaction
{
    public class MakeTransactionCommand : IRequest
    {
        public Guid SenderWalletId { get; set; }
        public Guid ReceiverWalletId { get; set; }
        public decimal Amount { get; set; }
        public Guid CurrencyId { get; set; }
    }
}