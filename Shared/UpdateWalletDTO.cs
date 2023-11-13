namespace Endava.TechCourse.BankApp.Shared
{
    public class UpdateWalletDTO
    {
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
    }
}