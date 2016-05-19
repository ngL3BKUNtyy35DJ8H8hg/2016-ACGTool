using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ACGTool
{
    public enum DateInterval
    {
        Day,
        DayOfYear,
        Hour,
        Minute,
        Month,
        Quarter,
        Second,
        Milisecond,
        Weekday,
        WeekOfYear,
        Year
    }
    

    public partial class Decompiler : Form
    {
         
        private List<string> controlList = new List<string>(){
            "Label",
            "Button",
            "TextBox",
            "CheckBox",
            "ComboBox",
            "RadioButton",
            "GroupBox",
            "NumericUpDown",

            "ToolStrip",
            "ToolStripButton",
            "ToolStripStatusLabel",
            "ToolStripMenuItem",
            "ToolStripSeparator",
            "ToolStripDropDownButton",
            "ContextMenuStrip",
            "StatusStrip",

            "TabControl",
            "TabPage",
            "TableLayoutPanel",
            
            "DataGridView",
            "ListBox",
            "ListControl",
            
            "Panel",
            "SplitContainer",
            "Splitter"
        };
        
        string _paramPattern = @"[a-zA-Z0-9-&_\-\.\(\)\]\[]+";
        private FileInfo _inputCSfile;

        public Decompiler()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Load file
        /// </summary>
        /// <param name="filePath"></param>
        private void LoadFile(string filePath)
        {
            _inputCSfile = new FileInfo(filePath);
            string content = _inputCSfile.OpenText().ReadToEnd().Trim();
            //content = Regex.Replace(content, @"^\s+|\s+$", string.Empty, RegexOptions.Multiline).Trim();
            richTextBoxBeforeDecompiler.Text = content;
        }

        private void btBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                labelResult.Text = "";

                //mo form cho nguoi ta chon co so du lieu
                OpenFileDialog1.Filter = "C# File(*.cs)|*.cs";
                OpenFileDialog1.FileName = txtFilePath.Text;
                OpenFileDialog1.ShowDialog();
                if (OpenFileDialog1.FileName != "")
                {
                    txtFilePath.Text = OpenFileDialog1.FileName;

                    Properties.Settings.Default.RecentDecompilerFilePath = OpenFileDialog1.FileName;
                    Properties.Settings.Default.Save();

                    LoadFile(txtFilePath.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        ///// <summary>
        ///// Operators.CompareString(name, "Pages", false) == 0
        ///// ==> name == "Pages"
        ///// </summary>
        ///// <param name="content"></param>
        ///// <returns></returns>
        //public string Replace_CompareString(string content)
        //{
        //    Regex r = new Regex(@"Operators\.CompareString\((?<name1>[a-zA-Z0-9-&_\-\.]+)\, ""(?<name2>[a-zA-Z0-9-&_\-\.]+)""\, false\) \=\= 0");
        //    MatchCollection matches = r.Matches(content);
        //    foreach (Match m in matches)
        //    {
        //        string replace = string.Format(@"{0} == ""{1}""", m.Groups["name1"].Value, m.Groups["name2"].Value);
        //        content = content.Replace(m.Value, replace);
        //    }
        //    return content;
        //}

        /// <summary>
        /// Operators.CompareString(name, "Pages", false) == 0
        /// ==> name == "Pages"
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Replace_Operators(string content)
        {
            //content = @"else if (Operators.CompareString(t1.ToString().Substring(num2, 1), "" * "", false) == 0)";
            string pattern = string.Format(@"Operators\.CompareString\((?<name1>{0}),\s*""(?<name2>{0})"",\s*false\) \=\= 0", _paramPattern);
            //string pattern = string.Format(@"Operators\.CompareString\((?<name1>{0})", _paramPattern);
            Regex r = new Regex(pattern);
            MatchCollection matches = r.Matches(content);
            if (matches.Count > 0)
                foreach (Match m in matches)
                {
                    string replace = string.Format(@"{0} == ""{1}""", m.Groups["name1"].Value, m.Groups["name2"].Value);
                    content = content.Replace(m.Value, replace);
                }
            else
            {
                //pattern = string.Format(@"Operators\.CompareString\((?<name1>{0}), ""(?<name2>.+)"", false\) == 0", _paramPattern);
                ////pattern = string.Format(@"Operators\.CompareString\((?<name1>{0})", _paramPattern);
                //r = new Regex(pattern);
                //matches = r.Matches(content);
                //foreach (Match m in matches)
                //{
                //    string replace = string.Format(@"{0} == ""{1}""", m.Groups["name1"].Value, m.Groups["name2"].Value);
                //    content = content.Replace(m.Value, replace);
                //}
            }

            pattern = string.Format(@"Operators\.MultiplyObject\((?<param1>{0}), (?<param2>{0})\)", _paramPattern);
            r = new Regex(pattern);
            matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = string.Format(@"{0} * {1}", m.Groups["param1"].Value, m.Groups["param2"].Value);
                content = content.Replace(m.Value, replace);
            }
            
            return content;
        }
        
        /// <summary>
        /// Conversions.ToSingle(dataReader.GetValue(7));
        /// ==> Convert.ToFloat(pFile)
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Replace_FileExists(string content)
        {
            Regex r = new Regex(@"MyProject.Computer.FileSystem.FileExists\((?<File>[A-Za-z0-9]+)\)");
            MatchCollection matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = string.Format("File.Exists({0}", m.Groups["ID"].Value);
                content = content.Replace(m.Value, replace);
            }
            return content;
        }

        /// <summary>
        /// 1. if(!Information.IsNothing(userConnection))
        /// ==> userConnection != null
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Replace_CheckNotNull(string content)
        {
            Regex r = new Regex(@"!Information.IsNothing\((?<para>[a-zA-Z0-9-&_\-\.]+)\)");
            MatchCollection matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = string.Format("{0} != null", m.Groups["para"].Value);
                content = content.Replace(m.Value, replace);
            }
            return content;
        }

        /// <summary>
        /// if(Information.IsNothing(userConnection))
        /// ==> userConnection == null
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Replace_CheckNull(string content)
        {
            Regex r = new Regex(@"Information.IsNothing\((?<para>[a-zA-Z0-9-&_\-\.]+)\)");
            MatchCollection matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = string.Format("{0} == null", m.Groups["para"].Value);
                content = content.Replace(m.Value, replace);
            }
            return content;
        }

        /// <summary>
        /// Conversions.ToString
        /// ==> Convert.ToString
        /// 
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Replace_Conversions(string content)
        {
            string find = "Conversions.ToString";
            string replace = "Convert.ToString";
            content = content.Replace(find, replace);

            find = "Conversions.ToSingle";
            replace = "Convert.ToSingle";
            content = content.Replace(find, replace);

            find = "Conversions.ToInteger";
            replace = "Convert.ToInt32";

            content = content.Replace(find, replace);

            find = "Conversions.ToBoolean";
            replace = "Convert.ToBoolean";
            content = content.Replace(find, replace);

            find = "Conversions.ToBoolean";
            replace = "Convert.ToBoolean";
            content = content.Replace(find, replace);
            return content;
        }

        /// <summary>
        /// catch (Exception arg_E3_0)\n{\nProjectData.SetProjectError(arg_E3_0);\nProjectData.ClearProjectError();\n}
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Replace_TryCatch(string content)
        {
            Regex r = new Regex(@"ProjectData.SetProjectError\((?<para>[a-zA-Z0-9-&_\-\.]+)\);\nProjectData.ClearProjectError\(\);");
            MatchCollection matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = string.Format("throw {0};", m.Groups["para"].Value);
                content = content.Replace(m.Value, replace);
            }
            
            r = new Regex(@"ProjectData.SetProjectError\((?<para>[a-zA-Z0-9-&_\-\.]+)\);");
            matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = string.Format(@"throw {0};", m.Groups["para"].Value);
                content = content.Replace(m.Value, replace);
            }

            r = new Regex(@"Interaction.MsgBox\(.+\);\n");
            matches = r.Matches(content);
            foreach (Match m in matches)
            {
                content = content.Replace(m.Value, "");
            }

            content = content.Replace("dbTransaction.Rollback();\n", "");
            content = content.Replace("ProjectData.ClearProjectError();\n", "");
            
            return content;
        }

        /// <summary>
        /// CreateParameter("Cx", 8, flightNode.node.C.x, 0)
        /// CreateParameter("CachVong", 10, flightNode.node.CachVong, 0)
        /// CreateParameter("Flight_ID", 11, pFlight.Flight_ID, 0)
        /// </summary>
        /// <returns></returns>
        public string Replace_DBType(string content)
        {
            //Regex r = new Regex(@"CreateParameter\(""(?<para1>[a-zA-Z0-9-&_\-\.]+)"", (?<para2>[a-zA-Z0-9-&_\-\.]+), (?<para3>[a-zA-Z0-9-&_\-\.\]\[\""\)\(]+), (?<para4>[a-zA-Z0-9-&_\-\.\]\[\""\)\(]+)\)");
            
            //MatchCollection matches = r.Matches(content);
            //foreach (Match m in matches)
            //{
            //    System.Data.DbType valueType = (System.Data.DbType)Convert.ToInt32(m.Groups["para2"].Value);
            //    string replace = string.Format(@"CreateParameter(""{0}"", DbType.{1}, {2}, {3})", m.Groups["para1"].Value, valueType.ToString(), m.Groups["para3"].Value, m.Groups["para4"].Value);
            //    content = content.Replace(m.Value, replace);
            //}


            Regex r = new Regex(@"CreateParameter\(""(?<para1>[a-zA-Z0-9-&_\-\.]+)"", (?<para2>[a-zA-Z0-9-&_\-\.]+),");

            MatchCollection matches = r.Matches(content);
            foreach (Match m in matches)
            {
                System.Data.DbType valueType = (System.Data.DbType)Convert.ToInt32(m.Groups["para2"].Value);
                string replace = string.Format(@"CreateParameter(""{0}"", DbType.{1},", m.Groups["para1"].Value, valueType.ToString());
                content = content.Replace(m.Value, replace);
            }

            return content;
        }

        /// <summary>
        /// CreateParameter("Cx", 8, flightNode.node.C.x, 0)
        /// CreateParameter("CachVong", 10, flightNode.node.CachVong, 0)
        /// CreateParameter("Flight_ID", 11, pFlight.Flight_ID, 0)
        /// </summary>
        /// <returns></returns>
        public string Replace_String(string content)
        {
            //(?<para1>[a-zA-Z0-9-&_\-\.]+)

            Regex r = new Regex(string.Format(@"Strings\.Len\((?<para>{0})\)", _paramPattern));

            MatchCollection matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = string.Format(@"{0}.ToString().Length", m.Groups["para"].Value);
                content = content.Replace(m.Value, replace);
            }

            //Strings.Format(num6, "#,###");
            r = new Regex(string.Format(@"Strings\.Format\((?<para>{0}),", _paramPattern));
            matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = string.Format(@"{0}.ToString(", m.Groups["para"].Value);
                content = content.Replace(m.Value, replace);
            }

            r = new Regex(string.Format(@"Conversions\.ToString\((?<para>{0})\)", _paramPattern));
            matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = string.Format(@"{0}.ToString()", m.Groups["para"].Value);
                content = content.Replace(m.Value, replace);
            }

            //Conversion.Val(array[2])
            r = new Regex(string.Format(@"Conversion\.Val\((?<para>{0})\)", _paramPattern));
            matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = string.Format(@"double.Parse({0})", m.Groups["para"].Value);
                content = content.Replace(m.Value, replace);
            }

            //Strings.Mid(Convert.ToString(t1), num2, 1)
            r = new Regex(string.Format(@"Strings\.Mid\((?<para1>{0}),\s*(?<para2>{1}),\s*(?<para3>{2})\)", _paramPattern, _paramPattern, _paramPattern));
            matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = string.Format(@"{0}.Substring({1},{2})", m.Groups["para1"].Value, m.Groups["para2"].Value, m.Groups["para3"].Value);
                content = content.Replace(m.Value, replace);
            }
            return content;
        }


        /// <summary>
        /// Xử lý các biến dạng arg_141_0
        /// Ví dụ: AxMap arg_141_0 = this.m_Map;
        ///
        /// Replace code:
        ///     NumericUpDown arg_64B_0 = this.nudYDo;
		///     arg_64B_0.Maximum = num;
        /// 
        /// ==>this.nudYDo.Maximum = num;
        ///
        /// </summary>
        /// <returns></returns>
        public string Replace_Arg1(string content)
        {
            Regex r = new Regex(@"\n\t*.+ (?<param1>arg_[a-zA-Z0-9]+_[a-zA-Z0-9]+) = (?<param2>.+);");
            MatchCollection matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = string.Format(@"{0}.ToString().Length", m.Groups["para"].Value);
                content = content.Replace(m.Value, "");
                content = content.Replace(m.Groups["param1"].Value, m.Groups["param2"].Value);
            }

            return content;
        }

        /// <summary>
        /// Xử lý trường hợp
        /// int arg_3D_0 = 0;
	    /// for (int i = arg_3D_0; i <= upperBound; i = checked(i + 1))
        ///	==>
        /// for (int i = 0; i <= upperBound; i = checked(i + 1))
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Replace_Arg2(string content)
        {
            Regex r = new Regex(@"\n\t*.+ int (?<param1>arg_[a-zA-Z0-9]+_[a-zA-Z0-9]+) = 0;");
            MatchCollection matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = string.Format(@"for (int i = {0};", m.Groups["para"].Value);
                content = content.Replace(m.Value, "");
                content = content.Replace(m.Groups["param1"].Value, m.Groups["param2"].Value);
            }

            return content;
        }
        

        /// <summary>
        ///	    try
        ///	    {
		///			IEnumerator enumerator = this.List.GetEnumerator();
		///			while (enumerator.MoveNext())
		///			{
		///				CPage cPage = (CPage)enumerator.Current;
		///				wr.WriteStartElement("Page");
		///				wr.WriteAttributeString("Name", cPage.Value);
		///				wr.WriteAttributeString("Checked", cPage.Checked.ToString());
		///				wr.WriteAttributeString("Symbols", cPage.Symbols);
		///				wr.WriteElementString("ID", cPage.ID.ToString());
		///				wr.WriteEndElement();
		///			}
		///		}
		///		finally
		///		{
		///			IEnumerator enumerator;
		///			if (enumerator is IDisposable)
		///			{
		///				(enumerator as IDisposable).Dispose();
		///			}
        ///		}
        ///		
        /// ==>
        ///        foreach (CPage cPage in this.List)
        ///        {
        ///            wr.WriteStartElement("Page");
        ///            wr.WriteAttributeString("Name", cPage.Value);
        ///            wr.WriteAttributeString("Checked", cPage.Checked.ToString());
        ///            wr.WriteAttributeString("Symbols", cPage.Symbols);
        ///            wr.WriteElementString("ID", cPage.ID.ToString());
        ///            wr.WriteEndElement();
        ///        }
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Replace_Foreach(string content)
        {
            /*==========================
            //Samples:
            //try { List<CFlight>.Enumerator enumerator2 = this.m_Flights.GetEnumerator(); while (enumerator2.MoveNext()) { CFlight current2 = enumerator2.Current; 
            //private int GetMaxID()\n{\nint num = 0;\ntry\n{\nIEnumerator enumerator = this.List.GetEnumerator();\nwhile (enumerator.MoveNext())\n{\nCPage cPage = (CPage)enumerator.Current;\nif (cPage.ID > num)\n{\nnum = cPage.ID;\n}\n}\n}\nfinally\n{\nIEnumerator enumerator;\nif (enumerator is IDisposable)\n{\n(enumerator as IDisposable).Dispose();\n}\n}\nreturn num;\n}
            ==========================*/
            
            //Regex r = new Regex(@"try\n\t*\s*{\n\t*\s*(List<[a-zA-Z0-9-&_\-\.]+>.Enumerator|IEnumerator)\s+enumerator[a-zA-Z0-9-&_\-\.]+\s*\=\s*(?<para1>[a-zA-Z0-9-&_\-\.]+).GetEnumerator\(\)\;\n\t*\s*while\s*\(enumerator[a-zA-Z0-9-&_\-\.]+.MoveNext\(\)\)\n\t*\s*{\n\t*\s*(?<para2>[a-zA-Z0-9-&_\-\.\s+]+)\s*\=\s*(\([a-zA-Z0-9-&_\-\.]\)|)enumerator[a-zA-Z0-9-&_\-\.]+.Current\;");
            //Regex r = new Regex(@"try\n\t*\s*{\n\t*\s*(List<[a-zA-Z0-9-&_\-\.]+>.Enumerator|IEnumerator)");
            //Regex r = new Regex(@"try\n\t*\s*{\n\t*\s*(List<[a-zA-Z0-9-&_\-\.]+>.Enumerator|IEnumerator)\s*enumerator[a-zA-Z0-9-&_\-\.]*\s*");
            
            //IEnumerator enumerator = ((IEnumerable)modHuanLuyen.fBaiTapHinhThai.Tops).GetEnumerator();
            //string pattern = @"try\n\t*\s*{\n\t*\s*(List<(?<para0>[a-zA-Z0-9-&_\-\.\s+]+)>.Enumerator|IEnumerator)\s+enumerator[a-zA-Z0-9-&_\-\.]*\s*\=\s*(?<para1>[a-zA-Z0-9-&_\-\.\(\)]+).GetEnumerator\(\)\;";
            string pattern = @"try\n\t*\s*{\n\t*\s*(List<(?<para0>[a-zA-Z0-9-&_\-\.\s+]+)>.Enumerator|IEnumerator)\s+enumerator[a-zA-Z0-9-&_\-\.]*\s*\=\s*(?<para1>.+).GetEnumerator\(\)\;";
            pattern += @"\n\t*\s*while\s*\(enumerator[a-zA-Z0-9-&_\-\.]*.MoveNext\(\)\)\n\t*\s*{\n\t*\s*(?<para2>[a-zA-Z0-9-&_\-\.\s+]+)\s*\=\s*(\([a-zA-Z0-9-&_\-\.]+\)|)enumerator[a-zA-Z0-9-&_\-\.]*.Current\;";
            content = Replace_BeginForeachWithPattern(pattern, content);
            
            
            //}\nfinally\n{\nList<CFlight>.Enumerator enumerator2;\n((IDisposable)enumerator2).Dispose();\n}
            //}\nfinally\n{\nIEnumerator enumerator;\nif (enumerator is IDisposable)\n{\n(enumerator as IDisposable).Dispose();\n}\n}\nreturn num;\n}
            //pattern = @"}\nfinally\n{\nIEnumerator enumerator[a-zA-Z0-9-&_\-\.]*;\nif (enumerator[a-zA-Z0-9-&_\-\.]* is IDisposable)\n{\n(enumerator[a-zA-Z0-9-&_\-\.]* as IDisposable).Dispose();\n}\n}";


            pattern = @"}\n\t*\s*finally\n\t*\s*{\n\t*\s*IEnumerator enumerator[a-zA-Z0-9-&_\-\.]*;\n\t*\s*if \(enumerator[a-zA-Z0-9-&_\-\.]* is IDisposable\)\n\t*\s*{\n\t*\s*\(enumerator[a-zA-Z0-9-&_\-\.]* as IDisposable\).Dispose\(\);\n\t*\s*}\n\t*\s*}";
            content = Replace_EndForeachWithPattern(pattern, content);

            pattern = @"}\n\t*\s*finally\n\t*\s*{\n\t*\s*List<[a-zA-Z0-9-&_\-\.]+>.Enumerator enumerator[a-zA-Z0-9-&_\-\.]*;\n\t*\s*\(\(IDisposable\)enumerator[a-zA-Z0-9-&_\-\.]*\).Dispose\(\);\n\t*\s*}";
            content = Replace_EndForeachWithPattern(pattern, content);
            return content;
        }

        public string Replace_BeginForeachWithPattern(string pattern, string content)
        {
            Regex r = new Regex(pattern);
            MatchCollection matches = r.Matches(content);
            foreach (Match m in matches)
            {
                if (m.Groups["para2"].Value.Trim().Contains(" "))
                {
                    string replace = "foreach(" + m.Groups["para2"].Value.Trim() + " in " + m.Groups["para1"].Value.Trim() + "){";
                    content = content.Replace(m.Value, replace);
                }
                else
                {
                    string replace = "foreach(" + m.Groups["para0"].Value.Trim() + " " + m.Groups["para2"].Value.Trim() + " in " + m.Groups["para1"].Value.Trim() + "){";
                    content = content.Replace(m.Value, replace);
                }
            }

            return content;
        }

        /// <summary>
        /// }\nfinally\n{\nList<CFlight>.Enumerator enumerator2;\n((IDisposable)enumerator2).Dispose();\n}
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Replace_EndForeachWithPattern(string pattern, string content)
        {
            //}\nfinally\n{\nList<CFlight>.Enumerator enumerator2;\n((IDisposable)enumerator2).Dispose();\n}
            Regex r = new Regex(pattern);
            MatchCollection matches = r.Matches(content);
            foreach (Match m in matches)
            {
                content = content.Replace(m.Value, "");
            }

            return content;
        }

        /// <summary>
        /// Xử lý các hàm For sai thứ tự khởi tạo do chuyển từ ngôn ngữ VB.NET
        /// for (int i = 0; i <= upperBound; i = i + 1)
        ///	==>
        /// for (int i = 0; i < upperBound; i = i + 1)
        /// 
        /// hoặc
        /// 
        /// for (int i = 1; i <= upperBound; i = i + 1)
        ///	==>
        /// for (int i = 0; i < upperBound; i = i + 1)
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Replace_For(string content)
        {
            Replace_Arg2(content);
            Regex r = new Regex(@"for \(int (?<para1>[a-zA-Z0-9_\-\.]+) = (?<para2>[0-9]+); i <= (?<para3>[a-zA-Z0-9_\-\.]+); ([a-zA-Z0-9_\-\.]+)\+\+\)");
            MatchCollection matches = r.Matches(content);
            foreach (Match m in matches)
            {

                int initValue = 0;
                if (int.Parse(m.Groups["para2"].Value) > 0)
                    initValue -= 1;
                content = content.Replace(m.Value, string.Format("for (int {0} = {1}; i < {2}; {0}++)", m.Groups["para1"].Value, initValue, m.Groups["para3"].Value));
            }
            return content;
        }
        public string Replace_DateTime(string content)
        {
            content = content.Replace("DateAndTime.Now", "DateTime.Now");
            content = content.Replace("DateAndTime.Today", "DateTime.Now");

            //DateTime.DateAdd(DateInterval.Second, cFlight.Path[0].node.t2next + cFlight.Path[0].node.tspeed, cFlight.Departure);
            //==>cFlight.Departure.AddDate(

            //Regex r = new Regex(@"DateTime.DateAdd((?<para1>[a-zA-Z0-9-&_\-\.\]\[\""\)\(]+), (?<para2>[a-zA-Z0-9-&_\-\.\]\[\""\)\(\]+), (?<para3>[a-zA-Z0-9-&_\-\.\]\[\""\)\(]+)\)");
            Regex r = new Regex(@"DateAndTime\.DateAdd\((?<para1>.+)\)");
            MatchCollection matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string[] values = m.Groups["para1"].Value.Trim().Split(new char[] { ',' });
                string replace = "";
                switch (values[0].Trim())
                {
                    case "DateInterval.Day":
                        replace = string.Format("{0}.AddDays({1})", values[2], values[1]);
                        content = content.Replace(m.Value, replace);
                        break;
                    case "DateInterval.DayOfYear":
                        break;
                    case "DateInterval.Hour":
                        replace = string.Format("{0}.AddHours({1})", values[2], values[1]);
                        content = content.Replace(m.Value, replace);
                        break;
                    case "DateInterval.Minute":
                        replace = string.Format("{0}.AddMinutes({1})", values[2], values[1]);
                        content = content.Replace(m.Value, replace);
                        break;
                    case "DateInterval.Month":
                        replace = string.Format("{0}.AddMonths({1})", values[2], values[1]);
                        content = content.Replace(m.Value, replace);
                        break;
                    case "DateInterval.Quarter":
                        replace = string.Format("{0}.AddMonths({1}*3)", values[2], values[1]);
                        content = content.Replace(m.Value, replace);
                        break;
                    case "DateInterval.Second":
                        replace = string.Format("{0}.AddSeconds({1})", values[2], values[1]);
                        content = content.Replace(m.Value, replace);
                        break;
                    case "DateInterval.WeekOfYear":
                        break;
                    case "DateInterval.Year":
                        replace = string.Format("{0}.AddYears({1})", values[2], values[1]);
                        content = content.Replace(m.Value, replace);
                        break;
                }
                
            }

            return content;
        }

        /// <summary>
        /// Hàm tìm tất cả các index của dấu ngoặc đơn như (), {} hoặc []
        /// Dùng để xác định các hàm lồng nhau
        /// </summary>
        /// <param name="content"></param>
        /// <param name="keyString"></param>
        /// <param name="indexBracket"></param>
        /// <returns></returns>
        private List<SortedDictionary<int,int>> Find_Brackets(string content, string keyString, string openBracketKey, string closeBracketKey, int indexBracket)
        {
            List<SortedDictionary<int,int>> result = new List<SortedDictionary<int,int>>();
            string replacedContent = content;

            //Bước 1:
            //Nếu không còn chứa thì xử lý thay thế keyString
            int index = 0;
            SortedDictionary<int,int> openBracket = new SortedDictionary<int,int>();
            SortedDictionary<int,int> closeBracket = new SortedDictionary<int,int>();

            //Lưu index chứa keyString vào trong openBracket trước
            openBracket.Add(index++, indexBracket);

            //Bước 2: Tìm toàn bộ dấu các cặp ngoặc đơn () trong code 
            indexBracket += keyString.Length;
            do
            {
                int tempIndexOpen = replacedContent.IndexOf(openBracketKey, indexBracket);
                int tempIndexClose = replacedContent.IndexOf(closeBracketKey, indexBracket);

                if ((tempIndexOpen < tempIndexClose && tempIndexOpen > 0) || (tempIndexOpen > 0 && tempIndexClose == -1))
                {
                    //Nếu tìm thấy "(" trước thì thêm vào openBracket
                    while (openBracket.ContainsKey(index))
                    {
                        index++; // Trường hợp dấu bracket cùng cấp
                    }
                    openBracket.Add(index, tempIndexOpen);
                    indexBracket = tempIndexOpen + 1;
                    index++;
                }
                else if ((tempIndexClose < tempIndexOpen && tempIndexClose > 0) || (tempIndexClose > 0 && tempIndexOpen == -1))
                {
                    index--;
                    while (closeBracket.ContainsKey(index))
                    {
                        index--; // Trường hợp dấu bracket cùng cấp
                    }
                    //Nếu vẫn tìm thấy ")" trước thì thêm vào closeBracket
                    closeBracket.Add(index, tempIndexClose);
                    indexBracket = tempIndexClose + 1;
                }

            } while (index > 0);
            result.Add(openBracket);
            result.Add(closeBracket);
            return result;
        }

        /// <summary>
        /// Xóa đoạn code chứa "check { }"
        /// </summary>
        /// <param name="content"></param>
        /// <param name="indexBracket"></param>
        /// <returns></returns>
        public string Replace_Checked(string content)
        {
            //Xóa các đoạn code "check {"
            Regex r = new Regex(@"\n\t*checked\n\t*\{");
            MatchCollection mc = r.Matches(content);
            do
            {
                foreach (Match m in mc)
                {
                    string keyString = m.Value;
                    List<SortedDictionary<int, int>> result = Find_Brackets(content, keyString, "{", "}", m.Index);
                    SortedDictionary<int, int> openBracket = result[0];
                    SortedDictionary<int, int> closeBracket = result[1];

                    if (openBracket.Count > 0 && closeBracket.Count > 0)
                    {
                        content = content.Remove(closeBracket[0], 2);
                        content = content.Remove(openBracket[0], keyString.Length);
                    }
                    break;
                }
                mc = r.Matches(content);
            } while (mc.Count > 0);
            return content;
        }
        /// <summary>
        /// Interaction.IIf(text.Length > 0, "Tọa độ 5x5 = " + text, "Ngoài Tiêu đồ 5x5")
        /// Interaction.IIf(stt > -1, pRada.SoHieu + stt.ToString("00"), "Chưa XH")
        /// Interaction.IIf(pLoaiTopID == 1, "Địch", RuntimeHelpers.GetObjectValue(Interaction.IIf(pLoaiTopID == 3, "Qtế", RuntimeHelpers.GetObjectValue(Interaction.IIf(pLoaiTopID == 4, "QCảnh", "Ta")))))
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Replace_IIf(string content, string keyString, int indexBracket)
        {
            string replacedContent = content;
            //====================
            indexBracket = replacedContent.IndexOf(keyString, indexBracket);

            //Nếu không tìm thấy thì trở ra
            if (indexBracket == -1)
                return replacedContent;

            //Nếu contentIIf vẫn còn chứa keyString thì đệ quy xử lý tiếp và trả về kết quả
            if (replacedContent.IndexOf(keyString, indexBracket + keyString.Length) > 0)
            {
                replacedContent = Replace_IIf(replacedContent, keyString, indexBracket + keyString.Length);
            };
            //====================

            List<SortedDictionary<int, int>> result = Find_Brackets(replacedContent, keyString, "(", ")", indexBracket);
            SortedDictionary<int,int> openBracket = result[0];
            SortedDictionary<int,int> closeBracket = result[1];

            //Bước 3: Thay ký tự tìm thấy trong ngoặc đơn () bằng ký tự c và lưu sang replaceTemp
            string replaceTemp = replacedContent;
            foreach (KeyValuePair<int, int> pair in openBracket)
            {
                //Lấy các giá trị trong ngoặc đơn nhưng không phải chứa keyString
                if (replaceTemp.Substring(openBracket[pair.Key], keyString.Length) != keyString)
                {
                    string value = replaceTemp.Substring(openBracket[pair.Key], closeBracket[pair.Key] - openBracket[pair.Key] + 1);
                    //Thay giá trị trong ngoặc đơn tìm thấy bằng ký tự 'c'
                    string replaceValue = new string('c', value.Length);
                    replaceTemp = replaceTemp.Replace(value, replaceValue);
                }
            }

            if (openBracket.Count > 0 && closeBracket.Count > 0)
            {
                //Bước 4: Tìm index của 2 dấu phẩy trong hàm IIf tại vị trí openBracket[0]
                //Tìm dấu phẩy ',' đầu tiên trong replaceTemp
                int firstIndex = replaceTemp.IndexOf(",", openBracket[0]);
                //Tìm dấu phẩy ',' thứ hai trong replaceTemp
                int sencondIndex = replaceTemp.IndexOf(",", firstIndex + 1);
                //Thay hai dấu phẩy trong hàm IIf thành ký tự so sánh trong C#
                replacedContent = replacedContent.Remove(firstIndex, 1).Insert(firstIndex, "?");
                replacedContent = replacedContent.Remove(sencondIndex, 1).Insert(sencondIndex, ":");

                //Xóa keyString và ")" liên quan đến keyString
                replacedContent = replacedContent.Remove(closeBracket[0], 1);
                replacedContent = replacedContent.Remove(openBracket[0], keyString.Length);
            }

            return replacedContent;
        }

        public void Test_RunTime()
        {
            string content = @"Interaction.IIf(pLoaiTopID == 1, ""Địch"", RuntimeHelpers.GetObjectValue(Interaction.IIf(pLoaiTopID == 3, ""Qtế"", RuntimeHelpers.GetObjectValue(Interaction.IIf(pLoaiTopID == 4, ""QCảnh"", ""Ta"")))))";
            string keyString = "RuntimeHelpers.GetObjectValue(";
            content = Replace_FunctionWithBracket(content, keyString, 0);
            keyString = "Interaction.IIf(";
            content = Replace_IIf(content, keyString, 0);

            


        }

        /// <summary>
        /// Xử lý bỏ hàm lồng nhau. Ví dụ:
        /// RuntimeHelpers.GetObjectValue(Interaction.IIf(pLoaiRadaID == 2, "HL", "CG"))
        /// Interaction.IIf(pLoaiTopID == 1, "Địch", RuntimeHelpers.GetObjectValue(Interaction.IIf(pLoaiTopID == 3, "Qtế", RuntimeHelpers.GetObjectValue(Interaction.IIf(pLoaiTopID == 4, "QCảnh", "Ta")))))
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Replace_FunctionWithBracket(string content, string keyString, int indexBracket)
        {
            string replacedContent = content;
            
            //====================
            indexBracket = replacedContent.IndexOf(keyString, indexBracket);

            //Nếu không tìm thấy thì trở ra
            if (indexBracket == -1)
                return replacedContent;

            //Nếu contentIIf vẫn còn chứa keyString thì đệ quy xử lý tiếp và trả về kết quả
            if (replacedContent.IndexOf(keyString, indexBracket + keyString.Length) > 0)
            {
                replacedContent = Replace_FunctionWithBracket(replacedContent, keyString, indexBracket + keyString.Length);
            };
            //====================

            List<SortedDictionary<int, int>> result = Find_Brackets(replacedContent, keyString, "(", ")", indexBracket);
            SortedDictionary<int,int> openBracket = result[0];
            SortedDictionary<int,int> closeBracket = result[1];

            if (openBracket.Count > 0 && closeBracket.Count > 0)
            {
                //Xóa keyString và ")" liên quan đến keyString
                replacedContent = replacedContent.Remove(closeBracket[0], 1);
                replacedContent = replacedContent.Remove(openBracket[0], keyString.Length);
            }

            return replacedContent;
        }

        

        /// <summary>
        /// RuntimeHelpers.GetObjectValue(Interaction.IIf(pLoaiRadaID == 2, "HL", "CG"))
        /// Interaction.IIf(pLoaiTopID == 1, "Địch", RuntimeHelpers.GetObjectValue(Interaction.IIf(pLoaiTopID == 3, "Qtế", RuntimeHelpers.GetObjectValue(Interaction.IIf(pLoaiTopID == 4, "QCảnh", "Ta")))))
        /// </summary>
        /// <param name="contentIIf"></param>
        /// <returns></returns>
        public string Replace_Msg(string content)
        {
            //Xử lý MsgBox sang MessageBox
            int startCode = 0;
            do
            {
                startCode = content.IndexOf("Interaction.MsgBox", startCode);
                if (startCode > 0)
                {
                    int endCode = content.IndexOf("\n", startCode);
                    string code = content.Substring(startCode, endCode - startCode - 1);
                    string replacedCode = "";
                    if (code.Length > 0)
                    {
                        if (code.Contains("MsgBoxStyle.Critical") && code.Contains(".Message"))
                        {
                            //Interaction.MsgBox(e.Message, MsgBoxStyle.Critical, "Pop General Error")
                            replacedCode = code;
                            replacedCode = replacedCode.Replace(@"Interaction.MsgBox(e.Message, MsgBoxStyle.Critical, ", @"MessageBox.Show(e.Message,");
                            replacedCode = replacedCode.Replace(@"Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, ", @"MessageBox.Show(e.Message,");
                            replacedCode = replacedCode.Replace(@")", @", MessageBoxIcon.Information)");
                        }
                        else
                        {
                            //Interaction.MsgBox(\"Thật sự muốn xoá \" + seleRada.Ten + \"?\", MsgBoxStyle.YesNo, null) == MsgBoxResult.Yes
                            replacedCode = code;
                            replacedCode = replacedCode.Replace(@"Interaction.MsgBox(", @"MessageBox.Show(");
                            replacedCode = replacedCode.Replace(@", MsgBoxStyle.", @", ""Thông báo"", MessageBoxButtons.");
                            replacedCode = replacedCode.Replace(@", null)", @")");
                            replacedCode = replacedCode.Replace(@"== MsgBoxResult.", @"== DialogResult.");
                        }
                        replacedCode = replacedCode.Replace("Ok", "OK");
                        replacedCode = replacedCode.Replace("Only", "");
                        content = content.Replace(code, replacedCode);
                    }
                    startCode = endCode;
                }
            } while (startCode > 0);
            return content;
        }

        /// <summary>
        /// Xử lý các đoạn code khởi tạo location, size, .. trong hàm InitializeComponent()
        /// Control arg_102_0 = this.btnTaoBT;\n\t\t\tSystem.Drawing.Point location = new System.Drawing.Point(238, 12);\n\t\t\targ_102_0.Location = location;
        /// TabPage arg_485_0 = this.TabPage2;\n\t\t\tlocation = new System.Drawing.Point(4, 22);\n\t\t\targ_485_0.Location = location;
        /// </summary>
        /// <param name="content"></param>
        /// <param name="keyString"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        private string InitializeComponent_ProcessingNew(string content, string keyString)
        {
            int startIndex = 0;
            int endIndex = -1;

            startIndex = content.IndexOf(keyString);
            
            while (startIndex > 0)
            {
                //Lấy cả chuỗi text như dưới
                //\n\t\t\tControl arg_102_0 = this.btnTaoBT;\n\t\t\tSystem.Drawing.Point location = new System.Drawing.Point(238, 12);\n\t\t\targ_102_0.Location = location;
                //\n\t\t\tTabPage arg_485_0 = this.TabPage2;\n\t\t\tlocation = new System.Drawing.Point(4, 22);\n\t\t\targ_485_0.Location = location;
                //ToolStripItem arg_2131_0 = this.ToolStripSeparator4;

                endIndex = content.IndexOf(";", startIndex);
                endIndex = content.IndexOf(";", endIndex + 1);
                endIndex = content.IndexOf(";", endIndex + 1);
                string subContent = content.Substring(startIndex, endIndex - startIndex);
                string[] lines = subContent.Split(new char[] { ';' });

                //Lấy param dòng 1
                string param1 = lines[0].Split(new char[] { '=' })[1].Trim();

                //Lấy dòng 2
                string param2 = lines[1].Split(new char[] { '=' })[1].Trim();

                //Lấy dòng 3
                //new System.Drawing.Point(0, 0); 
                //new Size(0, 0);
                string param3 = lines[2].Split(new char[] { '=' })[0].Replace(";", "").Trim().Split(new char[] { '.' })[1].Trim();

                string replaceContent = string.Format("{0}.{1} = {2}", param1, param3, param2);
                content = content.Replace(subContent, replaceContent);
                startIndex = content.IndexOf(keyString);
            }
            content = content.Replace(" = new Size(", " = new System.Drawing.Size(");
            content = content.Replace(" = new SizeF(", " = new System.Drawing.SizeF(");
            return content;
        }

        /// <summary>
        /// 1. Lấy các EventHandler trong hàm interval virtual
        ///     Ví dụ: EventHandler value2 = new EventHandler(this.btnOpen_Click);
        /// 2. Thay các EventHandler lấy được thành 
        ///     Ví dụ: btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
        /// 3. Đưa các EventHandler được xử lý vào trong hàm InitializeComponent()
        /// </summary>
        /// <param name="content"></param>
        /// <param name="keyString"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        private string CreateCSFile_ProcessingEventHandlers(string content)
        {
            Dictionary<string, string> eventControls = new Dictionary<string, string>();
            string keyString = "internal virtual";

            //Tìm param. Ví dụ: internal virtual System.Windows.Forms.Button btnOpen
            Regex r = new Regex(@"\n\t*internal virtual [a-zA-Z0-9_\-\.]+ (?<para1>[a-zA-Z0-9_\-\.]+)\n\t*{");
            MatchCollection matches = r.Matches(content);
            string param = "";
            string subContent = "";
            int startIndex, endIndex;
            startIndex = endIndex = -1;
            string endKeyString = "\n\t\t\t}\n\t\t}";

            foreach (Match m in matches)
            {
                param = m.Groups["para1"].Value.Trim();
                keyString = m.Value;
                startIndex = content.IndexOf(keyString);
                endIndex = content.IndexOf(endKeyString, startIndex) + endKeyString.Length;
                subContent = content.Substring(startIndex, endIndex - startIndex);
                //Kiểm tra trong đoạn keyString này không có chứa EventHandler thì bỏ qua
                if (!subContent.Contains("EventHandler"))
                {
                    continue;
                }

                //Tìm EventHandler. Ví dụ: 
                //  + EventHandler value2 = new EventHandler(this.btnOpen_Click);
                //  + DataGridViewCellEventHandler value2 = new DataGridViewCellEventHandler(this.grdTops_CellValueChanged);
                //  + AxMapXLib.CMapXEvents_DrawUserLayerEventHandler value2 = new AxMapXLib.CMapXEvents_DrawUserLayerEventHandler(this.AxMap1_DrawUserLayer);
                Regex r1 = new Regex(@"([a-zA-Z0-9_\.]+|)EventHandler (?<value1>[a-zA-Z0-9]+) = (?<value2>new ([a-zA-Z0-9_\.]+|)EventHandler\([a-zA-Z0-9_\.\-]+\));");
                MatchCollection matches1 = r1.Matches(subContent);
                Dictionary<string, string> handlerContent1 = new Dictionary<string, string>();
                foreach (Match m1 in matches1)
                {
                    handlerContent1.Add(m1.Groups["value1"].Value, m1.Groups["value2"].Value);
                }

                //Tìm this._btnOpen.Click += value2;
                Regex r2 = new Regex(@"this\._(?<value1>[a-zA-Z0-9_\.\-]+) \+= (?<value2>[a-zA-Z0-9]+);");
                MatchCollection matches2 = r2.Matches(subContent);
                Dictionary<string, string> handlerContent2 = new Dictionary<string, string>();
                foreach (Match m2 in matches2)
                {
                    handlerContent2.Add(m2.Groups["value1"].Value, m2.Groups["value2"].Value);
                }

                //Thay bằng new System.EventHandler. Ví dụ: btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
                foreach (KeyValuePair<string, string> pair in handlerContent2)
                {
                    string code = string.Format(@"{0}this.{1} += {2};", "\n\t\t\t", pair.Key.Replace("this._", "this."), handlerContent1[pair.Value]);
                    if (eventControls.ContainsKey(param))
                        eventControls[param] += code;
                    else
                        eventControls.Add(param, code);
                }
            }

            //===============================================================
            //Remove các hàm interval virtual sau khi xử lý xong các eventhandler
            foreach (Match m in matches)
            {
                keyString = m.Value;
                startIndex = content.IndexOf(keyString);
                endIndex = content.IndexOf(endKeyString, startIndex) + endKeyString.Length;
                content = content.Remove(startIndex, endIndex - startIndex);
            }

            
            //===============================================================
            //Xử lý đặt các eventhandler ở dòng cuối của đoạn control liên quan trong hàm InitializeComponent
            //Lấy code của hàm InitializeComponent
            string initComponent = CreateCSFile_GetInitializeComponent(content);
            if (initComponent != "")
            {
                string replacedInitComponent = initComponent;
                foreach (KeyValuePair<string, string> pair in eventControls)
                {
                    string pattern = string.Format(@"this\.{0}\.([a-zA-Z0-9]+) = (.+);", pair.Key);
                    Regex r3 = new Regex(pattern);
                    MatchCollection matches3 = r3.Matches(replacedInitComponent);
                    if (matches3.Count > 0)
                    {
                        //Chèn ghi chú của control
                        string code = matches3[0].Value;
                        replacedInitComponent = replacedInitComponent.Insert(replacedInitComponent.IndexOf(code), string.Format("//\n\t\t\t// {0}\n\t\t\t//\n\t\t\t", pair.Key));
                        //Insert event vào content
                        code = matches3[matches3.Count - 1].Value;
                        replacedInitComponent = replacedInitComponent.Insert(replacedInitComponent.IndexOf(code) + code.Length, pair.Value);
                    }
                }

                //Thay nội dung code initcomponent đã thêm event handler
                content = content.Replace(initComponent, replacedInitComponent);
            }

            return content;
        }

       

        private string GetControlName(string controlName)
        {
            string control = "";
            if (controlList.Contains(controlName))
                    control = "System.Windows.Forms." + controlName;
            return control;
        }
        /// <summary>
        /// Xử lý các thuộc tính của class
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string CreateCSFile_ProcessingProperties(string content)
        {
            content = content.Replace("private IContainer components;\n", "");
            content = content.Replace("[DebuggerNonUserCode]\n", "");
            content = content.Replace("[DebuggerStepThrough]\n", "");

            Regex r = new Regex(@"\t*[AccessedThroughProperty\(""[a-zA-Z0-9_\-]+""\)]\n");
            content = r.Replace(content, "");

            //Bỏ dấu _ của các thuộc tính. Ví dụ: private TabControl _TabControl1;
            //r = new Regex(@"private (?<param1>[a-zA-Z0-9_\.\-]+) _(?<param2>[a-zA-Z0-9_\-]+);");
            r = new Regex(@"private (?<param1>[a-zA-Z0-9]+) _(?<param2>[a-zA-Z0-9_\-]+);");
            MatchCollection mc = r.Matches(content);
            string controlName = "";
            foreach (Match m in mc)
            {
                controlName = GetControlName(m.Groups["param1"].Value);
                if (controlName != "")
                {
                    //content = content.Replace(m.Value, string.Format("private {0} {1};", controlName, m.Groups["param2"].Value.Substring(0,1) == "_" ? m.Groups["param2"].Value.Remove(0,1) : m.Groups["param2"].Value));
                    content = content.Replace(m.Value, string.Format("private {0} {1};", controlName, m.Groups["param2"].Value));
                }
            }

            r = new Regex(@"private (?<param1>(System\.Windows\.Forms\.|)[a-zA-Z0-9]+) _(?<param2>[a-zA-Z0-9_\-]+);");
            mc = r.Matches(content);
            foreach (Match m in mc)
            {
                content = content.Replace(m.Value, string.Format("private {0} {1};", m.Groups["param1"].Value, m.Groups["param2"].Value));
            }
            return content;
        }

        /// <summary>
        /// Bỏ đoạn code Dispose()
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string CreateCSFile_RemoveDispose(string content)
        {
            //Xóa các đoạn code "check {"
            Regex r = new Regex(@"\t*\s*protected override void Dispose\(bool disposing\)\n\t*\s*\{");
            MatchCollection mc = r.Matches(content);
            do
            {
                foreach (Match m in mc)
                {
                    string keyString = m.Value;
                    List<SortedDictionary<int, int>> result = Find_Brackets(content, keyString, "{", "}", m.Index);
                    SortedDictionary<int, int> openBracket = result[0];
                    SortedDictionary<int, int> closeBracket = result[1];

                    if (openBracket.Count > 0 && closeBracket.Count > 0)
                    {
                        content = content.Remove(openBracket[0], closeBracket[0] - openBracket[0] + 1);
                    }
                    break;
                }
                mc = r.Matches(content);
            } while (mc.Count > 0);
            return content;
        }

        /// <summary>
        /// Bỏ đoạn code Dispose()
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string CreateCSFile_GetInitializeComponent(string content)
        {
            //Remove protected override void Dispose(bool disposing)
            int startIndex = content.IndexOf("private void InitializeComponent()");
            Regex r = new Regex(@"this\.ResumeLayout\(false\);(\n\t*this\.PerformLayout\(\);|)\n\t*}");
            MatchCollection mc = r.Matches(content);
            if (mc.Count > 0)
            {
                string endInitStr = mc[0].Value;
                int endIndex = content.IndexOf(endInitStr, startIndex);
                endIndex += endInitStr.Length;
                content = content.Substring(startIndex, endIndex - startIndex + 1);
            }
            else
                content = "";
            return content;
        }

        /// <summary>
        /// Tạo file .Design.cs
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string CreateCSFile_DesignFile(string content)
        {
            //Find namespace
            string namespace_name = "";
            Regex r = new Regex(@"namespace (?<para>[a-zA-Z0-9_\-\.]+)");
            MatchCollection mc = r.Matches(content);
            foreach (Match m in mc)
            {
                namespace_name = m.Groups["para"].Value;
            }

            //Find class name
            string class_name = "";
            r = new Regex(@"public class (?<para>[a-zA-Z0-9_\-\.]+)");
            mc = r.Matches(content);
            foreach (Match m in mc)
            {
                class_name = m.Groups["para"].Value;
            }

            //Chỉ xử lý riêng đoạn code của hàm InitializeComponent()
            content = CreateCSFile_GetInitializeComponent(content);

            //Xử lý các control khởi tạo Location, Size, ...
            content = InitializeComponent_ProcessingNew(content, "Control ");
            content = InitializeComponent_ProcessingNew(content, "TabPage ");
            content = InitializeComponent_ProcessingNew(content, "ToolStripItem ");


            content = content.Replace(" FontStyle", " System.Drawing.FontStyle");
            content = content.Replace(" GraphicsUnit", " System.Drawing.GraphicsUnit");
            content = content.Replace("(Image)", "(System.Drawing.Image)");
            

            content = content.Replace(" SystemColors", " System.Drawing.SystemColors");
            content = content.Replace(" Color", " System.Drawing.Color");
            content = content.Replace(" Resources", " Properties.Resources");

            content = content.Replace("ToolStripItemDisplayStyle", "System.Windows.Forms.ToolStripItemDisplayStyle");
            content = content.Replace("ComponentResourceManager", "System.ComponentModel.ComponentResourceManager");
            content = content.Replace("ISupportInitialize", "System.ComponentModel.ISupportInitialize");
            content = content.Replace("AnchorStyles", "System.Windows.Forms.AnchorStyles");
            content = content.Replace("Padding", "System.Windows.Forms.Padding");
            content = content.Replace("DataGridViewColumnHeadersHeightSizeMode", "System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode");
            content = content.Replace("SizeType", "System.Windows.Forms.SizeType");
            content = content.Replace("DateTimePickerFormat", "System.Windows.Forms.DateTimePickerFormat");

            content = content.Replace("= FormBorderStyle", " = System.Windows.Forms.FormBorderStyle");
            content = content.Replace("= FormStartPosition", " = System.Windows.Forms.FormStartPosition");
            content = content.Replace("= AutoScaleMode", "= System.Windows.Forms.AutoScaleMode");
            content = content.Replace("= DialogResult", "= System.Windows.Forms.DialogResult");
            content = content.Replace("= BorderStyle", "= System.Windows.Forms.BorderStyle");
            content = content.Replace("= DockStyle", "= System.Windows.Forms.DockStyle");
            content = content.Replace("= CheckState", "= System.Windows.Forms.CheckState");

            content = content.Replace("new Container", "new System.ComponentModel.Container");
            content = content.Replace("new EventHandler", "new System.EventHandler");
            content = content.Replace("new CancelEventHandler", "new System.ComponentModel.CancelEventHandler");
            content = content.Replace("new ToolStripItemClickedEventHandler", "new System.Windows.Forms.ToolStripItemClickedEventHandler");
            
            content = content.Replace("new ToolStripItem", "new System.Windows.Forms.ToolStripItem");
            content = content.Replace("new RowStyle", "new System.Windows.Forms.RowStyle");
            content = content.Replace("new ColumnStyle", "new System.Windows.Forms.ColumnStyle");
            content = content.Replace("new Font", "new System.Drawing.Font");
            content = content.Replace("new ContextMenuStrip", "new System.Windows.Forms.ContextMenuStrip");
            content = content.Replace("new ToolStripSplitButton", "new System.Windows.Forms.ToolStripSplitButton");
            content = content.Replace("new Point", "new System.Drawing.Point");
            content = content.Replace("new KeyEventHandler", "new System.Windows.Forms.KeyEventHandler");
            content = content.Replace("new DateTimePicker", "new System.Windows.Forms.DateTimePicker");
            
            
            content = content.Replace("this.AxMap1 = new AxMap();", "this.AxMap1 = new AxMapXLib.AxMap();");
            content = content.Replace("AxMap AxMap1;", "AxMapXLib.AxMap AxMap1;");
            content = content.Replace("AxHost.State", "System.Windows.Forms.AxHost.State");
            content = content.Replace("componentResourceManager = ", "resources = ");
            content = content.Replace("componentResourceManager.GetObject", "resources.GetObject");
            
            
            //SizeF autoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
            //this.AutoScaleDimensions = autoScaleDimensions;
            r = new Regex(@"SizeF autoScaleDimensions = (?<param1>.+);\n\t*this\.AutoScaleDimensions = autoScaleDimensions;");
            mc = r.Matches(content);
            foreach (Match m in mc)
            {
                content = content.Replace(m.Value, string.Format("this.AutoScaleDimensions = {0};", m.Groups["param1"].Value));
            }

            //size = new System.Drawing.Size(249, 249);
            //this.ClientSize = size;
            r = new Regex(@"size = (?<param1>.+);\n\t*this\.ClientSize = size;");
            mc = r.Matches(content);
            foreach (Match m in mc)
            {
                content = content.Replace(m.Value, string.Format("this.ClientSize = {0};", m.Groups["param1"].Value));
            }


            //Bỏ dấu _ của các thuộc tính. Ví dụ: private System.Windows.Forms.TextBox _txtSoPhut;
            r = new Regex(@"= new (?<param1>[a-zA-Z0-9]+)\(\);");
            mc = r.Matches(content);
            foreach (Match m in mc)
            {
                string controlName = GetControlName(m.Groups["param1"].Value);
                if (controlName != "")
                    content = content.Replace(m.Value, string.Format("= new {0}();", controlName));
            }

            /* Replace code:
                NumericUpDown arg_64B_0 = this.nudYDo;
			    arg_64B_0.Maximum = num;
             * 
             * ==>this.nudYDo.Maximum = num;
             */
            content = Replace_Arg1(content);

            //LoadFile
            FileInfo fileInfo = new FileInfo(Application.ExecutablePath);
            string tempDesignFile = fileInfo.Directory + "\\" + "Temp.Design.cs";
            string tempDesignContent = File.ReadAllText(tempDesignFile);

            tempDesignContent = tempDesignContent.Replace("TEMP_NAMESPACE", namespace_name);
            tempDesignContent = tempDesignContent.Replace("TEMP_CLASS", class_name);
            tempDesignContent = tempDesignContent.Replace("TEMP_INIT", content);

            return tempDesignContent;
        }

        //private string GetDesignCSContent(string content)
        //{
        //    int startIndex = content.IndexOf("private IContainer components;");
        //    if (startIndex == -1)
        //        return "";
        //    int endIndex = content.IndexOf("private void InitializeComponent()");
        //    endIndex = content.IndexOf("}", endIndex);
        //    if (endIndex == -1)
        //        return "";

        //    return content.Substring(startIndex + "private IContainer components;".Length, endIndex - startIndex + 1);
        //}

        private void CreateCSFile(string content)
        {
            //1. Xử lý các thuộc tính
            content = CreateCSFile_ProcessingProperties(content);
            //===========================
            //2. Xử lý các EventHandler trong internal virtual 
            content = CreateCSFile_ProcessingEventHandlers(content);

            string csContent = CreateCSFile_CodeFile(content);
            richTextBoxCSFile.Text = csContent;
            Clipboard.SetText(csContent);
            if (txtOutput.Text != "" && _inputCSfile != null)
            {
                File.WriteAllText(txtOutput.Text + "\\" + _inputCSfile.Name, csContent);
            }

            if (content.Contains("private void InitializeComponent()"))
            {
                

                string designCSContent = CreateCSFile_DesignFile(content);
                richTextBoxDesignCSFile.Text = designCSContent;
                if (txtOutput.Text != "" && _inputCSfile != null)
                {
                    File.WriteAllText(txtOutput.Text + "\\" + _inputCSfile.Name.Replace(".cs", ".Designer.cs"), designCSContent);
                }
            }

        }

        private string CreateCSFile_CodeFile(string content)
        {
            //Bỏ các hàm Dispose()
            content = CreateCSFile_RemoveDispose(content);
            //Bỏ hàm InitializeComponent()
            string initComponent = CreateCSFile_GetInitializeComponent(content);
            if (initComponent.Length > 0)
                content = content.Replace(initComponent, "");

            //Bỏ một số đoạn code khác
            content = content.Replace("[DesignerGenerated]", "");
            content = content.Replace("public class", "public partial class");
            content = content.Replace("using Microsoft.VisualBasic;\n", "");
            content = content.Replace("using Microsoft.VisualBasic.CompilerServices;\n", "");
            content = content.Replace("MyProject.Computer.FileSystem.FileExists", "File.Exists");

            //Các biến chưa khởi tạo xử lý thêm khởi tạo
            Regex r = new Regex(@"(?<para1>(int|double|float|PointF)) (?<para2>[a-zA-Z0-9-&_\-\.]+);");
            MatchCollection matches = r.Matches(content);
            foreach (Match m in matches)
            {
                string replace = "";
                if (m.Groups["para1"].Value == "PointF")
                    replace = string.Format("PointF {0} = new PointF();", m.Groups["para2"].Value);
                else
                    replace = string.Format("{0} {1} = 0;", m.Groups["para1"].Value, m.Groups["para2"].Value);
                content = content.Replace(m.Value, replace);
            }

            content = Replace_Msg(content);

            string keyString = "RuntimeHelpers.GetObjectValue(";
            content = Replace_FunctionWithBracket(content, keyString, 0);

            keyString = "Interaction.IIf(";
            content = Replace_IIf(content, keyString, 0);

            content = Replace_Checked(content);

            content = Replace_String(content);

            content = Replace_DateTime(content);

            content = Replace_Conversions(content);

            content = Replace_Operators(content);

            content = Replace_CheckNotNull(content);

            content = Replace_CheckNull(content);

            content = Replace_DBType(content);

            /* Replace code:
                NumericUpDown arg_64B_0 = this.nudYDo;
			    arg_64B_0.Maximum = num;
             * 
             * ==>this.nudYDo.Maximum = num;
             */
            content = Replace_Arg1(content);

            content = Replace_For(content);
            if (checkBoxTryCatch.Checked)
                content = Replace_TryCatch(content);

            if (checkBoxForeach.Checked)
                content = Replace_Foreach(content);

            return content;
        }

        private void btnDecompiler_Click(object sender, EventArgs e)
        {
            labelResult.Text = "Decompiling ...";
            CreateCSFile(richTextBoxBeforeDecompiler.Text);
            
            labelResult.Text = "Complete ...";
        }

        private void btnDirectory_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            //folderBrowser.ShowNewFolderButton = true;
            //folderBrowser.SelectedPath = Properties.Settings.Default.RecentFolderPath;
  
            //DialogResult result = folderBrowser.ShowDialog();
            
            //if (folderBrowser.SelectedPath == "")
            //    return;

            //Properties.Settings.Default.RecentFolderPath = folderBrowser.SelectedPath;
            //Properties.Settings.Default.Save();

            //txtDirectory.Text = folderBrowser.SelectedPath;
            
            //var files = Directory.EnumerateFiles(folderBrowser.SelectedPath, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".cs"));
            //foreach (string filepath in files)
            //{
            //    string content = "";
            //    using (System.IO.StreamReader file = new System.IO.StreamReader(filepath))
            //    {
            //        content = file.ReadToEnd().Trim();
            //        content = Regex.Replace(content, @"^\s+|\s+$", string.Empty, RegexOptions.Multiline).Trim();
            //        string csContent = CreateCSFile_CodeFile(content);
            //        string designCsContent = CreateCSFile_DesignFile(content);
                    
            //        System.IO.File.WriteAllText(filepath, csContent);
            //        System.IO.File.WriteAllText(filepath.Replace(".cs", ".Design.cs"), csContent);
            //    }
            //}

        }

        private void Decompiler_Load(object sender, EventArgs e)
        {
            checkBoxForeach.Checked = true;
            checkBoxTryCatch.Checked = true;

            if (Properties.Settings.Default.RecentDecompilerFilePath != "")
            {
                txtFilePath.Text = Properties.Settings.Default.RecentDecompilerFilePath;
                LoadFile(txtFilePath.Text);
            }

            txtOutput.Text = Properties.Settings.Default.RecentOuputFolderPath;
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.SelectedPath = Properties.Settings.Default.RecentOuputFolderPath;

            DialogResult result = folderBrowser.ShowDialog();

            if (folderBrowser.SelectedPath == "")
                return;

            Properties.Settings.Default.RecentOuputFolderPath = folderBrowser.SelectedPath;
            Properties.Settings.Default.Save();

            txtOutput.Text = folderBrowser.SelectedPath;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //Test_RunTime();

            Replace_Arg1(richTextBoxBeforeDecompiler.Text);

        }


    }
}
