using Radzen;

namespace WebApp.UILibrary.Components.Buttons
{
    public enum EnumButtonType
    {
        Search,
        Save,
        SaveAsDraft,
        SaveAsPost,
        Add,
        Multiple,
        Edit,
        View,
        Delete,
        Pdf,
        Excel,
        Cancel,
        Upload,
        Download,
        Custom
    }
    public record ButtonCustStyle
    {
        public string Icon { get; set; } = "";
        public string Title { get; set; } = "";
        public ButtonStyle Style { get; set; } = new();
    }
}
