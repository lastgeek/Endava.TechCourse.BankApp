using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
    public class Wallet : BaseEntity
    {
        public string Code { get; set; }
        public WalletType Type { get; set; }
        public Guid TypeId { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }
        public Guid CurrencyId { get; set; }
        public Guid UserId { get; set; }
        public bool MainWallet { get; set; }
        public bool Active { get; set; }
    }
}