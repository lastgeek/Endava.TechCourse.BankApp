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
            _context = context;
        }

        public async Task Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == new Guid(request.UserId));
            var currency = _context.Currency.FirstOrDefault(c => c.CurrencyCode == request.CurrencyCode);
            var type = _context.WalletType.FirstOrDefault(t => t.Id == new Guid(request.TypeId));
            if (currency == null || user == null || type == null)
            {
                throw new Exception($"User not found.");
            }
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
                .ToListAsync();

            wallet.MainWallet = userWallets.Count == 0;

            if (currency.CanBeRemoved) currency.CanBeRemoved = false;
            if (type.CanBeRemoved) type.CanBeRemoved = false;

            await _context.Wallets.AddAsync(wallet, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}