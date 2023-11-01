using BaseEntityPack.Core;
using Core.PMV.Assets.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using Asset.Core.Models.Assets.Entities;
using Asset.Core.Notifications.Assets;

namespace Asset.Core.Models.Assets;

public class InternalAsset : AggregateRoot<int>
{

    public static InternalAsset Update(
                                    int id,
                                    int cid,
                                    string subCatCode,
                                    string description,
                                    string brandCode,
                                    string model,
                                    int year,
                                    string color,
                                    string plateNo,
                                    string engineNo,
                                    string chasisNo,
                                    DateTime? firstRegDate,
                                    DateTime? purchaseDate,
                                    float netValue,
                                    string vendorCode,
                                    string companyCode,
                                    string managedBy,
                                    string lpoNo,
                                    int kmPerHr,
                                    int conditionRank,
                                    int status,
                                    string rentOwned,
                                    string rateType,
                                    float rate,
                                    bool? modifiedAsset,
                                    string remarks,
                                    string? accountCategory,
                                    float rateOfDepreciation,
                                    string? itemCode,
                                    float purchaseAmount,
                                    DateTime? dateOfSelling,
                                    float soldAmount,
                                    float parkingArea,
                                    string employeeCode)
    {

        return new InternalAsset {
            SlNo = id,
            Cid = cid,
            SubCatCode = subCatCode,
            AssetDesc = description,
            BrandCode = brandCode,
            Model = model,
            Year = year,
            Color = color,
            PlateNo = plateNo,
            EngineNo = engineNo,
            ChasisNo = chasisNo,
            FirstRegDate = firstRegDate,
            PurchaseDate = purchaseDate,
            NetValue = netValue,
            VendorCode = vendorCode,
            CompanyCode = companyCode,
            ManagedBy = managedBy,
            LPONo = lpoNo,
            KmPerHr = kmPerHr,
            ConditionRank = conditionRank,
            Status = status,
            RentOrOwned = rentOwned,
            RateType = rateType,
            Rate = rate,
            ModifiedAsset = modifiedAsset ?? false,
            Remarks = remarks,
            AccountCategory = accountCategory,
            RateOfDepreciation = rateOfDepreciation,
            ItemCode = itemCode,
            PurchaseAmount = purchaseAmount,
            DateOfSelling = dateOfSelling,
            SoldAmount = soldAmount,
            ParkingArea = parkingArea,
            UpdatedAt = DateTime.Now,
            UpdatedBy = employeeCode
        };
    }

    public static InternalAsset Create(
                                    int cid,
                                    string subCatCode,
                                    int assetNo,
                                    string description,
                                    string brandCode,
                                    string model,
                                    int year,
                                    string color,
                                    string plateNo,
                                    string engineNo,
                                    string chasisNo,
                                    DateTime? firstRegDate,
                                    DateTime? purchaseDate,
                                    float netValue,
                                    string vendorCode,
                                    string companyCode,
                                    string managedBy,
                                    string lpoNo,
                                    int kmPerHr,
                                    int conditionRank,
                                    int status,
                                    string rentOwned,
                                    string rateType,
                                    float rate,
                                    bool? modifiedAsset,
                                    string remarks,
                                    string? accountCategory,
                                    float rateOfDepreciation,
                                    string? itemCode,
                                    float purchaseAmount,
                                    DateTime? dateOfSelling,
                                    float soldAmount,
                                    float parkingArea,
                                    string employeeCode)
    {

        var assetNumber = assetNo + 1;
        var assetCode = $"{subCatCode}{assetNumber.ToString("000")}";

        return new InternalAsset
        {
            SlNo = 0,
            Cid = cid,
            AssetCode = assetCode,
            SubCatCode = subCatCode,
            AssetNo = assetNumber,
            AssetDesc = description,
            BrandCode = brandCode,
            Model = model,
            Year = year,
            Color = color,
            PlateNo = plateNo,
            EngineNo = engineNo,
            ChasisNo = chasisNo,
            FirstRegDate = firstRegDate,
            PurchaseDate = purchaseDate,
            NetValue = netValue,
            VendorCode = vendorCode,
            CompanyCode = companyCode,
            ManagedBy = managedBy,
            LPONo = lpoNo,
            KmPerHr = kmPerHr,
            ConditionRank = conditionRank,
            Status = status,
            RentOrOwned = rentOwned,
            RateType = rateType,
            Rate = rate,
            ModifiedAsset = modifiedAsset ?? false,
            Remarks = remarks,
            AccountCategory = accountCategory,
            RateOfDepreciation = rateOfDepreciation,
            ItemCode = itemCode,
            PurchaseAmount = purchaseAmount,
            DateOfSelling = dateOfSelling,
            SoldAmount = soldAmount,
            ParkingArea = parkingArea,
            CreatedAt = DateTime.Now,
            CreatedBy = employeeCode
        };
    }


