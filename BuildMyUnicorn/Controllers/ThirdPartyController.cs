using BuildMyUnicorn.Business_Layer;
using Business_Model.Helper;
using Business_Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.html;
using System.Web.UI;
using iTextSharp.text.html.simpleparser;

namespace BuildMyUnicorn.Controllers
{

    public class ThirdPartyController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> AddPackageOrder(string id)
        {
            //Client Model2 = new Client();
            //Model.StartupName = "SMS";
            //Model.Email = "wwww99123@yopmail.com";
            //Model.Phone = "997877798789";
            //Model.FirstName = "Jhon";
            //Model.LastName = "DOE";
           Client Model = new ClientManager().GetClient(Guid.Parse(User.Identity.Name.ToString()));
           // Order ExistOrder = new ClientManager().GetClientSingleOrder(Model.ClientID);
            //var CustomerID = await new ThirdPartyManager().AddCustomerinGateway(Model);
            //if (ExistOrder == null)
            //    CustomerID = await new ThirdPartyManager().AddCustomerinGateway(Model);
          //  else CustomerID = ExistOrder.GatewayClientID.ToString();
            if (Model != null)
            {
                Model.PlanID = Guid.Parse(id);
                string CustomerID = string.Empty;
                Order ExistOrder = new ClientManager().GetClientSingleOrder(Model.ClientID);
                if (ExistOrder == null)
                    CustomerID = await new ThirdPartyManager().AddCustomerinGateway(Model);
                else CustomerID = ExistOrder.GatewayClientID.ToString();
                string PublicId = await new ClientManager().AddPackageinGateway(Model, CustomerID);

                Order OrderObj = new Order();
                OrderObj.OrderID = Guid.NewGuid();
                OrderObj.ClientID = Model.ClientID;
                OrderObj.OrderStatus = OrderStatus.Pending;
                OrderObj.PlanID = Model.PlanID;
                OrderObj.Order_ID = Keygen.Random();
                OrderObj.OrderType = OrderType.Package;
                OrderObj.GatewayClientID = Guid.Parse(CustomerID);
                OrderObj.GatewayOrderID = Guid.Parse(PublicId);
                OrderObj.OrderPublicID = Guid.Parse(PublicId);
                new ClientManager().AddNewOrder(OrderObj);

                // Order order = new ClientManager().GetClientOrder(Model.ClientID);
                return Json(new { status = "SUCCESS", data = OrderObj }, JsonRequestBehavior.AllowGet);

            }
            else
                return Json(new { status = "FAILED", msg = "Check the log" }, JsonRequestBehavior.AllowGet);

            //}
            //else
            //    return Json(new { status = "FAILED", msg = "The " + Model.Email.ToString() + " already exist in the system" }, JsonRequestBehavior.AllowGet);

        }

        public void SendPackageInvoice(Guid OrderID)
        {
            new ThirdPartyManager().SendPackageInvoivce(OrderID);
        }


        public void GenerateInvoice(string id)
        {
           
            var Orderquery = $@"select * from tbl_order where OrderID= '{Guid.Parse(id)}'";
            Order Order = SharedManager.GetSingle<Order>(Orderquery);
            var queryPackage = $@"SELECT *, tbl_currency.Code FROM tbl_supplier_package INNER JOIN  dbo.tbl_currency ON tbl_currency.CurrencyID = tbl_supplier_package.CurrencyID where SupplierPackageID = '{Order.PlanID}'";
            Package Package = SharedManager.GetSingle<Package>(queryPackage);
            var querySupplier = $@"select * from tbl_supplier WHERE SupplierID = '{Package.SupplierID}'";
            Supplier Supplier = SharedManager.GetSingle<Supplier>(querySupplier);
            var querClient = $@"select * from tbl_client where ClientID = '{Order.ClientID}'";
            Client Client = SharedManager.GetSingle<Client>(querClient);

            CreateCustomerRes reqCustomer = new CreateCustomerRes();
            Order OrderObj = new Order();

            Document pdfDoc = new Document(PageSize.A4, 10, 10, 10, 10);
            MemoryStream PDFData = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, PDFData);

            var titleFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            var titleFontBlue = FontFactory.GetFont("Arial", 14, Font.NORMAL, BaseColor.BLUE);
            var boldTableFont = FontFactory.GetFont("Arial", 8, Font.BOLD);
            var bodyFont = FontFactory.GetFont("Arial", 8, Font.NORMAL);
            var EmailFont = FontFactory.GetFont("Arial", 8, Font.NORMAL, BaseColor.BLUE);
            BaseColor TabelHeaderBackGroundColor = WebColors.GetRGBColor("#EEEEEE");

            Rectangle pageSize = writer.PageSize;
            // Open the Document for writing
            pdfDoc.Open();
            //Add elements to the document here

