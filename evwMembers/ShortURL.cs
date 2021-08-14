using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace evwMembers
{
    public class ShortURL
    {

        /// <summary>
        /// This is a "homebrewed" URL shortener.
        /// It just strips the vowels out of the URL to make it shorter, while maintaining the format.
        /// This could be improved upon in any number of ways in the future. I implemented something simple for time's sake.
        /// </summary>
        /// <param name="URL">The URL to shorten.</param>
        /// <returns>A shortened URL</returns>
       public String CreateShortURL(String URL)
        {
            String sUrl = URL;
            sUrl = sUrl.ToLower();
            sUrl = sUrl.Replace("a", "");
            sUrl = sUrl.Replace("e", "");
            sUrl = sUrl.Replace("i", "");
            sUrl = sUrl.Replace("o", "");
            sUrl = sUrl.Replace("u", "");
            sUrl = sUrl.Replace("y", "");
            return sUrl;
        }

    }
}