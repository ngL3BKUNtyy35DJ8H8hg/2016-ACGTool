using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDTCLib.Scripts.Actions
{
    public class TargetAction
    {
        public float X;
        public float Y;
        public float Z;

        public TargetAction(XmlNode nodeAction)
        {
            X = float.Parse(nodeAction.Attributes["X"].Value);
            Y = float.Parse(nodeAction.Attributes["Y"].Value);
            Z = float.Parse(nodeAction.Attributes["Z"].Value);
        }
    }
    
}
