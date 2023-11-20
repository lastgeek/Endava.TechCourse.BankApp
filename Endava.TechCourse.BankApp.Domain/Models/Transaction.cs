using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
    public class Transaction : BaseEntity
    {
        public Guid SourceWalletId { get; set; }
        public string SourceUserId { get; set; }
        public Guid DestinationWalletId { get; set; }
        public string DestinationUserId { get; set; }
        public decimal Amount { get; set; }
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}