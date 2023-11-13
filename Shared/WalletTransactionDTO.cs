namespace Endava.TechCourse.BankApp.Shared
{
    public class WalletTransactionDTO
    {
        public Guid SenderWalletId { get; set; }
        public Guid ReceiverWalletId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
}