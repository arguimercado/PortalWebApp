namespace WebApp.UILibrary.Providers;

public static class IconProvider
{
    public static string GetIconMenu(string name)
    {

        switch (name)
        {
            case "meter":
                return "speed";
            case "fuel":
                return "local_gas_station";
            case "asset":
                return "commute";
            case "car":
                return "directions_car";
            case "group_alert":
                return "warning";
            case "external-asset":
                return "commute";
            case "internal-asset":
                return "commute";
            case "timesheet":
                return "pending_actions";
            case "manage":
                return "source";
            case "report":
                return "description";
            case "dashboard":
                return "dashboard";
            case "back":
                return "arrow_back_ios";
            case "next":
                return "arrow_forward_ios";
            case "post":
                return "send";
            case "print":
                return "print";
            case "search":
                return "filter_alt";
            case "addnew":
                return "add_circle_outline";
            case "save":
                return "save";
            case "delete":
                return "delete";
            case "cancel":
                return "close";
            case "pdf":
                return "picture_as_pdf";
            case "export_excel":
                return "pivot_table_chart";
            case "upload":
                return "upload_file";
            default:
                return "home";

        }
    }
}
