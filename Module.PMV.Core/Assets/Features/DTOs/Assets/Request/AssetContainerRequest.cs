namespace Module.PMV.Core.Assets.Features.DTOs.Assets.Request;

public class AssetContainerRequest
{
    public InternalAssetRequest? Internal { get; set; } = new();
    public ExternalAssetRequest? External { get; set; } = new();

    public string AssetType { get; set; } = "internal";
}
