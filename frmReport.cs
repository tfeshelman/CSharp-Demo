using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EldenRingInfo
{
    public partial class frmReport : Form
    {
        /// <summary>
        /// Non-overloaded report, used for the main menu
        /// </summary>
        public frmReport()
        {
            InitializeComponent();
            MainMenuReport();
        }

        /// <summary>
        /// Overloaded report, calling the correct function based on incoming data
        /// </summary>
        /// <param name="frmType">Determines which report we are running</param>
        /// <param name="itemRef">References which item we are asking about</param>
        /// <param name="Attack">Incoming array for easier csv split</param>
        /// <param name="Defence">Same idea as the Attack param</param>
        public frmReport(string frmType, string itemRef, String[] Attack = null, String[] Defence = null)
        {
            InitializeComponent();
            if (frmType == "weapon") { WeaponReport(itemRef, Attack, Defence); }
        }

        /// <summary>
        /// Load the report with details about the weapon selected
        /// </summary>
        /// <param name="itemRef">Details about the item (per the id from the database)</param>
        /// <param name="lstIncomingA">Attack array</param>
        /// <param name="lstIncomingD">Defence array</param>
        public void WeaponReport(string itemRef, String[] lstIncomingA, String[] lstIncomingD)
        {
            lblFormName.Text = "ADDITIONAL DETAILS";

            // Querying db based on incoming id
            clsData WeaponDetail = new clsData();
            WeaponDetail.SQL = "SELECT * FROM tblWeapons WHERE id='" + itemRef + "'";

            // Setting up html - Head
            string strReportHeader;
            strReportHeader = "<html><head><title>Weapon Details</title>";
            strReportHeader += "<style type='text/css'>";
            strReportHeader += "h1, h3, #list, p { margin:0; color:white; font-family:'Raleway', sans-serif; font-weight:lighter }"; // REEEEALLY wished this worked
            strReportHeader += "#list { width:100%; }";
            strReportHeader += "#list td, #list th { border: 1px solid #ddd; padding: 8px; font-size:16px; }";
            strReportHeader += "#list tr:nth-child(even){ background-color: #f2f2f2; }";
            strReportHeader += "#list tr:hover {background-color: #ddd;}";
            strReportHeader += "#list th { padding-top: 12px; padding-bottom: 12px; text-align: left; background-color:Gainsboro;";
            strReportHeader += "white-space: pre-line";
            strReportHeader += "</style></head>";

            // Body
            string strReportBody = "<body style='background-color:GrayText'>";
            strReportBody += "<h1>" + WeaponDetail.dt.Rows[0]["name"] + "</b></i></h1>";
            strReportBody += "<hr/>";

            // Table
            strReportBody += "<table id='list'>";
            strReportBody += "<tr><td>ID</td><td>" + WeaponDetail.dt.Rows[0]["id"] + "</td></tr>";
            strReportBody += "<tr><td>Category</td><td>" + WeaponDetail.dt.Rows[0]["category"] + "</td></tr>";
            strReportBody += "<tr><td>Weight</td><td>" + WeaponDetail.dt.Rows[0]["weight"] + "</td></tr>";
            strReportBody += "<tr><td>Attack Details</td><td>";

            // Attack array, disseminated in a human-readable way
            if (lstIncomingA != null)
            {
                for (int i = 0; i < lstIncomingA.Length; i += 2)
                {
                    strReportBody += lstIncomingA[i] + " = " + lstIncomingA[i + 1] + "<br>";
                }
                strReportBody += "</td></tr>";
            }

            // Defence array, same as attack array
            if (lstIncomingD != null)
            {
                strReportBody += "<tr><td>Defence Details</td><td>";
                for (int i = 0; i < lstIncomingD.Length; i += 2)
                {
                    strReportBody += lstIncomingD[i] + " = " + lstIncomingD[i + 1] + "<br>";
                }
                strReportBody += "</td></tr>";
            }

            strReportBody += "<tr><td>Description</td><td>" + WeaponDetail.dt.Rows[0]["description"] + "</td></tr>";
            strReportBody += "</table></body>";

            // Dump to web browser object
            wbReport.DocumentText = strReportHeader + strReportBody;
        }

        /// <summary>
        /// Setting up and displaying the main menu report
        /// </summary>
        public void MainMenuReport()
        {
            // Setting up html
            string strReportHeader;
            strReportHeader = "<html><head><title>Main Menu Report</title>";
            strReportHeader += "<style type='text/css'>";
            strReportHeader += "h1, h3, #list, p { margin:0; color:white; font-family:'Raleway', sans-serif; font-weight:lighter }"; // REEEEALLY wished this worked
            strReportHeader += "#list { width:100%; }";
            strReportHeader += "#list td, #list th { border: 1px solid #ddd; padding: 8px; font-size:16px; }";
            strReportHeader += "#list tr:nth-child(even){ background-color: #f2f2f2; }";
            strReportHeader += "#list tr:hover {background-color: #ddd;}";
            strReportHeader += "#list th { padding-top: 12px; padding-bottom: 12px; text-align: left; background-color:Gainsboro;";
            strReportHeader += "</style></head>";

            string strReportBody = "<body style='background-color:GrayText'>";
            strReportBody += "<h1>Main Menu</h1>";
            strReportBody += "<hr/><p>";
            strReportBody += "Greetings, fellow tarnished! This application is to serve as a wiki of sorts, to demonstrate a bit of my programming ability " +
                "as well as some figure details about various parts of the Elden Ring universe. Click on the 'Report' button on each page for a bit of detail " +
                "about certain elements.";
            strReportBody += "</p><br><br>";
            strReportBody += "<a href=\"http://www.tylereshelman.com\" target=\"_blank\" style='color:white'>Created by Tyler Eshelman</a>";

            wbReport.DocumentText = strReportHeader + strReportBody;
        }

        /// <summary>
        /// Exit button on a label, with mouse hover event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void lblExit_MouseEnter(object sender, EventArgs e)
        {
            lblExit.ForeColor = Color.Goldenrod;
        }
        private void lblExit_MouseLeave(object sender, EventArgs e)
        {
            lblExit.ForeColor = Color.White;
        }
    }
}