            #region Top table
            // Create the header table 
            PdfPTable headertable = new PdfPTable(3);
            headertable.HorizontalAlignment = 0;
            headertable.WidthPercentage = 100;
            headertable.SetWidths(new float[] { 100f, 320f, 100f });  // then set the column's __relative__ widths
            headertable.DefaultCell.Border = Rectangle.NO_BORDER;
            //headertable.DefaultCell.Border = Rectangle.BOX; //for testing           

            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(Server.MapPath("~/Content/images/logo-black.png"));
            logo.ScaleToFit(100, 15);

            {
                PdfPCell pdfCelllogo = new PdfPCell(logo);
                pdfCelllogo.Border = Rectangle.NO_BORDER;
                pdfCelllogo.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                pdfCelllogo.BorderWidthBottom = 1f;
                headertable.AddCell(pdfCelllogo);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = Rectangle.NO_BORDER;
                middlecell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                middlecell.BorderWidthBottom = 1f;
                headertable.AddCell(middlecell);
            }

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase(Supplier.CompanyName, titleFont));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase(Supplier.Address, bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase(Supplier.Website, bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase(Supplier.Email, EmailFont));
                nextPostCell4.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                headertable.AddCell(nesthousing);
            }


            PdfPTable Invoicetable = new PdfPTable(3);
            Invoicetable.HorizontalAlignment = 0;
            Invoicetable.WidthPercentage = 100;
            Invoicetable.SetWidths(new float[] { 100f, 320f, 100f });  // then set the column's __relative__ widths
            Invoicetable.DefaultCell.Border = Rectangle.NO_BORDER;

            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("INVOICE TO:", bodyFont));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase(Client.StartupName, titleFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                PdfPCell nextPostCell3 = new PdfPCell(new Phrase(Client.FirstName + " " + Client.LastName, bodyFont));
                nextPostCell3.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell3);
                PdfPCell nextPostCell4 = new PdfPCell(new Phrase(Client.Email, EmailFont));
                nextPostCell4.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell4);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Invoicetable.AddCell(nesthousing);
            }

            {
                PdfPCell middlecell = new PdfPCell();
                middlecell.Border = Rectangle.NO_BORDER;
                //middlecell.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //middlecell.BorderWidthBottom = 1f;
                Invoicetable.AddCell(middlecell);
            }


            {
                PdfPTable nested = new PdfPTable(1);
                nested.DefaultCell.Border = Rectangle.NO_BORDER;
                PdfPCell nextPostCell1 = new PdfPCell(new Phrase("INVOICE:" + Order.Order_ID, titleFontBlue));
                nextPostCell1.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell1);
                PdfPCell nextPostCell2 = new PdfPCell(new Phrase("Date of Invoice: " + Order.OrderDateTime.ToShortDateString(), bodyFont));
                nextPostCell2.Border = Rectangle.NO_BORDER;
                nested.AddCell(nextPostCell2);
                //PdfPCell nextPostCell3 = new PdfPCell(new Phrase("Due Date: " + DateTime.Now.AddDays(30).ToShortDateString(), bodyFont));
                //nextPostCell3.Border = Rectangle.NO_BORDER;
                //nested.AddCell(nextPostCell3);
                nested.AddCell("");
                PdfPCell nesthousing = new PdfPCell(nested);
                nesthousing.Border = Rectangle.NO_BORDER;
                //nesthousing.BorderColorBottom = new BaseColor(System.Drawing.Color.Black);
                //nesthousing.BorderWidthBottom = 1f;
                nesthousing.Rowspan = 5;
                nesthousing.PaddingBottom = 10f;
                Invoicetable.AddCell(nesthousing);
            }


            pdfDoc.Add(headertable);
            Invoicetable.PaddingTop = 10f;

            pdfDoc.Add(Invoicetable);
            #endregion

            #region Items Table
            //Create body table
            PdfPTable itemTable = new PdfPTable(5);

            itemTable.HorizontalAlignment = 0;
            itemTable.WidthPercentage = 100;
            itemTable.SetWidths(new float[] { 5, 40, 10, 20, 25 });  // then set the column's __relative__ widths
            itemTable.SpacingAfter = 40;
            itemTable.DefaultCell.Border = Rectangle.BOX;
            PdfPCell cell1 = new PdfPCell(new Phrase("NO", boldTableFont));
            cell1.BackgroundColor = TabelHeaderBackGroundColor;
            cell1.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell1);
            PdfPCell cell2 = new PdfPCell(new Phrase("DESCRIPTION", boldTableFont));
            cell2.BackgroundColor = TabelHeaderBackGroundColor;
            cell2.HorizontalAlignment = 1;
            itemTable.AddCell(cell2);
            PdfPCell cell3 = new PdfPCell(new Phrase("QUANTITY", boldTableFont));
            cell3.BackgroundColor = TabelHeaderBackGroundColor;
            cell3.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell3);
            PdfPCell cell4 = new PdfPCell(new Phrase("UNIT AMOUNT", boldTableFont));
            cell4.BackgroundColor = TabelHeaderBackGroundColor;
            cell4.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell4);
            PdfPCell cell5 = new PdfPCell(new Phrase("TOTAL", boldTableFont));
            cell5.BackgroundColor = TabelHeaderBackGroundColor;
            cell5.HorizontalAlignment = Element.ALIGN_CENTER;
            itemTable.AddCell(cell5);
            //foreach (DataRow row in dt.Rows)
            {
                PdfPCell numberCell = new PdfPCell(new Phrase("1", bodyFont));
                numberCell.HorizontalAlignment = 1;
                numberCell.PaddingLeft = 10f;
                numberCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(numberCell);

                var _phrase = new Phrase();
                _phrase.Add(new Chunk(Package.PackageTitle.ToString() + "\n", EmailFont));
                // _phrase.Add(new Chunk("Subscription Plan description will add here.", bodyFont));
                PdfPCell descCell = new PdfPCell(_phrase);
                descCell.HorizontalAlignment = 0;
                descCell.PaddingLeft = 10f;
                descCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(descCell);

                PdfPCell qtyCell = new PdfPCell(new Phrase("1", bodyFont));
                qtyCell.HorizontalAlignment = 1;
                qtyCell.PaddingLeft = 10f;
                qtyCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(qtyCell);

                PdfPCell amountCell = new PdfPCell(new Phrase(Package.PackageAmount.ToString(), bodyFont));
                amountCell.HorizontalAlignment = 1;
                amountCell.PaddingLeft = 10f;
                amountCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(amountCell);

                PdfPCell totalamtCell = new PdfPCell(new Phrase(Package.Symbol.ToString() + Package.PackageAmount.ToString(), bodyFont));
                totalamtCell.HorizontalAlignment = 1;
                totalamtCell.Border = Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER;
                itemTable.AddCell(totalamtCell);

            }
            // Table footer
            PdfPCell totalAmtCell1 = new PdfPCell(new Phrase(""));
            totalAmtCell1.Border = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell1);
            PdfPCell totalAmtCell2 = new PdfPCell(new Phrase(""));
            totalAmtCell2.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell2);
            PdfPCell totalAmtCell3 = new PdfPCell(new Phrase(""));
            totalAmtCell3.Border = Rectangle.TOP_BORDER; //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            itemTable.AddCell(totalAmtCell3);
            PdfPCell totalAmtStrCell = new PdfPCell(new Phrase("Total Amount", boldTableFont));
            totalAmtStrCell.Border = Rectangle.TOP_BORDER;   //Rectangle.NO_BORDER; //Rectangle.TOP_BORDER;
            totalAmtStrCell.HorizontalAlignment = 1;
            itemTable.AddCell(totalAmtStrCell);
            PdfPCell totalAmtCell = new PdfPCell(new Phrase(Package.Symbol.ToString() + Package.PackageAmount.ToString(), boldTableFont));
            totalAmtCell.HorizontalAlignment = 1;
            itemTable.AddCell(totalAmtCell);

            PdfPCell cell = new PdfPCell(new Phrase("***NOTICE: A finance charge of 1.5% will be made on unpaid balances after 30 days. ***", bodyFont));
            cell.Colspan = 5;
            cell.HorizontalAlignment = 1;
            itemTable.AddCell(cell);
            pdfDoc.Add(itemTable);
            #endregion

            PdfContentByte cb = new PdfContentByte(writer);


            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, true);
            cb = new PdfContentByte(writer);
            cb = writer.DirectContent;
            cb.BeginText();
            cb.SetFontAndSize(bf, 8);
            cb.SetTextMatrix(pageSize.GetLeft(120), 20);
            cb.ShowText("Invoice was created on a computer and is valid without the signature and seal. ");
            cb.EndText();

            //Move the pointer and draw line to separate footer section from rest of page
            cb.MoveTo(40, pdfDoc.PageSize.GetBottom(50));
            cb.LineTo(pdfDoc.PageSize.Width - 40, pdfDoc.PageSize.GetBottom(50));
            cb.Stroke();

            pdfDoc.Close();

            DownloadPDF(PDFData);


        }

        protected void DownloadPDF(System.IO.MemoryStream PDFData)
        {

            // Clear response content & headers
            HttpContext.Response.Clear();
            HttpContext.Response.ClearContent();
            HttpContext.Response.ClearHeaders();
            HttpContext.Response.ClearHeaders();
            HttpContext.Response.ContentType = "application/pdf";
            HttpContext.Response.Charset = string.Empty;
            HttpContext.Response.Cache.SetCacheability(System.Web.HttpCacheability.Public);
            HttpContext.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Invoice-{0}.pdf", "OrderNo"));
            HttpContext.Response.OutputStream.Write(PDFData.GetBuffer(), 0, PDFData.GetBuffer().Length);
            // httpContext.Response.BinaryWrite(GetFileAsByteArray(Server.MapPath(fileName)));
            // Files.copy(Server.MapPath("~/Content/images/logo-black.png"), HttpContext.Response.OutputStream.Write(PDFData.GetBuffer(), 0, PDFData.GetBuffer().Length));
            HttpContext.Response.OutputStream.Flush();
            HttpContext.Response.OutputStream.Close();
            HttpContext.Response.End();
        }
    }
}