using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;
using System.Data;

namespace evwMembers
{
    public partial class MemberDetail : System.Web.UI.Page
    {

        /// <summary>
        /// Handles the page load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildPage();
        }


        /// <summary>
        /// Builds the MemberDetail page.
        /// </summary>
        private void BuildPage()
        {
            GetProfile(Request.QueryString["id"]);
            GetFriends(Request.QueryString["id"]);
        }

        /// <summary>
        /// Gets the Member profile.
        /// </summary>
        /// <param name="id"></param>
        private void GetProfile(string id)
        {
            AppLogic app = new AppLogic();
            DataTable dt = app.GetMemberProfile(id);

            if (dt != null)
            {
                Debug.Print("Dynamically render the DataTable model to the litProfile HTML view..");
                litProfile.Text = RenderMemberProfileHtmlView(dt);
            }
        }

        /// <summary>
        /// Gets the Member's friends.
        /// </summary>
        /// <param name="id"></param>
        private void GetFriends(string id)
        {
            AppLogic app = new AppLogic();
            DataTable dt = app.GetMemberFriends(id);

            if (dt != null)
            {
                Debug.Print("Dynamically render the DataTable model to the litProfile HTML view..");
                litFriends.Text = RenderMemberFriendsHtmlView(dt);
            }
        }


        /// <summary>
        /// Renders the Html for the Member's friends view.
        /// </summary>
        /// <param name="dt">A DataTable containing the Member's Friends.</param>
        /// <returns></returns>
        private string RenderMemberFriendsHtmlView(DataTable dt)
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
                                row["name"] = "<a href='" + "./MemberDetail.aspx?id=" + row["relChild"] + "'>" + row["name"] + "</a>";
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
        /// Renders the Member profile Html view
        /// </summary>
        /// <param name="dt">A DataTable containing the Member Profile.</param>
        /// <returns></returns>
        private string RenderMemberProfileHtmlView(DataTable dt)
        {
            String t = "";
            
            if (dt != null)
            {
                if (dt.Rows != null)
                {
                    if (dt.Rows.Count == 1)
                    {

                        foreach (DataRow row in dt.Rows)
                        {
                            t = "<table>";

                            //Name
                            t += "<tr>";
                            t += "<td>Member Name:</td><td>" + row["name"] + "</td>";
                            t += "</tr>";

                            //Website
                            t += "<tr>";
                            t += "<td>Website:</td><td>" + "<a href='" + row["website"] + "'>" + row["website"] + "</a>" + "</td>";
                            t += "</tr>";

                            //Short URL
                            t += "<tr>";
                            t += "<td>Short URL:</td><td>" + "<a href='" + row["website"] + "'>" + row["shortURL"] + "</a>" + "</td>";
                            t += "</tr>";

                            //H1
                            t += "<tr>";
                            t += "<td>Heading 1:</td><td>" + row["H1"] + "</td>";
                            t += "</tr>";

                            //H2
                            t += "<tr>";
                            t += "<td>Heading 2:</td><td>" + row["H2"] + "</td>";
                            t += "</tr>";

                            //H3
                            t += "<tr>";
                            t += "<td>Heading 3:</td><td>" + row["H3"] + "</td>";
                            t += "</tr>";

                            //Friends Count
                            t += "<tr>";
                            t += "<td>Friends:</td><td>" + row["Friends"] + "</td>";
                            t += "</tr>";

                            t += "</table>";
                        }
                    }
                }
            }

            return t;
        }

        /// <summary>
        /// Handles the Add Friend button click event.
        /// Redirects the user to the MemberFriendAdd page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddFriend_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/MemberFriendAdd.aspx?id=" + Request.QueryString["id"]);
        }

        /// <summary>
        /// Handles the Directory button click event.
        /// Redirects the user to the main Member directorty page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDirectory_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Members.aspx");
        }

        /// <summary>
        /// Handles the Search button click event.
        /// Redirects the user to the Search page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Search.aspx?id=" + Request.QueryString["id"] + "&query=" + txtSearch.Text);
        }
    }
}

