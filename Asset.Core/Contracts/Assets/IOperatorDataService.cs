using Asset.Core.Models.Assets.Entities;

namespace Asset.Core.Contracts.Assets
{
    public interface IOperatorDataService
    {   
        Task SaveOperator(OperatorDriver driver);
        Task<IEnumerable<OperatorDriver>> GetOperator(string assetCode);
        
    }
}
