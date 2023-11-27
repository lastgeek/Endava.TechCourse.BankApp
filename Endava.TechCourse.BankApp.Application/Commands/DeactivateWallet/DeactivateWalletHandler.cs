using Endava.TechCourse.BankApp.Application.Commands.MakeTransaction;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.DeactivateWallet
{
    public class DeactivateWalletHandler : IRequestHandler<DeactivateWalletCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public DeactivateWalletHandler(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task<bool> Handle(DeactivateWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Id == request.WalletId);
            var mainWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.MainWallet);

            if (wallet == null || mainWallet == null || wallet.MainWallet)
            {
                return false;
            }

            var command = new MakeTransactionCommand()
            {
                SenderWalletCode = wallet.Code,
                ReceiverWalletCode = mainWallet.Code,
                Amount = wallet.Amount,
                CurrencyId = wallet.CurrencyId
            };

            await _mediator.Send(command);

            wallet.Active = false;
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}