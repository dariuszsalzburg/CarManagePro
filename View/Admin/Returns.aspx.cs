using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace OnlineCarRental.View.Admin
{
    public partial class Returns : System.Web.UI.Page
    {
        Models.Functions Conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            Conn = new Models.Functions();
            ShowReturn();

            if (Session["UserName"] == null || Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void ShowReturn()
        {
            string Query = "SELECT * FROM ReturnTbl";
            RentList.DataSource = Conn.GetData(Query);
            RentList.DataBind();
        }

        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            GeneratePDF();
        }

        private void GeneratePDF()
        {
            // Create a new PDF document
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
            pdfDoc.Open();

            // Add title to the PDF document
            Font titleFont = FontFactory.GetFont("Arial", 16, Font.BOLD);
            Paragraph title = new Paragraph("Zwrócone Samochody", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(title);

            // Add a blank line
            pdfDoc.Add(new Paragraph(" "));

            // Create a PDF table with one less column than the GridView
            PdfPTable pdfTable = new PdfPTable(RentList.HeaderRow.Cells.Count - 1);

            // Add header row excluding the first cell
            for (int i = 1; i < RentList.HeaderRow.Cells.Count; i++)
            {
                TableCell headerCell = RentList.HeaderRow.Cells[i];
                Font font = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD);
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                pdfTable.AddCell(pdfCell);
            }

            // Add data rows excluding the first cell
            foreach (GridViewRow gridViewRow in RentList.Rows)
            {
                for (int i = 1; i < gridViewRow.Cells.Count; i++)
                {
                    TableCell tableCell = gridViewRow.Cells[i];
                    Font font = new Font(Font.FontFamily.HELVETICA, 12, Font.NORMAL);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text, font));
                    pdfTable.AddCell(pdfCell);
                }
            }

            // Add the table to the document
            pdfDoc.Add(pdfTable);

            // Close the PDF document
            pdfDoc.Close();
            writer.Close();

            // Send the PDF document to the browser
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=ZwróconeSamochody.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }
    }
}
