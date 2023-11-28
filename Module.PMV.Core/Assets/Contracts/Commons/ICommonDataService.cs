using WebApp.SharedServer.Commons;

namespace Module.PMV.Core.Assets.Contracts.Commons;

public interface ICommonDataService
{
    Task<IEnumerable<SelectItem>> GetSelections(string type);
}
