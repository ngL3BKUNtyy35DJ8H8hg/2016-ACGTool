using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ACGTool
{
    public static class GeneralClass
    {
        public static List<String> GetAllFiles(string directory)
        {
            return Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories).ToList();
        }

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

        private static List<String> DirSearch(string sDir)
        {
            List<String> files = new List<String>();
            try
            {
                foreach (string f in Directory.GetFiles(sDir))
                {
                    files.Add(f);
                }
                foreach (string d in Directory.GetDirectories(sDir))
                {
                    files.AddRange(DirSearch(d));
                }
            }
            catch (System.Exception excpt)
            {
                throw excpt;
            }

            return files;
        }

        /// <summary>
        /// Thêm tab
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string AddTab(int num)
        {
            string tab = "";
            for (int i = 0; i < num; i++)
                tab += "\t";
            return tab;
        }


        
    }
}
