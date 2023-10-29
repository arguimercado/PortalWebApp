namespace WebApp.UILibrary.Commons
{

    public class SelectItem
    {
        public required string Text { get; set; }
        public required string Value { get; set; }
        public string? Type { get; set; }
        public int HoldingValueInt { get; set; }
        public string? HoldingValueStr { get; set; }

        public bool IsSelected { get; set; } = false;
    }

}
