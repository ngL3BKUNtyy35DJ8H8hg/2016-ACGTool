using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.IO; 						//streamer io
using System.Runtime.Serialization;     // io
using System.Runtime.Serialization.Formatters.Binary; // io
using System.Drawing.Printing;
using HTMapLib.Properties;
using HTMapLib.VectorShapes.Shapes;
using MapXLib;
using Point = System.Drawing.Point;


namespace HTMapLib
{
    
    public partial class ucVectShapes :  UserControl
    {
        //private string Status;
        public enumStatus Status;

        //public string Option;
        public enumOption Option;
        
        //private string redimStatus = "";
        public enumRedimStatus redimStatus = enumRedimStatus.NO;

        protected  string msg = "";
        [CategoryAttribute("Debug"), DescriptionAttribute("ShowDebugInfo")]
        public bool ShowDebug { get; set; }


        protected  int startX;
        protected  int startY;
        protected  MapShapes s;
        public MapShapes ObjShapes
        {
            get { return s; }
        }

        //HT Code: lưu mapZoom
        protected  float _MapZoom = 1;

        protected  float _Zoom = 1;
        protected  bool _A4 = true;

        protected  int _dx = 0;
        protected  int _dy = 0;
        protected  int startDX = 0;
        protected  int startDY = 0;
        protected  int truestartX = 0;
        protected  int truestartY = 0;

        //PEN TOOL START
        protected  ArrayList VisPenPointList;
        protected  ArrayList PenPointList;
        protected  int PenPrecX;
        protected  int PenPrecY;
        //PEN TOOL END

        protected  Bitmap offScreenBmp;
        protected  Bitmap offScreenBackBmp;

        // Grid
        public int _gridSize=0;
        public bool fit2grid=true;

        //Graphic
        protected  System.Drawing.Drawing2D.CompositingQuality _CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
        protected  System.Drawing.Text.TextRenderingHint _TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        protected  System.Drawing.Drawing2D.SmoothingMode _SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        protected  System.Drawing.Drawing2D.InterpolationMode _InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;

        // Drawing Rect
        protected  bool MouseSx; //Cho  biết trạng thái chuột đang nhấn giữ
        protected int tempX;
        protected int tempY;

        //HT Code: Vẽ poly bằng nhiều line
        protected List<Point> polyPoints = new List<Point>(); //Lưu tạm thời các node của poly đang vẽ khi click chuột trái lên bản đồ
        //Cho biết có hiển thị các đối tượng vẽ hay ẩn đi
        public bool isShowDrawingRoute;

        // Preview & print
        protected Anteprima AnteprimaFrm;

        // richTextBoxEditForm
        protected richForm2 editorFrm;

        public RichTextBox r;

        public Color CreationPenColor;
        public float CreationPenWidth;
        public Color CreationFillColor;
        public bool CreationFilled;

        //public PropertyGrid propGrid;

        //EVENT
        public event VectShapeOptionChanged optionChanged;
        public event ObjectSelected objectSelected;

        //Image1.tif
        public Cursor AddPointCur = getCursor("newPoint3.cur", Cursors.Cross);
        public Cursor DelPointCur = getCursor("delPoint3.cur", Cursors.Default);
        // Gets he *.cur file in a. 
        public static Cursor getCursor(string a, Cursor defCur)
        {
            try
            {
                return new Cursor(a);
            }
            catch
            {
                return defCur;
            }
        }

        

        public ucVectShapes()
        {
            InitializeComponent();
        }

        //Graphic
        [CategoryAttribute("Graphics"), DescriptionAttribute("Interp.Mode")]
        public System.Drawing.Drawing2D.InterpolationMode InterpolationMode
        {
            get
            {
                return _InterpolationMode;
            }
            set
            {
                _InterpolationMode = value;
            }
        }


        [CategoryAttribute("Graphics"), DescriptionAttribute("Smooth.Mode")]
        public System.Drawing.Drawing2D.SmoothingMode SmoothingMode
        {
            get
            {
                return _SmoothingMode;
            }
            set
            {
                _SmoothingMode = value;
            }
        }

        [CategoryAttribute("Graphics"), DescriptionAttribute("Txt.Rend.Hint")]
        public System.Drawing.Text.TextRenderingHint TextRenderingHint
        {
            get
            {
                return _TextRenderingHint;
            }
            set
            {
                _TextRenderingHint = value;
            }
        }

        [CategoryAttribute("Graphics"), DescriptionAttribute("Comp.Quality")]
        public System.Drawing.Drawing2D.CompositingQuality CompositingQuality
        {
            get
            {
                return _CompositingQuality;
            }
            set
            {
                _CompositingQuality = value;
            }
        }



        [CategoryAttribute(" "), DescriptionAttribute("Canvas")]
        public string ObjectType
        {
            get
            {
                return "Canvas";
            }
        }
        
        [CategoryAttribute("  "), DescriptionAttribute("Grid Size")]
        public int gridSize
        {
            get
            {
                return _gridSize;
            }
            set
            {
                if (value >= 0)
                {
                    _gridSize = value;
                }
                if (_gridSize > 0)
                {
                    this.dx = _gridSize * (int)(this.dx / _gridSize);
                    this.dy = _gridSize * (int)(this.dy / _gridSize);
                }
                redraw(true); //redraw all=true 
            }
        }


        [CategoryAttribute("  "), DescriptionAttribute("Canvas Zoom")]
        public float Zoom
        {
            get
            {
                return _Zoom;
            }
            set
            {
                if (value > 0)
                {
                    _Zoom = value;
                    redraw(true); //redraw all=true 
                }
                else
                {
                    _Zoom = 1;
                    redraw(true); //redraw all=true 
                }
            }
        }

        //HT Code: biến lưu map zoom
        [CategoryAttribute("  "), DescriptionAttribute("Map Zoom")]
        public float MapZoom
        {
            get
            {
                return _MapZoom;
            }
            set
            {
                if (value > 0)
                {
                    _MapZoom = value;
                    redraw(true); //redraw all=true 
                }
                else
                {
                    _MapZoom = 1;
                    redraw(true); //redraw all=true 
                }
            }
        }

        [CategoryAttribute("  "), DescriptionAttribute("Show A4")]
        public bool A4
        {
            get
            {
                return _A4;
            }
            set
            {
                 _A4 = value;
            }
        }

        [CategoryAttribute("  "), DescriptionAttribute("Canvas OriginX")]
        public int dx
        {
            get
            {
                return _dx;
            }
            set
            {
                    _dx = value;
            }
        }

