using Module.PMV.Core.Assets.Contracts.Assets;

namespace Module.PMV.Core.Assets.Features.Queries.Assets;

public class GetNewAssetNo
{

    public record Query(string SubCategory) : IRequest<Result<string>>;

    public class QueryHandler : IRequestHandler<Query, Result<string>>
    {
        private readonly IAssetDataService _assetService;

        public QueryHandler(IAssetDataService assetService)
        {
            _assetService = assetService;
        }

        public async Task<Result<string>> Handle(Query request, CancellationToken cancellationToken)
        {
            try
            {   
                var assetNo = await _assetService.GetInternalAssetNo(request.SubCategory);
                assetNo = assetNo + 1; //increment by 1
                var assetCode = $"{request.SubCategory}{assetNo.ToString("000")}";
                
                return Result.Ok(assetCode);

            }
            catch (Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
