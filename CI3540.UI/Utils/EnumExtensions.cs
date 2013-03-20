using System.ComponentModel;
using System.Reflection;

namespace CI3540.UI.Utils
{
    public static class EnumExtensions
    {
        // Taken from Stackoverflow
        // http://stackoverflow.com/questions/2650080/how-to-get-c-sharp-enum-description-from-value

        public static string DescriptionAttr<T>(this T source)
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) 
                return attributes[0].Description;
            
            return source.ToString();
        }
    }
}