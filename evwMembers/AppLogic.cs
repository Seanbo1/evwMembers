using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace evwMembers
{
    public class AppLogic : DataAccess
    {

        /// <summary>
        /// Gets a DataTable containing the members.
        /// </summary>
        /// <returns>A DataTable containing the members.</returns>
        public DataTable GetMembers()
        {
            
            DataTable dt = ExecuteStoredProcedureToDataTable("SelectMembers", null);
            return dt;
        }

        /// <summary>
        /// Get a single member profile.
        /// </summary>
        /// <param name="id">The id of the member to request.</param>
        /// <returns>A DataTable containing the member information.</returns>
        public DataTable GetMemberProfile(string id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("id", Convert.ToInt32(id));
            DataTable dt = ExecuteStoredProcedureToDataTable("SelectMemberProfile", parameters);

            return dt;
        }

        /// <summary>
        /// Returns the "immediate" friends of the specified Member.
        /// </summary>
        /// <param name="id">The Member id</param>
        /// <returns>A DataTable containing the member's friends.</returns>
        public DataTable GetMemberFriends(string id)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("id", Convert.ToInt32(id));
            DataTable dt = ExecuteStoredProcedureToDataTable("SelectMemberFriends", parameters);
            return dt;
        }


        /// <summary>
        /// Get a DataTable of possible friends to add to a member.
        /// Takes into account the friends the member already has.
        /// </summary>
        /// <param name="id">The id of the member.</param>
        /// <returns>A DataTable of Friends to add with associated metaData.</returns>
        public DataTable GetMemberFriendsToAdd(string id)
        {

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Id", Convert.ToInt32(id));
            DataTable dt = ExecuteStoredProcedureToDataTable("SelectMemberFriendsToAdd", parameters);
            return dt;
        }

        /// <summary>
        /// Adds a new Member to the MemberTable
        /// </summary>
        /// <param name="name">The Member Name</param>
        /// <param name="website">The Member Website</param>
        /// <param name="shortURL">The ShortURL for the Member WebSite</param>
        /// <param name="h1">Heading 1 content from the Member WebSite</param>
        /// <param name="h2">Heading 2 content from the Member WebSite</param>
        /// <param name="h3">Heading 3 content from the Member WebSite</param>
        /// <param name="keywords">Keywords from the Member WebSite</param>
        public void AddNewMember(string name, string website, string shortURL, string h1, string h2, string h3, string keywords)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("name", name);
            parameters.Add("website", website);
            parameters.Add("shortURL", shortURL);
            parameters.Add("h1", h1);
            parameters.Add("h2", h2);
            parameters.Add("h3", h3);
            parameters.Add("keywords", keywords);
            parameters.Add("friends", 0);  // On first insert, a member has no friends.

            ExecuteStoredProcedure("InsertMember", parameters);
        }


        /// <summary>
        /// Adds a new Friend by inserting into the MemberFriendsTable
        /// </summary>
        /// <param name="relParent">The Member id</param>
        /// <param name="relChild">The Friend id</param>
        public void AddNewFriend(string relParent, string relChild)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("relParent", relParent);
            parameters.Add("relChild", relChild);

            ExecuteStoredProcedure("InsertMemberFriend", parameters);
        }


    }
}