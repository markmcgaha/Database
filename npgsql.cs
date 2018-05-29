using System;
using System.Collections.Generic;
using System.Data;
using Npgsql;

public static partial class Database
{
    public static int ExecuteNonQuery(String connection, String sql, List<NpgsqlParameter> parameterList, CommandType cmdType = CommandType.Text)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(connection))
        {
            conn.Open();
            return ExecuteNonQuery(conn, sql, parameterList, cmdType);
        }
    }

    public static DataTable ExecuteReader(String connection, String sql, List<NpgsqlParameter> parameterList, CommandType cmdType = CommandType.Text, DataReaderDelegate fn = null)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(connection))
        {
            conn.Open();
            return ExecuteReader(conn, sql, parameterList, cmdType, fn);
        }
    }

    public static Object ExecuteScalar(String connection, String sql, List<NpgsqlParameter> parameterList, CommandType cmdType = CommandType.Text)
    {
        using (NpgsqlConnection conn = new NpgsqlConnection(connection))
        {
            conn.Open();
            return ExecuteScalar(conn, sql, parameterList, cmdType);
        }
    }
}