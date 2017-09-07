using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace DangKyModels
{

    public static class modSaBan
    {
        public static char cDecSepa = Char.Parse(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

        public static char cGrpSepa = Char.Parse(CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator);

        public static bool myHienDanhSach = true;

        public static string myMuiTenFile = "..\\Defas\\MuiTen.txt";

        public static string myExplodeFile = "..\\Defas\\Explode.txt";

        public static string myCTpara = "SaBan.para";

        public static string myDiaHinhDef = "E:\\fromMinh\\20100327\\SaBanData\\LongThanh\\ThucHanh3.diahinh";

        public static string myCurrentDirectory = "E:\\fromMinh\\20100327\\SaBanData\\LongThanh";

        public static string myKHCnnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=I:\\VIEN CNTT\\PROGRAMMING\\SaBanXYZ\\SaBanCT\\CacKyHieu.mdb";

        public static string mySoundsPath = "..\\sounds";

        public static string myImagesPath = "..\\images";

        public static string myD3DModelMeshFile = "..\\MeshDef\\D3DModelMesh.xml";

        public static string myBillboardMeshFile = "..\\MeshDef\\D3DBillboardMesh.xml";

        public static string mySpriteTexsFile = "..\\MeshDef\\D3DSpriteTexs.xml";

        public static string myLastSaBan = "ThucHanh3.last";

        public static string mySaBanDir = "ThucHanh3";

        public static string LastBdTC = "ThucHanh3\\ThucHanhCD.BDTC";

        public static string myTextureFile = "BanDo\\LongThanh.jpg";

        public static double myMap1X = 106.9418258;

        public static double myMap1Y = 10.8443307;

        public static double myMap2X = 107.0017013;

        public static double myMap2Y = 10.7747964;

        public static string myGridDataFile = "BanDo\\GridData.txt";

        public static string myGridDataType = "txt";

        public static int myGRID_WIDTH = 101;

        public static int myGRID_HEIGHT = 119;

        public static float mySCALE_FACTOR = 6.5f;

        public static double mySurf1X = 0.0;

        public static double mySurf1Y = 0.0;

        public static double mySurf2X = 2047.0;

        public static double mySurf2Y = 2405.0;

        public static int my3DSoPixelsPer1000m = 312;

        public static double GetDouble(string str1)
        {
            string value = str1.Replace(modSaBan.cGrpSepa, modSaBan.cDecSepa);
            return Double.Parse(value);
        }

        public static float GetSingle(string str1)
        {
            string value = str1.Replace(modSaBan.cGrpSepa, modSaBan.cDecSepa);
            return Single.Parse(value);
        }

        public static PointF GetSurfPosition(double pMapX, double pMapY)
        {
            PointF result = new PointF(0f, 0f);
            double num = 0;
            if (modSaBan.myGridDataType == "xyz")
            {
                num = (pMapX - modSaBan.myMap2X) * (modSaBan.mySurf2X - modSaBan.mySurf1X) / (modSaBan.myMap1X - modSaBan.myMap2X);
            }
            else
            {
                num = (modSaBan.myMap1X - pMapX) * (modSaBan.mySurf2X - modSaBan.mySurf1X) / (modSaBan.myMap1X - modSaBan.myMap2X) + modSaBan.mySurf1X;
            }
            double num2 = (modSaBan.myMap1Y - pMapY) * (modSaBan.mySurf2Y - modSaBan.mySurf1Y) / (modSaBan.myMap1Y - modSaBan.myMap2Y) + modSaBan.mySurf1Y;
            result.X = (float)num;
            result.Y = (float)num2;
            return result;
        }

        public static bool imagenameOK(string name)
        {
            bool result = false;
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(modSaBan.mySpriteTexsFile);
            DataTable dataTable = dataSet.Tables[0];
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    string text = dataRow[0].ToString();
                    if (text.IndexOf(name) > -1)
                    {
                        result = true;
                        break;
                    }
                }

            }
            return result;
        }

        public static bool File2Para(string pFileName)
        {
            bool result = false;
            try
            {
                XmlTextReader xmlTextReader = new XmlTextReader(pFileName);
                modSaBan.XML2Para(xmlTextReader);
                xmlTextReader.Close();
                result = true;
            }
            catch (Exception expr_19)
            {
                throw expr_19;
            }
            return result;
        }

        //public static bool ParaOK()
        //{
        //    bool flag = true;
        //    if (Directory.Exists(modSaBan.myCurrentDirectory))
        //    {
        //        Directory.SetCurrentDirectory(modSaBan.myCurrentDirectory);
        //        return Directory.Exists(modSaBan.mySoundsPath) && Directory.Exists(modSaBan.myImagesPath) && File.Exists(modSaBan.myLastSaBan) && File.Exists(modSaBan.myMuiTenFile) && File.Exists(modSaBan.myExplodeFile) && File.Exists(modSaBan.myD3DModelMeshFile) && File.Exists(modSaBan.myBillboardMeshFile) && File.Exists(modSaBan.mySpriteTexsFile) && File.Exists(modSaBan.myTextureFile) && File.Exists(modSaBan.myGridDataFile) && flag;
        //    }
        //    return false;
        //}

        private static void XML2Para(XmlTextReader rr)
        {
            try
            {
                modSaBan.mySoundsPath = "";
                modSaBan.myImagesPath = "";
                modSaBan.myCurrentDirectory = "";
                while (rr.Read())
                {
                    XmlNodeType nodeType = rr.NodeType;
                    XmlNodeType xmlNodeType = nodeType;
                    if (xmlNodeType == XmlNodeType.Element)
                    {
                        string name = rr.Name;
                        if (name == "PARA" && rr.AttributeCount > 0)
                        {
                            while (rr.MoveToNextAttribute())
                            {
                                string name2 = rr.Name;
                                if (name2 == "myCurrentDirectory")
                                {
                                    modSaBan.myCurrentDirectory = rr.Value;
                                }
                                else if (name2 == "myKHCnnString")
                                {
                                    modSaBan.myKHCnnString = rr.Value;
                                }
                                else if (name2 == "myLastSaBan")
                                {
                                    modSaBan.myLastSaBan = rr.Value;
                                }
                                else if (name2 == "myD3DModelMeshFile")
                                {
                                    modSaBan.myD3DModelMeshFile = rr.Value;
                                }
                                else if (name2 == "myBillboardMeshFile")
                                {
                                    modSaBan.myBillboardMeshFile = rr.Value;
                                }
                                else if (name2 == "mySpriteTexsFile")
                                {
                                    modSaBan.mySpriteTexsFile = rr.Value;
                                }
                                else if (name2 == "myImagesPath")
                                {
                                    modSaBan.myImagesPath = rr.Value;
                                }
                                else if (name2 == "mySoundsPath")
                                {
                                    modSaBan.mySoundsPath = rr.Value;
                                }
                                else if (name2 == "myMuiTenFile")
                                {
                                    modSaBan.myMuiTenFile = rr.Value;
                                }
                                else if (name2 == "myExplodeFile")
                                {
                                    modSaBan.myExplodeFile = rr.Value;
                                }
                                else if (name2 == "myTextureFile")
                                {
                                    modSaBan.myTextureFile = rr.Value;
                                }
                                else if (name2 == "myMap1X")
                                {
                                    modSaBan.myMap1X = modSaBan.GetDouble(rr.Value);
                                }
                                else if (name2 == "myMap1Y")
                                {
                                    modSaBan.myMap1Y = modSaBan.GetDouble(rr.Value);
                                }
                                else if (name2 == "myMap2X")
                                {
                                    modSaBan.myMap2X = modSaBan.GetDouble(rr.Value);
                                }
                                else if (name2 == "myMap2Y")
                                {
                                    modSaBan.myMap2Y = modSaBan.GetDouble(rr.Value);
                                }
                                else if (name2 == "myGridDataFile")
                                {
                                    modSaBan.myGridDataFile = rr.Value;
                                }
                                else if (name2 == "myGridDataType")
                                {
                                    modSaBan.myGridDataType = rr.Value;
                                }
                                else if (name2 == "myGRID_WIDTH")
                                {
                                    modSaBan.myGRID_WIDTH = Convert.ToInt32(rr.Value);
                                }
                                else if (name2 == "myGRID_HEIGHT")
                                {
                                    modSaBan.myGRID_HEIGHT = Convert.ToInt32(rr.Value);
                                }
                                else if (name2 == "mySCALE_FACTOR")
                                {
                                    modSaBan.mySCALE_FACTOR = modSaBan.GetSingle(rr.Value);
                                }
                                //else if (name2 == "myLightDir")
                                //{
                                //    string value = rr.Value;
                                //    string[] array = value.Split(new char[]{','});
                                //    if (array.GetUpperBound(0) == 2)
                                //    {
                                //        modSaBan.myLightDir.X = modSaBan.GetSingle(array[0]);
                                //        modSaBan.myLightDir.Y = modSaBan.GetSingle(array[1]);
                                //        modSaBan.myLightDir.Z = modSaBan.GetSingle(array[2]);
                                //    }
                                //}
                                //else if (name2 == "myCamPos")
                                //{
                                //    string value2 = rr.Value;
                                //    string[] array2 = value2.Split(new char[]{','});
                                //    if (array2.GetUpperBound(0) == 2)
                                //    {
                                //        modSaBan.myCamPos.X = modSaBan.GetSingle(array2[0]);
                                //        modSaBan.myCamPos.Y = modSaBan.GetSingle(array2[1]);
                                //        modSaBan.myCamPos.Z = modSaBan.GetSingle(array2[2]);
                                //    }
                                //}
                                //else if (name2 != "myDefaFileName")
                                //{
                                //    if (name2 != "myKHCnnString")
                                //    {
                                //        if (name2 != "myMapGst")
                                //        {
                                //            if (name2 != "myMapNhoGst")
                                //            {
                                //                if (name2 != "mySaiSo")
                                //                {
                                //                    if (name2 != "myTinhChinhGocQuay")
                                //                    {

                                //                    }
                                //                }
                                //            }
                                //        }
                                //    }
                                //}
                            }
                        }
                    }
                }
            }
            catch (Exception expr_465)
            {
                throw expr_465;
            }
        }

        public static void LoadDienTap(string pFileName)
        {
            try
            {
                XmlTextReader xmlTextReader = new XmlTextReader(pFileName);
                modSaBan.XML2DienTap(xmlTextReader);
                xmlTextReader.Close();
            }
            catch (Exception expr_15)
            {
                throw expr_15;
            }
        }

        private static void XML2DienTap(XmlTextReader rr)
        {
            try
            {
                while (rr.Read())
                {
                    XmlNodeType nodeType = rr.NodeType;
                    XmlNodeType xmlNodeType = nodeType;
                    if (xmlNodeType == XmlNodeType.Element)
                    {
                        string name = rr.Name;
                        if (name == "LAST" && rr.AttributeCount > 0)
                        {
                            while (rr.MoveToNextAttribute())
                            {
                                string name2 = rr.Name;
                                if (name2 == "mySaBanDir")
                                {
                                    modSaBan.mySaBanDir = rr.Value;
                                }
                                else if (name2 == "LastBdTC")
                                {
                                    modSaBan.LastBdTC = rr.Value;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception expr_82)
            {
                throw expr_82;
            }
        }

        public static void LoadLastSaBan(string pFileName)
        {
            try
            {
                XmlTextReader xmlTextReader = new XmlTextReader(pFileName);
                modSaBan.XML2LastSaBan(xmlTextReader);
                xmlTextReader.Close();
            }
            catch (Exception expr_15)
            {
                throw expr_15;
            }
        }

        private static void XML2LastSaBan(XmlTextReader rr)
        {
            try
            {
                while (rr.Read())
                {
                    XmlNodeType nodeType = rr.NodeType;
                    XmlNodeType xmlNodeType = nodeType;
                    if (xmlNodeType == XmlNodeType.Element)
                    {
                        string name = rr.Name;
                        if (name == "LAST" && rr.AttributeCount > 0)
                        {
                            while (rr.MoveToNextAttribute())
                            {
                                string name2 = rr.Name;
                                if (name2 == "mySaBanDir")
                                {
                                    modSaBan.mySaBanDir = rr.Value;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception expr_65)
            {
                throw expr_65;
            }
        }

        //public static void DienTap2File(string pFileName)
        //{
        //	StreamWriter w = new StreamWriter(pFileName);
        //	XmlTextWriter xmlTextWriter = new XmlTextWriter(w);
        //	modSaBan.DienTap2xml(ref xmlTextWriter);
        //	xmlTextWriter.Close();
        //}

        //private static void DienTap2xml(ref XmlTextWriter wr)
        //{
        //	wr.WriteStartElement("LAST");
        //	wr.WriteAttributeString("mySaBanDir", modSaBan.mySaBanDir);
        //	wr.WriteAttributeString("LastBdTC", modSaBan.LastBdTC);
        //	wr.WriteEndElement();
        //}

        //public static void DiaHinh2File(string pFileName)
        //{
        //    StreamWriter w = new StreamWriter(pFileName);
        //    XmlTextWriter xmlTextWriter = new XmlTextWriter(w);
        //    modSaBan.DiaHinh2xml(ref xmlTextWriter);
        //    xmlTextWriter.Close();
        //}

        //private static void DiaHinh2xml(ref XmlTextWriter wr)
        //{
        //    wr.WriteStartElement("PARA");
        //    wr.WriteAttributeString("myCurrentDirectory", modSaBan.myCurrentDirectory);
        //    modSaBan.myKHCnnString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", Application.StartupPath + "CacKyHieu.mdb");
        //    wr.WriteAttributeString("myKHCnnString", modSaBan.myKHCnnString);
        //    wr.WriteEndElement();
        //}

        public static void LoadLastDiaHinh(string pFileName)
        {
            try
            {
                XmlTextReader xmlTextReader = new XmlTextReader(pFileName);
                modSaBan.XML2LastDiaHinh(xmlTextReader);
                xmlTextReader.Close();
            }
            catch (Exception expr_15)
            {
                throw expr_15;
            }
        }

        private static void XML2LastDiaHinh(XmlTextReader rr)
        {
            try
            {
                while (rr.Read())
                {
                    XmlNodeType nodeType = rr.NodeType;
                    XmlNodeType xmlNodeType = nodeType;
                    if (xmlNodeType == XmlNodeType.Element)
                    {
                        string name = rr.Name;
                        if (name == "PARA" && rr.AttributeCount > 0)
                        {
                            while (rr.MoveToNextAttribute())
                            {
                                string name2 = rr.Name;
                                if (name2 == "myDiaHinhDef")
                                {
                                    modSaBan.myDiaHinhDef = rr.Value;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception expr_65)
            {
                throw expr_65;
                Exception ex = expr_65;
                throw ex;
            }
        }

        public static void LastDiaHinh2File(string pFileName)
        {
            StreamWriter w = new StreamWriter(pFileName);
            XmlTextWriter xmlTextWriter = new XmlTextWriter(w);
            modSaBan.LastDiaHinh2xml(ref xmlTextWriter);
            xmlTextWriter.Close();
        }

        private static void LastDiaHinh2xml(ref XmlTextWriter wr)
        {
            wr.WriteStartElement("PARA");
            wr.WriteAttributeString("myDiaHinhDef", modSaBan.myDiaHinhDef);
            wr.WriteEndElement();
        }
    }
}