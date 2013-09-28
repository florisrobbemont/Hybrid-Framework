using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Eksponent.CropUp;
using Umbraco.Core.Models;
using Umbraco.Web;
using UmbracoTemplate.Web.Models;

namespace UmbracoTemplate.Web.Extensions
{
    public static class DampExtensionMethods
    {
        /// <summary>
        /// Return the media item.
        /// </summary>
        public static MediaItem GetMediaItem(this IPublishedContent content, string alias, string altAlias = "", string placeholder = "")
        {
            //Get all media items from DAMP.
            var dampModel = content.GetPropertyValue<DAMP.PropertyEditorValueConverter.Model>(alias);

            if (!dampModel.Any)
            {
                if (!string.IsNullOrEmpty(placeholder))
                {
                    return new MediaItem()
                    {
                        Alt = "Placeholder",
                        Url = placeholder
                    };
                }

                return new MediaItem();
            }

            //Get the first media item from DAMP.
            var dampMedia = dampModel.First;

            //Return the media item with the properties set.
            return new MediaItem()
            {
                Url = dampMedia.Url,
                Alt = !string.IsNullOrEmpty(altAlias) ? dampMedia.GetProperty(altAlias) : dampMedia.Alt,
                TrackLabel = !string.IsNullOrEmpty(dampMedia.GetProperty("trackLabel")) ? dampMedia.GetProperty("trackLabel") : "Media"
            };
        }

        /// <summary>
        /// Return a croped image based on the width and height.
        /// </summary>
        public static MediaItemCrop GetCroppedImage(this IPublishedContent content, string alias, int width, int? height = null, string cropAlias = "", 
                                                    int? quality = null, bool slimmage = false, string placeholder = "", string altAlias = "")
        {
            //Get all media items from DAMP.
            var dampModel = content.GetPropertyValue<DAMP.PropertyEditorValueConverter.Model>(alias);

            if (!dampModel.Any)
            {
                if (!string.IsNullOrEmpty(placeholder))
                {
                    return new MediaItemCrop()
                    {
                        Alt = "Placeholder",
                        Url = placeholder,
                        Crop = placeholder
                    };
                }

                return new MediaItemCrop();
            }

            //Get the first media item from DAMP.
            var dampMedia = dampModel.First;

            //Return the media item with the properties set.
            return new MediaItemCrop()
            {
                Url = dampMedia.Url,
                Alt = !string.IsNullOrEmpty(altAlias) ? dampMedia.GetProperty(altAlias) : dampMedia.Alt,
                Crop = CropUpExtendedUrl(dampMedia.Url, new ImageSizeArguments() { Width = width, Height = height, CropAlias = cropAlias }, "cropUpZoom=true" + (slimmage ? "&slimmage=true" : string.Empty) + (quality != null ? "&quality=" + quality : null)),
                TrackLabel = !string.IsNullOrEmpty(dampMedia.GetProperty("trackLabel")) ? dampMedia.GetProperty("trackLabel") : "Media"
            };
        }

        /// <summary>
        /// Return all media items.
        /// </summary>
        public static IEnumerable<MediaItem> GetMediaItems(this IPublishedContent content, string alias, string altAlias = "")
        {
            //Get all media items from DAMP.
            var dampModel = content.GetPropertyValue<DAMP.PropertyEditorValueConverter.Model>(alias);

            if (!dampModel.Any())
            {
                //Return an empty IEnumerable if DAMP doesn't have any media items.
                return Enumerable.Empty<MediaItem>();
            }

            //Return the media items with the properties set.
            return
            (
                from m in dampModel
                select new MediaItem()
                {
                    Url = m.Url,
                    Alt = !string.IsNullOrEmpty(altAlias) ? m.GetProperty(altAlias) : m.Alt,
                    TrackLabel = !string.IsNullOrEmpty(m.GetProperty("trackLabel")) ? m.GetProperty("trackLabel") : "Media"
                }
            );
        }

        /// <summary>
        /// Return all images with crop based on the width and height.
        /// </summary>
        public static IEnumerable<MediaItemCrop> GetCroppedImages(this IPublishedContent content, string alias, int width, int? height = null, string cropAlias = "", int? quality = null, bool slimmage = false, string altAlias = "")
        {
            //Get all media items from DAMP.
            var dampModel = content.GetPropertyValue<DAMP.PropertyEditorValueConverter.Model>(alias);

            if (!dampModel.Any())
            {
                //Return an empty IEnumerable if DAMP doesn't have any media items.
                return Enumerable.Empty<MediaItemCrop>();
            }

            //Return the media items with the properties set.
            return
            from m in dampModel
            select new MediaItemCrop()
            {
                Url = m.Url,
                Alt = !string.IsNullOrEmpty(altAlias) ? m.GetProperty(altAlias) : m.Alt,
                Crop = CropUpExtendedUrl(m.Url, new ImageSizeArguments() { Width = width, Height = height, CropAlias = cropAlias }, "cropUpZoom=true" + (slimmage ? "&slimmage=true" : string.Empty) + (quality != null ? "&quality=" + quality : null)),
                TrackLabel =
                    !string.IsNullOrEmpty(m.GetProperty("trackLabel")) ? m.GetProperty("trackLabel") : "Media"
            };
        }


        /// <summary>
        /// The crop up extended url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="sizeArgs">
        /// The size args.
        /// </param>
        /// <param name="additionalVars">
        /// The additional query string variables
        /// </param>
        /// <returns>
        /// The full CropUp media item url
        /// </returns>
        private static string CropUpExtendedUrl(string url, ImageSizeArguments sizeArgs, string additionalVars = "")
        {
            // Check if the url returned by CropUp contains a ? so that the additional variables are appended correctly
            var cropUpUrl = CropUp.GetUrl(url, sizeArgs);
            var queryStringSymbol = cropUpUrl.IndexOf('?') == -1 ? "?" : "&";
            if (!string.IsNullOrEmpty(additionalVars))
            {
                return cropUpUrl + queryStringSymbol + additionalVars;
            }

            return cropUpUrl;
        }
    }
}