        [CategoryAttribute("  "), DescriptionAttribute("Canvas OriginY")]
        public int dy
        {
            get
            {
                return _dy;
            }
            set
            {
                _dy = value;
            }
        }

        
        public void zoomIn()
        {
            this.Zoom = (int)(this.Zoom + 1);
        }

        public void zoomOut()
        {
            if (this.Zoom > 1)
            {
                this.Zoom = (int)(this.Zoom - 1);
            }
        }

        public void mergePolygons()
        {
            s.mergePolygons();
            redraw(true); //redraw all=true 
        }
        public void delPoints()
        {
            this.s.delPoint();
            redraw(true); //redraw all=true 
        }

        public void linkNodes()
        {
            this.s.linkNodes();
            redraw(true); //redraw all=true 
        }

        public void delNodes()
        {
            this.s.delNodes();
            redraw(true); //redraw all=true 
        }


        public void extPoints()
        {
            this.s.extPoints();
            redraw(true); //redraw all=true 
        }


        public void XMirror()
        {
            this.s.XMirror();
            redraw(true); //redraw all=true 
        }
        public void YMirror()
        {
            this.s.YMirror();
            redraw(true); //redraw all=true 
        }
        public void Mirror()
        {
            this.s.Mirror();
            redraw(true); //redraw all=true 
        }

        protected  void myInit(Graphics g)
        { 
            //Status = "";
            changeStatus(enumStatus.NONE);
            //Option = "select";
            Option = enumOption.SELECT;

            

            //retrive printer resolution
            PrintDocument pd = new PrintDocument();            
            //MessageBox.Show( pd.PrinterSettings.DefaultPageSettings.PrinterResolution.X.ToString() );
            //MessageBox.Show(pd.PrinterSettings.DefaultPageSettings.PrinterResolution.Y.ToString() );
            Graphics g_pr = pd.PrinterSettings.CreateMeasurementGraphics();
            SizeF sizef;
            float x_pr, y_pr = 0;

            sizef = g_pr.MeasureString("YourStringHere", Font);
            x_pr = sizef.Width;
            y_pr = sizef.Height;
            //y_pr = Font.Height;

            float x_vi, y_vi = 0;

            sizef = g.MeasureString("YourStringHere", Font);
            x_vi = sizef.Width;
            y_vi = sizef.Height; 




            //s = new Shapes((pd.PrinterSettings.DefaultPageSettings.PrinterResolution.X / g.DpiX), pd.PrinterSettings.DefaultPageSettings.PrinterResolution.Y);
            s = new MapShapes(g.DpiX * (x_pr / x_vi), g.DpiY * (y_pr / y_vi));

//            undoB = new UndoBuffer(5);
            g.Dispose();

            editorFrm = new richForm2();
            this.r = editorFrm.richTextBox1;

            CreationPenColor=Color.Red;
            CreationPenWidth=2f;
            CreationFillColor=Color.Black;
            CreationFilled=false;

            this.optionChanged += new VectShapeOptionChanged(FakeOptionChange);
            this.objectSelected += new ObjectSelected(FakeObjectSelected);

            offScreenBackBmp = new Bitmap(this.Width, this.Height);
            offScreenBmp = new Bitmap(this.Width, this.Height);
        }

        protected  void FakeOptionChange(object sender, VectShapeOptionEventArgs e)
        { }

        protected  void FakeObjectSelected(object sender, PropertyEventArgs e)
        { }


        protected  void changeStatus(enumStatus s)
        {
            this.Status = s;            
        }

        //protected  void changeOption(string s)
        protected  void changeOption(enumOption s)
        {
            this.Option = s;
            // Notify Option change to "listening object" (i.e: toolBbox)
            VectShapeOptionEventArgs e = new VectShapeOptionEventArgs(this.Option);
            optionChanged(this, e);// raise event
        }

        public void anteprimaStampa(float zoom)
        {
            InitializePrintPreviewControl(zoom);
        }

        #region Print & Preview

        public void Stampa()
        {
            this.s.deSelect();

            AnteprimaFrm = new Anteprima();
            AnteprimaFrm.PrintPreviewControl1.Name = "PrintPreviewControl1";
            AnteprimaFrm.PrintPreviewControl1.Document = AnteprimaFrm.docToPrint;

            AnteprimaFrm.PrintPreviewControl1.Zoom = 1;

            AnteprimaFrm.PrintPreviewControl1.Document.DocumentName = "Anteprima";

            AnteprimaFrm.PrintPreviewControl1.UseAntiAlias = true;

            AnteprimaFrm.docToPrint.PrintPage +=
                new System.Drawing.Printing.PrintPageEventHandler(
                docToPrint_PrintPage);

            // Per stampare
            AnteprimaFrm.docToPrint.Print();

            AnteprimaFrm.Dispose();

        }

        protected  void InitializePrintPreviewControl(float zoom   )
        {
            this.s.deSelect();

            AnteprimaFrm = new Anteprima();
            // Construct the PrintPreviewControl.
            //AnteprimaFrm.PrintPreviewControl1 = new PrintPreviewControl();

            // Set location, name, and dock style for PrintPreviewControl1.
            //AnteprimaFrm.PrintPreviewControl1.Location = new Point(88, 80);
            AnteprimaFrm.PrintPreviewControl1.Name = "Preview";
            //AnteprimaFrm.PrintPreviewControl1.Dock = DockStyle.Fill;

            // da testare??
            //AnteprimaFrm.PrintPreviewControl1.BackColor = this.BackColor;
            //AnteprimaFrm.PrintPreviewControl1.BackgroundImage = this.BackgroundImage;


            // Set the Document property to the PrintDocument 
            // for which the PrintPage event has been handled.
            AnteprimaFrm.PrintPreviewControl1.Document = AnteprimaFrm.docToPrint;

            // Set the zoom to 25 percent.
            AnteprimaFrm.PrintPreviewControl1.Zoom = zoom;

            // Set the document name. This will show be displayed when 
            // the document is loading into the control.
            AnteprimaFrm.PrintPreviewControl1.Document.DocumentName = "Preview";

            // Set the UseAntiAlias property to true so fonts are smoothed
            // by the operating system.
            AnteprimaFrm.PrintPreviewControl1.UseAntiAlias = true;

            // Add the control to the form.
            //AnteprimaFrm.Controls.Add(AnteprimaFrm.PrintPreviewControl1);

            // Associate the event-handling method with the
            // document's PrintPage event.
            AnteprimaFrm.docToPrint.PrintPage +=
                new System.Drawing.Printing.PrintPageEventHandler(
                docToPrint_PrintPage);

            // Per stampare
            //AnteprimaFrm.docToPrint.Print();
            AnteprimaFrm.ShowDialog();
        }

