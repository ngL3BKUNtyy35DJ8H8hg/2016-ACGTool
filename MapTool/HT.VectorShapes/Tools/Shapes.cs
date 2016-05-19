using System;
using System.Collections;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using HT.VectorShapes.Shapes;

//using System.Collections.Generic;

namespace HT.VectorShapes.Tools
{

    //Grouped objs properties. When you decide to create a group with the
    // selected objs, U can preset these pprops in the objs:
    // see: 
        //_onGroupXRes 
        //_onGroupX1Res
        //_onGroupYRes 
        //_onGroupY1Res 
    public enum OnGroupResize { Move, Resize, Nothing};

    public enum GroupDisplay { Default, Intersect, Xor, Exclude, Union, Complement, Replace };

    #region Events & Delegates (Used by the controls vectShape & toolBox )
    public delegate void VectShapeOptionChanged(object sender, VectShapeOptionEventArgs e);

    public delegate void OptionChanged(object sender, OptionEventArgs e);

    public delegate void ObjectSelected(object sender, PropertyEventArgs e);

    public class PropertyEventArgs : EventArgs
    {
        public Ele[] ele;
        public bool Undoable; //for enable/disable udo/redoBtn
        public bool Redoable; //for enable/disable udo/redoBtn
        //Constructor.
        public PropertyEventArgs(Ele[] a,bool r, bool u)
        {
            this.ele = a;
            this.Undoable = u;
            this.Redoable = r;
        }
    }

    public class OptionEventArgs : EventArgs 
    {  
      public string option ;
      
      //Constructor.
      //
      public OptionEventArgs(string s) 
      {
         this.option = s;
      }
    }

    public class VectShapeOptionEventArgs : EventArgs
    {
        public enumDrawingOption option;

        //Constructor.
        //
        //public OptionEventArgs(string s) 
        public VectShapeOptionEventArgs(enumDrawingOption s)
        {
            this.option = s;
        }
    }

    #endregion

    #region Main class Shapes: the collection of Ele

    /// <summary>
    /// Gestisce l'insieme degli oggetti vettoriali 
    /// </summary>
    [Serializable]
    public class Shapes
    {
        // List of objects on the canvas
        public ArrayList List;
        // The Handles Obj
        public AbstractSel sRec; //HT Note: biến vẽ hình chữ nhật đối tượng được chọn
        //The selected ele
        public Ele selEle;

        public int minDim = 10;

        //the undo/redo buffer 
        [NonSerialized]
            private UndoBuffer undoB;

        public Shapes(float dpix,float dpiy)
        {
            List = new ArrayList();
            initUndoBuff();
                    Ele.dpix = dpix;
                    Ele.dpiy = dpiy;
        }

        private void initUndoBuff()
        {
            undoB = new UndoBuffer(20);// set dim of undo buffer
        }

        public bool IsEmpty()
        {
            return ( List.Count>0 );
        }

        public void afterLoad()
        {
            // UndoBuff is not serialized, I must reinit it after LOAD from file
            initUndoBuff();
            foreach (Ele e in this.List)
                e.AfterLoad();
        }

        /// <summary>
        /// Copy all selected Items
        /// </summary>
        public void CopyMultiSelected(int dx, int dy)
        {
            ArrayList tmpList = new ArrayList();
            foreach (Ele elem in this.List)
            {
                if (elem.Selected)
                {
                    Ele eL = elem.Copy();
                    elem.Selected = false;
                    eL.move(dx, dy);
                    tmpList.Add(eL);
                    //
                    sRec = new SelRect(eL);
                    selEle = eL;
                    selEle.endMoveRedim();
                }
            }
            foreach (Ele tmpElem in tmpList)
            {
                this.List.Add(tmpElem);
                // store the operation in undo/redo buffer
                storeDo("I", tmpElem);
            }
                        
        }


        /// <summary>
        /// returns a Copy of selected element
        /// </summary>
        public Ele CpSelected()
        {
            if (this.selEle != null)
            {
                Ele L = selEle.Copy();
                return L;
            }
            return null;
        }

        /// <summary>
        /// Copy selected Item 
        /// </summary>
        public void CopySelected(int dx,int dy)
        {
            if (this.selEle != null)
            {

                Ele L = this.CpSelected();
                L.move(dx, dy);
                this.deSelect();
                this.List.Add(L);
                
                // store the operation in undo/redo buffer
                storeDo("I", L);

                sRec = new SelRect(L);
                //sRec.isaLine = L.isaLine;
                selEle = L;
                selEle.endMoveRedim();
            }
        }

