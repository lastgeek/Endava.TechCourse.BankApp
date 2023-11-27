using Endava.TechCourse.BankApp.Application.Commands.AddWalletType;
using Endava.TechCourse.BankApp.Application.Commands.DeleteWalletType;
using Endava.TechCourse.BankApp.Application.Queries.GetWalletType;
using Endava.TechCourse.BankApp.Application.Queries.GetWalletTypeById;
using Endava.TechCourse.BankApp.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Endava.TechCourse.BankApp.Server.Controllers
{
    [Route("api/type")]
    [ApiController]
    public class WalletTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WalletTypeController(IMediator mediator)
        {
            ArgumentNullException.ThrowIfNull(mediator);
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddWalletType([FromBody] WalletTypeDTO walletTypeDTO)
        {
            var command = new AddWalletTypeCommand()
            {
                TypeName = walletTypeDTO.TypeName,
                Commision = walletTypeDTO.Commision
            };

            var result = await _mediator.Send(command);
            return result.IsSuccessful ? Ok(result) : BadRequest(result.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWalletType(Guid id)
        {
            var command = new DeleteWalletTypeCommand { WalletTypeId = id };
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
        public async Task<IActionResult> GetWalletTypeById(Guid id)
        {
            var query = new GetWalletTypeByIdQuery { WalletTypeId = id };
            var walletType = await _mediator.Send(query);

            if (walletType == null)
            {
                return NotFound();
            }

            var dto = new WalletTypeDTO()
            {
                TypeName = walletType.TypeName,
                Commision = walletType.Commision,
                CanBeRemoved = walletType.CanBeRemoved
            };

            return Ok(dto);
        }

        [HttpGet]
        public async Task<List<WalletTypeDTO>> GetWalletType()
        {
            var query = new GetWalletTypeQuery();

            var walletTypes = await _mediator.Send(query);
            var dtos = new List<WalletTypeDTO>();

            foreach (var walletType in walletTypes)
            {
                var dto = new WalletTypeDTO()
                {
                    Id = walletType.Id.ToString(),
                    TypeName = walletType.TypeName,
                    Commision = walletType.Commision,
                    CanBeRemoved = walletType.CanBeRemoved
                };
                dtos.Add(dto);
            }
            return dtos;
        }
    }
}