    public InternalAsset() : base(0)
    {

    }

    


    public void InsertAdditional(string owner,
        string operatedBy,
        string hireMethod,
        string dimension,
        DateTime? warranty,
        DateTime? registrationExpiry,
        DateTime? insurance,
        string warrantyPath,
        string regPath,
        string insurancePath,
        string warrantyName,
        string regName,
        string insuranceName,
        string assetType)
    {
        AssetAdditional = AssetAdditional.Instance(AssetCode, owner, operatedBy,
           hireMethod, dimension, warranty, registrationExpiry, insurance,
           warrantyPath, regPath, insurancePath, warrantyName, regName, insuranceName, assetType);
    }

    public int SlNo { get; set; }
    public int Cid { get; set; }
    public string SubCatCode { get; set; } = string.Empty;
    public int AssetNo { get; set; }
    public string AssetCode { get; set; } = string.Empty;
    public string? AssetDesc { get; set; } = null;
    public string BrandCode { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? Color { get; set; }
    public string? PlateNo { get; set; }
    public string? EngineNo { get; set; }
    public string? ChasisNo { get; set; }
    public DateTime? FirstRegDate { get; set; }
    public DateTime? PurchaseDate { get; set; }
    public float NetValue { get; set; }
    public string VendorCode { get; set; } = string.Empty;
    public string CompanyCode { get; set; } = string.Empty;
    public string ManagedBy { get; set; } = string.Empty;
    public string LPONo { get; set; } = string.Empty;
    public string? DeliveryNote { get; set; } = string.Empty;
    public int KmPerHr { get; set; }
    public int ConditionRank { get; set; }
    public int Status { get; set; }
    public string StatusDescription { get; set; } = string.Empty;
    public int TankCapacity { get; set; }
    public string GetStatusDesc()
    {
        var StatusDesc = "";

        if (Status == 0)
        {
            StatusDesc = "Inactive";
        }
        else if (Status == 1)
        {
            StatusDesc = "Active";
        }
        else if (Status == 2)
        {
            StatusDesc = "Sold";
        }
        else if (Status == 3)
        {
            StatusDesc = "Traded-In";
        }
        else if (Status == 4)
        {
            StatusDesc = "Scrapped";
        }

        return StatusDesc;

    }

    public string RentOrOwned { get; set; } = string.Empty;
    public string RateType { get; set; } = string.Empty;
    public float Rate { get; set; }
    public float DefaultHour { get; set; } = 10;
    public bool? ModifiedAsset { get; set; }
    public string Remarks { get; set; } = string.Empty;
    public string AccountCategory { get; set; } = string.Empty;
    public float RateOfDepreciation { get; set; }
    public float AccountDepreciation { get; set; }
    public string ItemCode { get; set; } = string.Empty;
    public float PurchaseAmount { get; set; }
    public float ParkingArea { get; set; } = 0f;

    public DateTime? DateOfSelling { get; set; }

    public float SoldAmount { get; set; }

    public bool Completed { get; set; }
    public string? ApprovedBy { get; set; }

    public DateTime? ApprovedAt { get; set; }

    public AssetCategory Category { get; set; }

    public AssetAdditional AssetAdditional { get; private set; }

    public AssetRecord AssetRecord { get; set; } = new();

    public List<OperatorDriver> Drivers { get; set; } = new();

    private List<ServiceAlert> _dues = new();
    public IEnumerable<ServiceAlert> Dues => _dues.ToList();

    private List<AssetDocument> _documents = new();
    public IEnumerable<AssetDocument> Documents => _documents.ToList();

    public void Update(int cid,
                        string subCatCode,
                        string description,
                        string brandCode,
                        string model,
                        int year,
                        string color,
                        string plateNo,
                        string engineNo,
                        string chasisNo,
                        DateTime? firstRegDate,
                        DateTime? purchaseDate,
                        float netValue,
                        string vendorCode,
                        string companyCode,
                        string managedBy,
                        string lpoNo,
                        int kmPerHr,
                        int conditionRank,
                        int status,
                        string rentOwned,
                        string rateType,
                        float rate,
                        bool? modifiedAsset,
                        string remarks,
                        string? accountCategory,
                        float rateOfDepreciation,
                        string? itemCode,
                        float purchaseAmount,
                        DateTime? dateOfSelling,
                        float soldAmount,
                        float parkingArea,
                        string employeeCode)
    {
        var oldKmPerHr = this.KmPerHr;

        Cid = cid;
        SubCatCode = subCatCode;
        AssetDesc = description;
        BrandCode = brandCode;
        Model = model;
        Year = year;
        Color = color;
        PlateNo = plateNo;
        EngineNo = engineNo;
        ChasisNo = chasisNo;
        FirstRegDate = firstRegDate;
        PurchaseDate = purchaseDate;
        NetValue = netValue;
        VendorCode = vendorCode;
        CompanyCode = companyCode;
        ManagedBy = managedBy;
        LPONo = lpoNo;
        KmPerHr = kmPerHr;
        ConditionRank = conditionRank;
        Status = status;
        RentOrOwned = rentOwned;
        RateType = rateType;
        Rate = rate;
        ModifiedAsset = modifiedAsset ?? false;
        Remarks = remarks;
        AccountCategory = accountCategory;
        RateOfDepreciation = rateOfDepreciation;
        ItemCode = itemCode;
        PurchaseAmount = purchaseAmount;
        DateOfSelling = dateOfSelling;
        SoldAmount = soldAmount;
        ParkingArea = parkingArea;
        UpdatedBy = employeeCode;

        AddDomainEvent(new OnAssetUpdateNotification(this, oldKmPerHr, kmPerHr));
    }

   

    public void AddDocuments(AssetDocument document)
    {
        _documents.Add(document);
    }

    public void AddAdditional(AssetAdditional additional)
    {
        AssetAdditional = additional;
    }


    public void AddDue(ServiceAlert serviceDue)
    {
        _dues.Add(serviceDue);
    }

    public void AddDues(IEnumerable<ServiceAlert> serviceDues)
    {
        _dues = serviceDues.ToList();
    }
    public void UpdateSMU(int newSMUReading)
    {
        KmPerHr = newSMUReading;
        if (Dues.Count() > 0)
        {
            foreach (var due in Dues)
            {
                due.CurrentSMUReading = newSMUReading;
                due.Tracker = "u";
            }
        }
    }

    #region UpdateDue Overload
    public void UpdateDue(string dueId, int lastReading, DateTime? lastServiceDate, int kmAlert, int kmInterval, string remarks, string updatedBy)
    {

        var due = _dues.FirstOrDefault(d => d.Id == Guid.Parse(dueId));

        if (due is not null)
        {
            due.LastSMUReading = lastReading;
            if (lastServiceDate.HasValue)
            {
                due.LastServiceDate = lastServiceDate.Value;
            }
            due.Remarks = remarks;
            due.KmAlert = kmAlert;
            due.KmInterval = kmInterval;
            due.UpdatedBy = updatedBy;
            due.Tracker = "u";
        }
    }

    public void UpdateDue(string dueId, int lastReading, DateTime? lastServiceDate)
    {

        var due = _dues.FirstOrDefault(d => d.Id == Guid.Parse(dueId));

        if (due is not null)
        {
            due.LastSMUReading = lastReading;
            if (lastServiceDate.HasValue)
            {
                due.LastServiceDate = lastServiceDate.Value;
            }

            due.Tracker = "u";
        }
    }
    #endregion

    public void RemoveFromDue(string dueId)
    {
        var due = _dues.FirstOrDefault(d => d.Id == Guid.Parse(dueId));
        if (due is not null)
        {
            due.Tracker = "d";
        }
    }

    public void AddDocument(AssetDocument document)
    {
        _documents.Add(document);
    }

    [NotMapped]
    public string FullDesc => $"{AssetCode} - {AssetDesc}";
}


