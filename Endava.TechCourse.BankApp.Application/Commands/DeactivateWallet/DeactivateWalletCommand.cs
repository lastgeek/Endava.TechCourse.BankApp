using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.DeactivateWallet
{
    public class DeactivateWalletCommand : IRequest<bool>
    {
        public Guid WalletId { get; set; }
    }
}