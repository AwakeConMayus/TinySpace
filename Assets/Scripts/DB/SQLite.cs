using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;

public class SQLite : MonoBehaviour
{
    private const string database_name = "my_db";

    public string db_connection_string;
    public IDbConnection db_connection;

    public SQLite()
    {
        db_connection_string = "URI=file:" + Application.persistentDataPath + "/" + database_name;
        Debug.Log("db_connection_string" + db_connection_string);
        db_connection = new SqliteConnection(db_connection_string);
        db_connection.Open();
    }

    ~SQLite()
    {
        db_connection.Close();
    }

    public IDbCommand getDbCommand()
    {
        return db_connection.CreateCommand();
    }


    public IDataReader getAllData(string table_name)
    {
        IDbCommand dbcmd = db_connection.CreateCommand();
        dbcmd.CommandText =
            "SELECT * FROM " + table_name;
        IDataReader reader = dbcmd.ExecuteReader();
        return reader;
    }

    protected IDataReader GetRowUsingKeyStringByString(string tableName, string s, string keyName)
    {
        IDbCommand dbcmd = db_connection.CreateCommand();
        dbcmd.CommandText = "SELECT * FROM " + tableName + " WHERE " + keyName + " = " + s;
        IDataReader reader = dbcmd.ExecuteReader();
        return reader;
    }

    public void close()
    {
        db_connection.Close();
    }
}
