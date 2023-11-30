using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.DeactivateWallet
{
    public class DeactivateWalletCommand : IRequest<CommandStatus>
    {
        public string WalletId { get; set; }
    }
}