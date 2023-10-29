using Asset.Core.Models.Assets.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Core.DTOs.Assets
{
    public class InternalAssetRequest
    {
        public int Cid { get; set; }
        public string SubCatCode { get; set; } = string.Empty;
        public string? AssetDesc { get; set; } = null;
        public string? BrandCode { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public string? PlateNo { get; set; }
        public string? EngineNo { get; set; }
        public string? ChasisNo { get; set; }
        public DateTime? FirstRegDate { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public float NetValue { get; set; }
        public string? VendorCode { get; set; }
        public string? CompanyCode { get; set; }
        public string? ManagedBy { get; set; }
        public string? LPONo { get; set; }
        public string? DeliveryNote { get; set; }
        public int KmPerHr { get; set; }
        public int ConditionRank { get; set; }
        public int Status { get; set; } = 1;
        public string? RentOrOwned { get; set; }
        public string? RateType { get; set; }
        public float Rate { get; set; }
        public bool? ModifiedAsset { get; set; }
        public string? Remarks { get; set; }
        public string? AccountCategory { get; set; }
        public float RateOfDepreciation { get; set; }
        public float AccountDepreciation { get; set; }
        public string? ItemCode { get; set; }
        public float PurchaseAmount { get; set; }
        public DateTime? DateOfSelling { get; set; }
        public float SoldAmount { get; set; }
        public bool Completed { get; set; }
        public AssetAdditional? Additional { get; set; } = new();
        public float ParkingArea { get; set; }
        public int TankCapacity { get; set; } = 0;
    }
}
