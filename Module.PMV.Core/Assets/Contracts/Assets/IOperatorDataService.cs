using Module.PMV.Core.Assets.Models.Assets.Entities;

namespace Module.PMV.Core.Assets.Contracts.Assets;

public interface IOperatorDataService
{
    Task SaveOperator(OperatorDriver driver);
    Task<IEnumerable<OperatorDriver>> GetOperators(string assetCode);

    Task<OperatorDriver?> FindOperatorAsync(int id);

}
