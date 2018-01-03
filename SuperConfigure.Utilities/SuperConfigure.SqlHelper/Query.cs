using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace SuperConfigure.SqlHelper
{
    public class Query
    {
        static SqlConnection connection;
        public Query(string ConnectionString)
        {
            try
            {
                if (string.IsNullOrEmpty(ConnectionString))
                    throw new Exception("error: invalid connection string!");

                connection = new SqlConnection(ConnectionString);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Execute Non Query
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <param name="varilables"></param>
        public void ExecuteNonQuery(string query, CommandType commandType, Dictionary<string, object> varilables)
        {
            SqlTransaction trans = null;
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                trans = connection.BeginTransaction();

                if (varilables != null)
                {
                    foreach (var kv in varilables)
                    {
                        query = query.Replace(kv.Key, kv.Value.ToString());
                    }
                }

                using (SqlCommand cmd = new SqlCommand(query, connection, trans))
                {
                    cmd.CommandType = commandType;
                    cmd.CommandTimeout = 60;
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// Execute Query & Return Data Table Result
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <param name="varilables"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string query, CommandType commandType, Dictionary<string, object> varilables)
        {
            SqlTransaction trans = null;
            DataSet ds = new DataSet();
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                trans = connection.BeginTransaction();

                if (varilables != null)
                {
                    foreach (var kv in varilables)
                    {
                        query = query.Replace(kv.Key, kv.Value.ToString());
                    }
                }

                using (SqlCommand cmd = new SqlCommand(query, connection, trans))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 60;
                    SqlDataAdapter da = new SqlDataAdapter();

                    da.SelectCommand = cmd;
                    da.Fill(ds);
                }
                trans.Commit();
                if (ds.Tables.Count == 1)
                    return ds.Tables[0];
                else
                    return null;
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// Execute Query & Return Boolen Result
        /// </summary>
        /// <param name="query"></param>
        /// <param name="commandType"></param>
        /// <param name="varilables"></param>
        public bool ExecuteBoolen(string query, CommandType commandType, Dictionary<string, object> varilables)
        {
            SqlTransaction trans = null;bool isSuccess = false;
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                trans = connection.BeginTransaction();

                if (varilables != null)
                {
                    foreach (var kv in varilables)
                    {
                        query = query.Replace(kv.Key, kv.Value.ToString());
                    }
                }

                using (SqlCommand cmd = new SqlCommand(query, connection, trans))
                {
                    cmd.CommandType = commandType;
                    cmd.CommandTimeout = 60;
                    if (cmd.ExecuteNonQuery() > 0)
                        isSuccess = true;
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            return isSuccess;
        }
    }
}
