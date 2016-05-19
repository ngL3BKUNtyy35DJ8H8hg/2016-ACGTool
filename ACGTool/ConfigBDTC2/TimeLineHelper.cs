using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BDTCLib;
using BDTCLib.Scripts;
using BDTCLib.Scripts.Actions;
using Braincase.GanttChart;

namespace ConfigBDTC
{
    public static class TimeLineHelper
    {
        public static DiaHinh _objDiaHinh;
        public static MyMnu _objMyMnu;

        public static MyTask CreateTask(ProjectManager _mManager, string key, string name, enumGanttChartTimer beginStatus, string startTemplate, float start, float duration, string Tooltip)
        {
            var task = new MyTask(_mManager) { Name = string.Format("{0}", name) };
            task.BeginStatus = beginStatus;
            _mManager.Add(task);
            
            _mManager.SetStart(task, start);
            _mManager.SetDuration(task, duration);

            string resource = "";
            if (startTemplate != "")
            {
                resource = string.Format("ID={0}\nStart={1}(={2}); Duration={3}; Stop={4}\n{5}",
                                         key, start, startTemplate, duration, start + duration, Tooltip);
            }
            else
            {
                resource = string.Format("ID={0}\nStart={1}; Duration={2}; Stop={3}\n{4}",
                                         key, start, duration, start + duration, Tooltip);
            }

            var res = new MyResource()
                          {
                              Name = resource
                
            };
            // Add some resources
            _mManager.Assign(task, res);
            return task;
        }

        public static void InitChart(ProjectManager _mManager, Chart objChart)
        {
            // Initialize the Chart with our ProjectManager and CreateTaskDelegate
            objChart.Init(_mManager);
            objChart.CreateTaskDelegate = delegate() { return new MyTask(_mManager); };

            // Attach event listeners for events we are interested in
            //objChart.PaintOverlay += _mOverlay.ChartOverlayPainter;
            objChart.AllowTaskDragDrop = true;

            // Set Time information
            _mManager.TimeScale = TimeScale.Day;
            //var span = DateTime.Today - _mManager.Start;
            _mManager.Now = 0;// (int)Math.Round(span.TotalDays); // set the "Now" marker at the correct date
            //objChart.TimeScaleDisplay = TimeScaleDisplay.DayOfYear; // Set the chart to display days of week in header
            objChart.ShowRelations = true;
            objChart.Invalidate();
        }

