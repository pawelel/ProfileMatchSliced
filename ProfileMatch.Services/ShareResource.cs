using Microsoft.Extensions.Localization;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProfileMatch.Services
{
    public class ShareResource
    {
        private readonly IStringLocalizer _localizer;

        public ShareResource(IStringLocalizerFactory factory)
        {
            var type = typeof(LanguageService);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("LanguageService", assemblyName.Name);
        }

        public LocalizedString GetKey(string key)
        {
            return _localizer[key];
        }
        public static bool IsEn()
        {
            return CultureInfo.CurrentCulture.ToString().Contains("en") || string.IsNullOrEmpty(CultureInfo.CurrentCulture.ToString());
        }
        public static string GetString(object t, string property)
        {
            var language = CultureInfo.CurrentCulture.ToString();
            if (language is not null and not "en")
            {
                foreach (var p in t.GetType().GetProperties().Where(p => p.Name.ToLower().Contains(property.ToLower() + language) && !string.IsNullOrWhiteSpace((string)p.GetValue(t))))
                {
                    return (string)p.GetValue(t);
                }
            }
            foreach (var p in typeof(object).GetProperties().Where(p => p.Name == property))
            {
                return (string)p.GetValue(t);
            }
            return "";
        }
    }

}
