namespace Endava.TechCourse.BankApp.Shared
{
    public class UpdateWalletDTO
    {
        public decimal Amount { get; set; }
        public Guid CurrencyId { get; set; }
    }
}