        /// <summary>
        /// Hiển thị timeline của MnuItem
        /// </summary>
        /// <param name="mManager"></param>
        /// <param name="objChart"></param>
        /// <param name="mnuItemNode"></param>
        public static void LoadMnuItemContent_TimeLine(ProjectManager mManager, Chart objChart, Dictionary<string, MyTask> taskDict, TreeNode mnuItemNode, float start)
        {
            // Create a Project and some Tasks
            MnuItem objMnuItem = (MnuItem) mnuItemNode.Tag;
            MyTask mnuItemTask = null;
            string taskName = "";
            //Nếu mnuitem được load từ MnuItemRef thì tạo start và duration
            if (mnuItemNode.Parent != null)
            {
                MnuItemRefScript objMnuItemRefScript = (MnuItemRefScript)mnuItemNode.Parent.Tag;
                //Start của MnuItem chính là của MnuItemRefScript
                taskName = string.Format("<MnuItemRef ID={0}>-[{1}]", objMnuItem.ID, objMnuItem.Name);

                //Thời gian start của MnuItem chính là thời gian start của <Script> chứa MnuItemRef
                //Ví dụ: <Script ID="2" MnuItemRef="Mnu_1.3.2" Start="Start(1)+4" /> có thời gian Start="Start(1)+4"
                //thì <MnuItem ID="Mnu_1.3.2" Name="3_Ta_Mui3_LuiRa.xml" PosX="1000" PosY="220" Width="170" Height="20" Title=""> cũng sẽ có thời gian Start="Start(1)+4"
                mnuItemTask = CreateTask(mManager, mnuItemNode.Name, taskName, enumGanttChartTimer.None, objMnuItemRefScript.ObjTimerCalc.StartTemplate, start, objMnuItem.Duration, objMnuItem.ToString());
                mManager.Group(taskDict[mnuItemNode.Parent.Name], mnuItemTask);
            }
            else
            {
                taskName = string.Format("<MnuItem ID={0}>-[{1}]", objMnuItem.ID, objMnuItem.Name);
                mnuItemTask = CreateTask(mManager, mnuItemNode.Name, taskName, enumGanttChartTimer.None, "", 0, objMnuItem.Duration, objMnuItem.ToString());
            }

            //Add vào dictionary task
            taskDict.Add(mnuItemNode.Name, mnuItemTask);

            
            enumGanttChartTimer beginStatus;
            foreach (TreeNode scriptNode in mnuItemNode.Nodes)
            {
                MyTask scriptTask = null;
                
                if (scriptNode.Tag is MnuItemRefScript)
                {
                    MnuItemRefScript objMnuItemRefScript = (MnuItemRefScript) scriptNode.Tag;
                    MnuItem objMnuItemRef = objMnuItemRefScript.ObjMnuItem_Ref;
                    
                    //Khởi tạo task
                    taskName = string.Format("<Script ID={0}>-[{1}]-[{2}]", objMnuItemRefScript.ID, objMnuItemRef.ID, objMnuItemRef.Name); //pairAction.Key + "-" + obj.ID + "-" + obj.Name;
                    //Nếu tồn tại ID Ref thì tạo relation
                    beginStatus = enumGanttChartTimer.None;
                    if (objMnuItemRefScript.ObjTimerCalc.IDRefValue != "")
                    {
                        if (objMnuItemRefScript.ObjTimerCalc.eTimer == enumTimer.Start)
                            beginStatus = enumGanttChartTimer.Start;
                        else if (objMnuItemRefScript.ObjTimerCalc.eTimer == enumTimer.Stop)
                            beginStatus = enumGanttChartTimer.Stop;
                    }
                    //Thời gian bắt đầu chạy script
                    float scriptTaskStart = start + objMnuItemRefScript.ObjTimerCalc.Start;
                    scriptTask = CreateTask(mManager, scriptNode.Name, taskName, beginStatus, objMnuItemRefScript.ObjTimerCalc.StartTemplate, scriptTaskStart, objMnuItemRefScript.ObjTimerCalc.Duration, objMnuItemRefScript.ToString());
                    
                    //Tạo relationship giữa các task nếu có
                    if (objMnuItemRefScript.ObjTimerCalc.IDRefValue != "")
                    {
                        mManager.Relate(taskDict[mnuItemNode.Name + "-->" + objMnuItemRefScript.ObjTimerCalc.IDRefValue], scriptTask);
                    }
                    taskDict.Add(scriptNode.Name, scriptTask);
                    //mManager.Relate(taskDict[objMnuItem.ID + "." + objMnuItemRefScript.ObjTimerCalc.IDRefValue], task);
                    //taskDict.Add(scriptKey, task);

                    //Do scriptNode là MnuItemRefScript thì child node của scriptNode phải là một MnuItem
                    LoadMnuItemContent_TimeLine(mManager, objChart, taskDict, scriptNode.Nodes[0], scriptTaskStart);
                }
                else if (scriptNode.Tag is ScrptFileScript)
                {
                    ScrptFileScript objScrptFileScript = (ScrptFileScript) scriptNode.Tag;
                    string fileName = Path.GetFileName(_objDiaHinh._objMyLastSaban._mySaBanDirFullPath + "\\" + objScrptFileScript.ScrptFile);
                    
                    //Khởi tạo task
                    taskName = string.Format("<Script ID={0}>-[{1}]", objScrptFileScript.ID, fileName);
                    //Nếu tồn tại ID Ref thì tạo relation
                    beginStatus = enumGanttChartTimer.None;
                    if (objScrptFileScript.ObjTimerCalc.IDRefValue != "")
                    {
                        if (objScrptFileScript.ObjTimerCalc.eTimer == enumTimer.Start)
                            beginStatus = enumGanttChartTimer.Start;
                        else if (objScrptFileScript.ObjTimerCalc.eTimer == enumTimer.Stop)
                            beginStatus = enumGanttChartTimer.Stop;
                    }
                    //Thời gian bắt đầu chạy script
                    float scriptTaskStart = start + objScrptFileScript.ObjTimerCalc.Start;
                    scriptTask = CreateTask(mManager, scriptNode.Name, taskName, beginStatus, objScrptFileScript.ObjTimerCalc.StartTemplate, scriptTaskStart, objScrptFileScript.ObjScriptXmlFile.Duration, objScrptFileScript.ToString());

                    //Tạo relationship giữa các task
                    if (objScrptFileScript.ObjTimerCalc.IDRefValue != "")
                    {
                        mManager.Relate(taskDict[mnuItemNode.Name + "-->" + objScrptFileScript.ObjTimerCalc.IDRefValue], scriptTask);
                    }
                    taskDict.Add(scriptNode.Name, scriptTask);
                    //mManager.Relate(taskDict[objMnuItem.ID + "." + objScrptFileScript.ObjTimerCalc.IDRefValue], task);
                    //taskDict.Add(scriptKey, task);
                    

                    //Tạo Task gồm các action trong file script
                    foreach (TreeNode actionNode in scriptNode.Nodes)
                    {
                        AbstractAction action = (AbstractAction)actionNode.Tag;
                        if (action is CommentAction)
                            taskName = string.Format("<Action ID={0}>-[{1}]", action.ID, action.Type);
                        else if (action is CornerTitleAction)
                            taskName = string.Format("<Action ID={0}>-[{1}]-[{2}]", action.ID, action.Type, action.CornerText);
                        else if (action is DescriptionAction)
                            taskName = string.Format("<Action ID={0}>-[{1}]-[{2}]", action.ID, action.Type, action.DescText);
                        else
                        {
                            taskName = string.Format("<Action ID={0}>-[{1}]-[{2}]", action.ID, action.Type, (action.ObjName != null ? "-" + action.ObjName : ""));
                        }
                        //Nếu tồn tại ID Ref thì tạo relation
                        beginStatus = enumGanttChartTimer.None;
                        if (action.ObjTimerCalc.IDRefValue != "")
                        {
                            if (action.ObjTimerCalc.eTimer == enumTimer.Start)
                                beginStatus = enumGanttChartTimer.Start;
                            else if (action.ObjTimerCalc.eTimer == enumTimer.Stop)
                                beginStatus = enumGanttChartTimer.Stop;
                        }
                        float actionTaskStart = scriptTaskStart + action.ObjTimerCalc.Start;
                        MyTask actionTask = CreateTask(mManager, actionNode.Name, taskName, beginStatus, action.ObjTimerCalc.StartTemplate, actionTaskStart, action.ObjTimerCalc.Duration, action.ToString());
                        //Nếu action có thuộc tính ẩn sau khi hiện
                        if (!mManager.IsGroup(actionTask))
                        {
                            //if (bool.Parse(action.Hide) && action.Type.Contains("Appear"))
                            if (bool.Parse(action.Hide) && (action.Type.Contains("Appear") || action.Type.Contains("Unhide")))
                            {
                                //Xác định % Task bị ẩn sau khi hiện
                                actionTask.Complete = 1f - float.Parse(action.Duration)/action.ObjTimerCalc.Duration;
                            }
                        }
                        if (action.ObjTimerCalc.IDRefValue != "")
                        {
                            mManager.Relate(taskDict[scriptNode.Name + "-->" + action.ObjTimerCalc.IDRefValue], actionTask);
                        }
                        mManager.Group(scriptTask, actionTask);
                        taskDict.Add(actionNode.Name, actionTask);
                    }
                }

                //Tạo group
                mManager.Group(mnuItemTask, scriptTask);
            }
            
            
        }
    }

