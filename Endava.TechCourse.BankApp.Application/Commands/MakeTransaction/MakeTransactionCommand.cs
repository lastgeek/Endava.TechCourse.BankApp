using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.MakeTransaction
{
    public class MakeTransactionCommand : IRequest
    {
        public string SenderWalletCode { get; set; }
        public string ReceiverWalletCode { get; set; }
        public decimal Amount { get; set; }
        public Guid CurrencyId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}