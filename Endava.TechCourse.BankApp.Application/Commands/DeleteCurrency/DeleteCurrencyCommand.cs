using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteCurrency
{
    public class DeleteCurrencyCommand : IRequest<bool>
    {
        public Guid CurrencyId { get; set; }
    }
}