        // The PrintPreviewControl will display the document
        // by handling the documents PrintPage event
        protected  void docToPrint_PrintPage(
            object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            // Insert code to render the page here.
            // This code will be called when the control is drawn.

            // The following code will render a simple
            // message on the document in the control.
            //string text = "In docToPrint_PrintPage method.";
            //System.Drawing.Font printFont =
            //    new Font("Arial", 35, FontStyle.Regular);
            //  e.Graphics.DrawString(text, printFont,
            //      Brushes.Black, 10, 10);



            Graphics g = e.Graphics;




            //Do Double Buffering
            //Bitmap offScreenBmp;
            // Graphics offScreenDC;
            //offScreenBmp = new Bitmap(this.Width, this.Height);
            //
            //offScreenDC = Graphics.FromImage(offScreenBmp);

            //offScreenDC.Clear(this.BackColor);

            //background image
            //if ((this.loadImage) & (this.visibleImage))
            //    offScreenDC.DrawImage(this.loadImg, 0, 0);
            //

            //offScreenDC.SmoothingMode=SmoothingMode.AntiAlias;
            
            // test
            //MessageBox.Show("dipx : " + g.DpiX + " ; dipy : " + g.DpiY);

            //s.Draw(offScreenDC);
            if (this.BackgroundImage != null)
                g.DrawImage(this.BackgroundImage, 0, 0);

            //TEST !!!!!!!!!!!!!!!!!!!!!!!!!!!1
            //this.DrawMesure(g);

            s.Draw(g,0,0,1);
            
            //if (this.MouseSx)
            //{
            //    System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Red);
            //    offScreenDC.DrawRectangle(myPen, new Rectangle(this.startX, this.startY, tmpX - this.startX, tmpY - this.startY));
            //    myPen.Dispose();
            //}

            //g.DrawImageUnscaled(offScreenBmp, 0, 0);

            g.Dispose();
        }


        #endregion



        public bool UndoEnabled()
        {
            return this.s.UndoEnabled();
        }

        public bool RedoEnabled()
        {
            return this.s.RedoEnabled();
        }

        public void Undo()
        {
           // this.s = this.undoB.Undo();
           // this.redraw();
            this.s.Undo();
            this.s.deSelect();
            this.redraw(true);
        }

        public void Redo()
        {
            this.s.Redo();
            this.s.deSelect();
            this.redraw(true);
        }


        #region SELECTED SHAPE COMMANDS

        public void setPenColor(Color c)
        {
            CreationPenColor = c;
            if (s.selEle != null)
            {
                this.s.selEle.penColor = c;
            }            
        }

        public void setFillColor(Color c)
        {
            CreationFillColor = c;
            if (s.selEle != null)
            {
                this.s.selEle.fillColor = c;
            }
        }

        public void setFilled(bool f)
        {
            CreationFilled = f;
            if (s.selEle != null)
            {
                this.s.selEle.filled = f;
            }
        }

        public void setPenWidth(float f)
        {
            CreationPenWidth = f;
            if (s.selEle != null)
            {
                this.s.selEle.penWidth = f;
            }
        }
        
  

        public void groupSelected()
        {            
            this.s.groupSelected();

            // show properties
            PropertyEventArgs e1 = new PropertyEventArgs(this.s.getSelectedArray(), this.s.RedoEnabled(), this.s.UndoEnabled());
            objectSelected(this, e1);// raise event


            redraw(true);
        }


        public void deGroupSelected()
        {
            this.s.deGroupSelected();
            // show properties
            PropertyEventArgs e1 = new PropertyEventArgs(this.s.getSelectedArray(), this.s.RedoEnabled(), this.s.UndoEnabled());
            objectSelected(this, e1);// raise event

            redraw(true);
        }

        

        public void rmSelected()
        {
            
            this.s.rmSelected();
            redraw(true);
        }

        //Test
        public void cpSelected()
        {
            //this.s.CopySelected(30, 20);
            this.s.CopyMultiSelected(25, 15);
            redraw(true);
        }

        public void primoPiano()
        {
            this.s.primoPiano();
            redraw(true);
        }

        public void secondoPiano()
        {
            this.s.secondoPiano();
            redraw(true);
        }

        #endregion

        #region LOAD/SAVE

