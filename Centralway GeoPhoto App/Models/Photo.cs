using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Centralway_GeoPhoto_App.Models
{
    public class Photo
    {
        public string Title { get; set; }

        public string ImageURL { get; set; }

        public static Photo FromFlickrPhoto(FlickrNet.Photo photo)
        {
            return new Photo
            {
                Title = photo.Title,
                ImageURL = photo.LargeUrl
            };
        }
    }
}