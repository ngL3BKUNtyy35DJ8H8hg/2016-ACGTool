using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BDTCLib
{
    public class BaseMyFile
    {
        public string KEY_NAME;
        public string VALUE_NAME;
        public string PATH_FILE;
        public string FULLPATH_FILE;

        public BaseMyFile()
        {
        }

        public BaseMyFile(string key, string value)
        {
            KEY_NAME = key;
            VALUE_NAME = value;
            PATH_FILE = value;
            try
            {
                FULLPATH_FILE = Path.GetFullPath(PATH_FILE);
            }
            catch (Exception)
            {
                FULLPATH_FILE = "";
            }

        }
    }
}