        public bool Loader()
        {
            try
            {
                Stream StreamRead;
                OpenFileDialog DialogueCharger = new OpenFileDialog();
                DialogueCharger.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                DialogueCharger.DefaultExt = "shape";
                DialogueCharger.Title = "Load shape";
                DialogueCharger.Filter = "frame files (*.shape)|*.shape|All files (*.*)|*.*";
                if (DialogueCharger.ShowDialog() == DialogResult.OK)
                {
                    if ((StreamRead = DialogueCharger.OpenFile()) != null)
                    {
                        
                        BinaryFormatter BinaryRead = new BinaryFormatter();
                        this.s = (MapShapes) BinaryRead.Deserialize(StreamRead);
                        //g_l = (ExtGrpLst)BinaryRead.Deserialize(StreamRead);
                        StreamRead.Close();

                        this.s.afterLoad();

                        redraw(true);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception:" + e.ToString(), "Load error:");
            }
            return false;
        }

        /// <summary>
        /// HT Code: Load trực tiếp file
        /// </summary>
        /// <returns></returns>
        public bool VectShapeLoader()
        {
            try
            {
                string file = Path.GetDirectoryName(Application.ExecutablePath) + "\\routes.shape";
                FileStream StreamRead = new FileStream(file, FileMode.Open);

                BinaryFormatter BinaryRead = new BinaryFormatter();
                this.s = (MapShapes)BinaryRead.Deserialize(StreamRead);
                StreamRead.Close();
                this.s.afterLoad();

                //   redraw(true);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception:" + e.ToString(), "Load error:");
            }
            return false;
        }

        public bool Saver()
        {
            try
            {
                Stream StreamWrite;
                SaveFileDialog DialogueSauver = new SaveFileDialog();
                DialogueSauver.InitialDirectory = Path.GetDirectoryName(Application.ExecutablePath);
                DialogueSauver.DefaultExt = "shape";
                DialogueSauver.Title = "Save as shape";
                DialogueSauver.Filter = "shape files (*.shape)|*.shape|All files (*.*)|*.*";
                if (DialogueSauver.ShowDialog() == DialogResult.OK)
                {
                    if ((StreamWrite = DialogueSauver.OpenFile()) != null)
                    {
                        BinaryFormatter BinaryWrite = new BinaryFormatter();
                        BinaryWrite.Serialize(StreamWrite, this.s);
                        StreamWrite.Close();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception:" + e.ToString(), "Save error:");
            }
            return false;
        }

        /// <summary>
        /// HT Code: Save trực tiếp 
        /// </summary>
        /// <returns></returns>
        public bool VectShapeSaver()
        {
            try
            {
                //if (this.s.List.Count > 0)
                //{
                    string file = Path.GetDirectoryName(Application.ExecutablePath) + "\\routes.shape";
                    FileStream StreamWrite = new FileStream(file, FileMode.Create);
                    BinaryFormatter BinaryWrite = new BinaryFormatter();
                    BinaryWrite.Serialize(StreamWrite, this.s);
                    StreamWrite.Close();
                    return true;
                //}
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception:" + e.ToString(), "Save error:");
            }
            return false;
        }

        public bool SaveSelected()
        {
            ArrayList a = this.s.getSelectedList();
            if ((a!=null)&&(a.Count >0 ))
            {
                try
                {
                    Stream StreamWrite;
                    SaveFileDialog DialogueSauver = new SaveFileDialog();
                    DialogueSauver.DefaultExt = "sobj";
                    DialogueSauver.Title = "Save as sobj";
                    DialogueSauver.Filter = "sobj files (*.sobj)|*.sobj|All files (*.*)|*.*";
                    if (DialogueSauver.ShowDialog() == DialogResult.OK)
                    {
                        if ((StreamWrite = DialogueSauver.OpenFile()) != null)
                        {
                            BinaryFormatter BinaryWrite = new BinaryFormatter();
                            BinaryWrite.Serialize(StreamWrite, a);
                            StreamWrite.Close();
                            return true;
                        }
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show("Exception:" + e.ToString(), "Save error:");
                }             
            }
            return false;
        }

        public bool LoadObj()
        {
            try
            {
                Stream StreamRead;
                OpenFileDialog DialogueCharger = new OpenFileDialog();
                DialogueCharger.DefaultExt = "sobj";
                DialogueCharger.Title = "Load sobj";
                DialogueCharger.Filter = "frame files (*.sobj)|*.sobj|All files (*.*)|*.*";
                if (DialogueCharger.ShowDialog() == DialogResult.OK)
                {
                    if ((StreamRead = DialogueCharger.OpenFile()) != null)
                    {
                        BinaryFormatter BinaryRead = new BinaryFormatter();
                        ArrayList a = (ArrayList)BinaryRead.Deserialize(StreamRead);
                        //this.s = (ArrayList)BinaryRead.Deserialize(StreamRead);
                        this.s.setList(a);
                        StreamRead.Close();
                        this.s.afterLoad();
                        redraw(true);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception:" + e.ToString(), "Load error:");
            }
            return false;
        }


        #endregion

        #region IMG LOADER
        
        public string imgLoader()
        {
            try
            {
                OpenFileDialog DialogueCharger = new OpenFileDialog();
                DialogueCharger.Title = "Load background image";
                DialogueCharger.Filter = "jpg files (*.jpg)|*.jpg|bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
                //DialogueCharger.DefaultExt = "frame";
                if (DialogueCharger.ShowDialog() == DialogResult.OK)
                {
                    return (DialogueCharger.FileName);
                }
            }
            catch { }
            return null;
        }
        
        #endregion  

   
        
        public virtual void ucVectShapes_Paint(object sender, PaintEventArgs e)
        {
                
        }

        #region DRAWING

        protected  void GraphicSetUp(Graphics g)
        {
            g.CompositingQuality = this.CompositingQuality;
            g.TextRenderingHint = this.TextRenderingHint;
            g.SmoothingMode = this.SmoothingMode;
            g.InterpolationMode = this.InterpolationMode;
            //g.PageUnit = GraphicsUnit.Millimeter;
        }

        /// <summary>
        /// Testing save to *bmp
        /// </summary>
        public void saveBmp()
        {
            offScreenBackBmp.Save("test.bmp");
        }

        /// <summary>
        /// redraws this.s on this control
        /// All=true : redraw all graphic
        /// All=false : redraw only selected objects
        /// </summary>
        public virtual void redraw(bool All)
        {
            
        }



        protected  void drawDebugInfo(Graphics g)
        {
            //Draw msg
            if (ShowDebug)
            {
             msg = " Status : " + this.Status;
             msg = msg + " Option : " + this.Option;
             msg = msg + " redimStatus : " + this.redimStatus;
             Font tmpf = new System.Drawing.Font("Arial", 7);
             g.DrawString(msg, tmpf, new SolidBrush(Color.Gray), new PointF((tempX + this.dx) * this.Zoom, (tempY + this.dy) * this.Zoom), StringFormat.GenericDefault);
             tmpf.Dispose();
            }
        }


        public virtual void redraw(Graphics g,bool All)
        {
            
        }

        protected  void DrawMesure(Graphics g)
        {
            /* TEST*/
            GraphicsUnit u = g.PageUnit;
            g.PageUnit = GraphicsUnit.Millimeter;
            
            // draws in millimeters
            System.Drawing.Pen myPen = new System.Drawing.Pen(System.Drawing.Color.Black, 0.5f);
             g.DrawLine(myPen, 0, 5, 210, 5);
             for (int i = 0; i < 210;i = i + 10 )
             {
                g.DrawLine(myPen, i, 5, i, 0);   
             }
            g.PageUnit = u;
            
        }

        #endregion

        

        public void addPointSet(ArrayList a)
        {
            ArrayList tmpa = new ArrayList();
            foreach (PointWr p in a)
            {
                PointColor tmpp = new PointColor(p.point);
                tmpp.col = Color.Red;
                tmpa.Add(tmpp);
            }
              
            //this.s.addPoly(0, 0, 0, 0, Color.Black, Color.Black, 1, false, a, true);
            this.s.addColorPoinySet(0, 0, 0, 0, Color.Black, Color.Black, 1, false, tmpa, true);
        }
        public ArrayList getPointSet()
        {
            if (this.s.selEle != null)
                if (this.s.selEle is PointSet)
                    return ((PointSet)this.s.selEle).getRealPosPoints();

            return null;
            
        }

        public void propertyChanged()
        {
            this.s.Propertychanged();
        }

        public void HelpForm(string s)
        {
            //Help h = new Help();
            //h.setMsg(s);
            //h.ShowDialog();
        }

        protected  void vectShapes_Resize(object sender, EventArgs e)
        {
            if (this.Width > 0 & this.Height > 0)
            {
                offScreenBackBmp = new Bitmap(this.Width, this.Height);
                offScreenBmp = new Bitmap(this.Width, this.Height);
                redraw(true);
            }
        }

        private void vectShapes_Load(object sender, EventArgs e)
        {

        }

        protected void MouseDownEvent(object sender, int x, int y, MouseButtons button)
        {
            this.startX = (int)(x / Zoom - this.dx);
            this.startY = (int)(y / Zoom - this.dy);
            //this.startX = (int)( e.x- this.dx  );
            //this.startY = (int)( e.y - this.dy );

            this.truestartX = x;
            this.truestartY = y;

            if (button == MouseButtons.Left)
            {
                #region START LEFT MOUSE BUTTON PRESSED
                this.MouseSx = true; // I start pressing SX
                switch (this.Option)
                {
                    case enumOption.SELECT: // case "select":

                        if (this.redimStatus != enumRedimStatus.NO)
                        {
                            // I'm over an object or Object redim handle
                            changeStatus(enumStatus.REDIM);// I'm starting redim/move action
                        }
                        else
                        {
                            // I'm pressing SX in an empty space, I'm starting a select rect
                            changeStatus(enumStatus.SELRECT);// I'm starting a select rect
                        }

                        break;
                    case enumOption.PEN: //case "PEN":
                        PenPointList = new ArrayList();//reset pints buffer
                        VisPenPointList = new ArrayList();//reset pints buffer
                        PenPrecX = this.startX;
                        PenPrecY = this.startY;
                        PenPointList.Add(new PointWr(0, 0));
                        VisPenPointList.Add(new PointWr(0, 0));
                        break;
                    //case "NEWPOLY":

                    //    /* if Option != "select" then I'm going tocreate an object*/
                    //    //                    this.MouseSx = true; // I start pressing SX
                    //    changeStatus("drawrect"); //I 'm startring drawing a new object
                    //    break;
                    default:
                        /* if Option != "select" then I'm going tocreate an object*/
                        //                    this.MouseSx = true; // I start pressing SX
                        changeStatus(enumStatus.DRAWRECT); ; //I 'm startring drawing a new object
                        break;
                }
                #endregion
            }
            else
            {
                #region START RIGHT MOUSE BUTTON PRESSED
                this.startDX = this.dx;
                this.startDY = this.dy;
                #endregion
            }

        }

        protected void MouseMoveEvent(object sender, int x, int y, MouseButtons button)
        {
            //Nếu đang vẽ đối tượng
            if (this.MouseSx)
            {
                tempX = (int)(x / Zoom);
                tempY = (int)(y / Zoom);
                if (fit2grid & this.gridSize > 0)
                {
                    tempX = this.gridSize * (int)((x / Zoom) / this.gridSize);
                    tempY = this.gridSize * (int)((y / Zoom) / this.gridSize);
                }
                tempX = tempX - this.dx;
                tempY = tempY - this.dy;

            }

            if (button == MouseButtons.Left)
            #region left mouse button pressed
            {
                //HT Note: xảy ra khi thay đổi kích thước shape
                if (this.Status == enumStatus.REDIM)
                {
                    int tmpX = (int)(x / Zoom - this.dx);
                    int tmpY = (int)(y / Zoom - this.dy);
                    int tmpstartX = startX;
                    int tmpstartY = startY;
                    if (fit2grid & this.gridSize > 0)
                    {
                        tmpX = this.gridSize * (int)((x / Zoom - this.dx) / this.gridSize);
                        tmpY = this.gridSize * (int)((y / Zoom - this.dy) / this.gridSize);
                        tmpstartX = this.gridSize * (int)(startX / this.gridSize);
                        tmpstartY = this.gridSize * (int)(startY / this.gridSize);
                        s.Fit2grid(this.gridSize);
                        s.sRec.Fit2grid(this.gridSize);
                    }

                    switch (this.redimStatus)
                    {
                        //Poly's point
                        case enumRedimStatus.POLY: //HT Note: trường hợp chọn các node của một poly để redim
                            // Move selected
                            if (s.selEle != null & s.sRec != null)
                            {
                                //s.movePoint(tmpstartX - tempX, tmpstartY - tempY);
                                s.movePoint(tmpstartX - tmpX, tmpstartY - tmpY);
                            }
                            if (fit2grid & this.gridSize > 0)
                            {
                                s.Fit2grid(this.gridSize);
                                s.sRec.Fit2grid(this.gridSize);
                            }
                            break;
                        //Graph's point
                        case enumRedimStatus.GRAPH:
                            // Move selected
                            if (s.selEle != null & s.sRec != null)
                            {
                                s.movePointG(tmpstartX - tmpX, tmpstartY - tmpY);
                            }
                            if (fit2grid & this.gridSize > 0)
                            {
                                s.Fit2grid(this.gridSize);
                                s.sRec.Fit2grid(this.gridSize);
                            }
                            break;
                        //case "C":
                        case enumRedimStatus.C:
                            // Move selected
                            if (s.selEle != null & s.sRec != null)
                            {
                                s.move(tmpstartX - tmpX, tmpstartY - tmpY);
                                s.sRec.move(tmpstartX - tmpX, tmpstartY - tmpY);
                            }
                            break;
                        //case "ROT":
                        case enumRedimStatus.ROT:
                            // rotate selected
                            if (s.selEle != null & s.sRec != null)
                            {
                                s.selEle.Rotate(tmpX, tmpY);
                                s.sRec.Rotate(tmpX, tmpY);
                            }
                            break;
                        case enumRedimStatus.ZOOM:
                            // rotate selected
                            if (s.selEle != null & s.sRec != null)
                            {
                                if (s.selEle is Group)
                                {
                                    ((Group)s.selEle).setZoom(tmpX, tmpY);
                                    s.sRec.setZoom(((Group)s.selEle).GrpZoomX, ((Group)s.selEle).GrpZoomY);

                                    //// redim selected
                                    //if (s.selEle != null & s.sRec != null)
                                    //{
                                    //    this.redimStatus = "E";
                                    //    s.selEle.redim(tmpX - tmpstartX, 0, this.redimStatus);
                                    //    s.sRec.redim(tmpX - tmpstartX, 0, this.redimStatus);

                                    //    this.redimStatus = "S";
                                    //    s.selEle.redim(0, tmpY - tmpstartY, this.redimStatus);
                                    //    s.sRec.redim(0, tmpY - tmpstartY, this.redimStatus);
                                    //    this.redimStatus = "ZOOM";
                                    //}
                                }
                            }
                            break;
                        default:
                            // redim selected
                            if (s.selEle != null & s.sRec != null)
                            {
                                //HT Code: chuyển kiểu enum sang string
                                string redimStatusValue = Enum.GetName(typeof(enumRedimStatus), this.redimStatus);
                                s.selEle.redim(tmpX - tmpstartX, tmpY - tmpstartY, redimStatusValue);
                                s.sRec.redim(tmpX - tmpstartX, tmpY - tmpstartY, redimStatusValue);
                            }
                            break;
                    }

                }

                else
                {
                    //if (this.Option == "PEN")
                    if (this.Option == enumOption.PEN)
                    {
                        //this.s.addEllipse(tempX,tempY,tempX+1,tempY+1,Color.Blue,Color.Blue,1f,false);
                        VisPenPointList.Add(new PointWr(tempX - this.startX, tempY - this.startY));
                        if (Math.Sqrt(Math.Pow(PenPrecX - tempX, 2) + Math.Pow(PenPrecY - tempY, 2)) > 15)
                        {
                            PenPointList.Add(new PointWr(tempX - this.startX, tempY - this.startY));
                            PenPrecX = this.tempX;
                            PenPrecY = this.tempY;
                        }

                        this.redraw(false);
                    }
                }

                redraw(false);

            }
            #endregion
            else if (button == MouseButtons.Right)
            #region right mouse button pressed
            {

                this.dx = (this.startDX + this.truestartX - (int)x);
                this.dy = (this.startDY + this.truestartY - (int)y);
                if (fit2grid & this.gridSize > 0)
                {
                    this.dx = this.gridSize * (int)((this.dx) / this.gridSize);
                    this.dy = this.gridSize * (int)((this.dy) / this.gridSize);
                }
                this.redraw(true);
            }
            #endregion
        }

        protected void MouseUpEvent(object sender, int x, int y, MouseButtons button)
        {
            int tmpX = (int)((x) / Zoom - this.dx);
            int tmpY = (int)((y) / Zoom - this.dy);
            if (fit2grid & this.gridSize > 0)
            {
                tmpX = this.gridSize * (int)((x / Zoom - this.dx) / this.gridSize);
                tmpY = this.gridSize * (int)((y / Zoom - this.dy) / this.gridSize);
            }

            if (button == MouseButtons.Left)
            #region left up
            {
                //HT Code: chuyển tọa độ màn hình sang tọa độ lon, lat
                double lon, lat, lon1, lat1;
                lon = lat = lon1 = lat1 = 0;
                ConvertScreenToMapCoord(startX, startY, tmpX, tmpY, ref lon, ref lat, ref lon1, ref lat1);
                //if (s.selEle !=null)
                  //  this.s.selEle.UpdateLonLatCoord(lon, lat, lon1, lat1);
                

                switch (this.Option)
                {
                    #region selectrect

                    case enumOption.SELECT: //case "select":
                        if (this.Status != enumStatus.REDIM)
                        {
                            this.s.click(tmpX, tmpY, this.r);
                        }
                        else
                        {
                            if (this.s.selEle is PointSet)
                            {
                                //POLY MANAGEMENT START
                                s.addPoint(); //Thêm node mới nếu click vào node newpoint handle
                                //((PointSet)this.s.selEle).rePos();
                                if (fit2grid & this.gridSize > 0)
                                {
                                    this.s.Fit2grid(this.gridSize);
                                    //this.s.sRec = new SelPoly(this.s.selEle);//create handling rect
                                }
                                switch (this.redimStatus)
                                {
                                    case enumRedimStatus.ROT:
                                        this.s.selEle.CommitRotate(tmpX, tmpY);
                                        //this.s.sRec = new SelPoly(this.s.selEle);//create handling rect                                     
                                        break;
                                    default:
                                        break;
                                } //POLY MANAGEMENT END
                            }
                            else if (this.s.selEle is Graph)
                            {
//GRAPH MANAGEMENT START
                                s.addPoint();
                                //((PointSet)this.s.selEle).rePos();
                                if (fit2grid & this.gridSize > 0)
                                {
                                    this.s.Fit2grid(this.gridSize);
                                    //this.s.sRec = new SelPoly(this.s.selEle);//create handling rect
                                }
                                switch (this.redimStatus)
                                {
                                    case enumRedimStatus.ROT:
                                        this.s.selEle.CommitRotate(tmpX, tmpY);
                                        //this.s.sRec = new SelPoly(this.s.selEle);//create handling rect                                     
                                        break;
                                    default:
                                        break;
                                } //GRAPH MANAGEMENT END
                            }
                            else
                            {
                                this.s.selEle.UpdateLonLatCoord(lon, lat, lon1, lat1);
                            }

                        }

                        if (this.Status == enumStatus.SELRECT)
                        {
                            if ((((x)/Zoom - this.dx - this.startX) + ((y)/Zoom - this.dy - this.startY)) > 12)
                            {
                                // manage multi objeect selection
                                this.s.multiSelect(this.startX, this.startY, (int) ((x)/Zoom - this.dx),
                                                   (int) ((y)/Zoom - this.dy), this.r);
                            }
                        }
                        
                        //Thay đổi status về empty
                        changeStatus(enumStatus.NONE);
                        break;

                        #endregion

                        #region Rect

                    case enumOption.DR: //case "DR": //DrawRect

                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            this.s.addRect(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor,
                                           CreationPenWidth, CreationFilled);
                            this.s.selEle.UpdateLonLatCoord(lon, lat, lon1, lat1);
                            //this.Option = "select";
                            //changeOption("select");
                            changeOption(enumOption.SELECT);
                        }
                        break;

                        #endregion

                        //#region LINk TEST
                        //case "LINK": //Link//test
                        //    if (this.Status == enumStatus.DRAWRECT)
                        //    {
                        //        changeOption("select");
                        //    }
                        //    break;
                        //#endregion

                        #region Arc

                    case enumOption.ARC: //case "ARC": //Arc
                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            this.s.addArc(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor,
                                          CreationPenWidth, CreationFilled);
                            this.s.selEle.UpdateLonLatCoord(lon, lat, lon1, lat1);
                            //changeOption("select");
                            changeOption(enumOption.SELECT);
                        }
                        break;

                        #endregion

                        #region Poly & Pen & Graph

                    case enumOption.PEN: //case "PEN":
                        //if (this.Status == enumStatus.DRAWRECT)
                        //{
                        this.s.addPoly(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor,
                                       CreationPenWidth, CreationFilled, PenPointList, true);
                        this.s.selEle.UpdateLonLatCoord(lon, lat, lon1, lat1);
                        PenPointList = null;
                        VisPenPointList = null;
                        changeOption(enumOption.SELECT);
                        //}
                        break;
                    case enumOption.POLY: //case "POLY": //polygon/pointSet/curvedshape..
                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            ArrayList aa = new ArrayList();
                            aa.Add(new PointWr(0, 0));
                            aa.Add(new PointWr(tmpX - startX, tmpY - startY));
                            //aa.Add(new PointWr(tmpX - startX + 50, tmpY - startY + 50));
                            this.s.addPoly(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor,
                                           CreationPenWidth, CreationFilled, aa, false);
                            this.s.selEle.UpdateLonLatCoord(lon, lat, lon1, lat1);
                            //this.s.addPoly(startX, startY, tmpX - startX + 50, tmpY - startY + 50, this.CreationPenColor, CreationFillColor, CreationPenWidth, CreationFilled, aa, false);
                            changeOption(enumOption.SELECT);
                        }
                        break;
                    case enumOption.NEWPOLY: //case "NEWPOLY": //polygon/pointSet/curvedshape..
                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            this.polyPoints.Add(new Point(tmpX, tmpY));
                            //redraw(true);
                        }
                        break;
                    case enumOption.GRAPH: //case "GRAPH":
                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            ArrayList aa = new ArrayList();
                            aa.Add(new PointWr(0, 0));
                            //aa.Add(new PointWr(System.Math.Abs(startX - tmpX), System.Math.Abs(startY - tmpY)));
                            aa.Add(new PointWr(tmpX - startX, tmpY - startY));
                            this.s.addGraph(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor,
                                            CreationPenWidth, CreationFilled, aa);
                            this.s.selEle.UpdateLonLatCoord(lon, lat, lon1, lat1);
                            changeOption(enumOption.SELECT);
                        }
                        break;

                        #endregion

                        #region RRect

                    case enumOption.DRR: //case "DRR": //DrawRRect
                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            this.s.addRRect(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor,
                                            CreationPenWidth, CreationFilled);
                            this.s.selEle.UpdateLonLatCoord(lon, lat, lon1, lat1);
                            //this.Option = "select";
                            changeOption(enumOption.SELECT);
                        }
                        break;

                        #endregion

                        #region Ellipse

                    case enumOption.ELL: //case "ELL": //DrawEllipse

                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            this.s.addEllipse(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationFillColor,
                                              CreationPenWidth, CreationFilled);
                            this.s.selEle.UpdateLonLatCoord(lon, lat, lon1, lat1);
                            //this.Option = "select";
                            changeOption(enumOption.SELECT);
                        }
                        break;

                        #endregion

                        #region DrawTextBox

                    case enumOption.TB: //case enumOption.TB: //DrawTextBox
                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            ////this.ucMap1.Map.Cursor = Cursors.WaitCursor;
                            //this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miHourglassCursor;
                            //editorFrm.ShowDialog();
                            //this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miDefaultCursor;
                            //this.s.addTextBox(startX, startY, tmpX, tmpY, r, this.CreationPenColor, CreationFillColor,
                            //                  CreationPenWidth, CreationFilled);
                            //changeOption(enumOption.SELECT);
                        }
                        break;

                        #endregion

                        #region DrawSimpleTextBox

                    case enumOption.STB: //case "STB": //DrawSimpleTextBox
                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            ////this.ucMap1.Map.Cursor = Cursors.WaitCursor;
                            //this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miHourglassCursor;
                            //editorFrm.ShowDialog();
                            //this.ucMap1.Map.MousePointer = MapXLib.CursorConstants.miDefaultCursor;
                            //this.s.addSimpleTextBox(startX, startY, tmpX, tmpY, r, this.CreationPenColor,
                            //                        CreationFillColor, CreationPenWidth, CreationFilled);
                            //changeOption(enumOption.SELECT);
                        }
                        break;

                        #endregion

                        #region ImgBox

                    case enumOption.IB: //case "IB": //DrawImgBox

                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            // load image

                            string f_name = this.imgLoader();
                            this.s.addImgBox(startX, startY, tmpX, tmpY, f_name, this.CreationPenColor, CreationPenWidth);
                            this.s.selEle.UpdateLonLatCoord(lon, lat, lon1, lat1);
                            //this.Option = "select";
                            changeOption(enumOption.SELECT);
                        }
                        break;

