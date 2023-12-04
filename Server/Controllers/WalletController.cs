using Endava.TechCourse.BankApp.Application.Commands.CreateWallet;
using Endava.TechCourse.BankApp.Application.Commands.DeactivateWallet;
using Endava.TechCourse.BankApp.Application.Commands.DeleteWallet;
using Endava.TechCourse.BankApp.Application.Commands.UpdateWallet;
using Endava.TechCourse.BankApp.Application.Queries.GetWallets;
using Endava.TechCourse.BankApp.Server.Services;
using Endava.TechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly WalletService _walletService;

        public WalletController(IMediator mediator, WalletService walletService)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            ArgumentNullException.ThrowIfNull(walletService);
            _mediator = mediator;
            _walletService = walletService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWallet([FromBody] CreateWalletDTO createWalletDTO)
        {
            var walletCode = await _walletService.GenerateUniqueWalletCode();

            var command = new CreateWalletCommand()
            {
                Code = walletCode,
                TypeId = createWalletDTO.Type,
                Amount = createWalletDTO.Amount,
                CurrencyCode = createWalletDTO.CurrencyCode,
                UserId = createWalletDTO.UserId
            };

            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallet(Guid id)
        {
            var command = new DeleteWalletCommand { WalletId = id };
            var result = await _mediator.Send(command);

            if (result.IsSuccessful)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("deactivate")]
        public async Task<IActionResult> DeactivateWallet([FromBody] string id)
        {
            var command = new DeactivateWalletCommand { WalletId = id };
            var result = await _mediator.Send(command);

            if (result.IsSuccessful)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("{code}")]
        public async Task<IActionResult> UpdateWallet(string code, [FromBody] UpdateWalletDTO updateWalletDTO)
        {
            var command = new UpdateWalletCommand
            {
                WalletCode = code,
                UpdateAmount = updateWalletDTO.Amount,
                Currency = updateWalletDTO.CurrencyId
            };

            try
            {
                await _mediator.Send(command);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WalletDTO>> GetWalletById(Guid id)
        {
            var command = new GetWalletByIdQuery { Id = id };
            var wallet = await _mediator.Send(command);
            if (wallet == null || !wallet.Active)
            {
                return NotFound("Wallet not found");
            }

            var dto = new WalletDTO()
            {
                Id = wallet.Id.ToString(),
                Code = wallet.Code,
                Currency = wallet.Currency?.CurrencyCode,
                Type = wallet.Type.TypeName,
                Amount = wallet.Amount,
                MainWallet = wallet.MainWallet
            };

            return Ok(dto);
        }

        [HttpGet("getwallets/{userId}")]
        public async Task<ActionResult<List<WalletDTO>>> GetWallets(Guid userId)
        {
            var query = new GetWalletsQuery { UserId = userId };
            var wallets = await _mediator.Send(query);

            if (wallets == null)
            {
                return NotFound("Wallets not found");
            }

            var dtos = new List<WalletDTO>();

            foreach (var wallet in wallets)
            {
                var dto = new WalletDTO()
                {
                    Id = wallet.Id.ToString(),
                    Code = wallet.Code,
                    Currency = wallet.Currency?.CurrencyCode,
                    Type = wallet.Type.TypeName,
                    Amount = wallet.Amount,
                    MainWallet = wallet.MainWallet
                };
                dtos.Add(dto);
            }

            return Ok(dtos.OrderByDescending(x => x.MainWallet));
        }
    }
}