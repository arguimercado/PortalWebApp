using Module.PMV.Core.Assets.Models.GroupAlerts;

namespace Module.PMV.Core.Assets.Contracts.GroupAlerts;

public interface IGroupAlertRepository
{

    void Add(GroupAlert groupAlert);
    void Update(GroupAlert groupAlert);
    void Delete(GroupAlert groupAlert);
    Task<GroupAlert?> FindById(Guid Id);

    Task<IEnumerable<GroupAlert>> GetAlerts();
    Task<bool> IsGroupExists(string groupName);
    Task<GroupAlert?> GetAlertServiceSetup(string category);
}