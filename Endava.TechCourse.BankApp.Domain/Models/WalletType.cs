using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
    public class WalletType : BaseEntity
    {
        public string TypeName { get; set; }
        public decimal Commision { get; set; }
        public bool CanBeRemoved { get; set; }
    }
}