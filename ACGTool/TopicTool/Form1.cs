using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using VBTopicTool;
using Excel = Microsoft.Office.Interop.Excel;

namespace TopicTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public IQueryable GetThueKhoan()
        {

            ConvertNumberToString convertNumber = new ConvertNumberToString();

            CultureInfo enUS = CultureInfo.CreateSpecificCulture("en-US");
            //CultureInfo frFR = CultureInfo.CreateSpecificCulture("fr-FR");
            
            TopicDataClassesDataContext dc = new TopicDataClassesDataContext();
            var query = from p in dc.ThueKhoans
                        select new
                                   {
                                       p.id_thuekhoan,

                                       //Cá nhân thuê khoán
                                       p.ThanhVien.hoten,
                                       p.ThanhVien.gioitinh,
                                       p.ThanhVien.hocvi,
                                       p.ThanhVien.quanham,
                                       p.ThanhVien.cmnd,
                                       p.ThanhVien.ngaycmnd,
                                       p.ThanhVien.noicap,
                                       p.ThanhVien.diachi,
                                       p.ThanhVien.dienthoai,
                                       p.ThanhVien.sotk,
                                       p.ThanhVien.nganhang,
                                       
                                       tongtien = GetTongTien(p.id_thuekhoan),
                                       ////Kinh phí
                                       //tongtien_bangchu = convertNumber.DoiSoRaChu((double)(p.tongtien_chuyende)),
                                       //thuetncn = p.tongtien_chuyende * 0.1,
                                       //thuetncn_bangchu = convertNumber.DoiSoRaChu((double)(p.tongtien_chuyende * 0.1)),
                                       //chuyenkhoan = p.tongtien_chuyende - p.tongtien_chuyende * 0.1,
                                       //chuyenkhoan_bangchu = convertNumber.DoiSoRaChu((double)(p.tongtien_chuyende - p.tongtien_chuyende * 0.1)),

                                       //Chủ nhiệm hợp đồng
                                       hoten_CNHD = p.ThanhVien1.hoten,
                                       hocvi_CNHD = p.ThanhVien1.hocvi,
                                       quanham_CNHD = p.ThanhVien1.quanham,
                                       cmnd_CNHD = p.ThanhVien1.cmnd,
                                       ngaycmnd_CNHD = p.ThanhVien1.ngaycmnd,
                                       noicapcmnd_CNHD = p.ThanhVien1.noicap,
                                       diachi_CNHD = p.ThanhVien1.diachi,
                                       dienthoai_CNHD = p.ThanhVien1.dienthoai,
                                       sotk_CNHD = p.ThanhVien1.sotk,
                                       nganhang_CNHD = p.ThanhVien1.nganhang,

                                       //Tổ chức chủ trì
                                       hoten_TCCT = p.DeTai.ThanhVien.hoten,
                                       chucvu_TCCT = p.DeTai.ThanhVien.chucvu,
                                       kyten_TCCT = "Phó Viện trưởng",
                                       hocvi_TCCT = p.DeTai.ThanhVien.hocvi,
                                       quanham_TCCT = p.DeTai.ThanhVien.quanham,

                                       //Hợp đồng thuê khoán
                                       p.sohd_thuekhoan,
                                       p.ngayky_thuekhoan,
                                       ngayky_thuekhoan_Text = string.Format("ngày {0} tháng {1} năm {2}", p.ngayky_thuekhoan.Value.Day.ToString(), p.ngayky_thuekhoan.Value.Month.ToString(), p.ngayky_thuekhoan.Value.Year.ToString()),
                                       p.ngayketthuc_thuekhoan,
                                       ngayketthuc_thuekhoan_Text = string.Format("ngày {0} tháng {1} năm {2}", p.ngayketthuc_thuekhoan.Value.Day.ToString(), p.ngayketthuc_thuekhoan.Value.Month.ToString(), p.ngayketthuc_thuekhoan.Value.Year.ToString()),
                                       p.ngaynghiemthu_thuekhoan,
                                       ngaynghiemthu_thuekhoan_Text = string.Format("ngày {0} tháng {1} năm {2}", p.ngaynghiemthu_thuekhoan.Value.Day.ToString(), p.ngaynghiemthu_thuekhoan.Value.Month.ToString(), p.ngaynghiemthu_thuekhoan.Value.Year.ToString()),
                                       p.ngaythanhly_thuekhoan,
                                       ngaythanhly_thuekhoan_Text = string.Format("ngày {0} tháng {1} năm {2}", p.ngaythanhly_thuekhoan.Value.Day.ToString(), p.ngaythanhly_thuekhoan.Value.Month.ToString(), p.ngaythanhly_thuekhoan.Value.Year.ToString()),
                                       p.ngaybiennhan_thuekhoan,
                                       ngaybiennhan_thuekhoan_Text = string.Format("ngày {0} tháng {1} năm {2}", p.ngaybiennhan_thuekhoan.Value.Day.ToString(), p.ngaybiennhan_thuekhoan.Value.Month.ToString(), p.ngaybiennhan_thuekhoan.Value.Year.ToString()),

                                       //Hợp đồng đề tài
                                       p.DeTai.sohd_detai,
                                       p.DeTai.ngayky_detai,
                                       p.DeTai.ten_detai
                                   };
            return query;
        }

        public int GetTongTien(int id_thuekhoan)
        {
            TopicDataClassesDataContext dc = new TopicDataClassesDataContext();
            var tongtien = (from p in dc.CTThueKhoans
                         where p.id_thuekhoan == id_thuekhoan
                         select p.ChuyenDe.sotien).Sum();
            return (int)tongtien;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetThueKhoan();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetThueKhoan();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp = new Excel.Application();
            string mFileTemplate = System.Windows.Forms.Application.StartupPath + "\\DeTaiTemplate.xlsx";
            string mFileName = System.Windows.Forms.Application.StartupPath + "\\DeTai.xlsx";
            try
            {
                System.IO.File.Copy(mFileTemplate, mFileName, true);
            }
            catch
            {
                string strErr = "File " + mFileTemplate + " đang mở hoặc không có, không thể thực hiện tiếp.";
                MessageBox.Show(strErr);
                return;
            }
            Excel.Workbook xlBook = xlApp.Workbooks.Open(mFileName);
            Excel.Worksheet xlSheet = xlBook.Worksheets["02_Ký HĐ với Cá nhân"];
            Excel.Range mRange;
            xlApp.Visible = true;
            int rowIndex = 1;
            ConvertNumberToString convertNumber = new ConvertNumberToString();
            CultureInfo cult = CultureInfo.CreateSpecificCulture("pt-BR");
            CultureInfo enUS = CultureInfo.CreateSpecificCulture("en-US");
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                rowIndex++;
                xlSheet.Cells[rowIndex, 2] = dr.Cells["hoten"].Value;
                xlSheet.Cells[rowIndex, 3] = dr.Cells["gioitinh"].Value;
                xlSheet.Cells[rowIndex, 4] = dr.Cells["hocvi"].Value;
                xlSheet.Cells[rowIndex, 5] = dr.Cells["quanham"].Value;
                xlSheet.Cells[rowIndex, 6] = dr.Cells["cmnd"].Value;
                xlSheet.Cells[rowIndex, 7] = dr.Cells["ngaycmnd"].Value;
                xlSheet.Cells[rowIndex, 8] = dr.Cells["noicap"].Value;
                xlSheet.Cells[rowIndex, 9] = dr.Cells["diachi"].Value;
                xlSheet.Cells[rowIndex, 10] = dr.Cells["dienthoai"].Value;
                xlSheet.Cells[rowIndex, 11] = dr.Cells["sotk"].Value;
                xlSheet.Cells[rowIndex, 12] = dr.Cells["nganhang"].Value;

                int tongtien = GetTongTien((int) dr.Cells["id_thuekhoan"].Value);
                xlSheet.Cells[rowIndex, 13] = string.Format(enUS, "{0:0,0}", tongtien);
                var value = double.Parse(tongtien.ToString(), cult);
                //xlSheet.Cells[rowIndex, 17] = value.ToString("0,000", cult);
                xlSheet.Cells[rowIndex, 14] = value.ToString("0,000", cult);
                xlSheet.Cells[rowIndex, 15] = convertNumber.DoiSoRaChu((double)tongtien);

                int thuetncn = (int)(tongtien*0.1);
                xlSheet.Cells[rowIndex, 16] = string.Format(enUS, "{0:0,0}", thuetncn);
                value = double.Parse(thuetncn.ToString(), cult);
                xlSheet.Cells[rowIndex, 17] = value.ToString("0,000", cult);

                int chuyenkhoan = tongtien - thuetncn;
                xlSheet.Cells[rowIndex, 18] = string.Format(enUS, "{0:0,0}", chuyenkhoan);
                value = double.Parse(chuyenkhoan.ToString(), cult);
                xlSheet.Cells[rowIndex, 19] = value.ToString("0,000", cult);
                xlSheet.Cells[rowIndex, 20] = convertNumber.DoiSoRaChu((double)chuyenkhoan);

                xlSheet.Cells[rowIndex, 21] = dr.Cells["hoten_CNHD"].Value;
                xlSheet.Cells[rowIndex, 22] = dr.Cells["hocvi_CNHD"].Value;
                xlSheet.Cells[rowIndex, 23] = dr.Cells["quanham_CNHD"].Value;
                xlSheet.Cells[rowIndex, 24] = dr.Cells["cmnd_CNHD"].Value;
                xlSheet.Cells[rowIndex, 25] = dr.Cells["ngaycmnd_CNHD"].Value;
                xlSheet.Cells[rowIndex, 26] = dr.Cells["noicapcmnd_CNHD"].Value;
                xlSheet.Cells[rowIndex, 27] = dr.Cells["diachi_CNHD"].Value;
                xlSheet.Cells[rowIndex, 28] = dr.Cells["dienthoai_CNHD"].Value;
                xlSheet.Cells[rowIndex, 29] = dr.Cells["sotk_CNHD"].Value;
                xlSheet.Cells[rowIndex, 30] = dr.Cells["nganhang_CNHD"].Value;

                xlSheet.Cells[rowIndex, 31] = dr.Cells["hoten_TCCT"].Value;
                xlSheet.Cells[rowIndex, 32] = dr.Cells["chucvu_TCCT"].Value;
                xlSheet.Cells[rowIndex, 33] = dr.Cells["kyten_TCCT"].Value;
                xlSheet.Cells[rowIndex, 34] = dr.Cells["hocvi_TCCT"].Value;
                xlSheet.Cells[rowIndex, 35] = dr.Cells["quanham_TCCT"].Value;

                //Thuê khoán
                xlSheet.Cells[rowIndex, 36] = dr.Cells["sohd_thuekhoan"].Value;
                xlSheet.Cells[rowIndex, 37] = dr.Cells["ngayky_thuekhoan_Text"].Value;
                xlSheet.Cells[rowIndex, 38] = dr.Cells["ngayketthuc_thuekhoan_Text"].Value;
                xlSheet.Cells[rowIndex, 39] = dr.Cells["ngaynghiemthu_thuekhoan_Text"].Value;
                xlSheet.Cells[rowIndex, 40] = dr.Cells["ngaythanhly_thuekhoan_Text"].Value;
                xlSheet.Cells[rowIndex, 41] = dr.Cells["ngaybiennhan_thuekhoan_Text"].Value;

                //Đề tài
                xlSheet.Cells[rowIndex, 42] = dr.Cells["sohd_detai"].Value;
                xlSheet.Cells[rowIndex, 43] = DateTime.Parse(dr.Cells["ngayky_detai"].Value.ToString()).ToString("dd/MM/yyyy");
                xlSheet.Cells[rowIndex, 44] = dr.Cells["ten_detai"].Value;
            }
        }

        public string GetNoiDungChuyenDe(int id_thuekhoan)
        {
            TopicDataClassesDataContext dc = new TopicDataClassesDataContext();
            var query = from p in dc.CTThueKhoans
                            where p.id_thuekhoan == id_thuekhoan
                            select p.ChuyenDe.noidung_tomtat;
            string noidung = "";
            if (query.Count() > 1)
            {
                int index = 1;
                foreach (var r in query)
                {

                    noidung += string.Format(" {0}) {1};", index, r);
                    index++;
                }
            }
            else
            {
                foreach (var r in query)
                {
                    noidung += r;
                }
            }
            
            return noidung;
        }

        private void btnBangKe_Click(object sender, EventArgs e)
        {
            Excel.Application xlApp = new Excel.Application();
            string mFileTemplate = System.Windows.Forms.Application.StartupPath + "\\BangKeTemplate.xlsx";
            string mFileName = System.Windows.Forms.Application.StartupPath + "\\BangKe.xlsx";
            try
            {
                System.IO.File.Copy(mFileTemplate, mFileName, true);
            }
            catch
            {
                string strErr = "File " + mFileTemplate + " đang mở hoặc không có, không thể thực hiện tiếp.";
                MessageBox.Show(strErr);
                return;
            }
            Excel.Workbook xlBook = xlApp.Workbooks.Open(mFileName);
            Excel.Worksheet xlSheet = xlBook.Worksheets["BangKeChungTu"];
            Excel.Range mRange;
            xlApp.Visible = true;
            int rowIndex = 11;
            string noidung_thuekhoan;
            CultureInfo cult = CultureInfo.CreateSpecificCulture("pt-BR");
            CultureInfo enUS = CultureInfo.CreateSpecificCulture("en-US");
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                rowIndex += 2;
                noidung_thuekhoan = GetNoiDungChuyenDe((int) dr.Cells["id_thuekhoan"].Value) + "\n";
                noidung_thuekhoan += string.Format("  + Hợp đồng số: {0}, ngày {1}", dr.Cells["sohd_thuekhoan"].Value, DateTime.Parse(dr.Cells["ngayky_thuekhoan"].Value.ToString()).ToString("dd/MM/yyyy")) + "\n";
                noidung_thuekhoan += string.Format("  + Biên bản nghiệm thu kỹ thuật: {0}", DateTime.Parse(dr.Cells["ngaynghiemthu_thuekhoan"].Value.ToString()).ToString("dd/MM/yyyy")) + "\n";
                noidung_thuekhoan += string.Format("  + Biên bản bàn giao và thanh lý: {0}", DateTime.Parse(dr.Cells["ngaythanhly_thuekhoan"].Value.ToString()).ToString("dd/MM/yyyy")) + "\n";
                noidung_thuekhoan += string.Format("  + Người thực hiện: {0}", dr.Cells["hoten"].Value.ToString()) + "\n";
                //Thuê khoán chuyên môn
                xlSheet.Cells[rowIndex, 5] = noidung_thuekhoan;
                //Số tiền
                int tongtien = GetTongTien((int)dr.Cells["id_thuekhoan"].Value);
                int thuetncn = (int)(tongtien * 0.1);
                int chuyenkhoan = tongtien - thuetncn;
                xlSheet.Cells[rowIndex, 6] = chuyenkhoan;
                //Thuế TNCN
                xlSheet.Cells[rowIndex + 1, 5] = "Thuế TNCN (10%) khấu trừ";
                xlSheet.Cells[rowIndex + 1, 6] = thuetncn;
                

                
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dr in dataGridView1.Rows)
            {
                DateTime dt = DateTime.Parse(dr.Cells["ngayky_thuekhoan"].Value.ToString());
                if (dt.DayOfWeek == DayOfWeek.Saturday
                    || dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    MessageBox.Show(dt.ToString() + " là ngày cuối tuần");
                    return;
                }

                dt = DateTime.Parse(dr.Cells["ngayketthuc_thuekhoan"].Value.ToString());
                if (dt.DayOfWeek == DayOfWeek.Saturday
                    || dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    MessageBox.Show(dt.ToString() + " là ngày cuối tuần");
                    return;
                }

                dt = DateTime.Parse(dr.Cells["ngaynghiemthu_thuekhoan"].Value.ToString()); 
                if (dt.DayOfWeek == DayOfWeek.Saturday
                    || dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    MessageBox.Show(dt.ToString() + " là ngày cuối tuần");
                    return;
                }

                dt = DateTime.Parse(dr.Cells["ngaythanhly_thuekhoan"].Value.ToString());
                if (dt.DayOfWeek == DayOfWeek.Saturday
                    || dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    MessageBox.Show(dt.ToString() + " là ngày cuối tuần");
                    return;
                }

                dt = DateTime.Parse(dr.Cells["ngaybiennhan_thuekhoan"].Value.ToString());
                if (dt.DayOfWeek == DayOfWeek.Saturday
                    || dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    MessageBox.Show(dt.ToString() + " là ngày cuối tuần");
                    return;
                }

               
            }
        }


    }
}
