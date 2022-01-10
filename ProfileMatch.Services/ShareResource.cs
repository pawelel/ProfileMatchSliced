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
    }

}
