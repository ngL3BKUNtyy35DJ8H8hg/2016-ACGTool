using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BDTCLib
{
    public class MyKHCnnString : BaseMyFile
    {
        public MyKHCnnString(string key, string value)
        {
            KEY_NAME = key;
            VALUE_NAME = value;
            PATH_FILE = value.Replace("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=", "");
            try
            {
                FULLPATH_FILE = Path.GetFullPath(PATH_FILE);
            }
            catch (Exception)
            {
                FULLPATH_FILE = "";
            }
            
        }


        public bool IsExist()
        {
            bool result = false;
            if (File.Exists(FULLPATH_FILE))
                result = true;
            return result;
        }
    }
}
