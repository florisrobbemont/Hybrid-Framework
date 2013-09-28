namespace UmbracoTemplate.Web.ViewModels.Custom
{
    public class UrlPicker
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public bool NewWindow { get; set; }
        public string Target
        {
            get
            {
                return NewWindow ? "_blank" : "_self";
            }
        }
    }
}