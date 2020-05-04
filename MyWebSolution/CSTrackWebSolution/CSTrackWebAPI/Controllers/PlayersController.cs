using AutoMapper;
using CSTrackWebAPI.Managers.Abstraction;
using CSTrackWebAPI.Model.DTO;
using CSTrackWebAPI.Model.DTO.View;
using CSTrackWebAPI.Model.Entities;
using CytometrixWebAPI.APISettings.Swagger;
using CytometrixWebAPI.Controllers.Base;
using CytometrixWebAPI.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CytometrixWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]

    public class PlayersController : BaseController
    {
        public PlayersController(
            IMapper mapper,
            IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
        }

        /// <summary>
        /// Get All Players
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiExplorerSettings(GroupName = APIConstants.PlayersControllerGroupName)]
        [ProducesResponseType(typeof(List<PlayerSmallDetailsDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> List()
        {
            var allPlayers = await UnitOfWork.PlayerRepository.GetAll().AsNoTracking().ToListAsync();

            var mappedEntity = Mapper.Map<List<PlayerSmallDetailsDTO>>(allPlayers);

            return Ok(mappedEntity);
        }


        /// <summary>
        /// Get Player By Identifier
        /// </summary>
        /// <param name="identifier">Player Identifier</param>
        /// <returns></returns>
        [HttpGet("{identifier}", Name = "Get Player By Identifier")]
        [ApiExplorerSettings(GroupName = APIConstants.PlayersControllerGroupName)]
        [ProducesResponseType(typeof(PlayerDetailsDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(Guid identifier)
        {
            Player player = await UnitOfWork.PlayerRepository.GetAll().AsNoTracking()
              .Where(c => c.Id.Equals(identifier)).FirstOrDefaultAsync();

            if (player == null)
            {
                return NotFound("Player not found");
            }

            var mappedEntity = Mapper.Map<PlayerDetailsDTO>(player);

            return Ok(mappedEntity);
        }

        /// <summary>
        /// Create Player
        /// </summary>
        /// <param name="model">Player Model</param>
        /// <returns></returns>
        [HttpPost]
        [ApiExplorerSettings(GroupName = APIConstants.PlayersControllerGroupName)]
        [ProducesResponseType(typeof(IdentifierResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] PlayerCreateDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.GetErrorsMessage());
            }

            var mappedEntity = Mapper.Map<Player>(model);

            await UnitOfWork.PlayerRepository.AddAsync(mappedEntity);

            await UnitOfWork.CommitAsync();

            return Ok(new IdentifierResponse() { Id = mappedEntity.Id });
        }


        /// <summary>
        /// Delete Player By Identifier
        /// </summary>
        /// <param name="identifier">Player Identifier</param>
        /// <returns></returns>
        [HttpDelete("{identifier}", Name = "Delete Player By Identifier")]
        [ApiExplorerSettings(GroupName = APIConstants.PlayersControllerGroupName)]
        [ProducesResponseType(typeof(OkResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid identifier)
        {
            Player player = await UnitOfWork.PlayerRepository.GetByIdAsync(identifier);

            if (player == null)
            {
                return NotFound("Player not found");
            }        

            await UnitOfWork.PlayerRepository.RemoveAsync(player);

            await UnitOfWork.CommitAsync();

            return Ok();
        }

    }
}