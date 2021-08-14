using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace evwMembers
{
    public class AppLogicGraph : DataAccess
    {
        /// <summary>
        /// Gets the graph nodes.
        /// </summary>
        public DataTable GetGraphNodes()
        {
            DataTable dt = ExecuteStoredProcedureToDataTable("SelectNodes", null);
            return dt;
        }

        /// <summary>
        /// Gets the graph edges.
        /// </summary>
        /// <returns></returns>
        public DataTable GetGraphEdges()
        {
            DataTable dt = ExecuteStoredProcedureToDataTable("SelectEdges", null);
            return dt;
        }

        /// <summary>
        /// Gets all the graph nodes containing a keyword match.
        /// </summary>
        /// <param name="keywords"></param>
        /// <returns></returns>
        public DataTable GetNodeHits(string id, string keywords)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Id", Convert.ToInt32(id));
            parameters.Add("Keywords", keywords);
            DataTable dt = ExecuteStoredProcedureToDataTable("SelectNodeHits", parameters);
            return dt;
        }

    }
}