using System.Runtime.InteropServices;
using System.Text;

namespace LoginSystem.IO.Flatfile
{
    /// <summary>
    /// Simple way to get in put information into an INI file.
    /// This could be way better, but this is not one of my focus.
    /// </summary>
    class IniParser
    {
        [DllImport("kernel32.dll")]
        private static extern int WritePrivateProfileString(string applicationName, string keyName, string strValue, string fileName);
        [DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string applicationName, string keyName, string defaultValue, StringBuilder returnString, int nSize, string fileName);

        /// <summary>
        /// Allows you to write a value to an Ini file under a section already created.
        /// </summary>
        /// <param name="sectionName">Name of the section to write under EX: [Smallxmac]</param>
        /// <param name="keyName">Name of the key you will be writing to EX: Name </param>
        /// <param name="keyValue">The value you will be writing under that key.</param>
        /// <param name="fileName">Name of the file you will be writing to.</param>
        public static void WriteValue(string sectionName, string keyName, string keyValue, string fileName)
        {
            WritePrivateProfileString(sectionName, keyName, keyValue, fileName);
        }

        /// <summary>
        /// Allows you to read a value to an Ini file under a section already created.
        /// </summary>
        /// <param name="sectionName">Name of the section to read under EX: [Smallxmac]</param>
        /// <param name="keyName">Name of the key you will be reading from EX: Name</param>
        /// <param name="fileName">Name of the file you will be reading from.</param>
        /// <returns>Returns the value of the key you provided.</returns>
        public static string ReadValue(string sectionName, string keyName, string fileName)
        {
            StringBuilder szStr = new StringBuilder(255);
            GetPrivateProfileString(sectionName, keyName, "", szStr, 255, fileName);
            return szStr.ToString().Trim();
        }
    }
}
