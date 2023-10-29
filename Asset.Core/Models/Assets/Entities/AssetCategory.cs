using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asset.Core.Models.Assets.Entities;


public class AssetCategory
{
    
    public int Cid { get; set; }


    public string CatName { get; set; } = "";
}

public class AssetSubCategory
{

    public string SubCatCode { get; set; } = "";
    public string SubCatName { get; set; } = "";
    public int Cid { get; set; }
}
