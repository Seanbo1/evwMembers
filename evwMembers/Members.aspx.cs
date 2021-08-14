using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace evwMembers
{

    public partial class Members : System.Web.UI.Page
    {

        /// <summary>
        /// Handles the page load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Page.IsPostBack == false)
            {
                GetMembers();
            }
        }

        /// <summary>
        /// Gets the Memebers to display on the Members page.
        /// </summary>
        private void GetMembers()
        {
            AppLogic app = new AppLogic();
            DataTable dt = app.GetMembers();
            
            if (dt != null)
            {
                //Dynamically render the DataTable model to the litMembers HTML view...
                litMembers.Text = RenderHtmlView(dt);
            }
        }

        /// <summary>
        /// Renders an Html view of the members (with associated metadata)...
        /// </summary>
        /// <param name="dt">The dataTable containing the Members / MetaData</param>
        /// <returns>The HTML to insert into the ASP.Net Literal control.</returns>
        private String RenderHtmlView(DataTable dt)
        {
            String t = "";
            t = "<table>";

            //Build the table header....
            t += "<tr>";
            t += "<td>Member Name</td>";
            t += "<td>Web Page</td>";
            t += "<td>Number of Friends</td>";
            t += "</tr>";

            if (dt != null)
            {
                if (dt.Rows != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {
                            t += "<tr>";
                            row["name"] = "<a href='" + "./MemberDetail.aspx?id=" + row["objectid"] + "'>" + row["name"] + "</a>";
                            t += "<td>";
                            t += row["name"];
                            t += "</td>";
                            t += "<td>";
                            t += "<a href='" + row["website"] + "'>" + row["shortURL"] + "</a>";
                            t += "</td>";
                            t += "<td>";
                            t += row["friends"];
                            t += "</td>";
                            t += "</tr>";
                        }
                    }
                }
            }

            t += "</table>";
            return t;
        }

        /// <summary>
        /// Handles the Add Member button click event.
        /// Navigates the user from the Member page to the MemberAdd page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            Response.Redirect("MemberAdd.aspx");
        }


    }

}