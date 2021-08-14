using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace evwMembers
{
    public partial class FriendAdd : System.Web.UI.Page
    {

        /// <summary>
        /// Handles the page load event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Add the Friend, and Redirect to the MemberDetail page.
            string member = Request.QueryString["id"];
            string friend = Request.QueryString["friend"];
            AppLogic app = new AppLogic();
            app.AddNewFriend(member, friend);
            Response.Redirect("~/MemberDetail.aspx?id=" + member);
        }
    }
}