using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.CreateWallet
{
    public class CreateWalletHandler : IRequestHandler<CreateWalletCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateWalletHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var currency = _context.Currency.FirstOrDefault(c => c.CurrencyCode == request.CurrencyCode);
            if (currency == null)
            {
                throw new Exception($"Currency with code '{request.CurrencyCode}' not found.");
            }
            var wallet = new Wallet
            {
                Type = request.Type,
                Amount = request.Amount,
                Currency = currency
            };

            await _context.Wallets.AddAsync(wallet, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}