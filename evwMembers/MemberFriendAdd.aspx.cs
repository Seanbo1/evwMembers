using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace evwMembers
{
    public partial class MemberFriendAdd : System.Web.UI.Page
    {
        /// <summary>
        /// Handles the page load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];
            GetFriendsToAdd(id);
        }

        /// <summary>
        /// Displays the members that can be added as friends.
        /// </summary>
        /// <param name="id"></param>
        private void GetFriendsToAdd(string id)
        {
            AppLogic app = new AppLogic();
            DataTable dt = app.GetMemberFriendsToAdd(id);

            if (dt != null)
            {
                //Dynamically render the DataTable model to the litFriendsToAdd HTML view...
                litFriendsToAdd.Text = RenderFriendsToAddHtmlView(dt);
            }
        }


        /// <summary>
        /// Renders the Html view of the friends to add.
        /// </summary>
        /// <param name="dt">DataTable containing the friends to add</param>
        /// <returns></returns>
        private string RenderFriendsToAddHtmlView(DataTable dt)
        {
            String t = "";
            t = "<table>";

            //Build the table header....
            t += "<tr>";
            t += "<td>Friend Name</td>";
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
     
                            t += "<td>";
                            t += "<a href='" + "./FriendAdd.aspx?id=" + Request.QueryString["id"] + "&friend=" + row["objectId"] + "'>" + row["name"] + "</a>";
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


    } 
}