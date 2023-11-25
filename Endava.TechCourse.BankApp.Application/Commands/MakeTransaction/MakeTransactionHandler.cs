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
            var senderWallet = _context.Wallets.FirstOrDefault(w => w.Code == request.SenderWalletCode);
            var receiverWallet = _context.Wallets.FirstOrDefault(w => w.Code == request.ReceiverWalletCode);
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
                WalletCode = request.SenderWalletCode,
                UpdateAmount = -request.Amount,
                Currency = request.CurrencyId
            };

            var receiverWalletUpdate = new UpdateWalletCommand
            {
                WalletCode = request.ReceiverWalletCode,
                UpdateAmount = request.Amount,
                Currency = request.CurrencyId
            };

            await _mediator.Send(senderWalletUpdate);
            await _mediator.Send(receiverWalletUpdate);

            var transaction = new Transaction
            {
                SourceWalletCode = request.SenderWalletCode,
                SourceUserId = senderWallet.UserId.ToString(),
                DestinationWalletCode = request.ReceiverWalletCode,
                DestinationUserId = receiverWallet.UserId.ToString(),
                Amount = request.Amount,
                CurrencyId = request.CurrencyId,
                Currency = currency,
                CreationDate = DateTime.Now
            };

            await _context.Transactions.AddAsync(transaction, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}