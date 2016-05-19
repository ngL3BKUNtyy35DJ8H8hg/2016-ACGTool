using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace ConfigBDTC2.Scripts
{
    public class ActionScript
    {
        /// <summary>
        /// <Action ID="1" Type="ExplodeDcl" ImageFile="explosion1.bmp" Width="2" Height="2" ShiftZ="2" Start="0" Speed="80" Duration="1" SoundName="Explosion1.wav" SoundLoop="0">
        /// </Action>
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public XmlElement CreateExplodeDclAction(XmlDocument xmlDoc)
        {
            //Action element
            XmlElement actionNode = xmlDoc.CreateElement("Action");
            actionNode.SetAttribute("ID", "Boom");
            actionNode.SetAttribute("Type", "ExplodeDcl");
            actionNode.SetAttribute("ImageFile", "explosion1.bmp");
            actionNode.SetAttribute("Width", "2");
            actionNode.SetAttribute("Height", "2");
            actionNode.SetAttribute("ShiftZ", "2");
            actionNode.SetAttribute("Start", "0");
            actionNode.SetAttribute("Speed", "80");
            actionNode.SetAttribute("Duration", "1");
            actionNode.SetAttribute("SoundName", "Explosion1.wav");
            actionNode.SetAttribute("SoundLoop", "0");
            return actionNode;
        }

        public XmlElement CreateBombardAction(XmlDocument xmlDoc, PointF p, int id, string objName, int objNameId, float curr, bool isDich_DanCoi)
        {
            //Action element
            XmlElement actionNode = xmlDoc.CreateElement("Action");
            actionNode.SetAttribute("ID", id.ToString());
            actionNode.SetAttribute("Type", "Bombard");
            
            if (objNameId !=-1)
                actionNode.SetAttribute("ObjName", string.Format("{0}_{1}",objName, objNameId));
            else
                actionNode.SetAttribute("ObjName", objName);

            if (isDich_DanCoi)
                actionNode.SetAttribute("ImageFile", "Dich_DanCoi.png");
            else
                actionNode.SetAttribute("ImageFile", "Ta_DanCoi.png");

            actionNode.SetAttribute("Width", "0.5");
            actionNode.SetAttribute("Height", "0.5");
            actionNode.SetAttribute("Start", curr.ToString());
            actionNode.SetAttribute("Duration", "1");
            actionNode.SetAttribute("Speed", "1");
            actionNode.SetAttribute("dAngle", "-45");
            actionNode.SetAttribute("SoundName", "gun_shotgun1.wav");
            actionNode.SetAttribute("SoundLoop", "1");
            actionNode.SetAttribute("ExplID", "Boom");
            actionNode.SetAttribute("Repeat", "1");

            //Target element
            XmlElement targetNode = xmlDoc.CreateElement("Target");
            targetNode.SetAttribute("X", p.X.ToString());
            targetNode.SetAttribute("Y", p.Y.ToString());
            targetNode.SetAttribute("Z", "0");
            actionNode.AppendChild(targetNode);
            
            return actionNode;
        }

        public void CreateBoomXml(List<PointF> point3DList, string objName, int numOfObjNames, int start, float increment, int minGroup, int maxGroup, bool isDich_DanCoi)
        {
            XmlDocument xmlDoc = new XmlDocument();
            
            XmlElement rootNode = xmlDoc.CreateElement("Actions");
            //Tao qua no
            XmlElement actionNode = CreateExplodeDclAction(xmlDoc);
            rootNode.AppendChild(actionNode);

            //Tao dan ban
            int id = 1;
            float curr = start;
            Random random = new Random();
            int objNameId = -1;
            if (numOfObjNames > 1)
            {
                numOfObjNames++;
                objNameId = random.Next(1, numOfObjNames);
            }
            maxGroup++;
            int group = random.Next(minGroup, maxGroup);
            foreach (PointF p in point3DList)
            {
                actionNode = CreateBombardAction(xmlDoc, p, id, objName, objNameId, curr, isDich_DanCoi);
                rootNode.AppendChild(actionNode);
                id++;
                group--;
                if (group==0){
                    if (objNameId !=-1)
                        objNameId = random.Next(1, numOfObjNames);
                    group = random.Next(minGroup, maxGroup);
                    curr += increment;
                }
            }

            xmlDoc.AppendChild(rootNode);

            //Save
            string tempFile = "temp.xml";
            xmlDoc.Save(tempFile);

            //Open 
            StreamReader reader = new StreamReader(tempFile, Encoding.UTF8);
            string content = reader.ReadToEnd();
            reader.Close();
            //Write
            content = content.Replace("SoundLoop=\"0\" />","SoundLoop=\"0\"></Action>");
            content = content.Replace("Z=\"0\" />", "Z=\"0\"></Target>");
            StreamWriter writer = new StreamWriter(tempFile);
            writer.Write(content);
            writer.Close();

        }


        //public void CreateBoomAction(ref XmlWriter writer, PointF p, int id)
        //{
        //    //Action element
        //    writer.WriteStartElement("Action");
        //    writer.WriteAttributeString("ID", id.ToString());
        //    writer.WriteAttributeString("Type", "Bombard");
        //    writer.WriteAttributeString("ObjName", "Dich_Phao Lu1 2");
        //    writer.WriteAttributeString("ImageFile", "Dich_DanCoi.png");
        //    writer.WriteAttributeString("Width", "0.5");
        //    writer.WriteAttributeString("Height", "0.5");
        //    writer.WriteAttributeString("Start", "0");
        //    writer.WriteAttributeString("Duration", "1");
        //    writer.WriteAttributeString("Speed", "1");
        //    writer.WriteAttributeString("dAngle", "-45");
        //    writer.WriteAttributeString("SoundName", "gun_shotgun1.wav");
        //    writer.WriteAttributeString("SoundLoop", "1");
        //    writer.WriteAttributeString("ExpliID", "1");
        //    writer.WriteAttributeString("Repeat", "2");

        //    //Target element
        //    writer.WriteStartElement("Target");
        //    writer.WriteAttributeString("X", p.X.ToString());
        //    writer.WriteAttributeString("Y", p.Y.ToString());
        //    writer.WriteAttributeString("Z", "0");
        //    //Close tag
        //    writer.WriteEndElement();
        //    writer.WriteEndElement();
        //}

        

        //public void CreateBoomXml(List<PointF> point3DList)
        //{
        //    XmlDocument xmlDoc = new XmlDocument();
            
        //    XmlElement rootNode = xmlDoc.CreateElement("Actions");
            
        //    xmlDoc.AppendChild(rootNode);

        //    //Save
        //    string tempFile = "temp.xml";
        //    xmlDoc.Save(tempFile);

        //    XmlWriter writer = XmlWriter.Create("temp.xml");
        //    writer.WriteStartElement("Actions");
        //    int id = 1;
        //    foreach (PointF p in point3DList)
        //    {
        //        CreateBoomAction(ref writer, p, id);
        //        id++;
        //    }
        //    writer.WriteEndElement();
        //}
    }
}
