using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.UpdateCurrency
{
    public class UpdateCurrencyCommand : IRequest
    {
        public Guid Id { get; set; }
        public string CurrencyCode { get; set; }
        public string Name { get; set; }
        public decimal CurrencyRate { get; set; }
    }
}