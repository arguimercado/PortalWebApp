using Asset.Core.Models.GroupAlerts;

namespace Applications.UseCases.PMV.GroupAlerts.Interfaces;

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