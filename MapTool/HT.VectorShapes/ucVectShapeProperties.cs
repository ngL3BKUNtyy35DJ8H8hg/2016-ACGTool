using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using HT.VectorShapes.Tools;

namespace HT.VectorShapes
{
    public partial class ucVectShapeProperties : UserControl
    {
        protected ucVectShapes s;

        public ucVectShapeProperties()
        {
            InitializeComponent();
        }

        public void setVectShape(ucVectShapes inS)
        {
            this.s = inS;
            // mi registro all'evento 
            this.s.objectSelected += new ObjectSelected(OnObjectSelected);

        }

        private void OnObjectSelected(object sender, PropertyEventArgs e)
        {
            //this.propertyGrid1.SelectedObject = e.ele;
            if (e.ele.Length == 0)
                this.propertyGrid1.SelectedObject = sender;
            else
                this.propertyGrid1.SelectedObjects = e.ele;

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            this.s.propertyChanged();
            this.s.Refresh();   
        }

    }
}
