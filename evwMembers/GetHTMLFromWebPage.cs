using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Text;

namespace evwMembers
{
    public class GetHTMLFromWebPage
    {

        /// <summary>
        /// Gets the HTML contents of a Web Page
        /// </summary>
        /// <param name="strURL">The URL to get.</param>
        /// <returns>A String containing the HTML from the Web Page. If the web page can't be returned for any reason, this will return empty string.</returns>
        public string GetHtmlPage(string strURL)
        {
            String strResult = "";

            try
            {

                // Input checks...
                if (strURL is null) return strResult;
                if (strURL.Length == 0) return strResult;

                System.Net.WebResponse objResponse;

                ServicePointManager.SecurityProtocol = ServicePointManager.SecurityProtocol | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                System.Net.HttpWebRequest objRequest = (HttpWebRequest)System.Net.HttpWebRequest.Create(strURL);
                objRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.134 Safari/537.36";  //Spoof to look like a regular 'ole browser...

                objResponse = objRequest.GetResponse();
                using (System.IO.StreamReader sr = new System.IO.StreamReader(objResponse.GetResponseStream(), Encoding.UTF8))
                {
                    strResult = sr.ReadToEnd();
                    sr.Close();
                }
            }
           catch
            {
                strResult = "";  //If anything goes wrong during the http request, we will just continue and return an empty string.
            }
            return strResult;
        }
    }
}