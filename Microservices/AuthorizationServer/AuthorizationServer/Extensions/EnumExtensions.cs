using System.ComponentModel;
using System.Reflection;

namespace AuthorizationServer.Extensions;

public static class EnumExtensions
{
    public static string GetDescription<T>(this T @enum)
        where T : Enum
        => typeof(T).GetField(@enum.ToString())
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description
            ?? string.Empty;
}