        /// <summary>
        /// Elimina oggetto selezioanto
        /// </summary>
        public void rmSelected()
        {
            ArrayList tmpList = new ArrayList();
            foreach (Ele elem in this.List)
            {
                if (elem.Selected)
                {
                    tmpList.Add(elem);    
                }
            }

            if (this.selEle != null)
            {
                //this.List.Remove(selEle);
                selEle = null;
                this.sRec = null;
            }

            foreach (Ele tmpElem in tmpList)
            {
                this.List.Remove(tmpElem);
                
                // store the operation in undo/redo buffer
                storeDo("D", tmpElem);
                
            }

        }

        /// <summary>
        /// Grup selected objs
        /// </summary>
        public void groupSelected()
        {
            ArrayList tmpList = new ArrayList();
            foreach (Ele elem in this.List)
            {
                if (elem.Selected)
                {
                    tmpList.Add(elem);
                }
            }

            if (this.selEle != null)
            {
                //this.List.Remove(selEle);
                selEle = null;
                this.sRec = null;
            }

            foreach (Ele tmpElem in tmpList)
            {
                this.List.Remove(tmpElem);

                // store the operation in undo/redo buffer
                //storeDo("D", tmpElem);
            }

            Group g = new Group(tmpList);
            
            this.List.Add(g);
            // store the operation in undo/redo buffer
            //storeDo("I", g);

            sRec = new SelRect(g);
            //sRec.showHandles(true);
            selEle = g;
            selEle.Select();
            
            // when grouping / degrouping reset undoBuffer
            this.undoB = new UndoBuffer(20);
        }

        /// <summary>
        /// Grup selected objs
        /// </summary>
        public void deGroupSelected()
        {
            ArrayList tmpList = new ArrayList();
            foreach (Ele elem in this.List)
            {
                if (elem.Selected)
                {
                    tmpList.Add(elem);
                }
            }

            if (this.selEle != null)
            {
                //this.List.Remove(selEle);
                selEle = null;
                this.sRec = null;
            }
            bool found = false;
            foreach (Ele tmpElem in tmpList)
            {
                ArrayList tmpL = tmpElem.deGroup();

                if (tmpL != null)
                {
                    foreach (Ele e1 in tmpL)
                    {
                        this.List.Add(e1);
                    }
                    this.List.Remove(tmpElem);
                    found = true;
                }                
            }
            if (found)
            {
                // when grouping / degrouping reset undoBuffer
                this.undoB = new UndoBuffer(20);
            }


        }

        public void movePoint(int dx, int dy)
        {
            ((SelPoly)this.sRec).movePoints(dx,dy);
            ((SelPoly)this.sRec).reCreateCreationHandles((PointSet)this.selEle);
        }

        public void movePointG(int dx, int dy)
        {
            ((SelGraph)this.sRec).movePoints(dx, dy);
            ((SelGraph)this.sRec).reCreateCreationHandles((Graph)this.selEle);
        }


        public void addPoint()
        {
            if (this.sRec is SelPoly)
            {
                PointWr p = ((SelPoly)this.sRec).getNewPoint();
                int i = ((SelPoly)this.sRec).getIndex();
                if (i > 0)
                {
                    ((PointSet)this.selEle).points.Insert(i - 1, p);
                    sRec = new SelPoly(selEle);//create handling rect
                }
            }
            else
            {
                if (this.sRec is SelGraph)
                {

                    NewPointHandle hnd = ((SelGraph)this.sRec).getNewPointHandle();
                    if (hnd != null)
                    {
                        PointWr p = hnd.getPoint();
                        GrArc a = hnd.getArc();

                        if (p != null)
                        {
                            if (a != null)
                            {
                                //Destroy arc (s-e) a and build 3 new arcs (s-p,p-e,p-p1)
                                // s----e
                                //
                                //  s--p--e
                                //     |
                                //     p1

                                PointWr s = a.start;
                                PointWr e = a.end;


                                ((Graph)this.selEle).arcs.Remove(a);

                                PointWr p1 = new PointWr(p.X, p.Y + 10);
                                int i = ((SelGraph)this.sRec).getIndex();
                                if (i > 0)
                                {
                                    ((Graph)this.selEle).points.Insert(i - 1, p1);
                                    ((Graph)this.selEle).points.Insert(i - 1, p);


                                    ((Graph)this.selEle).arcs.Add(new GrArc(p, p1));
                                    ((Graph)this.selEle).arcs.Add(new GrArc(s, p));
                                    ((Graph)this.selEle).arcs.Add(new GrArc(p, e));

                                    sRec = new SelGraph(selEle);//create handling rect
                                }
                            }
                            else
                            {
                                PointWr realp = hnd.getRealPoint();
                                if (realp != null)
                                {
                                    ((Graph)this.selEle).arcs.Add(new GrArc(realp, p));
                                    ((Graph)this.selEle).points.Insert(0, p);

                                }
                            }
                        }
                    }
                }

            
            }


        }


