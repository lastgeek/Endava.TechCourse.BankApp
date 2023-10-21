using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
    internal class Transaction : BaseEntity
    {
        public Guid SourceWalletId { get; set; }
        public string SourceUserId { get; set; }
        public string SourceUserName { get; set; }
        public Guid DestinationWalletId { get; set; }
        public string DestinationUserId { get; set; }
        public string DestinationUserName { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
    }
}