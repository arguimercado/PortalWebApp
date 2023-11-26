using Asset.Core.Contracts.Assets;
using Asset.Core.Contracts.Commons;
using Asset.Core.Features.Queries.Assets.DTOs;

namespace Asset.Core.Features.Queries.Assets;

public static class FilterAssets 
{
    public class Query : IRequest<Result<AssetContainerResponse>>
    {
        public string AssetType { get; set; } = "";
        public string? AssetCode { get; set; }
        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public string? Brand { get; set; }
        public string? CompanyCode { get; set; }
        public string? VendorCode { get; set; }
        public string? Status { get; set; } = null;
        public string? PlateType { get; set; }
        public string? PlateNum { get; set; }
        public string? HireOrSubContract { get; set; }
        public bool IsPostBack { get; set; } = false;

        public bool IsRefresh { get; set; } = false;
    }


    public class QueryHandler : IRequestHandler<Query, Result<AssetContainerResponse>>
    {
        private readonly IAssetDataService _assetDataService;
        private readonly ICommonDataService _commonService;

        public QueryHandler(IAssetDataService assetDataService, ICommonDataService commonService)
        {
            _assetDataService = assetDataService;
            _commonService = commonService;
        }

        public Task<Result<AssetContainerResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
  


