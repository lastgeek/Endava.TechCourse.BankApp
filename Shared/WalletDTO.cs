namespace Endava.TechCourse.BankApp.Shared
{
    public class WalletDTO
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public bool MainWallet { get; set; }
    }
}