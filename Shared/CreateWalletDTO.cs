namespace Endava.TechCourse.BankApp.Shared
{
    public class CreateWalletDTO
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyName { get; set; }
        public decimal CurrencyRate { get; set; }
    }
}