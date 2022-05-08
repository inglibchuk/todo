namespace ToDo.Core.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string? target)
    {
        return string.IsNullOrWhiteSpace(target);
    }

    public static bool IsNotNullOrEmpty(this string target)
    {
        return !target.IsNullOrEmpty();
    }
}