namespace Endava.TechCourse.BankApp.Shared
{
    public class WalletTypeDTO
    {
        public string Id { get; set; }
        public string TypeName { get; set; }
        public decimal Commision { get; set; }
        public bool CanBeRemoved { get; set; }
    }
}