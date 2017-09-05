//שמות תכנתים:לירון חזן וכפיר ארגנטל

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;

namespace Store
{
  public  class PDF//            PDF מחלקה שמטפלת ביצירת קובץ   
    {
        Document doc;//                  PDF משתנה ליצירת מסמך  
        public PDF()//                                      בנאי
        {
            string folderPath = Application.StartupPath + @"\documents\";
            doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            PdfWriter.GetInstance(doc, new FileStream(folderPath + "NewPdf.pdf", FileMode.Create));
        }

        public PDF(string name) //                שינוי שם קובץ
        {
            string folderPath = Application.StartupPath + @"\documents\";
            doc = new Document();
            PdfWriter.GetInstance(doc, new FileStream(folderPath + name, FileMode.Create));
        }
        public void SetTitle(string title)//  הוספת כותרת לקובץ
         {
            doc.Open();
            Font font = new Font(Font.FontFamily.COURIER, 14, Font.ITALIC);
            doc.Add(new Paragraph("\n", font));
            font.Color = BaseColor.RED;
            Paragraph p = new Paragraph(title,font);
            p.Alignment = Element.ALIGN_CENTER;
            doc.Add(p);
            doc.Add(new Paragraph("\n", font));

         }

        public void CloseFile() //סגירת קובץ 
        {
            doc.Close();
        }

        public float[] GetHeaderWidths(Font font, params string[] headers) // פונקציה שבודקת את רוחב העמודות של המסך 
        {
            var total = 0;
            var columns = headers.Length;
            var widths = new int[columns];
            for (var i = 0; i < columns; ++i)
            {
                var w = font.GetCalculatedBaseFont(true).GetWidth(headers[i]);
                total += w;
                widths[i] = w;
            }
            var result = new float[columns];
            for (var i = 0; i < columns; ++i)
            {
                result[i] = (float)widths[i] / total * 100;
            }
            return result;
        }

        public void ExportCustomerOrderToPDF(DataGridView dgv) // ייצוא הזמנות של לקוח מסוים לקובץ PDF
        { 
    
            PdfPTable table = new PdfPTable(dgv.Columns.Count);
            string[] headers = new string[dgv.ColumnCount];
            int[] intTblWidth = { 30, 30, 30, 30, 65, 30, 40 };

            for (int j = 0; j < dgv.Columns.Count; j++)
            {
                Font font = new Font(Font.FontFamily.COURIER, 12, Font.ITALIC);
                font.Color = BaseColor.BLUE;

                if (j == 0)
                {
                    Phrase p1 = new Phrase(dgv.Columns[j].HeaderText, font);
                    table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.WidthPercentage = 110;
                    table.AddCell(p1);
                }

                else
                {
                    string header = dgv.Columns[j].HeaderText;
                    header = Regex.Replace(dgv.Columns[j].HeaderText, "[A-Z]", " $0").Trim();
                    Phrase p = new Phrase(header, font);
                    table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.WidthPercentage = 110;
                    table.AddCell(p);
                }

            }

            table.HeaderRows = 1;

            for (int i = 0; i < dgv.Rows.Count; i++)
                for (int k = 0; k < dgv.Columns.Count; k++)
                    if (dgv[k, i].Value != null)
                    {
                        Phrase p = new Phrase(dgv[k, i].Value.ToString());
                        table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(p);
                    }

            table.SetWidths(intTblWidth);


            doc.Add(table);
        }

        public void ExportWorkersListToPDF(DataGridView dgv) // ייצוא רשימת עובדים לקובץ PDF
        {

            PdfPTable table = new PdfPTable(dgv.Columns.Count);
            string[] headers = new string[dgv.ColumnCount];

            for (int j = 0; j < dgv.Columns.Count; j++)
            {
                Font font = new Font(Font.FontFamily.COURIER, 12, Font.ITALIC);
                font.Color = BaseColor.BLUE;

                if (j == 0)
                {
                    Phrase p1 = new Phrase(dgv.Columns[j].HeaderText, font);
                    table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.WidthPercentage = 110;
                    table.AddCell(p1);
                }

                else
                {
                    string header = dgv.Columns[j].HeaderText;
                    header = Regex.Replace(dgv.Columns[j].HeaderText, "[A-Z]", " $0").Trim();
                    Phrase p = new Phrase(header, font);
                    table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table.WidthPercentage = 110;
                    table.AddCell(p);
                }

            }

            table.HeaderRows = 1;

            for (int i = 0; i < dgv.Rows.Count; i++)
                for (int k = 0; k < dgv.Columns.Count; k++)
                    if (dgv[k, i].Value != null)
                    {
                        Phrase p = new Phrase(dgv[k, i].Value.ToString());
                        table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
                        table.AddCell(p);
                    }

            doc.Add(table);
        }

    }
}
