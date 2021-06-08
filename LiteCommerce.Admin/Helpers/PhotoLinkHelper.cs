using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin
{
    public class PhotoLinkHelper
    {
        public static string photoLink(string text)
        {
            string[] arrListStr = text.Split('&');
            return arrListStr[0];
        }
    }
}