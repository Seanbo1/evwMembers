using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using OpenScraping;
using OpenScraping.Config;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace evwMembers
{

    /// <summary>
    /// Leverages: https://github.com/microsoft/openscraping-lib-csharp
    /// </summary>
    public class WebScraper
    {

        /// <summary>
        /// The extracted H1 header
        /// </summary>
       public string H1 { get; set; }
       
        /// <summary>
        /// The extracted H2 header
        /// </summary>
        public string H2 { get; set; }
       
        /// <summary>
        /// The extracted H3 header
        /// </summary>
        public string H3 { get; set; }

        /// <summary>
        /// The keywords combined from the extracted H1, H2, and H3 headers.
        /// </summary>
       public string Keywords { get; set; }

        /// <summary>
        /// Scrapes the prodived URL, extracting the H1, H2, and H3 Headers.
        /// </summary>
        /// <param name="URL"></param>
        public void Scrape(string URL) 
           {

            H1 = "";
            H2 = "";
            H3 = "";
            Keywords = "";

            var configJson = @"
            {
                'h1': '//h1',
                'h2': '//h2',
                'h3': '//h3'
            }
            ";

            var config = StructuredDataConfig.ParseJsonString(configJson);

            GetHTMLFromWebPage getHtml = new GetHTMLFromWebPage();
            var html = getHtml.GetHtmlPage(URL);
           
            if (html != null)
            {
                if (html.Length > 0)
                {
                    var openScraping = new StructuredDataExtractor(config);
                    var scrapingResults = openScraping.Extract(html);
                    
                    try
                    {
                        H1 = (string)scrapingResults["h1"].ToString();
                    }
                    catch
                    {

                    }

                    try
                    {
                        H2 = (string)scrapingResults["h2"].ToString();
                    }
                    catch
                    {

                    }

                    try
                    {
                        H3 = (string)scrapingResults["h3"].ToString();
                    }
                    catch 
                    { 

                    }
                   
                    CreateKeywords();

                    H1 = CleanHeader(H1);
                    H2 = CleanHeader(H2);
                    H3 = CleanHeader(H3);

                    Debug.Print("H1 = " + H1);
                    Debug.Print("H2 = " + H2);
                    Debug.Print("H3 = " + H3);
                    Debug.Print("Keywords = " + Keywords);
                }
            }
            
        }


        /// <summary>
        /// Used to clean up the Header content.
        /// </summary>
        /// <param name="h">The header to clean up</param>
        /// <returns>The cleaned header</returns>
        private string CleanHeader(string h)
        {
            h = h.Replace("\"", "");
            h = h.Replace("[", "");
            h = h.Replace("]", "");
            h = h.Replace(",", ", ");
            h = h.Replace("\r", " ");
            h = h.Replace("\n", " ");
            h = h.Replace("    ", " ");
            h = h.Replace("   ", " ");
            h = h.Replace("  ", " ");
            h = h.Trim();
            return h;
        }

        /// <summary>
        /// Combines H1, H2, and H3, and cleans up the content a bit.
        /// </summary>
        private void CreateKeywords()
        {
            Keywords = "";
            string kw = H1 + " " + H2 + " " + H3;
            kw = kw.Replace(",", " ");
            kw = kw.Replace("\r", " ");
            kw = kw.Replace("\n", " ");
            kw = kw.Replace("\"", " ");
            kw = kw.Replace("[", "");
            kw = kw.Replace("]", "");
            kw = kw.Replace("    ", " ");
            kw = kw.Replace("   ", " ");
            kw = kw.Replace("  ", " ");
            kw = kw.Trim();
            Keywords = kw;
        }


        /// <summary>
        /// Test harness for the openScraping web scraper to verify it works.
        /// </summary>
        public void ScrapeTestHarness()
        {

            var configJson = @"
            {
                'h1': '//h1',
                'h2': '//h2',
                'h3': '//h3'
            }
            ";

            var config = StructuredDataConfig.ParseJsonString(configJson);
            var html = "<html><body><h1>Article title</h1><h2>Article subtitle</h2><h3>Article sub-subtitle</h3><div class='article'>Article contents</div></body></html>";
            var openScraping = new StructuredDataExtractor(config);
            var scrapingResults = openScraping.Extract(html);
            Debug.Print("H1 = " + scrapingResults["h1"]);
            Debug.Print("H2 = " + scrapingResults["h2"]);
            Debug.Print("H3 = " + scrapingResults["h3"]);
        }



    }
}