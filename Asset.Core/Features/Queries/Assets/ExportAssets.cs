using Asset.Core.Contracts.Assets;
using Asset.Core.Features.Queries.Assets.Filters;

namespace Asset.Core.Features.Queries.Assets;

public static class ExportAssets
{
    public record Query(FilterAssetParam AssetParam, string? Fields) : IRequest<Result<object>>;

    public class QueryHandler : IRequestHandler<Query, Result<object>>
    {
        private readonly IAssetDataService _assetDataService;

        public QueryHandler(IAssetDataService assetDataService)
        {
            _assetDataService = assetDataService;
        }
        public async Task<Result<object>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {

               var data = await _assetDataService.GetInternalDynamics(request.AssetParam.AssetCode,
                                request.AssetParam.Category,
                                request.AssetParam.SubCategory,
                                request.AssetParam.Brand,
                                request.AssetParam.CompanyCode,
                                string.IsNullOrEmpty(request.AssetParam.Status) ? null : request.AssetParam.Status,
                                request.Fields);

                return Result.Ok(data);
            }
            catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }

        }
    }
}







