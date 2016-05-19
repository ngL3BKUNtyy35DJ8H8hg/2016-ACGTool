using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ACGTool.Classes.LinqToSql
{
    public class RepositoryClass
    {
        //Using for AddObject store procedure
        public string _ADDCODEHERE1 = "//ADD CODE HERE 1\n";
        public string _ADDCODEHERE2 = "//ADD CODE HERE 2\n";
        public string _ADDCODEHERE3 = "//ADD CODE HERE 3\n";
        public string _ADDCODEHERE4 = "//ADD CODE HERE 4\n";
        public string _ADDCODEHERE5 = "//ADD CODE HERE 5\n";
        public string _ADDCODEHERE6 = "//ADD CODE HERE 6\n";

        public DataTable _db;
        public string _dbName;

        public RepositoryClass(string dbName, DataTable db)
        {
            _dbName = dbName;
            _db = db;
        }

       

        

        public string GetAttribute(string name)
        {
            return "_" + name;
        }

        public virtual string InitClass()
        {
            return String.Empty;
        }

        public virtual string DeclareAttribute(string type, string name, string des)
        {
            return string.Empty;
        }

        public virtual string DeclareProperty(string type, string name, string des)
        {
            return string.Empty;
        }

        public virtual string CreateClass()
        {
            return string.Empty;
        }

        public virtual string CreateRepositoryClass()
        {
            return string.Empty;
        }
    }


}
