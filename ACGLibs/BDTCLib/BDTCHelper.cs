using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace BDTCLib
{
    public static class BDTCHelper
    {
        /// <summary>
        /// Kiểm tra đường dẫn là folder hay file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsFolderPath(string path)
        {
            bool result = false;
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(path);
            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory) //Its a folder
            {
                result = true;
            }
            return result;
        }


        /// <summary>
        /// Kiểm tra đường dẫn là folder hay file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsFilePath(string path)
        {
            bool result = true;
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(path);
            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory) //Its a folder
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Kiểm tra link tồn tại hay không
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool CheckExistLink(string path)
        {
            bool result = false;
            string fullPath = Path.GetFullPath(path);
            if (!IsFolderPath(fullPath))
            {
                if (File.Exists(fullPath))
                    result = true;
            }
            else
            {
                if (System.IO.Directory.Exists(fullPath))
                    result = true;
            }
            return result;
        }

        /// <summary>
        /// Kiểm tra link tồn tại hay không
        /// </summary>
        /// <param name="lvItem"></param>
        /// <returns></returns>
        public static void CheckExistLink(ListViewItem lvItem)
        {
            string path = lvItem.SubItems[1].Text;
            lvItem.SubItems[2].Text = Path.GetFullPath(path);
            bool result = BDTCLib.BDTCHelper.CheckExistLink(path);
            if (!result)
            {
                lvItem.SubItems[3].Text = string.Format("{0}: đường dẫn không tồn tại {1}", lvItem.Name, lvItem.SubItems[2].Text);
                lvItem.ForeColor = Color.Red;
            }
            else
            {
                lvItem.SubItems[3].Text = "OK";
                lvItem.ForeColor = Color.Black;
            }

        }

        private static Dictionary<string, string> GetSameString(string[] value1, string[] value2)
        {
            List<string> key = new List<string>();
            List<string> value = new List<string>();
            for (int i = 0; i < value1.Length; i++)
            {
                if (value1[i] == value2[i])
                {
                    key.Add("..");
                    value.Add(value1[i]);
                }
                else
                {
                    break;
                }
            }
            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add(string.Join("\\", key.ToArray()), string.Join("\\", value.ToArray()));
            return result;
        }

        public static string ConvertAbsoluteToRelativePath(string absPath, string currFolder)
        {
            //f:\VIEN CNTT\PROGRAMMING\SaBanXYZ\2013\LQ2\6_Tran Bau Dieu
            //f:\VIEN CNTT\PROGRAMMING\SaBanXYZ\SaBanCT\CacKyHieu.mdb
            //Tìm đoạn string giống nhau
            string[] absPathArr = absPath.Split(new char[] { '\\' });
            string[] currFolderArr = currFolder.Split(new char[] { '\\' });
            Dictionary<string, string> refPath;
            if (absPathArr.Length > currFolderArr.Length)
                refPath = GetSameString(currFolderArr, absPathArr);
            else
                refPath = GetSameString(absPathArr, currFolderArr);

            //Uri file = new Uri(absPath);
            //Uri folder = new Uri(refPath);
            string relativePath = "";
            foreach (KeyValuePair<string, string> value in refPath)
            {
                relativePath = absPath.Replace(value.Value, value.Key);
                relativePath = relativePath.Substring(3, relativePath.Length - 3);
            }

            //string relativePath =
            //Uri.UnescapeDataString(
            //    folder.MakeRelativeUri(file)
            //        .ToString()
            //        .Replace('/', Path.DirectorySeparatorChar)
            //    );
            return relativePath;

        }

        /// <summary>
        /// Lấy chuỗi XML hiện tại không bao gồm nội dung XML bên trong
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static string GetCurrentXMLContent(XmlNode node)
        {
            //Add treeview
            XmlNode currNode = node.Clone();
            while (currNode.ChildNodes.Count > 0)
            {
                currNode.RemoveChild(currNode.ChildNodes[0]);
            }
            return currNode.OuterXml;
        }

        #region "treeViewScript"

        #region "treeViewScript Functions"

        //Add node dau tien cua treeview
        public static void FormatNode(ref TreeNode node)
        {
            node.ForeColor = Color.Blue;
            node.ImageIndex = 0;
            node.SelectedImageIndex = 0;
        }

        public static void FormatLeafNode(ref TreeNode node)
        {
            node.ForeColor = Color.DarkRed;
            node.ImageIndex = 1;
            node.SelectedImageIndex = 0;
        }

        public static void FormatLeafNode(ref TreeNode node, Color color)
        {
            node.ForeColor = color;
            node.ImageIndex = 1;
            node.SelectedImageIndex = 0;
        }


        //Add node dau tien cua treeview
        public static void FormatFolderNode(ref TreeNode node)
        {
            node.ForeColor = Color.Blue;
            node.ImageIndex = 0;
            node.SelectedImageIndex = 0;
        }

        #endregion

        #endregion
    }
}
