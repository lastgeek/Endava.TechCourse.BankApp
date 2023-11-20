using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.UpdateWallet
{
    public class UpdateWalletHandler : IRequestHandler<UpdateWalletCommand>
    {
        private readonly ApplicationDbContext _context;

        public UpdateWalletHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(UpdateWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Id == request.WalletId);
            var currency = await _context.Currency.FirstOrDefaultAsync(c => c.Id == request.Currency);
            var walletCurrency = await _context.Currency.FirstOrDefaultAsync(c => c.Id == wallet.CurrencyId);

            var conversedAmount = request.UpdateAmount * currency.CurrencyRate;
            conversedAmount /= walletCurrency.CurrencyRate;

            wallet.Amount += conversedAmount;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}