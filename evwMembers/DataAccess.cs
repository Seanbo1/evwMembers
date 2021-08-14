using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace evwMembers
{
    public class DataAccess
    {
        // Change the connection string here for connecting to the database for the entire Web App.
        private String _connection = "Server=tcp:evwmembers.database.windows.net,1433;Initial Catalog = evwMembers; Persist Security Info=False;User ID = evw123456; Password=evwConnect1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout = 30;";

        /// <summary>
        /// Executes a SQL query and returns the result in a DataTable.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <returns></returns>
        public DataTable DoQueryToDataTable(String query)
        {
            try
            {
                DataTable dt = new DataTable();
                System.Data.SqlClient.SqlConnection myConnection = new SqlConnection(_connection);
                {
                    SqlCommand myCommand = new SqlCommand(query, myConnection);
                    {
                        SqlDataAdapter da = new SqlDataAdapter(myCommand);
                        {
                            da.Fill(dt);
                        }
                    }

                }

                return dt;
            }
           catch (Exception ex)
            {
                Debug.Print("In DoQueryToDataTable, caught error: " + ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Executes a Stored Procedure
        /// </summary>
        /// <param name="storedProcedureName">The name of the Stored Procedure</param>
        /// <param name="parameters">A Key-Value list of Parameters</param>
        public void ExecuteStoredProcedure(String storedProcedureName, Dictionary<string, object> parameters)
        {
            System.Data.SqlClient.SqlConnection myConnection = new SqlConnection(_connection);
            {
                SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        foreach (KeyValuePair<string, object> kvp in parameters)
                            cmd.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                    }

                    myConnection.Open();
                    cmd.ExecuteNonQuery();
                    myConnection.Close();
                }
            }
        }

        /// <summary>
        /// Executes a Stored Procedure and returns a DataTable contianing the result.
        /// </summary>
        /// <param name="storedProcedureName">The stored procedure name.</param>
        /// <param name="parameters">The stored procedure parameters.</param>
        /// <returns></returns>
        public DataTable ExecuteStoredProcedureToDataTable(String storedProcedureName, Dictionary<string, object> parameters)
        {
            DataTable dt = new DataTable();
            System.Data.SqlClient.SqlConnection myConnection = new SqlConnection(_connection);
            {
                SqlCommand cmd = new SqlCommand(storedProcedureName, myConnection);
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        if (parameters != null)
                        {
                            foreach (KeyValuePair<string, object> kvp in parameters)
                                cmd.Parameters.Add(new SqlParameter(kvp.Key, kvp.Value));
                        }

                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }


    }
}