        public void linkNodes()
        {
            if (this.sRec is SelGraph)
            {
                ArrayList tmp = ((SelGraph)this.sRec).getSelPoints();
                //if (tmp.Count < ((Graph)this.selEle).points.Count - 1)
                if (tmp!=null)
                {
                    PointWr first = null;
                    PointWr second = null;
                    foreach (PointWr p in tmp)
                    {
                        if (p.selected)
                        {
                            if (first == null)
                                first = p;
                            else
                                if (second == null)
                                    second = p;
                                else
                                    break;
                        }
                    }
                    if (first != null & second != null)
                    {

                        ((Graph)this.selEle).addArc(first, second);
                        //GrArc a = new GrArc(first, second);
                        //((Graph)this.selEle).arcs.Add(a);
                    }
                }                
                sRec = new SelGraph(selEle);//create handling rect
            }
        }


        public void delNodes()
        {
            if (this.sRec is SelGraph)
            {
                ArrayList tmp = ((SelGraph)this.sRec).getSelPoints();
                if (tmp.Count < ((Graph)this.selEle).points.Count - 1)
                {
                    foreach (PointWr p in tmp)
                    {
                        if (p.selected)
                        {
                            ((Graph)this.selEle).delArcs(p);
                            ((Graph)this.selEle).points.Remove(p);
                        }
                    }
                }
                sRec = new SelGraph(selEle);//create handling rect
            }
        }


        public void delPoint()
        {
            if (this.sRec is SelPoly)
            {
                ArrayList tmp = ((SelPoly)this.sRec).getSelPoints();
                if (tmp.Count < ((PointSet)this.selEle).points.Count -1 )
                {
                    foreach (PointWr p in tmp)
                    {
                        ((PointSet)this.selEle).points.Remove(p);
                    }
                }
                sRec = new SelPoly(selEle);//create handling rect
            }
        }

        //Creates new polys from selected points
        public void extPoints()
        {
            if (this.sRec is SelPoly)
            {
                ArrayList tmp = ((SelPoly)this.sRec).getSelPoints();
                if (tmp.Count > 1)
                {
                    ArrayList newL = new ArrayList();
                    foreach (PointWr p in tmp)
                    {
                        //((PointSet)this.selEle).points.Remove(p);
                        newL.Add(new PointWr(p.point));
                    }
                    this.addPoly(sRec.getX(), sRec.getY(), sRec.getX1(), sRec.getY1(), sRec.penColor, sRec.fillColor, sRec.penWidth, sRec.filled, newL,false);
                }
                //sRec = new SelPoly(selEle);//create handling rect
            }
        }


        public void move(int dx, int dy)
        {
            foreach (Ele e in this.List)
            {
                if (e.Selected)
                {
                    e.move(dx, dy);
                }
            }
        }

        public void Fit2grid(int gridsize)
        {
            foreach (Ele e in this.List)
            {
                if (e.Selected)
                {
                    e.Fit2grid(gridsize);
                    
                }
            }
        }


        public void endMove()
        {
            foreach (Ele e in this.List)
            {
                if (e.Selected)
                {
                    e.endMoveRedim();
                    if (!e.AmIaGroup())
                        storeDo("U",e); 
                }
            }
        }

        public void Propertychanged()
        {
            foreach (Ele e in this.List)
            {
                if (e.Selected)
                {
                    storeDo("U",e);
                }
            }

        }

        #region undo/redo 

        public bool UndoEnabled()
        {
            return this.undoB.unDoable();
        }

        public bool RedoEnabled()
        {
            return this.undoB.unRedoable();
        }

        public void storeDo(string option, Ele e)
        {
            Ele olde = null;
            if (e.undoEle != null)
                olde = e.undoEle.Copy();
            Ele newe = e.Copy();
            buffEle buff = new buffEle(e, newe, olde,option);
            this.undoB.add2Buff(buff);
            e.undoEle = e.Copy();
        }

