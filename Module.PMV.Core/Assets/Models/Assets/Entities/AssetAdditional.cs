namespace Module.PMV.Core.Assets.Models.Assets.Entities;

public class AssetAdditional : Entity<Guid>
{
    public static AssetAdditional Instance(string assetCode, string owner, string operatedBy,
        string hireMethod, string dimension, DateTime? warranty,
        DateTime? registrationExpiry, DateTime? insurance,
        string warrantyPath, string regPath, string insurancePath,
        string warrantyName,
        string regName, string insuranceName, string assetType) =>
        new(Guid.NewGuid(), assetCode, owner, operatedBy, hireMethod, dimension,
            warranty, registrationExpiry, insurance, warrantyPath,
            regPath, insurancePath, warrantyName, regName, insuranceName, assetType);

    private AssetAdditional() : base(Guid.NewGuid()) { }
    private AssetAdditional(Guid guid, string assetCode, string owner, string operatedBy,
        string hireMethod, string dimension, DateTime? warranty,
        DateTime? registrationExpiry, DateTime? insurance,
        string warrantyPath, string regPath, string insurancePath,
        string warrantyName,
        string regName, string insuranceName, string assetType) : base(guid)
    {
        AssetCode = assetCode;
        Owner = owner;
        OperatedBy = operatedBy;
        HireMethod = hireMethod;
        Dimension = dimension;
        Warranty = warranty;
        RegistrationExpiry = registrationExpiry;
        Insurance = insurance;
        WarrantyPath = warrantyPath;
        RegPath = regPath;
        InsurancePath = insurancePath;
        WarrantyName = warrantyName;
        RegName = regName;
        InsuranceName = insuranceName;
        AssetType = assetType;
    }


    public string AssetCode { get; set; } = string.Empty;

    public string Owner { get; set; } = string.Empty;

    public string OperatedBy { get; set; } = string.Empty;

    public string HireMethod { get; set; } = string.Empty;

    public string Dimension { get; set; } = string.Empty;

    public DateTime? Warranty { get; set; }

    public DateTime? RegistrationExpiry { get; set; }

    public DateTime? Insurance { get; set; }

    public string WarrantyPath { get; set; } = string.Empty;

    public string RegPath { get; set; } = string.Empty;

    public string InsurancePath { get; set; } = string.Empty;

    public string WarrantyName { get; set; } = string.Empty;

    public string RegName { get; set; } = string.Empty;

    public string InsuranceName { get; set; } = string.Empty;

    public string AssetType { get; set; } = string.Empty;

}
