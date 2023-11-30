using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.DeleteWalletType
{
    public class DeleteWalletTypeHandler : IRequestHandler<DeleteWalletTypeCommand, CommandStatus>
    {
        private readonly ApplicationDbContext _context;

        public DeleteWalletTypeHandler(ApplicationDbContext context)
        {
            ArgumentNullException.ThrowIfNull(context);
            _context = context;
        }

        public async Task<CommandStatus> Handle(DeleteWalletTypeCommand request, CancellationToken cancellationToken)
        {
            var walletType = await _context.WalletType.FindAsync(request.WalletTypeId, cancellationToken);

            if (walletType == null)
            {
                return CommandStatus.Failed("Wallet type was not found.");
            }
            if (!walletType.CanBeRemoved)
            {
                return CommandStatus.Failed("Wallet type cannot be removed.");
            }

            _context.WalletType.Remove(walletType);
            await _context.SaveChangesAsync(cancellationToken);

            return new CommandStatus();
        }
    }
}