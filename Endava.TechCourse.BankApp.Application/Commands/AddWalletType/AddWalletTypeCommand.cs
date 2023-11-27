using MediatR;

namespace Endava.TechCourse.BankApp.Application.Commands.AddWalletType
{
    public class AddWalletTypeCommand : IRequest<CommandStatus>
    {
        public string TypeName { get; set; }
        public decimal Commision { get; set; }
    }
}