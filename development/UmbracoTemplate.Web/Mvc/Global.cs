using System;
using System.Web;

namespace UmbracoTemplate.Web.Mvc
{
    public class Global : Umbraco.Web.UmbracoApplication
    {
        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (custom.Equals("url", StringComparison.OrdinalIgnoreCase))
            {
                return "url=" + context.Request.Url.AbsoluteUri;
            }

            if (custom.Equals("url;device", StringComparison.OrdinalIgnoreCase))
            {
                var mobileDetection = new MobileDetection(context);
                var isSmartphone = mobileDetection.DetectSmartphone();
                return "url=" + context.Request.Url.AbsoluteUri + "&isSmartphone=" + isSmartphone;
            }

            return base.GetVaryByCustomString(context, custom);
        }
    }
}