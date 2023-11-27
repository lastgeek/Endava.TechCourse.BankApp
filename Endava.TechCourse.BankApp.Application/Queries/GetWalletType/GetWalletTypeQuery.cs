using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetWalletType
{
    public class GetWalletTypeQuery : IRequest<List<WalletType>>
    {
    }
}