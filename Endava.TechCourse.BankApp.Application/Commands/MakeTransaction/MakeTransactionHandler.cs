using Endava.TechCourse.BankApp.Application.Commands.UpdateWallet;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Transaction = Endava.TechCourse.BankApp.Domain.Models.Transaction;

namespace Endava.TechCourse.BankApp.Application.Commands.MakeTransaction
{
    public class MakeTransactionHandler : IRequestHandler<MakeTransactionCommand, CommandStatus>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public MakeTransactionHandler(ApplicationDbContext context, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(mediator);
            _context = context;
            _mediator = mediator;
        }

        public async Task<CommandStatus> Handle(MakeTransactionCommand request, CancellationToken cancellationToken)
        {
            var senderWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Code == request.SenderWalletCode, cancellationToken);

            if (senderWallet == null)
            {
                return CommandStatus.Failed("Sender wallet was not found.");
            }

            var receiverWallet = await _context.Wallets.FirstOrDefaultAsync(w => w.Code == request.ReceiverWalletCode, cancellationToken);

            if (receiverWallet == null)
            {
                return CommandStatus.Failed("Receiver wallet was not found.");
            }

            var currency = await _context.Currency.FirstOrDefaultAsync(c => c.Id == request.CurrencyId, cancellationToken);

            if (currency == null)
            {
                return CommandStatus.Failed("Currency was not found.");
            }

            if (senderWallet.Amount < request.Amount)
            {
                return CommandStatus.Failed("Not enough amount in wallet.");
            }

            var walletType = await _context.WalletType.FirstOrDefaultAsync(w => w.Id == senderWallet.TypeId, cancellationToken);

            if (walletType == null)
            {
                return CommandStatus.Failed("Wallet type was not found.");
            }

            var commision = request.Amount * walletType.Commision;
            var amountWithCommision = request.Amount - commision;

            var senderWalletUpdate = new UpdateWalletCommand
            {
                WalletCode = request.SenderWalletCode,
                UpdateAmount = -request.Amount,
                Currency = request.CurrencyId
            };

            var receiverWalletUpdate = new UpdateWalletCommand
            {
                WalletCode = request.ReceiverWalletCode,
                UpdateAmount = amountWithCommision,
                Currency = request.CurrencyId
            };

            await _mediator.Send(senderWalletUpdate, cancellationToken);
            await _mediator.Send(receiverWalletUpdate, cancellationToken);

            var transaction = new Transaction
            {
                SourceWalletCode = request.SenderWalletCode,
                SourceUserId = senderWallet.UserId.ToString(),
                DestinationWalletCode = request.ReceiverWalletCode,
                DestinationUserId = receiverWallet.UserId.ToString(),
                Amount = amountWithCommision,
                Commission = commision,
                CurrencyId = request.CurrencyId,
                Currency = currency,
                CreationDate = DateTime.Now
            };

            await _context.Transactions.AddAsync(transaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandStatus();
        }
    }
}