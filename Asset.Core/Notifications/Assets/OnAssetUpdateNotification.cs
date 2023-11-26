using Asset.Core.Models.Assets;

namespace Asset.Core.Notifications.Assets;

public record OnAssetUpdateNotification(InternalAsset Request,int OldKmHr, int NewKmHr) : INotification;

