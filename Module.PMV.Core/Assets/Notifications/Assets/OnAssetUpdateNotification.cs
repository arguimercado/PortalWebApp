using Module.PMV.Core.Assets.Models.Assets;

namespace Module.PMV.Core.Assets.Notifications.Assets;

public record OnAssetUpdateNotification(InternalAsset Request, int OldKmHr, int NewKmHr) : INotification;

