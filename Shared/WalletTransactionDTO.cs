namespace Endava.TechCourse.BankApp.Shared
{
    public class WalletTransactionDTO
    {
        public string SenderWalletCode { get; set; }
        public string ReceiverWalletCode { get; set; }
        public decimal Amount { get; set; }
        public Guid CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime CreationDate { get; set; }
    }
}