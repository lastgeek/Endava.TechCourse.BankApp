using Endava.TechCourse.BankApp.Domain.Models;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using Endava.TechCourse.BankApp.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WalletController(ApplicationDbContext dbContext)
        {
            ArgumentNullException.ThrowIfNull(dbContext);
            _context = dbContext;
        }

        [HttpPost]
        public IActionResult CreateWallet([FromBody] CreateWalletDTO createWalletDTO)
        {
            var wallet = new Wallet
            {
                Type = createWalletDTO.Type,
                Amount = createWalletDTO.Amount,
                Currency = new Currency
                {
                    Name = createWalletDTO.CurrencyName,
                    CurrencyCode = createWalletDTO.CurrencyName,
                    CurrencyRate = createWalletDTO.CurrencyRate,
                }
            };
            _context.Wallets.Add(wallet);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet("{Id}")]
        public ActionResult<WalletDTO> GetWalletDetails(Guid Id)
        {
            var wallet = _context.Wallets.Include(x => x.Currency).FirstOrDefault(x => x.Id == Id);

            if (wallet == null)
            {
                return NotFound();
            }

            var dto = new WalletDTO()
            {
                Id = wallet.Id.ToString(),
                Currency = wallet.Currency.Name,
                Type = wallet.Type,
                Amount = wallet.Amount,
            };
            return Ok(dto);
        }

        [HttpGet("getwallets")]
        public async Task<List<WalletDTO>> GetWallets()
        {
            var wallets = await _context.Wallets.Include(x => x.Currency).ToListAsync();
            var dtos = new List<WalletDTO>();

            foreach (var wallet in wallets)
            {
                var dto = new WalletDTO()
                {
                    Id = wallet.Id.ToString(),
                    Currency = wallet.Currency.Name,
                    Type = wallet.Type,
                    Amount = wallet.Amount,
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}