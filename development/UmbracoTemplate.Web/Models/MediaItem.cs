namespace UmbracoTemplate.Web.Models
{
    public class MediaItem
    {
        public string Url { get; set; }
        public string Alt { get; set; }
        public string TrackLabel { get; set; }
        public virtual bool HasValue
        {
            get
            {
                return !string.IsNullOrEmpty(Url) && !string.IsNullOrEmpty(Alt);
            }
        }
    }
}