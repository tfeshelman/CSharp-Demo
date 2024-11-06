using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.CodeDom.Compiler;

namespace EldenRingInfo
{
    /// <summary>
    /// Sets up how the data, taken from the database, is assigned to accessible variables, 
    /// as well as cleaning up the data a tad
    /// </summary>
    internal class clsDetails
    {
        /// <summary>
        /// Assigns variables for weapon items
        /// </summary>
        /// <param name="_weaponID"></param>
        /// <param name="_weaponName"></param>
        /// <param name="_weaponDescription"></param>
        /// <param name="_weaponType"></param>
        /// <param name="_weaponImage"></param>
        /// <param name="_weaponAttack"></param>
        /// <param name="_weaponDefence"></param>
        public clsDetails(string _weaponID, string _weaponName, string _weaponDescription, string _weaponType, string _weaponImage, string _weaponAttack = "", string _weaponDefence = "")
        {
            weaponID = _weaponID;
            weaponName = Clean(_weaponName);
            weaponDescription = Clean(_weaponDescription);
            weaponType = _weaponType;
            weaponImage = _weaponImage;
            weaponAttack = _weaponAttack.Replace("'","");
            weaponDefence = _weaponDefence.Replace("'","");

        }
        /// <summary>
        /// Assigns variables for location (based on number of overloads)
        /// </summary>
        /// <param name="_locationImage"></param>
        /// <param name="_locationName"></param>
        /// <param name="_locationDescription"></param>
        public clsDetails(string _locationImage, string _locationName, string _locationDescription)
        {
            locationImage = _locationImage;
            locationName = Clean(_locationName);
            locationDescription = Clean(_locationDescription);
        }

        // Access variables - Locations

        public string locationImage { get; }
        public string locationName { get; }
        public string locationDescription { get; }


        // Access variables - Weapons
        public string weaponID { get; }
        public string weaponName { get; }
        public string weaponDescription { get; }
        public string weaponType { get; }
        public string weaponImage { get; }
        public string weaponAttack { get; }
        public string weaponDefence { get; }

        /// <summary>
        /// Custom regex item for cleaning
        /// </summary>
        /// <param name="strIn">Incoming string to be cleaned</param>
        /// <returns></returns>
        private static string Clean(string strIn)
        {            
            return Regex.Replace(strIn, @"[^\w\.\'\s@-]", "");
        }

    }
}
