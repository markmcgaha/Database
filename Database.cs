using System;
using System.Collections.Generic;
using System.Data;

public static partial class Database
{
    private static int CommandTimeout = 30;

    public delegate void DataReaderDelegate(IDataReader dataRow);

    public static int ExecuteNonQuery<T>(IDbConnection connection, string sql, List<T> parameterList, CommandType cmdType) where T : IDbDataParameter
    {
        using (IDbCommand command = connection.CreateCommand())
        {
            command.Connection = connection;
            command.CommandType = cmdType;
            command.CommandTimeout = CommandTimeout;
            command.CommandText = sql;
            parameterList.ForEach(p => { command.Parameters.Add(p); });
            return command.ExecuteNonQuery();
        }
    }

    public static DataTable ExecuteReader<T>(IDbConnection connection, string sql, List<T> parameterList, CommandType cmdType, DataReaderDelegate fn = null) where T : IDbDataParameter
    {
        DataTable result = null;
        if (fn == null)
        {
            result = new DataTable();
            fn = delegate (IDataReader dataReader)
            {
                result.Load(dataReader);
            };
        }

        using (IDbCommand command = connection.CreateCommand())
        {
            command.Connection = connection;
            command.CommandType = cmdType;
            command.CommandTimeout = CommandTimeout;
            command.CommandText = sql;
            parameterList.ForEach(p => { command.Parameters.Add(p); });
            ExecuteReader(command, fn);
        }
        return result;
    }

    public static void ExecuteReader(IDbCommand command, DataReaderDelegate fn)
    {
        using (IDataReader dataRow = command.ExecuteReader())
        {
            fn(dataRow);
        }
    }

    public static Object ExecuteScalar<T>(IDbConnection connection, string sql, List<T> parameterList, CommandType cmdType) where T : IDbDataParameter
    {
        using (IDbCommand command = connection.CreateCommand())
        {
            command.Connection = connection;
            command.CommandType = cmdType;
            command.CommandTimeout = CommandTimeout;
            command.CommandText = sql;
            parameterList.ForEach(p => { command.Parameters.Add(p); });
            return command.ExecuteScalar();
        }
    }

    public static T GetColumnValue<T>(this DataRow dataRow, int column, T defaultValue)
    {
        return (dataRow.IsNull(column)) ? defaultValue : (T)dataRow[column];
    }

    public static T GetColumnValue<T>(this DataRow dataRow, string column, T defaultValue)
    {
        return (dataRow.IsNull(column)) ? defaultValue : (T)dataRow[column];
    }

    public static T GetColumnValue<T>(this IDataReader dataReader, int column, T defaultValue)
    {
        return (dataReader.IsDBNull(column)) ? defaultValue : (T)dataReader[column];
    }

    public static T GetColumnValue<T>(this IDataReader dataReader, string column, T defaultValue)
    {
        return GetColumnValue(dataReader, dataReader.GetOrdinal(column), defaultValue);
    }
}