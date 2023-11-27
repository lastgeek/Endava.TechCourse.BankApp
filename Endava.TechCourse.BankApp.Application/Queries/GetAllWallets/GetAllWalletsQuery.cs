using Endava.TechCourse.BankApp.Domain.Models;
using MediatR;

namespace Endava.TechCourse.BankApp.Application.Queries.GetAllWallets
{
    public class GetAllWalletsQuery : IRequest<List<Wallet>>
    {
    }
}