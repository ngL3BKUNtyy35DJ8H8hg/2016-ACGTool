using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACGTool.Classes
{
    public class ClassAction
    {
        private string action;
        private string xmlFile;

        public ClassAction(string strAction, string strXmlFile)
        {
            action = strAction;
            xmlFile = strXmlFile;
        }

        public string Action
        {
            get
            {
                return action;
            }
        }

        public string XmlFile
        {

            get
            {
                return xmlFile;
            }
        }


    }
}
