using Applications.UseCases.PMV.GroupAlerts.Interfaces;
using Asset.Core.Contracts.Assets;
using Asset.Core.DTOs.Assets;
using Asset.Core.Models.Assets.Entities;
using WebApp.SharedServer.Errors;

namespace Asset.Core.Features.Commands.Assets;

public static class CreateServiceAlert
{
    public record Command(int AssetId,string GroupAlertId,string EmployeeCode) : IRequest<Result<IEnumerable<ServiceAlertResponse>>>;

    public class CommandHandler : IRequestHandler<Command, Result<IEnumerable<ServiceAlertResponse>>>
    {
        private readonly IServiceAlertDataService _dataService;
        private readonly IAssetDataService _assetDataService;
        private readonly IGroupAlertRepository _groupAlertRepository;

        public CommandHandler(
            IServiceAlertDataService dataService,
            IAssetDataService assetDataService,
            IGroupAlertRepository groupAlertRepository)
        {
            _dataService = dataService;
            _assetDataService = assetDataService;
            _groupAlertRepository = groupAlertRepository;
            
        }
        public async Task<Result<IEnumerable<ServiceAlertResponse>>> Handle(Command request, CancellationToken cancellationToken)
        {
            try
            {
                var group = await _groupAlertRepository.FindById(Guid.Parse(request.GroupAlertId));

                if(group is null)
                    throw new NotFoundException(request.GroupAlertId);

                var asset = await _assetDataService.GetInternal(request.AssetId);

                foreach(var detail in group.Details)
                {
                    if(!asset.Dues.Any(d => d.GroupId == detail.Id.ToString()))
                    {
                        var serviceDue = new ServiceAlert(asset.SlNo,
                            detail.Id.ToString(),
                            detail.ServiceAlertId,
                            detail.Name,
                            asset.KmPerHr,
                            asset.KmPerHr,
                            detail.KmAlert,
                            detail.KmInterval,
                            request.EmployeeCode);
                        serviceDue.SMU = detail.SMU;
                        
                        await _dataService.CreateServiceAlert(serviceDue);
                        asset.AddDue(serviceDue);
                    } 
                }

                var dueResponse = asset.Dues.Select(m => new ServiceAlertResponse
                {
                    Code = m.Code,
                    Name = m.Name,
                    CurrentSMUReading = m.CurrentSMUReading,
                    GroupId = m.GroupId,
                    Id = m.Id.ToString(),
                    AlertDue = m.AlertDue,
                    IntervalDue = m.IntervalDue,
                    KmAlert = m.KmAlert,
                    KmInterval = m.KmInterval,
                    SMU = m.SMU,
                    LastServiceDate = m.LastServiceDate,
                    LastSMUReading = m.LastSMUReading,
                    Source = m.Source,
                    Status = m.Status
                }).OrderBy(c => c.Code)
                .ToList();

                return Result.Ok<IEnumerable<ServiceAlertResponse>>(dueResponse);

            }
            catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
    }
}
