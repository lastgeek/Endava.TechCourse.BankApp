﻿using Endava.TechCourse.BankApp.Application.Commands.CreateWallet;
using Endava.TechCourse.BankApp.Application.Commands.DeleteWallet;
using Endava.TechCourse.BankApp.Application.Queries.GetWallets;
using Endava.TechCourse.BankApp.Infrastracture.Persistance;
using Endava.TechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWallet(Guid id)
        {
            var command = new DeleteWalletCommand { WalletId = id };
            var result = await _mediator.Send(command);

            if (result)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WalletDTO>> GetWalletById(Guid id)
        {
            var command = new GetWalletByIdQuery { Id = id };
            var wallet = await _mediator.Send(command);
            if (wallet == null)
            {
                return NotFound();
            }

            var dto = new WalletDTO()
            {
                Id = wallet.Id.ToString(),
                Currency = wallet.Currency.CurrencyCode,
                Type = wallet.Type,
                Amount = wallet.Amount,
            };

            return Ok(dto);
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