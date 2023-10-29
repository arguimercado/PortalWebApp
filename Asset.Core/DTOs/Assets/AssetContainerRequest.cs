namespace Asset.Core.DTOs.Assets;

public class AssetContainerRequest
{
    public InternalAssetRequest? Internal { get; set; } = new();
    public ExternalAssetRequest? External { get; set; } = new();

    public string AssetType { get; set; } = "internal";
}
