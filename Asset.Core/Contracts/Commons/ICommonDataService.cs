using WebApp.SharedServer.Commons;

namespace Asset.Core.Contracts.Commons
{
    public interface ICommonDataService
    {
        Task<IEnumerable<SelectItem>> GetSelections(string type);
    }
}
