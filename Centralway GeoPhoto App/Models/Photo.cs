using FlickrNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Centralway_GeoPhoto_App.Models
{
    public class Photo
    {
        public string ImageURL { get; set; }

        public Photo(string url)
        {
            this.ImageURL = url;
        }

        public static Photo FromFlickrPhoto(FlickrNet.Photo photo)
        {
            return new Photo(photo.OriginalUrl);
        }
    }
}