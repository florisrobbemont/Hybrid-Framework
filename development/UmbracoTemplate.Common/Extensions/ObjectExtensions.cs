namespace UmbracoTemplate.Common.Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNumeric(this object str)
        {
            return Strings.IsNumeric(str);
        }
    }
}