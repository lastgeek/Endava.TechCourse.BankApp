namespace Endava.TechCourse.BankApp.Shared
{
    public class MakeTransactionDTO
    {
        public WalletDTO SenderWallet { get; set; }
        public string ReceiverWalletCode { get; set; }
        public string ReceiverEmail { get; set; }
        public decimal Amount { get; set; }
        public decimal Commission { get; set; }
        public CurrencyDTO SenderCurrency { get; set; }
        public CurrencyDTO Currency { get; set; }
    }
}