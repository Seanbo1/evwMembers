using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace evwMembers
{
    public partial class Search : System.Web.UI.Page
    {

        /// <summary>
        /// Handles the page load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            SearchForPotentialFriends();
        }


        /// <summary>
        /// Searches for potential friends for the Member based on the passed in keywords.
        /// </summary>
        private void SearchForPotentialFriends()
        {
            string id = Request.QueryString["id"];
            string query = Request.QueryString["query"];

            List<string> paths = DoSearch(id, query);
            RenderHtmlView(paths);
        }

        /// <summary>
        /// Exectutes the keyword search and returns the paths to potential friends.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private List<string> DoSearch(string id, string query)
        {
            //This is the search algorithm....
            Traversal graph = new Traversal();
            List<string> paths = graph.BuildGraph(id, query);
            return paths;
        }

        /// <summary>
        /// Renders the Html view for the Search results page.
        /// </summary>
        /// <param name="paths">The search results.</param>
        private void RenderHtmlView(List<string> paths)
        {
            if (paths != null)
            {
                if (paths.Count > 0)
                {
                    string lit = "<table>";
                    foreach (string p in paths)
                    {
                        lit += "<tr><td>" + p + "</td></tr>";
                    }
                    lit += "</table>";
                    litSearchResults.Text = lit;

                }
            }
        }
    }
}