                        #endregion

                        #region Line

                    case enumOption.LI: //case "LI": //Draw Line
                        if (this.Status == enumStatus.DRAWRECT)
                        {
                            this.s.addLine(startX, startY, tmpX, tmpY, this.CreationPenColor, CreationPenWidth);
                            this.s.selEle.UpdateLonLatCoord(lon, lat, lon1, lat1);
                            //this.Option = "select";
                            changeOption(enumOption.SELECT);
                        }
                        break;

                        #endregion

                    default:
                        //this.Status = "";
                        changeStatus(enumStatus.NONE);
                        break;
                }

                #region HT CODE: LEFT MOUSE UP NEWPOLY

                //HT Code: Trường hợp khi nhấn chuột trái mouse up mà không phải trường hợp đang vẽ poly
                //Chuyển tọa độ pixel sang tọa độ Lon, Lat và lưu lại
                //if (this.Option != "NEWPOLY")
                if (this.Option == enumOption.SELECT) // && this.Option != enumOption.NEWPOLY)
                {
                    // store start X,Y,X1,Y1 of selected item
                    if (this.s.selEle != null)
                    {
                        //Nếu shape là tập các điểm (poly)
                        if (this.s.selEle is PointSet)
                        {
                            ////============================================
                            ////HT Code: Chuyển tọa độ pixel sang tọa độ kinh vĩ độ
                            ////và lưu vào các biến this.s.selEle.LeftLon, this.s.selEle.TopLat, this.s.selEle.RightLon, this.s.selEle.BottomLat
                            ////và biến ((PointSet)this.s.selEle).mapPoints
                            //float x, y, x1, y1;
                            //x = this.s.selEle.X;
                            //y = this.s.selEle.Y;
                            //x1 = this.s.selEle.X1;
                            //y1 = this.s.selEle.Y1;

                            //ucMap1.Map.ConvertCoord(ref x, ref y, ref this.s.selEle.LeftLon, ref this.s.selEle.TopLat,
                            //                        ConversionConstants.miScreenToMap);
                            //ucMap1.Map.ConvertCoord(ref x1, ref y1, ref this.s.selEle.RightLon,
                            //                        ref this.s.selEle.BottomLat, ConversionConstants.miScreenToMap);
                            //if (this.s.selEle is PointSet)
                            //{
                            //Tính giá trị min trong poly 
                            //(không cần tính max vì max được tính ở hàm setupSize() của class PointSet)
                            //ArrayList mPoints = new ArrayList();
                            //foreach (PointWr p in ((PointSet) this.s.selEle).Points)
                            //{
                            //    float tempx = (float) (this.s.selEle.X + p.X);
                            //    float tempy = (float) (this.s.selEle.Y + p.Y);
                            //    double tempLon, tempLat;
                            //    tempLon = tempLat = 0;
                            //    ucMap1.Map.ConvertCoord(ref tempx, ref tempy, ref tempLon, ref tempLat, ConversionConstants.miScreenToMap);
                            //    PointF newp = new PointF((float) tempLon, (float) tempLat);
                            //    mPoints.Add(newp);
                            //}
                            //((PointSet) this.s.selEle).mapPoints = mPoints;
                            //}
                            //============================================
                        }
                    }

                #endregion

                    // store start X,Y,X1,Y1 of selected item
                    if (this.s.selEle != null)
                    {
                        //==========================
                        //Xác định lại kích thước vùng chọn của đối tượng poly
                        if (this.s.selEle is PointSet)
                        {
                            //POLY MANAGEMENT START
                            //Xác định kích thước rect bao quanh của shape
                            ((PointSet)this.s.selEle).setupSize();
                            //Tạo các handle nodes
                            this.s.sRec = new SelPoly(this.s.selEle); //create handling rect
                        }
                        else if (this.s.selEle is Graph)
                        {
                            //GRAPH MANAGEMENT START
                            ((Graph)this.s.selEle).setupSize();
                            this.s.sRec = new SelGraph(this.s.selEle); //create handling rect
                        }

                        //==========================
                        //Sau khi redim xong thì gọi endMove để kết thúc vẽ
                        if (this.redimStatus != enumRedimStatus.NO)
                        {
                            this.s.endMove();
                        }

                        if (this.s.sRec != null)
                        {
                            this.s.sRec.endMoveRedim();
                        }
                    }


                    // show properties
                    PropertyEventArgs e1 = new PropertyEventArgs(this.s.getSelectedArray(), this.s.RedoEnabled(),
                                                                 this.s.UndoEnabled());
                    objectSelected(this, e1); // raise event

                    redraw(true); //redraw all=true 

                    this.MouseSx = false; // end pressing SX
                }
            }
                #endregion
            else
            #region right up
            {
                // store start X,Y,X1,Y1 of selected item
                if (this.Status == enumStatus.DRAWRECT)
                {
                    //Nếu là đang vẽ NewPoly: click chuột phải để kết thúc vẽ
                    //if (this.Option == "NEWPOLY")
                    if (this.Option == enumOption.NEWPOLY)
                    {
                        int minX, minY, maxX, maxY;
                        minX = minY = int.MaxValue;
                        maxX = maxY = int.MinValue;
                        this.polyPoints.Add(new Point(tmpX, tmpY));

                        //Tính giá trị min, max trong poly 
                        foreach (Point p in this.polyPoints)
                        {
                            minX = Math.Min(minX, p.X);
                            minY = Math.Min(minY, p.Y);
                            maxX = Math.Max(maxX, p.X);
                            maxY = Math.Max(maxY, p.Y);
                        }

                        ArrayList aa = new ArrayList();
                        foreach (Point p in this.polyPoints)
                        {
                            aa.Add(new PointWr(p.X - minX, p.Y - minY));
                        }
                        //Chuyển đổi tọa độ các points từ pixel thành lon, lat
                        AddPoly(aa, minX, minY, maxX, maxY);

                        changeOption(enumOption.SELECT);

                        //reset  polyPoint
                        polyPoints = new List<Point>();

                        // store start X,Y,X1,Y1 of selected item
                        if (this.s.selEle != null)
                        {
                            //Nếu shape là tập các điểm (poly)
                            if (this.s.selEle is PointSet)
                            {
                                //POLY MANAGEMENT START
                                //Xác định kích thước rect bao quanh của shape
                                ((PointSet)this.s.selEle).setupSize();
                                //Tạo các handle nodes
                                this.s.sRec = new SelPoly(this.s.selEle); //create handling rect
                            }
                            else if (this.s.selEle is Graph)
                            {
                                //GRAPH MANAGEMENT START
                                ((Graph)this.s.selEle).setupSize();
                                this.s.sRec = new SelGraph(this.s.selEle); //create handling rect
                            }


                            if (this.redimStatus != enumRedimStatus.NO)
                            {
                                this.s.endMove();
                            }

                            if (this.s.sRec != null)
                            {
                                this.s.sRec.endMoveRedim();
                            }
                        }


                        // show properties
                        PropertyEventArgs e1 = new PropertyEventArgs(this.s.getSelectedArray(), this.s.RedoEnabled(),
                                                                     this.s.UndoEnabled());
                        objectSelected(this, e1); // raise event

                        redraw(true); //redraw all=true 

                        this.MouseSx = false; // end pressing SX
                    }

                }
                else
                {
                    // show properties
                    PropertyEventArgs e1 = new PropertyEventArgs(this.s.getSelectedArray(), this.s.RedoEnabled(),
                                                                 this.s.UndoEnabled());
                    objectSelected(this, e1); // raise event
                }

            }

            #endregion
        }

        protected virtual void AddPoly(ArrayList aa, int minX, int minY, int maxX, int maxY)
        {
            return;
        }

        protected virtual void ConvertScreenToMapCoord(int minX, int minY, int maxX, int maxY, ref double lon, ref double lat, ref double lon1, ref double lat1)
        {
            return;
        }

        public virtual void ChangeCursor()
        {
            return;
        }
    }
}

