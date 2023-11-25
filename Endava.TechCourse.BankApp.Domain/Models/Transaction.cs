using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
    public class Transaction : BaseEntity
    {
        public string SourceWalletCode { get; set; }
        public string SourceUserId { get; set; }
        public string DestinationWalletCode { get; set; }
        public string DestinationUserId { get; set; }
        public decimal Amount { get; set; }
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public DateTime CreationDate { get; set; }
    }
}