using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public static partial class Database
{
    public static int ExecuteNonQuery(String connection, String sql, List<SqlParameter> parameterList, CommandType cmdType = CommandType.Text)
    {
        using (SqlConnection conn = new SqlConnection(connection))
        {
            conn.Open();
            return ExecuteNonQuery(conn, sql, parameterList, cmdType);
        }
    }

    public static DataTable ExecuteReader(String connection, String sql, List<SqlParameter> parameterList, CommandType cmdType = CommandType.Text, DataReaderDelegate fn = null)
    {
        using (SqlConnection conn = new SqlConnection(connection))
        {
            conn.Open();
            return ExecuteReader(conn, sql, parameterList, cmdType, fn);
        }
    }

    public static Object ExecuteScalar(String connection, String sql, List<SqlParameter> parameterList, CommandType cmdType = CommandType.Text)
    {
        using (SqlConnection conn = new SqlConnection(connection))
        {
            conn.Open();
            return ExecuteScalar(conn, sql, parameterList, cmdType);
        }
    }
}