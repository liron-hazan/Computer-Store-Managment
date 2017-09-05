//שמות תכנתים:לירון חזן וכפיר ארגנטל

using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Store
{
    public class DbAccess //מחלקה שמטפלת בקישור בין בסיס הנתונים לתוכנית שלנו
    {
        protected OleDbConnection _conn = null;


        public DbAccess(string connectionString) //בנאי המקבל מחרוזת התחברות עם בסיס הנתונים
        {
            _conn = new OleDbConnection(connectionString);
        }


        protected void Connect()//פונקציה לפתיחת בסיס נתונים
        {
            if (_conn.State != ConnectionState.Open)
            {
                _conn.Open();
            }
        }

        protected void Disconnect()//פונקציה לסגירת בסיס נתונים
        {
            _conn.Close();
        }



        protected void ExecuteSimpleQuery(OleDbCommand command) //פונקציה לביצוע שאילתה
        {
            lock (_conn)
            {
                Connect();
                command.Connection = _conn;
                try
                {
                    command.ExecuteNonQuery();
                }
                finally
                {
                    Disconnect();
                }
            }
        }
      
        protected DataSet GetMultiplyQuery(OleDbCommand command) // ביצוע שאילתות מורכבות 
        {
            DataSet dataset = new DataSet();
            lock (_conn)
            {
                Connect();
                command.Connection = _conn;
                try
                {
                    OleDbDataAdapter adapter = new OleDbDataAdapter();
                    adapter.SelectCommand = command;
                    adapter.Fill(dataset);
                }
                finally
                {
                    Disconnect();
                }
                return dataset;
            }
        }



    }
}
