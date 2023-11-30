using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.CreateWallet
{
    public class CreateWalletHandler : IRequestHandler<CreateWalletCommand>
    {
        private readonly ApplicationDbContext _context;

        public CreateWalletHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
        }

        public async Task Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == new Guid(request.UserId), cancellationToken)
                ?? throw new InvalidOperationException("User not found.");
            var currency = await _context.Currency.FirstOrDefaultAsync(c => c.CurrencyCode == request.CurrencyCode, cancellationToken)
                ?? throw new InvalidOperationException("Currency not found.");
            var type = await _context.WalletType.FirstOrDefaultAsync(t => t.Id == new Guid(request.TypeId), cancellationToken)
                ?? throw new InvalidOperationException($"Wallet type not found.");
            var wallet = new Wallet
            {
                Code = request.Code,
                Type = type,
                Amount = request.Amount,
                Currency = currency,
                UserId = new Guid(request.UserId),
                Active = true
            };

            var userWallets = await _context.Wallets
                .Where(w => w.UserId == user.Id)
                .ToListAsync(cancellationToken);

            wallet.MainWallet = userWallets.Count == 0;

            if (currency.CanBeRemoved) currency.CanBeRemoved = false;
            if (type.CanBeRemoved) type.CanBeRemoved = false;

            await _context.Wallets.AddAsync(wallet, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}