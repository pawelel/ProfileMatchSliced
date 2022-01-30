using System;
using System.Collections.Generic;
using System.Globalization;


namespace ProfileMatch.Models.Entities
{

    public class CultureSwitcherModel
    {
        public CultureInfo CurrentUICulture { get; set; }
        public List<CultureInfo> SupportedCultures { get; set; }
    }
}
