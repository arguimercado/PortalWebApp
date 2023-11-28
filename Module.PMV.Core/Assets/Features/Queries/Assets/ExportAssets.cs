using Module.PMV.Core.Assets.Contracts.Assets;
using Module.PMV.Core.Assets.Features.DTOs.Assets.Request;

namespace Module.PMV.Core.Assets.Features.Queries.Assets;

public static class ExportAssets
{
    public record Query(FilterAssetRequest AssetParam) : IRequest<Result<IEnumerable<object>>>;

    public class QueryHandler : IRequestHandler<Query, Result<IEnumerable<object>>>
    {
        private readonly IAssetDataService _assetDataService;

        public QueryHandler(IAssetDataService assetDataService)
        {
            _assetDataService = assetDataService;
        }
        public async Task<Result<IEnumerable<object>>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {

                var data = await _assetDataService.GetInternalDynamics(request.AssetParam.AssetCode,
                                 request.AssetParam.Category,
                                 request.AssetParam.SubCategory,
                                 request.AssetParam.Brand,
                                 request.AssetParam.CompanyCode,
                                 string.IsNullOrEmpty(request.AssetParam.Status) ? null : request.AssetParam.Status,
                                 request.AssetParam.Fields);

                return Result.Ok(data);
            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}