    #region custom task and resource
    /// <summary>
    /// A custom resource of your own type (optional)
    /// </summary>
    [Serializable]
    public class MyResource
    {
        public string Name { get; set; }
    }
    /// <summary>
    /// A custom task of your own type deriving from the Task interface (optional)
    /// </summary>
    [Serializable]
    public class MyTask : Task
    {
        public MyTask(ProjectManager manager)
            : base()
        {
            Manager = manager;
        }

        private ProjectManager Manager { get; set; }

        public new float Start { get { return base.Start; } set { Manager.SetStart(this, value); } }
        public new float End { get { return base.End; } set { Manager.SetEnd(this, value); } }
        public new float Duration { get { return base.Duration; } set { Manager.SetDuration(this, value); } }
        public new float Complete { get { return base.Complete; } set { Manager.SetComplete(this, value); } }


    }
    #endregion custom task and resource

    #region overlay painter
    /// <summary>
    /// An example of how to encapsulate a helper painter for painter additional features on Chart
    /// </summary>
    public class OverlayPainter
    {
        /// <summary>
        /// Hook such a method to the chart paint event listeners
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChartOverlayPainter(object sender, ChartPaintEventArgs e)
        {
            // Don't want to print instructions to file
            if (this.PrintMode) return;

            var g = e.Graphics;
            var chart = e.Chart;

            // Demo: Static billboards begin -----------------------------------
            // Demonstrate how to draw static billboards
            // "push matrix" -- save our transformation matrix
            e.Chart.BeginBillboardMode(e.Graphics);

            // draw mouse command instructions
            int margin = 300;
            int left = 20;
            var color = chart.HeaderFormat.Color;
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("THIS IS DRAWN BY A CUSTOM OVERLAY PAINTER TO SHOW DEFAULT MOUSE COMMANDS.");
            builder.AppendLine("*******************************************************************************************************");
            builder.AppendLine("Left Click - Select task and display properties in PropertyGrid");
            builder.AppendLine("Left Mouse Drag - Change task starting point");
            builder.AppendLine("Right Mouse Drag - Change task duration");
            builder.AppendLine("Middle Mouse Drag - Change task complete percentage");
            builder.AppendLine("Left Doubleclick - Toggle collaspe on task group");
            builder.AppendLine("Right Doubleclick - Split task into task parts");
            builder.AppendLine("Left Mouse Dragdrop onto another task - Group drag task under drop task");
            builder.AppendLine("Right Mouse Dragdrop onto another task part - Join task parts");
            builder.AppendLine("SHIFT + Left Mouse Dragdrop onto another task - Make drop task precedent of drag task");
            builder.AppendLine("ALT + Left Dragdrop onto another task - Ungroup drag task from drop task / Remove drop task from drag task precedent list");
            builder.AppendLine("SHIFT + Left Mouse Dragdrop - Order tasks");
            builder.AppendLine("SHIFT + Middle Click - Create new task");
            builder.AppendLine("ALT + Middle Click - Delete task");
            builder.AppendLine("Left Doubleclick - Toggle collaspe on task group");
            var size = g.MeasureString(builder.ToString(), e.Chart.Font);
            var background = new Rectangle(left, chart.Height - margin, (int)size.Width, (int)size.Height);
            background.Inflate(10, 10);
            g.FillRectangle(new System.Drawing.Drawing2D.LinearGradientBrush(background, Color.LightYellow, Color.Transparent, System.Drawing.Drawing2D.LinearGradientMode.Vertical), background);
            g.DrawRectangle(Pens.Brown, background);
            g.DrawString(builder.ToString(), chart.Font, color, new PointF(left, chart.Height - margin));


            // "pop matrix" -- restore the previous matrix
            e.Chart.EndBillboardMode(e.Graphics);
            // Demo: Static billboards end -----------------------------------
        }

        public bool PrintMode { get; set; }
    }
    #endregion overlay painter
}
