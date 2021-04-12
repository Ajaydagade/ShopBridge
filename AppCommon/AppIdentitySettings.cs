using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopBridgeApp.AppCommon
{
    public class AppIdentitySettings
    {
        public APISettings APISettings { get; set; }
    }

    public class APISettings
    {
        public string Url { get; set; }
    }
}
