using Module.PMV.Core.Assets.Contracts.GroupAlerts;
using Module.PMV.Core.Assets.Models.GroupAlerts;

namespace Module.PMV.Core.Assets.Infrastructures.Services.GroupAlerts;

internal sealed class GroupAlertRepository : IGroupAlertRepository
{
    public void Add(GroupAlert groupAlert)
    {
        throw new NotImplementedException();
    }

    public void Delete(GroupAlert groupAlert)
    {
        throw new NotImplementedException();
    }

    public Task<GroupAlert?> FindById(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GroupAlert>> GetAlerts()
    {
        throw new NotImplementedException();
    }

    public Task<GroupAlert?> GetAlertServiceSetup(string category)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsGroupExists(string groupName)
    {
        throw new NotImplementedException();
    }

    public void Update(GroupAlert groupAlert)
    {
        throw new NotImplementedException();
    }
}
