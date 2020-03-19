using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;


namespace SomerenDAL
{
    public abstract class Base
    {
        private SqlDataAdapter adapter;
        private SqlConnection conn;
        public Base()
        {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SomerenDatabase"].ConnectionString);
                adapter = new SqlDataAdapter();
        }

        protected SqlConnection OpenConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }

        private void CloseConnection()
        {
            conn.Close();
        }

        /* For Insert/Update/Delete Queries with transaction */
        protected void ExecuteEditTranQuery(String query, SqlParameter[] sqlParameters, SqlTransaction sqlTransaction)
        {
            SqlCommand command = new SqlCommand(query, conn, sqlTransaction);
            try
            {
                command.Parameters.AddRange(sqlParameters);
                adapter.InsertCommand = command;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /* For Insert/Update/Delete Queries */
        protected void ExecuteEditQuery(String query, SqlParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();

            try
            {
                command.Connection = OpenConnection();
                command.CommandText = query;
                command.Parameters.AddRange(sqlParameters);
                adapter.InsertCommand = command;
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
               // Print.ErrorLog(e);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }



        /* For Select Queries */
        protected DataTable ExecuteSelectQuery(String query, params SqlParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();
            DataTable dataTable;
            DataSet dataSet = new DataSet();

            try
            {
                command.Connection = OpenConnection();
                command.CommandText = query;
                command.Parameters.AddRange(sqlParameters);
                command.ExecuteNonQuery();
                adapter.SelectCommand = command;
                adapter.Fill(dataSet);
                dataTable = dataSet.Tables[0];
            }
            catch (SqlException ex)
            {
               // Print.ErrorLog(e);
                return null;
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return dataTable;
        }

        /* For Insert Queries */
        public void ExecuteNonQuery(String query, params SqlParameter[] sqlParameters)
        {
            SqlCommand command = new SqlCommand();

            try
            {
                command.Connection = OpenConnection();
                command.CommandText = query;
                command.Parameters.AddRange(sqlParameters);
                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                // Print.ErrorLog(e);
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }

    }
}
