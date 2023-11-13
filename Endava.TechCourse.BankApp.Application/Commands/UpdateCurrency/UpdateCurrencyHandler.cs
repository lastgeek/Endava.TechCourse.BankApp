using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.UpdateCurrency
{
    public class UpdateCurrencyHandler : IRequestHandler<UpdateCurrencyCommand>
    {
        private readonly ApplicationDbContext _context;

        public UpdateCurrencyHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateCurrencyCommand request, CancellationToken cancellationToken)
        {
            var currency = await _context.Currency.FindAsync(request.Id);

            if (currency == null)
            {
                throw new Exception("Currency not found");
            }

            currency.CurrencyCode = request.CurrencyCode;
            currency.Name = request.Name;
            currency.CurrencyRate = request.CurrencyRate;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}