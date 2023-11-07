using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetCurrencyById
{
    public class GetCurrencyByIdHandler : IRequestHandler<GetCurrencyByIdQuery, Currency>
    {
        private readonly ApplicationDbContext _context;

        public GetCurrencyByIdHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Currency> Handle(GetCurrencyByIdQuery request, CancellationToken cancellationToken)
        {
            var currency = await _context.Currency.FindAsync(request.CurrencyId);

            if (currency == null)
            {
                return null;
            }

            return currency;
        }
    }
}