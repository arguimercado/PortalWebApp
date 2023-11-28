using MediatR;
using Microsoft.AspNetCore.Mvc;
using Module.PMV.Core.Assets.Features.DTOs.Assets.Request;
using Module.PMV.Core.Assets.Features.Queries.Assets;
using WebApp.Client.Providers.Exports;

namespace WebApp.Client.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AssetController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IExportObjectManager _exportObjectManager;

    public AssetController(IMediator mediator, IExportObjectManager exportObjectManager)
    {
        _mediator = mediator;
        _exportObjectManager = exportObjectManager;
    }

    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery]FilterAssetRequest request)
    {
        var results = await _mediator.Send(new ExportAssets.Query(request));
        if (!results.IsSuccess) {
            return BadRequest(results.Errors[0]);
        }

        var exportResults = await _exportObjectManager
                                    .GenerateExcelDynamic(results.Value.ToList(), $"asset_export_{Guid.NewGuid()}");

        return File(exportResults!.RawData, exportResults.ContentType, exportResults.FileName);
    }
}
