using Endava.TechCourse.BankApp.Application.Commands.MakeTransaction;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Application.Commands.DeactivateWallet
{
    public class DeactivateWalletHandler : IRequestHandler<DeactivateWalletCommand, CommandStatus>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public DeactivateWalletHandler(ApplicationDbContext context, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(mediator);
            _context = context;
            _mediator = mediator;
        }

        public async Task<CommandStatus> Handle(DeactivateWalletCommand request, CancellationToken cancellationToken)
        {
            var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Id == new Guid(request.WalletId), cancellationToken);
            if (wallet == null)
            {
                return CommandStatus.Failed("Wallet was not found.");
            }
            if (wallet.MainWallet)
            {
                return CommandStatus.Failed("Cannot deactivate main wallet.");
            }

            var mainWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.MainWallet && w.UserId == wallet.UserId, cancellationToken);

            if (mainWallet == null)
            {
                return CommandStatus.Failed("Main wallet was not found.");
            }

            var command = new MakeTransactionCommand()
            {
                SenderWalletCode = wallet.Code,
                ReceiverWalletCode = mainWallet.Code,
                Amount = wallet.Amount,
                CurrencyId = wallet.CurrencyId
            };

            await _mediator.Send(command, cancellationToken);

            wallet.Active = false;
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandStatus();
        }
    }
}