namespace UmbracoTemplate.Web.Models
{
    public class MediaItemCrop : MediaItem
    {
        public string Crop { get; set; }
        public override bool HasValue
        {
            get
            {
                return !string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(Alt) && !string.IsNullOrEmpty(Crop);
            }
        }
    }
}