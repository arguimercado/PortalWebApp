using System.Text;

namespace WebApp.SharedServer.Utilities;

public static class StringHelper
{
    public static string DefaultIf(this string value, string compare, string defaultValue = "")
    {
        if (value == compare)
            return defaultValue;

        return value;
    }

    public static DateTime? ConvertToDateTime(this string value, bool IsNullable = false)
    {


        if (string.IsNullOrEmpty(value))
            return null;

        DateTime result;

        if (!DateTime.TryParse(value, out result))
        {
            return null;
        }

        return result;

    }

    public static DateTime MergeAndConvert(this string date, string time)
    {

        return DateTime.Parse($"{date.ConvertToDateTime()!.Value.ToShortDateString()} {time}");
    }

    public static string WithSingleQuote(this string value, string symbol = "'")
    {

        if (string.IsNullOrEmpty(value)) return value;

        var items = value.Split(",");
        StringBuilder builder = new StringBuilder();

        foreach (var item in items)
        {
            builder.Append("'" + item + "'");
            builder.Append(",");
        }
        var returnItem = builder.ToString();
        return returnItem.Substring(0, returnItem.Length - 1);
    }
}

public static class DateHelper
{

    public static DateTime MergeDateTime(this DateTime date, string time)
    {

        return DateTime.Parse($"{date.ToShortDateString()} {time}");
    }
}