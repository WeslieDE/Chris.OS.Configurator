using System;

namespace Chris.OS.Configurator
{
    class Tools
    {
        public static String left(String text, String divider)
        {
            int p = text.IndexOf(divider);

            if (p <= 0)
                return text;

            return text.Substring(0, p);
        }

        public static String right(String text, String divider)
        {
            int p = text.IndexOf(divider);

            if (p <= 0)
                return text;

            return text.Substring(p+1, text.Length - (p+1));
        }
    }
}
