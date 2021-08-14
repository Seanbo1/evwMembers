using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace evwMembers
{
    public partial class MemberAdd : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the page load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
              
        }

        /// <summary>
        /// Handles the Add Member button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;

            // Scrape the URL to get H1, H2, and H3 content...
            WebScraper ws = new WebScraper();
            ws.Scrape(txtURL.Text);

            string H1 = ws.H1;
            string H2 = ws.H2;
            string H3 = ws.H3;
            string Keywords = ws.Keywords;

            // Create a "short" URL from the full URL.
            ShortURL su = new ShortURL();
            string tinyURL = su.CreateShortURL(txtURL.Text);
            Debug.Print("Tiny URL = " + tinyURL);

            //Insert the new Member.
            AppLogic insert = new AppLogic();
            insert.AddNewMember(name, txtURL.Text, tinyURL, H1, H2, H3, Keywords);
            
            // Redirect back to the Members page.
            Response.Redirect("~/Members.aspx");
        }
    }
}