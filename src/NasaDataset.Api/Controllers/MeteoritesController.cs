using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using NasaDataset.Application.Common.Models;
using NasaDataset.Application.Meteorites.Dtos;
using NasaDataset.Application.Meteorites.Interfaces;
using NasaDataset.Application.Meteorites.Requests.GetGroupedMeteorites;
using NasaDataset.Application.Meteorites.Requests.GetMeteorites;

namespace NasaDataset.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MeteoritesController : Controller
    {
        private readonly IGetFilterOptionsHandler _filterOptionsHandler;
        private readonly IGetMeteoritesHandler _getMeteoritesHandler;
        private readonly IGetGroupedMeteoritesHandler _getGroupedMeteoritesHandle;

        public MeteoritesController(
            IGetFilterOptionsHandler filterOptionsHandler,
            IGetMeteoritesHandler getMeteoritesHandler,
            IGetGroupedMeteoritesHandler getGroupedMeteoritesHandle
            )
        {
            _filterOptionsHandler = filterOptionsHandler;
            _getMeteoritesHandler = getMeteoritesHandler;
            _getGroupedMeteoritesHandle = getGroupedMeteoritesHandle;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<GetMeteoriteDto>>> Get([FromQuery] GetMeteoritesRequest request, CancellationToken cancellationToken)
        {

            var result = await _getMeteoritesHandler.HandleAsync(request, cancellationToken);
            return Ok(result);

        }

        [HttpGet("filters")]
        public async Task<ActionResult<MeteoriteFilterOptionsDto>> GetFilterOptions(CancellationToken cancellationToken)
        {
            var options = await _filterOptionsHandler.HandleAsync(cancellationToken);
            return Ok(options);

        }


        [HttpGet("groups")]
        public async Task<ActionResult<MeteoriteFilterOptionsDto>> GetGroups([FromQuery] GetGroupMeteoritRequest request, CancellationToken cancellationToken)
        {
            var options = await _getGroupedMeteoritesHandle.HandleAsync(request, cancellationToken);
            return Ok(options);

        }
    }
}
