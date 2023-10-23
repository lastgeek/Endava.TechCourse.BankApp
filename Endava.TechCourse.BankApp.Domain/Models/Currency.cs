using Endava.TechCourse.BankApp.Domain.Common;

namespace Endava.TechCourse.BankApp.Domain.Models
{
    internal class Currency : BaseEntity
    {
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public decimal CurrencyRate { get; set; }
    }
}