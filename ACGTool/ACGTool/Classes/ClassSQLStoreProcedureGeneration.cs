using System;
using System.Collections.Generic;
using System.Text;

namespace ACGTool.Classes
{
    class ClassSQLStoreProcedureGeneration
    {
        //Using for AddObject store procedure
        public string _ADDCODEHERE1 = "//ADD CODE HERE 1\n";
        public string _ADDCODEHERE2 = "//ADD CODE HERE 2\n";
        public string _ADDCODEHERE3 = "//ADD CODE HERE 3\n";

        //Using for add code here
        public string[] _ADDCODEHERE = {
            "//ADD CODE HERE 1\n",
            "//ADD CODE HERE 2\n",
            "//ADD CODE HERE 3\n",
            "//ADD CODE HERE 4\n",
            "//ADD CODE HERE 5\n",
            "//ADD CODE HERE 6\n",
            "//ADD CODE HERE 7\n",
            "//ADD CODE HERE 8\n",
            "//ADD CODE HERE 9\n",
            "//ADD CODE HERE 10\n",
            "//ADD CODE HERE 11\n",
        };

        
        
        public string AddTab(int num)
        {
            string tab = "";
            for (int i = 0; i < num; i++)
                tab += "\t";
            return tab;
        }

        public string InitClass(string tablename)
        {
            //tablename.Remove(0, 3) dung de bo di string "tbl" truoc cac ten bang
            string str;
            //Get
            str = "-- =============================================\n";
            str += "-- Author: Hoàng Tuấn\n";
            str += "-- Create date: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "\n";
            str += "-- Description: Lấy dữ liệu của bảng " + tablename + " theo primary key\n";
            str += "-- =============================================\n";
            str += "create procedure " + "sp_Get" + tablename.Remove(0, 3) + "\n";
            str += "(\n";
            str += _ADDCODEHERE[0];
            str += ")\n";
            str += "as \n";
            str += "begin\n";
            str += AddTab(1) + "select\n";
            str += _ADDCODEHERE[1];
            str += AddTab(1) + "from [" + tablename + "]\n";
            str += AddTab(1) + "where \n";
            str += _ADDCODEHERE[2];
            str += "end\n";
            str += "go\n";

            //GetAll
            str += "\n\n";
            str += "-- =============================================\n";
            str += "-- Author: Hoàng Tuấn\n";
            str += "-- Create date: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "\n";
            str += "-- Description: Lấy tất cả dữ liệu bảng " + tablename + "\n";
            str += "-- =============================================\n";
            str += "create procedure " + "sp_GetAll" + tablename.Remove(0, 3) + "\n";
            str += "as \n";
            str += "begin\n";
            str += AddTab(1) + "select * \n";
            str += AddTab(1) + "from [" + tablename + "]\n";
            str += "end\n";
            str += "go\n";

            //Add
            str += "\n\n";
            str += "-- =============================================\n";
            str += "-- Author: Hoàng Tuấn\n";
            str += "-- Create date: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "\n";
            str += "-- Description: Thêm dữ liệu mới cho bảng " + tablename + "\n";
            str += "-- =============================================\n";
            str += "create procedure " + "sp_Add" + tablename.Remove(0, 3) + "\n";
            str += "(\n";
            str += _ADDCODEHERE[3];
            str += ")\n";
            str += "as\n";
            str += "begin\n";
            str += AddTab(1) + "insert into " + tablename + "(\n";
            str += _ADDCODEHERE[4];
            str += AddTab(1) + ")\n";
            str += AddTab(1) + "values( \n";
            str += _ADDCODEHERE[5];
            str += AddTab(1) + ")\n";
            str += "end\n";
            str += "go\n";

            //Update
            str += "\n\n";
            str += "-- =============================================\n";
            str += "-- Author: Hoàng Tuấn\n";
            str += "-- Create date: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "\n";
            str += "-- Description: Cập nhật dữ liệu bảng " + tablename + " theo primary key\n";
            str += "-- =============================================\n";
            str += "create procedure " + "sp_Update" + tablename.Remove(0, 3) + "\n";
            str += "(\n";
            str += _ADDCODEHERE[6];
            str += ")\n";
            str += "as\n";
            str += "begin\n";
            str += AddTab(1) + "update " + tablename + "\n";
            str += AddTab(1) + "set\n";
            str += _ADDCODEHERE[7];
            str += AddTab(1) + "where \n";
            str += _ADDCODEHERE[8];
            str += "end\n";
            str += "go\n";

            //Delete
            str += "\n\n";
            str += "-- =============================================\n";
            str += "-- Author: Hoàng Tuấn\n";
            str += "-- Create date: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "\n";
            str += "-- Description: Xóa dữ liệu trong bảng " + tablename + " theo primary key\n";
            str += "-- =============================================\n";
            str += "create procedure " + "sp_Delete" + tablename.Remove(0, 3) + "\n";
            str += "(\n";
            str += _ADDCODEHERE[9];
            str += ")\n";
            str += "as\n";
            str += "begin\n";
            str += AddTab(1) + "delete ";
            str += AddTab(1) + "from " + tablename + "\n";
            str += AddTab(1) + "where \n";
            str += _ADDCODEHERE[10];
            str += "end\n";
            
            ////Nếu ID là tự động tăng thì thêm return ID
            //if (isReturnID)
            //{
            //    //Add and return ID
            //    str += "\n\n";
            //    str += "-- =============================================\n";
            //    str += "-- Author: Hoàng Tuấn\n";
            //    str += "-- Create date: " + DateTime.Now.Date.ToString("dd/MM/yyyy") + "\n";
            //    str += "-- Description: Thêm dữ liệu mới cho bảng " + tablename + " và trả về ID vừa mới thêm vào\n";
            //    str += "-- =============================================\n";
            //    str += "create procedure " + "sp_AddAndReturnID" + tablename.Remove(0, 3) + "\n";
            //    str += "(\n";
            //    str += _ADDCODEHERE[11];
            //    str += ")\n";
            //    str += "as\n";
            //    str += "begin\n";
            //    str += AddTab(1) + "insert into " + tablename + "(\n";
            //    str += _ADDCODEHERE[12];
            //    str += AddTab(1) + ")\n";
            //    str += AddTab(1) + "values( \n";
            //    str += _ADDCODEHERE[13];
            //    str += AddTab(1) + ")\n";
            //    str += "select "
            //    str += "end\n";
            //    str += "go\n";
            //}
            return str;
        }

#region "Templates"

