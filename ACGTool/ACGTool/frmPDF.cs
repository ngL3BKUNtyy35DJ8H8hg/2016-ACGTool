using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using iTextSharp;
using PdfDocument = PdfSharp.Pdf.PdfDocument;
using PdfPage = PdfSharp.Pdf.PdfPage;
using PdfReader = PdfSharp.Pdf.IO.PdfReader;

namespace ACGTool
{
    public partial class frmPDF : Form
    {
        public const string watermark = "mrforumvn®kilobooks";
        public const int emSize = 60;
        public string filename = "Final.pdf";

        public frmPDF()
        {
            InitializeComponent();
        }

        static string[] GetPDFFiles(DirectoryInfo dirInfo)
        {
            FileInfo[] fileInfos = dirInfo.GetFiles("*.pdf");
            ArrayList list = new ArrayList();
            foreach (FileInfo info in fileInfos)
            {
                // HACK: Just skip the protected samples file...
                if (info.Name.IndexOf("protected") == -1)
                    list.Add(info.FullName);
            }
            return (string[])list.ToArray(typeof(string));
        }

        ///// <summary>
        ///// Imports all pages from a list of documents.
        ///// </summary>
        //static void ConcatenateDocuments(DirectoryInfo dirInfo)
        //{
        //    // Get some file names
        //    string[] files = GetPDFFiles(dirInfo);

        //    // Open the output document
        //    PdfDocument outputDocument = new PdfDocument();

        //    // Iterate files
        //    foreach (string file in files)
        //    {
        //        // Open an existing document for editing and loop through its pages
        //        PdfDocument document = PdfReader.Open(file);

        //        // Set version to PDF 1.4 (Acrobat 5) because we use transparency.
        //        if (document.Version < 14)
        //            document.Version = 14;
        //        document.Close();

        //        // Open the document to import pages from it.
        //        PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);

        //        // Iterate pages
        //        int count = inputDocument.PageCount;
        //        for (int idx = 0; idx < count; idx++)
        //        {
        //            // Get the page from the external document...
        //            PdfPage page = inputDocument.Pages[idx];
        //            // ...and add it to the output document.
        //            outputDocument.AddPage(page);
        //        }
        //    }

        //    // Save the document...
        //    outputDocument.Save(filename);
            
        //}

        private void CreateWatermark(DirectoryInfo dirInfo)
        {
            // Get a fresh copy of the sample PDF file
            string filepath = Path.Combine(dirInfo.FullName, filename);
            File.Copy(filepath,
              Path.Combine(dirInfo.FullName, filename + ".bak"), true);

            // Create the font for drawing the watermark
            XFont font = new XFont("Times New Roman", emSize, XFontStyle.BoldItalic);

            // Open an existing document for editing and loop through its pages
            PdfDocument document = PdfReader.Open(filepath);

            // Set version to PDF 1.4 (Acrobat 5) because we use transparency.
            if (document.Version < 14)
                document.Version = 14;

            for (int idx = 0; idx < document.Pages.Count; idx++)
            {
                //if (idx == 1) break;
                PdfPage page = document.Pages[idx];

                // Variation 3: Draw watermark as transparent graphical path above text
                // Get an XGraphics object for drawing above the existing content
                XGraphics gfx = XGraphics.FromPdfPage(page, XGraphicsPdfPageOptions.Append);

                // Get the size (in point) of the text
                XSize size = gfx.MeasureString(watermark, font);

                // Define a rotation transformation at the center of the page
                gfx.TranslateTransform(page.Width/2, page.Height/2);
                gfx.RotateTransform(-Math.Atan(page.Height/page.Width)*180/Math.PI);
                gfx.TranslateTransform(-page.Width/2, -page.Height/2);

                // Create a graphical path
                XGraphicsPath path = new XGraphicsPath();

                // Add the text to the path
                path.AddString(watermark, font.FontFamily, XFontStyle.BoldItalic, emSize,
                               new XPoint((page.Width - size.Width)/2, (page.Height - size.Height)/2),
                               XStringFormats.Default);

                // Create a dimmed red pen and brush
                XPen pen = new XPen(XColor.FromArgb(50, 75, 0, 130), 3);
                XBrush brush = new XSolidBrush(XColor.FromArgb(50, 106, 90, 205));

                // Stroke the outline of the path
                gfx.DrawPath(pen, brush, path);
            }
            // Save the document...
            document.Save(filepath);
        }

