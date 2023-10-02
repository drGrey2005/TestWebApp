using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestRestApplication.Models;
using TestWebApp.Data.Repositories;
using TestWebApp.Models;
using TestWebApp.Models.Dto;

namespace TestWebApp.Controllers;

[ApiController]
[Route("/api/JsonTest")]
public class JsonTestController : ControllerBase
{
    private readonly IInfoDataRepository _infoDataRepository;
    private readonly IMapper _mapper;

    public JsonTestController(IInfoDataRepository infoDataRepository, IMapper mapper)
    {
        _infoDataRepository = infoDataRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Создание списка объектов в базе данных
    /// </summary>
    /// <param name="createDtoData">Коллекция формируемых объектов</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<InfoDto>> CreateJsonData([FromBody] IEnumerable<InfoDto>? createDtoData)
    {
        if (createDtoData == null)
        {
            return BadRequest();
        }

        await _infoDataRepository.SaveInfoData(createDtoData.OrderBy(d => d.Code).Select((d, idx) => new Info()
        {
            Code = d.Code,
            Value = d.Value,
            SerialNumber = idx + 1
        }));
        return Ok();
    }

    /// <summary>
    /// Получение списка объектов
    /// </summary>
    /// <param name="pageNumber">Номер получаемой страницы</param>
    /// <param name="pageSize">Размер страницы</param>
    /// <returns></returns>
    [HttpGet("{pageNumber}/{pageSize}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<InfoDataModelView>> GetJsonData(int pageNumber, int pageSize)
    {
        InfoDataModelView infoDataModelView = new InfoDataModelView()
        {
            InfoData = _mapper.Map<IEnumerable<InfoDto>>(
                await _infoDataRepository.GetAll(isTracked: false, pageNumber: pageNumber, pageSize: pageSize)),
            TotalCount = await _infoDataRepository.GetTotalCount()
        };

        return Ok(infoDataModelView);
    }
}