        public void Undo() 
        {
            buffEle buff = (buffEle)this.undoB.Undo();
            if (buff != null)
            {
                switch (buff.op)
                {
                    case "U":
                        buff.objRef.CopyFrom(buff.oldE);
                        break;
                    case "I":
                        //buff.objRef.CopyFrom(buff.oldE);
                        this.List.Remove(buff.objRef);
                        break;
                    case "D":
                        //buff.objRef.CopyFrom(buff.oldE);
                        this.List.Add(buff.objRef);
                        break;
                    default:
                        break;
                }
            }
        }

        public void Redo()
        {
            buffEle buff = (buffEle)this.undoB.Redo();
            if (buff != null)
            {
                switch (buff.op)
                {
                    case "U":
                        buff.objRef.CopyFrom(buff.newE);
                        break;
                    case "I":
                        //buff.objRef.CopyFrom(buff.oldE);
                        this.List.Add(buff.objRef);
                        break;
                    case "D":
                        //buff.objRef.CopyFrom(buff.oldE);
                        this.List.Remove(buff.objRef);
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        private int countSelected()
        {
            int i = 0;
            foreach (Ele e in this.List)
            {
                if (e.Selected)
                {
                    i++;
                }
            }
            return i;
        }

        /// <summary>
        /// Returns an array with the selected item. Used for property grid.
        /// </summary>
        public Ele[] getSelectedArray()
        {
            Ele[] myArray = new Ele[this.countSelected()];   
            int i = 0;
            foreach (Ele e in this.List)
            {
                if (e.Selected)
                {
                    myArray[i]=e;
                    i++;
                }
                    
            }
            return myArray;
        }

        /// <summary>
        /// Returns a List with the selected items. Used for SaveObj.
        /// </summary>
        public ArrayList getSelectedList()
        {
            ArrayList tmpL = new ArrayList();
            foreach (Ele e in this.List)
            {
                if (e.Selected)
                {
                    tmpL.Add(e);
                }
            }
            return tmpL;
        }

        /// <summary>
        /// Returns a List with the selected items. Used for SaveObj.
        /// </summary>
        public void setList(ArrayList a)
        {
            foreach (Ele e in a)
            {
                 this.List.Add(e);

            }
        }

        /// <summary>
        /// 2 front
        /// </summary>
        public void primoPiano()
        {
            if (this.selEle != null)
            {
                this.List.Remove(selEle);
                this.List.Add(selEle);
            }
        }

        /// <summary>
        /// 2 back
        /// </summary>
        public void secondoPiano()
        {
            if (this.selEle != null)
            {
                this.List.Remove(selEle);
                this.List.Insert(0,selEle);
                this.deSelect();
            }
        }

        public  void XMirror()
        {
            if (this.selEle is PointSet)
            {
                ((PointSet)selEle).CommitMirror(true, false);
                sRec = new SelPoly(selEle);//create handling rect
            }
        }
        public  void YMirror()
        {
            if (this.selEle is PointSet)
            {
                ((PointSet)selEle).CommitMirror(false, true);
                sRec = new SelPoly(selEle);//create handling rect
            }
        }
        public  void Mirror()
        {
            if (this.selEle is PointSet)
            {
                ((PointSet)selEle).CommitMirror(true, true);
                //((PointSet)selEle).setupSize();
                sRec = new SelPoly(selEle);//create handling rect
            }
        }



        /// <summary>
        /// Deselect 
        /// </summary>
        public void deSelect()
        {
            foreach (Ele obj in this.List)
            {
                obj.Selected = false;
            }
            selEle = null;
            sRec = null;
        }

        #region DRAW

        /// <summary>
        /// Draw all shapes
        /// </summary>
        public void Draw(Graphics g,int dx, int dy, float zoom)
        {
            bool almostOneSelected = false;
            foreach (Ele obj in this.List)
            {
                obj.Draw(g,dx,dy, zoom);
                if (obj.Selected)
                    almostOneSelected = true;                    
            }
            if (almostOneSelected)
                if (sRec !=null)
                    sRec.Draw(g,dx,dy,zoom);

            

        }


        /// <summary>
        /// Draw all Unselected shapes
        /// </summary>
        public void DrawUnselected(Graphics g, int dx , int dy,float zoom)
        {
            g.PageScale = 10;
            //bool almostOneSelected = false;
            foreach (Ele obj in this.List)
            {
                
                if (!obj.Selected)
                {
                    obj.Draw(g, dx, dy, zoom);
                }
            }
        }


        /// <summary>
        /// Draw all Unselected shapes
        /// </summary>
        public void DrawUnselected(Graphics g)
        {
            g.PageScale = 10;
            //bool almostOneSelected = false;
            foreach (Ele obj in this.List)
            {

                if (!obj.Selected)
                    obj.Draw(g,0,0,1);
            }
        }

        /// <summary>
        /// Draw all Selected shapes
        /// </summary>
        public void DrawSelected(Graphics g,int dx, int dy, float zoom)
        {
            bool almostOneSelected = false;
            
            foreach (Ele obj in this.List)
            {
                if (obj.Selected)
                {
                    obj.Draw(g,dx,dy,zoom);
                    almostOneSelected = true;
                }
            }
            if (almostOneSelected)
                if (sRec != null)
                    sRec.Draw(g, dx, dy, zoom);
        }

        /// <summary>
        /// Draw all Selected shapes
        /// </summary>
        public void DrawSelected(Graphics g)
        {
            bool almostOneSelected = false;
            foreach (Ele obj in this.List)
            {
                if (obj.Selected)
                {
                    obj.Draw(g,0,0,1);
                    almostOneSelected = true;
                }
            }
            if (almostOneSelected)
                if (sRec != null)
                    sRec.Draw(g,0,0,1);
        }

        #endregion

        #region ADD ELEMNTS METODS

        /// <summary>
        /// Adds Polygon
        /// </summary>
        public void addPoly(int x, int y, int x1, int y1, Color penC, Color fillC, float penW, bool filled, ArrayList aa, bool curv)
        {
            /*if (x1 - minDim <= x)
                x1 = x + minDim;
            if (y1 - minDim <= y)
                y1 = y + minDim;*/

            this.deSelect();
            PointSet r = new PointSet(x, y, x1, y1, aa);
            r.penColor = penC;
            r.penWidth = penW;
            r.fillColor = fillC;
            r.filled = filled;
            r.Curved = curv;

            this.List.Add(r);
            // store the operation in undo/redo buffer
            storeDo("I", r);

            sRec = new SelPoly(r);
            selEle = r;
            selEle.Select();
        }

        ///// <summary>
        ///// HT Code: Adds Polygon với tọa độ kinh vĩ độ
        ///// </summary>
        //public void addPoly(int x, int y, int x1, int y1, Color penC, Color fillC, float penW, bool filled, ArrayList aa, double lon, double lat, double lon1, double lat1, ArrayList mPoints, bool curv)
        //{
        //    /*if (x1 - minDim <= x)
        //        x1 = x + minDim;
        //    if (y1 - minDim <= y)
        //        y1 = y + minDim;*/

        //    this.deSelect();
        //    PointSet r = new PointSet(x, y, x1, y1, aa, lon, lat, lon1, lat1, mPoints);
        //    r.penColor = penC;
        //    r.penWidth = penW;
        //    r.fillColor = fillC;
        //    r.filled = filled;
        //    r.Curved = curv;

        //    this.List.Add(r);
        //    // store the operation in undo/redo buffer
        //    storeDo("I", r);

        //    sRec = new SelPoly(r);
        //    selEle = r;
        //    selEle.Select();
        //}

        /// <summary>
        /// Adds Graph
        /// </summary>
        public void addGraph(int x, int y, int x1, int y1, Color penC, Color fillC, float penW, bool filled, ArrayList aa)
        {

            this.deSelect();
            Graph r = new Graph(x, y, x1, y1, aa);
            r.penColor = penC;
            r.penWidth = penW;
            r.fillColor = fillC;
            r.filled = filled;

            this.List.Add(r);
            // store the operation in undo/redo buffer
            storeDo("I", r);

            sRec = new SelGraph(r); //!!!!!
            selEle = r;
            selEle.Select();
        }


        /// <summary>
        /// Adds Polygon
        /// </summary>
        public void addColorPoinySet(int x, int y, int x1, int y1, Color penC, Color fillC, float penW, bool filled, ArrayList aa, bool curv)
        {
            this.deSelect();
            PointColorSet r = new PointColorSet(x, y, x1, y1, aa);
            r.penColor = penC;
            r.penWidth = penW;
            r.fillColor = fillC;
            r.filled = filled;
            r.Curved = curv;

            this.List.Add(r);
            // store the operation in undo/redo buffer
            storeDo("I", r);

            sRec = new SelPoly(r);
            selEle = r;
            selEle.Select();
        }



        /// <summary>
        /// Adds Rect
        /// </summary>
        public void addRect(int x,int y , int x1, int y1, Color penC,Color fillC,float penW,bool filled)
        {
            if (x1 - minDim <= x)
                x1 = x + minDim;
            if (y1 - minDim <= y)
                y1 = y + minDim;             

            this.deSelect();
            Rect r = new Rect(x, y, x1, y1);
            r.penColor = penC;
            r.penWidth = penW;
            r.fillColor = fillC;
            r.filled = filled;
            
            this.List.Add(r);
            // store the operation in undo/redo buffer
            storeDo("I", r);

            sRec = new SelRect(r);
            selEle = r;
            selEle.Select();
        }

        /// <summary>
        /// Adds Link 
        /// </summary>
        public void addLink(int x, int y, int x1, int y1, Color penC, Color fillC, float penW, bool filled, ArrayList aa, bool curv)
        {

        }



        /// <summary>
        /// Adds Arc
        /// </summary>
        public void addArc(int x, int y, int x1, int y1, Color penC, Color fillC, float penW, bool filled)
        {
            if (x1 - minDim <= x)
                x1 = x + minDim;
            if (y1 - minDim <= y)
                y1 = y + minDim;

            this.deSelect();
            Arc r = new Arc(x, y, x1, y1);
            r.penColor = penC;
            r.penWidth = penW;
            r.fillColor = fillC;
            r.filled = filled;

            this.List.Add(r);
            // store the operation in undo/redo buffer
            storeDo("I", r);

            sRec = new SelRect(r);
            selEle = r;
            selEle.Select();
        }
        


        /// <summary>
        /// Adds Line
        /// </summary>
        public void addLine(int x, int y, int x1, int y1, Color penC, float penW)
        {

            this.deSelect();
            Linea r = new Linea(x, y, x1, y1);
            //VLine r = new VLine(x, y, x1, y1);
            //OLine r = new OLine(x, y, x1, y1);

            r.penColor = penC;
            r.penWidth = penW;

            this.List.Add(r);
            // store the operation in undo/redo buffer
            storeDo("I", r);

            sRec = new SelRect(r);
            selEle = r;
            selEle.Select();
        }

        /// <summary>
        /// Adds VLine
        /// </summary>
        public void addVLine(int x, int y, int x1, int y1, Color penC, float penW)
        {

            this.deSelect();
            //Linea r = new Linea(x, y, x1, y1);
            VLine r = new VLine(x, y, x1, y1);
            //OLine r = new OLine(x, y, x1, y1);

            r.penColor = penC;
            r.penWidth = penW;

            this.List.Add(r);
            // store the operation in undo/redo buffer
            storeDo("I", r);

            sRec = new SelRect(r);

            selEle = r;
            selEle.Select();
        }

        /// <summary>
        /// Adds OLine
        /// </summary>
        public void addOLine(int x, int y, int x1, int y1, Color penC, float penW)
        {

            this.deSelect();
            //Linea r = new Linea(x, y, x1, y1);
            //VLine r = new VLine(x, y, x1, y1);
            OLine r = new OLine(x, y, x1, y1);

            r.penColor = penC;
            r.penWidth = penW;

            this.List.Add(r);
            // store the operation in undo/redo buffer
            storeDo("I", r);

            sRec = new SelRect(r);

            selEle = r;
            selEle.Select();
        }


        /// <summary>
        /// Adds TextBox
        /// </summary>
        public void addTextBox(int x, int y, int x1, int y1, RichTextBox t, Color penC, Color fillC, float penW, bool filled)
        {
            if (x1 - minDim <= x)
                x1 = x + minDim;
            if (y1 - minDim <= y)
                y1 = y + minDim;             

            this.deSelect();
            BoxTesto r = new BoxTesto(x, y, x1, y1);
            //Stext r = new Stext(x, y, x1, y1);

            r.penColor = penC;
            r.penWidth = penW;
            r.fillColor = fillC;
            r.filled = filled;

            r.rtf = t.Rtf;

            this.List.Add(r);
            // store the operation in undo/redo buffer
            storeDo("I", r);

            sRec = new SelRect(r);
            selEle = r;
            selEle.Select();
        }

        /// <summary>
        /// Adds SimpleTextBox
        /// </summary>
        public void addSimpleTextBox(int x, int y, int x1, int y1, RichTextBox t, Color penC, Color fillC, float penW, bool filled)
        {
            if (x1 - minDim <= x)
                x1 = x + minDim;
            if (y1 - minDim <= y)
                y1 = y + minDim;

            this.deSelect();
            Stext r = new Stext(x, y, x1, y1);
            
            r.Text = t.Text;
            //r.CharFont = (Font)t.Font.Clone();
            r.CharFont = t.SelectionFont;  //t.Font;
            

            r.penColor = penC;
            r.penWidth = penW;
            r.fillColor = fillC;
            r.filled = filled;

            this.List.Add(r);
            // store the operation in undo/redo buffer
            storeDo("I", r);

            sRec = new SelRect(r);
            selEle = r;
            selEle.Select();
        }


        /// <summary>
        /// Adds RoundRect
        /// </summary>
        public void addRRect(int x, int y, int x1, int y1, Color penC, Color fillC, float penW, bool filled)
        {
            if (x1 - minDim <= x)
                x1 = x + minDim;
            if (y1 - minDim <= y)
                y1 = y + minDim;             

            this.deSelect();
            RRect r = new RRect(x, y, x1, y1);

            r.penColor = penC;
            r.penWidth = penW;
            r.fillColor = fillC;
            r.filled = filled;

            this.List.Add(r);
            // store the operation in undo/redo buffer
            storeDo("I", r);

            sRec = new SelRect(r);
            selEle = r;
            selEle.Select();
        }

        /// <summary>
        /// Adds ImageBox
        /// </summary>
        public void addImgBox(int x, int y, int x1, int y1, string st, Color penC, float penW)
        {
            if (x1 - minDim <= x)
                x1 = x + minDim;
            if (y1 - minDim <= y)
                y1 = y + minDim;             

            this.deSelect();
            ImgBox r = new ImgBox(x, y, x1, y1);
            r.penColor = penC;
            r.penWidth = penW;
            r.showBorder = false;

            this.List.Add(r);
            // store the operation in undo/redo buffer
            storeDo("I", r);

            if (!(st == null))
            {
                try
                {
                    Bitmap loadTexture = new Bitmap(st);
                    r.img = loadTexture;
                }
                catch { }
            }


            sRec = new SelRect(r);
            selEle = r;
            selEle.Select();
        }

        /// <summary>
        /// Adds Ellipse
        /// </summary>
        public void addEllipse(int x, int y, int x1, int y1, Color penC, Color fillC, float penW, bool filled)
        {
            if (x1 - minDim <= x)
                x1 = x + minDim;
            if (y1 - minDim <= y)
                y1 = y + minDim;             

            this.deSelect();
            Ell r = new Ell(x, y, x1, y1);
            r.penColor = penC;
            r.penWidth = penW;
            r.fillColor = fillC;
            r.filled = filled;

            this.List.Add(r);
            // store the operation in undo/redo buffer
            storeDo("I", r);

            sRec = new SelRect(r);
            selEle = r;
            selEle.Select();
        }

        #endregion

        /// <summary>
        /// Selects last shape containing x,y
        /// </summary>
        public void click(int x, int y, RichTextBox r)
        {
            sRec = null;
            selEle = null;
            foreach (Ele obj in this.List)
            {
                obj.Selected = false;
                obj.DeSelect();
                if (obj.contains(x, y))
                {
                    selEle = obj; //save found obj reference
                    // break;
                    // Do not break.. so, for overlapping objs,  we take the last obj added 
                }
            }
            if (selEle != null)
            {
                selEle.Selected = true;
                selEle.Select();
                selEle.Select(r);
                // now I create the handle obj.
                if (selEle is PointSet)
                    sRec = new SelPoly(selEle);//create handling rect
                else
                    sRec = new SelRect(selEle);//create handling rect
            }
        }

        public void mergePolygons()
        {
            bool first = true;
            int minX=0;
            int minY=0;
            ArrayList tmpPointList = new ArrayList();
            ArrayList tmpDelPolys = new ArrayList();
            PointSet tmpPS=null;
            foreach (Ele obj in this.List)
            {
                if (obj.Selected & obj is PointSet)
                {
                    if (first)
                    {
                        first = false;
                        minX = obj.getX();
                        minY = obj.getY();
                        tmpPS=(PointSet)obj;
                    }
                    else
                    {
                        if (minX > obj.getX())
                            minX = obj.getX();
                        if (minY > obj.getY())
                            minY = obj.getY();
                    }
                    tmpDelPolys.Add(obj);
                    tmpPointList.AddRange(((PointSet)obj).getRealPosPoints());
                }
            }
            if (tmpDelPolys.Count>1)
            {
                foreach (Ele obj in tmpDelPolys)
                {
                    this.List.Remove(obj);
                }
                this.addPoly(0, 0, tmpPS.getX1(), tmpPS.getY1(), tmpPS.penColor, tmpPS.fillColor, tmpPS.penWidth, tmpPS.filled, tmpPointList,false);
            }
            

        }


        /// <summary>
        /// Selects all shapes in imput rectangle 
        /// </summary>
        public void multiSelect(int startX, int startY, int endX, int endY, RichTextBox r)
        {
            sRec = null;
            selEle = null;
            foreach (Ele obj in this.List)
            {

                obj.Selected = false;
                obj.DeSelect();// to deselct points in polys
                int x = obj.getX();
                int x1 = obj.getX1();
                int y = obj.getY();
                int y1 = obj.getY1();
                int c = 0;
                if (x > x1)
                {
                    c = x;
                    x = x1;
                    x1 = c;
                }
                if (y > y1)
                {
                    c = y;
                    y = y1;
                    y1 = c;
                }
                //if (obj.getX() <= endX & obj.getX1() >= startX & obj.getY() <= endY & obj.getY1() >= startY)
                if (x <= endX & x1 >= startX & y <= endY & y1 >= startY)
                {
                    selEle = obj; //salvo il riferimento dell'ogg trovato
                    obj.Selected = true;//indico l'oggetto trovato come selezionato
                    obj.Select();
                    obj.Select(r);
                    obj.Select(startX, startY, endX, endY);
                }
            }
            if (selEle != null)
            {
                if (selEle is PointSet)
                    sRec = new SelPoly(selEle);//create handling rect
                else
                    sRec = new SelRect(selEle);//creo un gestore con maniglie
                //sRec.isaLine = selEle.isaLine;//indico se il gestore è per una linea
                //sRec.showHandles(selEle.AmIaGroup());
            }

        }

    }

    #endregion

    #region UNDO BUFFER 

    /// <summary>
    /// Undo buffer Ele element.
    /// </summary>
    public class buffEle
    {

        public Ele objRef;
        public string op; // U:Update, I:Insert, D:Delete
        public Ele oldE; //Start point
        public Ele newE; //Start point
      
        public buffEle(Ele refe,Ele newe, Ele olde, string o)
        {            
            objRef = refe;
            oldE = olde;
            newE = newe;
            op =o;
        }

    }

    /// <summary>
    /// Two Linked List Element
    /// </summary>
    public class buffObj
    {
        public buffObj Next ;
        public buffObj Prec ;
        public object elem;
        
        public buffObj(object o)
        {
            elem=o;
        }
        
    }

    /// <summary>
    /// Undo buffer. (Two Linked List)
    /// </summary>
    //[Serializable]
    public class UndoBuffer
    {
        private buffObj Top;
        private buffObj Bottom;
        private buffObj Current;
        private int _BuffSize;
        private int _N_elem;
        private bool At_Bottom;

        public UndoBuffer(int i)
        {

            this.BuffSize = i;
            this._N_elem = 0;
            Top = null;
            Bottom = null;
            Current = null;
            At_Bottom = true;
        }

        public int BuffSize
        {        
            get
            {
                return _BuffSize;
            }
            set
            {
                _BuffSize = value;
            }                    
        }

        public int N_elem
        {
            get
            {
                return _N_elem;
            }
        }

        public void add2Buff(object o)
        {
            if (o != null)
            {
                buffObj g = new buffObj(o);
                if (this.N_elem == 0)
                {
                    g.Next = null;
                    g.Prec = null;
                    Top = g;
                    Bottom = g;
                    Current = g;
                }
                else 
                {
                    g.Prec = Current;
                    g.Next = null;
                    Current.Next = g;                    
                    Top = g;
                    Current = g;
                    if (this.N_elem == 1)
                    {
                        Bottom.Next = g;
                    }
                }

                //this._N_elem = count();
                this._N_elem++;
                if (this.BuffSize < this.N_elem)
                {
                    this.Bottom = this.Bottom.Next;
                    this.Bottom.Prec = null;
                    this._N_elem--;
                }
                At_Bottom = false;
            }


        }
     
        public object Undo()
        {

            if (Current != null)
            {

                object obj = Current.elem;
                if (Current.Prec != null)
                {
                    Current = Current.Prec;
                    this._N_elem--;
                    this.At_Bottom = false;
                }
                else
                {
                    this.At_Bottom = true;
                }
                return obj;
            }
            //this._N_elem = count();
            return null;
        }

        public object Redo()
        {
            if (Current != null)
            {
                object obj;
                if (!At_Bottom)
                {
                    if (Current.Next != null)
                    {
                        Current = Current.Next;
                        this._N_elem++;
                    }
                }
                else 
                {
                    At_Bottom = false;
                }
                obj = Current.elem;

                return obj;
            }
            //this._N_elem = count();
            return null;
        }

        public bool unDoable()
        {
            return ! this.At_Bottom;
        }

        public bool unRedoable()
        {
            if (this.Current == null)
                return false;
            if (this.Current.Next == null)
                return false;
            return true;
        }
    }

    #endregion

}