        private bool _concatAndAddContent(DirectoryInfo dirInfo)
        {
            string[] folderNames = dirInfo.FullName.Split(new char[]{'\\'});
            //Rename pdf file
            filename = folderNames[folderNames.Length - 1] + folderNames[folderNames.Length - 2] + ".pdf";
            string filepath = Path.Combine(dirInfo.FullName, filename);
            if (File.Exists(filepath))
                File.Delete(filepath);

            // Get some file names
            string[] files = GetPDFFiles(dirInfo);
            if (files.Length == 0)
            {
                MessageBox.Show("Not found PDF files");
                return false;
            }

            File.Copy(files[0], Path.Combine(files[0], filepath), true);

            StreamWriter oStream = new StreamWriter(filepath);
            Document document = new Document();
            PdfCopy copy = new PdfCopy(document, oStream.BaseStream);
            document.Open();
            PdfImportedPage page;
            // this object **REQUIRED** to add content when concatenating PDFs
            //PdfCopy.PageStamp stamp;
            // image watermark/background 
            //Image pdfImage = Image.GetInstance("dog23.gif");
            //pdfImage.SetAbsolutePosition(200, 400);
            // set transparency
            //PdfGState state = new PdfGState();
            //state.FillOpacity = 0.3F;
            //state.StrokeOpacity = 0.3F;

            //PdfGState graphicsState = new PdfGState();
            //graphicsState.FillOpacity = 0.3F;
            //graphicsState.StrokeOpacity = 0.3F;

            foreach (string pPath in files)
            {
                string[] folders = pPath.Split(new char[] {'\\'});
                string[] ids = folders[folders.Length-1].Split(new char[] {'_'});
                try
                {
                    int.Parse(ids[0]);
                }
                catch
                {
                    continue;
                }

                iTextSharp.text.pdf.PdfReader reader = new iTextSharp.text.pdf.PdfReader(new RandomAccessFileOrArray(pPath), null);
                int pages = reader.NumberOfPages;
                // loop over document pages
                for (int i = 0; i < pages; )
                {
                    page = copy.GetImportedPage(reader, ++i);
                    ////add image watermark
                    //stamp = copy.CreatePageStamp(page);
                    //PdfContentByte cb = stamp.GetUnderContent();
                    //cb.SaveState();
                    //cb.SetGState(state);
                    //cb.AddImage(pdfImage);

                    ////add text watermark
                    // iTextSharp.text.Rectangle pageRectangle = reader.GetPageSizeWithRotation(i);
                    //stamp = copy.CreatePageStamp(page);
                    //PdfContentByte cb = stamp.GetUnderContent();
                    //cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252,
                    //    BaseFont.NOT_EMBEDDED), 32);
                    //cb.SetGState(graphicsState);
                    //cb.SetColorFill(iTextSharp.text.BaseColor.BLACK);
                    //cb.BeginText();
                    //cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "mrforumvn",
                    //    pageRectangle.Width / 2, pageRectangle.Height / 2, 45);
                    //cb.EndText();

                    //stamp.AlterContents();

                    copy.AddPage(page);
                }
            }
            document.Close();
            return true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                string currDir = txtPath.Text;
                string[] folders = Directory.GetDirectories(currDir);
                for (int i = int.Parse(txtStartIndex.Text); i < folders.Length; i++)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(currDir, folders[i]));
                    _concatAndAddContent(dirInfo);
                    //ConcatenateDocuments(dirInfo);
                    if (chkUseWatermaker.Checked)
                        CreateWatermark(dirInfo);
                    // ...and start a viewer.
                    //Process.Start(Path.Combine(dirInfo.FullName, filename));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            DialogResult result = folderBrowser.ShowDialog();
            txtPath.Text = folderBrowser.SelectedPath;
        }

        

        private void frmPDF_Load(object sender, EventArgs e)
        {
            txtPath.Text = @"e:\Documents\Upload\2011";
        }

        private void btnStart2_Click(object sender, EventArgs e)
        {
            try
            {
                int index = int.Parse(txtOnlyIndex.Text);
                if (index < 10)
                    txtOnlyIndex.Text = "0" + index.ToString();

                DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(txtPath.Text, txtOnlyIndex.Text));
                if (_concatAndAddContent(dirInfo))
                {
                    //ConcatenateDocuments(dirInfo);
                    if (chkUseWatermaker.Checked)
                        CreateWatermark(dirInfo);
                    // ...and start a viewer.
                    //Process.Start(Path.Combine(dirInfo.FullName, filename));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnConcatPDF_Click(object sender, EventArgs e)
        {

        }

        
    }
}
