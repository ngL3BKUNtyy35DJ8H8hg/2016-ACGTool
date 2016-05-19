using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BDTCLib.Scripts
{
    public enum enumTimer
    {
        None,
        Start,
        Stop
    }
    /// <summary>
    /// Dùng để tính toán thời gian bắt đầu, thời gian kết thúc của một kịch bản
    /// </summary>
    public class TimerCalc
    {
        public string StartTemplate;
        public float Start = -1; //Thời gian bắt đầu thực sự
        
        public float Stop //Thời gian kết thúc thực sự Stop = Start + Duration
        {
            get { return Start + Duration; }
        }

        //Tổng thời gian chạy Duration = Stop - Start
        public float Duration;
        //Cho biết bộ timer này có tham chiếu từ ID khác
        public string IDRefValue;   //Ví dụ: Start="Start(1)+3" thì IDRef = "1"
        public enumTimer eTimer;    //Cho biết thời điểm bắt đầu từ IDRef
        public float AddTime;       //Ví dụ: Start="Start(1)-3" thì AddTime = "-3"

        /// <summary>
        /// Xác định IDRef và giá trị thời gian AddTime trong attribute Start
        /// Ví dụ: 
        /// Start="Start(1)+5". IDRef = 1 và AddTime = 5
        /// Start="Stop(4)-2" . IDRef = 4 và AddTime = -2
        /// Start="0" . IDRef = "" và AddTime = 0
        /// Start="1". IDRef = "" và AddTime = 1
        /// </summary>
        /// <returns></returns>
        public string CalculateTimer(string strStart)
        {
            string msg = "";
            AddTime = 0;
            IDRefValue = "";
            StartTemplate = strStart;
            if (StartTemplate.Contains("Start"))
            {
                Regex r = new Regex(@"Start\((?<ID>.+)\)");
                Match m = r.Match(StartTemplate);
                if (m.Success)
                {
                    eTimer = enumTimer.Start;
                    IDRefValue = m.Groups["ID"].Value;
                    try
                    {
                        AddTime = float.Parse(StartTemplate.Replace(m.Value, ""));
                    }
                    catch (Exception)
                    {
                        AddTime = 0;
                    }
                }
                else
                {
                    msg = string.Format("{0} không format đúng", StartTemplate);
                }
            }
            else if (StartTemplate.Contains("Stop"))
            {
                Regex r = new Regex(@"Stop\((?<ID>.+)\)");
                Match m = r.Match(StartTemplate);
                if (m.Success)
                {
                    eTimer = enumTimer.Stop;
                    IDRefValue = m.Groups["ID"].Value;
                    try
                    {
                        AddTime = float.Parse(StartTemplate.Replace(m.Value, ""));
                    }
                    catch (Exception)
                    {
                        AddTime = 0;
                    }
                    
                }
                else
                {
                    msg = string.Format("{0} không format đúng", StartTemplate);
                }
            }
            else
            {
                try
                {
                    eTimer = enumTimer.None;
                    IDRefValue = "";
                    AddTime = float.Parse(StartTemplate);
                }
                catch (Exception ex)
                {
                    msg = string.Format("{0} không format đúng", StartTemplate);
                }
            }

            return msg;
        }
    }
}