        /* add a attribute in SELECT query in GetObject store procedure
         * Template: [name]
         * Sample: [hoten]
        */
        public string Template1(string name, bool isComma)
        {
            string attCode = "";
            if (isComma)
                attCode += AddTab(2) + "[" + name + "]" + ",\n";
            else
                attCode += AddTab(2) + "[" + name + "]" + "\n";
            return attCode;
        }

        /*add attribute in declare paramaters in GetObject store procedure
         * Template: @name type
         * Sample: @hoten nvarchar(30)
        */
        public string Template2(string type, string name, bool isComma)
        {
            string attCode = "";
            if (isComma)
                attCode += AddTab(1) + "@" + name + " " + type + ",\n";
            else
                attCode += AddTab(1) + "@" + name + " " + type + "\n";
            return attCode;
        }

        /*
         * declare WHERE condition
         * Template: [name] = @name
         * Sample: [hoten] = @hoten
         */
        public string Template3(string name, bool isAnd)
        {
            string attCode = "";
            if (isAnd)
                attCode += AddTab(2) + "[" + name + "]" + " = " + "@" + name + " and \n";
            else
                attCode += AddTab(2) + "[" + name + "]" + " = " + "@" + name + "\n";
            return attCode;
        }

        /*
         * set value UPDATE
         * Template: [name] = @name
         * Sample: [hoten] = @hoten
         */
        public string Template5(string name, bool isComma)
        {
            string attCode = "";
            if (isComma)
                attCode += AddTab(2) + "[" + name + "]" + " = " + "@" + name + ", \n";
            else
                attCode += AddTab(2) + "[" + name + "]" + " = " + "@" + name + "\n";
            return attCode;
        }


        /*
         * add attributes in VALUES statement of INSERT INTO
         * Template: @name
         * Sample: @hoten
         */
        public string Template4(string name, bool isComma)
        {
            string attCode = "";
            if (isComma)
                attCode += AddTab(2) + "@" + name + ",\n";
            else
                attCode += AddTab(2) + "@" + name + "\n";
            return attCode;
        }


        
        #endregion

    }
}
