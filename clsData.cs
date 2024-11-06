using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.IO;

namespace EldenRingInfo
{
    /// <summary>
    /// Class that sets up how the application will talk to the database
    /// </summary>
    internal class clsData
    {
        // Calls the db connection string
        string _strConnect = clsGlobalSettings.DBConnectionString;
        
        // Readys a string for SQL queries
        string _strSQL = "";

        // Data table for storage
        DataTable dtData;

        // Get/Set Connection info
        public string ConnString
        {
            get { return _strConnect; }
            set { _strConnect = value; FillDataTable(); }
        }

        // Get/Set SQL statement
        public string SQL
        {
            get { return _strSQL; }
            set { _strSQL = value; FillDataTable(); }
        }

        // Publicly exposed datatable
        public DataTable dt
        {
            get { return dtData; }
            set { dtData = value; }
        }

        /// <summary>
        /// Actually queries the db, then fills the dataset, then the datatable
        /// </summary>
        private void FillDataTable()
        {
            // Test Connection
            if(ConnString != "" && SQL != "") 
            {
                // Establish connection
                OleDbConnection conn = new OleDbConnection(ConnString);

                // Open connection
                conn.Open();

                //Create Dataset
                DataSet ds = new DataSet();

                // Fill w/ adapter
                OleDbDataAdapter adapter = new OleDbDataAdapter(SQL, ConnString);
                adapter.Fill(ds);

                // Close connection
                conn.Close();

                // Push to datatable
                dtData = ds.Tables[0];

            }
        }

    }
}
