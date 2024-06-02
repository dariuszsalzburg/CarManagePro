using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.SqlClient;

namespace OnlineCarRental.View.Customer
{
    public partial class PendingRentals : System.Web.UI.Page
    {
        Models.Functions Conn;
        protected void Page_Load(object sender, EventArgs e)
        {
            Conn = new Models.Functions();
            if (!IsPostBack)
            {
                ShowCars();
            }

            if (Session["UserName"] == null || Session["Role"] == null || Session["Role"].ToString() != "Customer")
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void ShowCars()
        {
            string Query = "SELECT * FROM RentTbl where customer = " + Login.CustId + "";
            CarList.DataSource = Conn.GetData(Query);
            CarList.DataBind();
        }

        protected void CarList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["SelectedRowIndex"] = CarList.SelectedIndex;
        }

        protected void AnulujBTN_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedRowIndex"] != null)
            {
                int rowIndex = (int)ViewState["SelectedRowIndex"];
                GridViewRow row = CarList.Rows[rowIndex];
                string rentId = row.Cells[1].Text;

                string deleteQuery = "DELETE FROM RentTbl WHERE Rentid = '" + rentId + "'";
                Conn.ExecuteQuery(deleteQuery);

                ShowCars();
                ViewState["SelectedRowIndex"] = null;
            }
        }

        protected void DodajUwageBTN_Click(object sender, EventArgs e)
        {
            if (ViewState["SelectedRowIndex"] != null)
            {
                int rowIndex = (int)ViewState["SelectedRowIndex"];
                GridViewRow row = CarList.Rows[rowIndex];
                string rentId = row.Cells[1].Text;
                string uwaga = UwagiTextBox.Text;

                string updateQuery = "UPDATE RentTbl SET Warnings = @Uwagi WHERE Rentid = @Rentid";
                Conn.ExecuteQueryy(updateQuery, new SqlParameter("@Uwagi", uwaga), new SqlParameter("@Rentid", rentId));

                ShowCars();
                UwagiTextBox.Text = string.Empty;
                ViewState["SelectedRowIndex"] = null;
            }
        }

        protected void UploadImageBTN_Click(object sender, EventArgs e)
        {
            List<string> uploadedFilePaths = new List<string>();

            foreach (HttpPostedFile uploadedFile in ImageUpload.PostedFiles)
            {
                string fileName = Path.GetFileName(uploadedFile.FileName);
                string filePath = Server.MapPath("~/Uploads/" + fileName);
                uploadedFile.SaveAs(filePath);
                uploadedFilePaths.Add(filePath);
            }

            ViewState["UploadedFilePaths"] = uploadedFilePaths;
        }

        protected void GeneratePDFBTN_Click(object sender, EventArgs e)
        {
            GeneratePDF();
        }

        private void GeneratePDF()
        {
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f);
            MemoryStream ms = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, ms);
            pdfDoc.Open();

            Font titleFont = FontFactory.GetFont("Arial", 10, Font.BOLD);
            Paragraph title = new Paragraph("Protokol wydania", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(title);

            pdfDoc.Add(new Paragraph(" "));

            PdfPTable pdfTable = new PdfPTable(CarList.HeaderRow.Cells.Count - 1);

            for (int i = 1; i < CarList.HeaderRow.Cells.Count; i++)
            {
                TableCell headerCell = CarList.HeaderRow.Cells[i];
                Font font = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD);
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                pdfTable.AddCell(pdfCell);
            }

            foreach (GridViewRow gridViewRow in CarList.Rows)
            {
                for (int i = 1; i < gridViewRow.Cells.Count; i++)
                {
                    TableCell tableCell = gridViewRow.Cells[i];
                    Font font = new Font(Font.FontFamily.HELVETICA, 8, Font.NORMAL);
                    PdfPCell pdfCell = new PdfPCell(new Phrase(tableCell.Text, font));
                    pdfTable.AddCell(pdfCell);
                }
            }

            pdfDoc.Add(pdfTable);

            if (ViewState["UploadedFilePaths"] != null)
            {
                List<string> imagePaths = (List<string>)ViewState["UploadedFilePaths"];
                foreach (string imagePath in imagePaths)
                {
                    if (File.Exists(imagePath))
                    {
                        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imagePath);
                        img.ScaleToFit(200f, 200f);
                        img.Alignment = Element.ALIGN_CENTER;
                        pdfDoc.Add(img);
                        pdfDoc.Add(new Paragraph(" "));
                    }
                }
            }

            pdfDoc.Close();
            writer.Close();

            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=Protokół.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }
    }
}
