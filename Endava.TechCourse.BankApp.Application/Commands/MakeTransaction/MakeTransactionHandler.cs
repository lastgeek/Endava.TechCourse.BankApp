using Endava.TechCourse.BankApp.Application.Commands.UpdateWallet;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Transaction = Endava.TechCourse.BankApp.Domain.Models.Transaction;

namespace Endava.TechCourse.BankApp.Application.Commands.MakeTransaction
{
    public class MakeTransactionHandler : IRequestHandler<MakeTransactionCommand>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public MakeTransactionHandler(ApplicationDbContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        public async Task Handle(MakeTransactionCommand request, CancellationToken cancellationToken)
        {
            var senderWallet = _context.Wallets.FirstOrDefault(w => w.Id == request.SenderWalletId);
            var receiverWallet = _context.Wallets.FirstOrDefault(w => w.Id == request.ReceiverWalletId);
            var currency = _context.Currency.FirstOrDefault(c => c.Id == request.CurrencyId);

            if (senderWallet == null || receiverWallet == null)
            {
                return;
            }

            if (senderWallet.Amount < request.Amount)
            {
                return;
            }

            var senderWalletUpdate = new UpdateWalletCommand
            {
                WalletId = request.SenderWalletId,
                UpdateAmount = -request.Amount,
                Currency = request.CurrencyId
            };

            var receiverWalletUpdate = new UpdateWalletCommand
            {
                WalletId = request.ReceiverWalletId,
                UpdateAmount = request.Amount,
                Currency = request.CurrencyId
            };

            await _mediator.Send(senderWalletUpdate);
            await _mediator.Send(receiverWalletUpdate);

            var transaction = new Transaction
            {
                SourceWalletId = request.SenderWalletId,
                SourceUserId = senderWallet.UserId.ToString(),
                DestinationWalletId = request.ReceiverWalletId,
                DestinationUserId = receiverWallet.UserId.ToString(),
                Amount = request.Amount,
                CurrencyId = request.CurrencyId,
                Currency = currency
            };

            await _context.Transactions.AddAsync(transaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}