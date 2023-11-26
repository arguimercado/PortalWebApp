using Asset.Core.Models.Assets.Entities;

namespace Asset.Core.Contracts.Assets
{
    public interface IOperatorDataService
    {   
        Task SaveOperator(OperatorDriver driver);
        Task<IEnumerable<OperatorDriver>> GetOperators(string assetCode);

        Task<OperatorDriver?> FindOperatorAsync(int id);

    }
}
