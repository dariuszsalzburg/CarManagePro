using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OnlineCarRental.View.Admin
{
    public partial class Rents : System.Web.UI.Page
    {
        Models.Functions Conn;

        protected void Page_Load(object sender, EventArgs e)
        {
            Conn = new Models.Functions();
            if (!IsPostBack)
            {
                ShowRents();
            }

            if (Session["UserName"] == null || Session["Role"] == null || Session["Role"].ToString() != "Admin")
            {
                Response.Redirect("../Login.aspx");
            }
        }

        private void ShowRents(string searchCriteria = null, string searchText = null)
        {
            string Query = "SELECT * FROM RentTbl";

            if (!string.IsNullOrEmpty(searchCriteria) && !string.IsNullOrEmpty(searchText))
            {
                Query += $" WHERE {searchCriteria} LIKE '%{searchText}%'";
            }

            RentList.DataSource = Conn.GetData(Query);
            RentList.DataBind();
        }

        string LicensePlate;

        protected void Rentlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            LicensePlate = RentList.SelectedRow.Cells[2].Text;
        }

        private void UpdateCar()
        {
            try
            {
                string Status = "Dostępny";
                string Query = "update CarTbl set Status = '{0}' where CplateNum = '{1}'";
                Query = string.Format(Query, Status, RentList.SelectedRow.Cells[2].Text);
                Conn.SetData(Query);
            }
            catch (Exception ex)
            {
                InfoMsg.InnerText = ex.Message;
            }
        }

        private void ReturnCar()
        {
            try
            {
                string Query = "DELETE from RentTbl where RentId={0}";
                Query = string.Format(Query, RentList.SelectedRow.Cells[1].Text);
                Conn.SetData(Query);
            }
            catch (Exception ex)
            {
                InfoMsg.InnerText = ex.Message;
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (RentList.SelectedRow.Cells[2].Text == "")
                {
                    InfoMsg.InnerText = "Wybierz samochód do zwrotu";
                }
                else
                {
                    string Query = "INSERT INTO ReturnTbl values('{0}', {1}, '{2}', '{3}', {4},'{5}','{6}','{7}')";
                    string formattedQuery = string.Format(Query, RentList.SelectedRow.Cells[2].Text, RentList.SelectedRow.Cells[3].Text, DateTime.Today.Date.ToString("yyyy-MM-dd"), DelayTb.Value, FineTb.Value, RepairsTb.Value, FeesTb.Value, AdminIssuesTb.Value);
                    Conn.SetData(formattedQuery);
                    UpdateCar();
                    ReturnCar();
                    ShowRents();
                    InfoMsg.InnerText = "Zwrócono Samochód";
                }
            }
            catch (Exception ex)
            {
                InfoMsg.InnerText = ex.Message;
            }
        }

        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            string searchCriteria = DropDownList5.SelectedValue;
            string searchText = search2.Text.Trim();
            ShowRents(searchCriteria, searchText);
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

            PdfPTable pdfTable = new PdfPTable(RentList.HeaderRow.Cells.Count - 1);

            for (int i = 1; i < RentList.HeaderRow.Cells.Count; i++)
            {
                TableCell headerCell = RentList.HeaderRow.Cells[i];
                Font font = new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD);
                PdfPCell pdfCell = new PdfPCell(new Phrase(headerCell.Text, font));
                pdfTable.AddCell(pdfCell);
            }

            foreach (GridViewRow gridViewRow in RentList.Rows)
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

