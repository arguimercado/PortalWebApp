using Asset.Core.Contracts.Assets;
using Asset.Core.DTOs.Assets;
using Asset.Core.Models.Assets;
using Asset.Core.Models.Assets.Entities;

namespace Asset.Core.Infrastructures.Services.Assets;

internal sealed class ServiceAlertDataService : IServiceAlertDataService
{
    private readonly ISqlQuery _sqlQuery;

    public ServiceAlertDataService(ISqlQuery sqlQuery)
    {
        _sqlQuery = sqlQuery;
    }

    public async Task CreateServiceAlert(ServiceAlert due) {

        var sql = @"IF NOT EXISTS(SELECT TOP 1 Id FROM HLMPMV.ServiceDue d WHERE d.GroupId = @groupId AND AssetId = @assetId)
		            BEGIN
			            INSERT INTO HLMPMV.ServiceDue (Id,AssetId,Name,GroupId,Code,LastSMUReading,CurrentSMUReading,KmAlert,KmInterval,SMU,Source,CreatedAt,CreatedBy) 
			            VALUES (@id,@assetId,@name,@groupId,@code,@lastSMUReading,@currentSMUReading,@kmAlert,@kmInterval,@smu,@source,@createdAt,@createdBy)
		            END";

        await _sqlQuery.DynamicExecute(sql, new {
            id = due.Id,
            assetId = due.AssetId,
            name = due.Name,
            groupId = due.GroupId,
            code = due.Code,
            lastSMUReading = due.LastSMUReading,
            currentSMUReading = due.CurrentSMUReading,
            kmAlert = due.KmAlert,
            kmInterval = due.KmInterval,
            smu = due.SMU,
            source = due.Source,
            createdAt = due.CreatedAt,
            createdBy = due.CreatedBy
        });
    }



    public async Task DeleteAlert(string dueId)
    {
        var sql = "DELETE FROM HLMPMV.ServiceDue WHERE Id=@id";
        await _sqlQuery.DynamicExecute(sql,new {id = dueId });

    }

    public Task<IEnumerable<ServiceAlert>> GetServiceAlert(int assetId)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAlert(ServiceAlert due, bool kmOnly = false)
    {
        var sql = "";
        object param = null;
        if (!kmOnly)
        {
            sql = @"UPDATE HLMPMV.ServiceDue 
                        SET LastServiceDate=@lastServiceDate,
                            CurrentSMUReading=@currentSMUReading,
                            LastSMUReading=@lastSMUReading, 
                            KmAlert=@kmAlert,
                            KmInterval=@kmInterval, 
                            Remarks=@remarks,
                            UpdatedAt=GETDATE(), 
                            UpdatedBy=@updatedBy 
                        WHERE Id=@id";
            param = new
            {
                lastServiceDate = due.LastServiceDate,
                lastSMUReading = due.LastSMUReading,
                currentSMUReading = due.CurrentSMUReading,
                kmAlert = due.KmAlert,
                kmInterval = due.KmInterval,
                remarks = due.Remarks,
                updatedBy = due.UpdatedBy,
                id = due.Id
            };
        }
        else
        {
            sql = "UPDATE HLMPMV.ServiceDue SET " +
                  "CurrentSMUReading=@currentSMUReading, " +
                  "UpdatedAt=GETDATE() " +
                  "WHERE Id=@id";

            param = new
            {
                currentSMUReading = due.CurrentSMUReading,
                id = due.Id
            };
        }

        await _sqlQuery.DynamicExecute(sql,param);

    }
}
