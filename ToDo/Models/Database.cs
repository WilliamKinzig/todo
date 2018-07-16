using System;
using MySql.Data.MySqlClient;
using ToDoList;

namespace ToDoList.Models
{
    public class DB
    {
        public static MySqlConnection Connection()
        {
            MySqlConnection conn = new MySqlConnection(DBConfiguration.ConnectionString);
            return conn;
        }
    }
}


// SELECT <expressions> FROM <tables> WHERE <conditions> AND <conditions> ORDER BY <expressions> <asc or desc>;
