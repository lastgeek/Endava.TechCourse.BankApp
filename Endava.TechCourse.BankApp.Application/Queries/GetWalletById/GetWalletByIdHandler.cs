using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class GetWalletByIdQueryHandler : IRequestHandler<GetWalletByIdQuery, Wallet>
{
    private readonly ApplicationDbContext _context;

    public GetWalletByIdQueryHandler(ApplicationDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context);
        _context = context;
    }

    public async Task<Wallet> Handle(GetWalletByIdQuery request, CancellationToken cancellationToken)
    {
        var wallet = await _context.Wallets
            .Include(w => w.Currency)
            .Include(w => w.Type)
            .FirstOrDefaultAsync(w => w.Id == request.Id, cancellationToken);

        if (wallet == null)
        {
            return null;
        }

        return wallet;
    }
}