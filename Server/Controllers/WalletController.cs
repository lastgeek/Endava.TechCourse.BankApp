using Endava.TechCourse.BankApp.Application.Commands.CreateWallet;
using Endava.TechCourse.BankApp.Application.Queries.GetWallets;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using Endava.TechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediator;

        public WalletController(ApplicationDbContext dbContext, IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(dbContext);
            ArgumentNullException.ThrowIfNull(mediator);
            _context = dbContext;
            _mediator = mediator;
        }

        [HttpPost]
        public IActionResult CreateWallet([FromBody] CreateWalletDTO createWalletDTO)
        {
            var command = new CreateWalletCommand()
            {
                Type = createWalletDTO.Type,
                Amount = createWalletDTO.Amount,
                CurrencyCode = createWalletDTO.CurrencyCode,
            };

            _mediator.Send(command);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<WalletDTO?> GetWalletById(Guid id)
        {
            var wallet = await _context.Wallets.Include(w => w.Currency).FirstOrDefaultAsync(w => w.Id == id);

            var dto = new WalletDTO()
            {
                Id = wallet.Id.ToString(),
                Currency = wallet.Currency.Name,
                Type = wallet.Type,
                Amount = wallet.Amount,
            };
            return dto;
        }

        [HttpGet("getwallets")]
        public async Task<List<WalletDTO>> GetWallets()
        {
            var query = new GetWalletsQuery();

            var wallets = await _mediator.Send(query);
            var dtos = new List<WalletDTO>();

            foreach (var wallet in wallets)
            {
                var dto = new WalletDTO()
                {
                    Id = wallet.Id.ToString(),
                    Currency = wallet.Currency.CurrencyCode,
                    Type = wallet.Type,
                    Amount = wallet.Amount,
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}