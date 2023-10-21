using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
    internal class CashByCode : BaseEntity
    {
        public Guid SourceWalletId { get; set; }
        public string SourceUserId { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string GeneratedCode { get; set; }
    }
}