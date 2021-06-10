using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Web.Mvc;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.IO;
using Syncfusion.Lic;
using Syncfusion.Licensing;
using System.Drawing;

namespace BuildMyUnicorn.Controllers
{
    public class ExcelController : WebController
    {
        // GET: Excel
        public ActionResult Index()
        {
         
            // Creating a new document.
            WordDocument document = new WordDocument();
            //Adding a new section to the document.
            WSection section = document.AddSection() as WSection;
            //Set Margin of the section
            section.PageSetup.Margins.All = 72;
            //Set page size of the section
            section.PageSetup.PageSize = new System.Drawing.SizeF(612, 792);

            //Create Paragraph styles
            WParagraphStyle style = document.AddParagraphStyle("Normal") as WParagraphStyle;
            style.CharacterFormat.FontName = "Calibri";
            style.CharacterFormat.FontSize = 11f;
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 8;
            style.ParagraphFormat.LineSpacing = 13.8f;

            style = document.AddParagraphStyle("Heading 1") as WParagraphStyle;
            style.ApplyBaseStyle("Normal");
            style.CharacterFormat.FontName = "Calibri Light";
            style.CharacterFormat.FontSize = 16f;
            style.CharacterFormat.TextColor = System.Drawing.Color.FromArgb(46, 116, 181);
            style.ParagraphFormat.BeforeSpacing = 12;
            style.ParagraphFormat.AfterSpacing = 0;
            style.ParagraphFormat.Keep = true;
            style.ParagraphFormat.KeepFollow = true;
            style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            // Gets the image stream.
            //IWPicture picture = paragraph.AppendPicture(new Bitmap("AdventureCycle.jpg")) as WPicture;
            //picture.TextWrappingStyle = TextWrappingStyle.InFrontOfText;
            //picture.VerticalOrigin = VerticalOrigin.Margin;
            //picture.VerticalPosition = -45;
            //picture.HorizontalOrigin = HorizontalOrigin.Column;
            //picture.HorizontalPosition = 263.5f;
            //picture.WidthScale = 20;
            //picture.HeightScale = 15;

            paragraph.ApplyStyle("Normal");
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            WTextRange textRange = paragraph.AppendText("Adventure Works Cycles") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.Red;

            //Appends paragraph.
            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
          
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            textRange = paragraph.AppendText("1. Business Information") as WTextRange;
            textRange.CharacterFormat.FontSize = 18f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            //textRange.CharacterFormat.HighlightColor = Color.Yellow;
            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            textRange = paragraph.AppendText("Firstly, please provide some basic information about your business.") as WTextRange;
            textRange.CharacterFormat.FontSize = 15f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            textRange = paragraph.AppendText("Business Name:") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;


            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Build MyUnicorn:") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;



            //Appends paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("Adventure Works Cycles, the fictitious company on which the AdventureWorks sample databases are based, is a large, multinational manufacturing company. The company manufactures and sells metal and composite bicycles to North American, European and Asian commercial markets. While its base operation is in Bothell, Washington with 290 employees, several regional sales teams are located throughout their market base.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            //Appends paragraph.
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.FirstLineIndent = 36;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            textRange = paragraph.AppendText("In 2000, AdventureWorks Cycles bought a small manufacturing plant, Importadores Neptuno, located in Mexico. Importadores Neptuno manufactures several critical subcomponents for the AdventureWorks Cycles product line. These subcomponents are shipped to the Bothell location for final product assembly. In 2001, Importadores Neptuno, became the sole manufacturer and distributor of the touring bicycle product group.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;

            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("Product Overview") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            //Appends table.
            IWTable table = section.AddTable();
            table.ResetCells(3, 2);
            table.TableFormat.Borders.BorderType = BorderStyle.None;
            table.TableFormat.IsAutoResized = true;

            //Appends paragraph.
            paragraph = table[0, 0].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            //Appends picture to the paragraph.
            //picture = paragraph.AppendPicture(new Bitmap("Mountain-200.jpg")) as WPicture;
            //picture.TextWrappingStyle = TextWrappingStyle.TopAndBottom;
            //picture.VerticalOrigin = VerticalOrigin.Paragraph;
            //picture.VerticalPosition = 4.5f;
            //picture.HorizontalOrigin = HorizontalOrigin.Column;
            //picture.HorizontalPosition = -2.15f;
            //picture.WidthScale = 79;
            //picture.HeightScale = 79;

            //Appends paragraph.
            paragraph = table[0, 1].AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.AppendText("Mountain-200");
            //Appends paragraph.
            paragraph = table[0, 1].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            paragraph.BreakCharacterFormat.FontName = "Times New Roman";

            textRange = paragraph.AppendText("Product No: BK-M68B-38\r") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange = paragraph.AppendText("Size: 38\r") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange = paragraph.AppendText("Weight: 25\r") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange = paragraph.AppendText("Price: $2,294.99\r") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            //Appends paragraph.
            paragraph = table[0, 1].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.BreakCharacterFormat.FontSize = 12f;

            //Appends paragraph.
            paragraph = table[1, 0].AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.AppendText("Mountain-300 ");
            //Appends paragraph.
            paragraph = table[1, 0].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            paragraph.BreakCharacterFormat.FontName = "Times New Roman";
            textRange = paragraph.AppendText("Product No: BK-M47B-38\r") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange = paragraph.AppendText("Size: 35\r") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange = paragraph.AppendText("Weight: 22\r") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange = paragraph.AppendText("Price: $1,079.99\r") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            //Appends paragraph.
            paragraph = table[1, 0].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.BreakCharacterFormat.FontSize = 12f;

            //Appends paragraph.
            paragraph = table[1, 1].AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.LineSpacing = 12f;
            //Appends picture to the paragraph.
            //picture = paragraph.AppendPicture(new Bitmap("Mountain-300.jpg")) as WPicture;
            //picture.TextWrappingStyle = TextWrappingStyle.TopAndBottom;
            //picture.VerticalOrigin = VerticalOrigin.Paragraph;
            //picture.VerticalPosition = 8.2f;
            //picture.HorizontalOrigin = HorizontalOrigin.Column;
            //picture.HorizontalPosition = -14.95f;
            //picture.WidthScale = 75;
            //picture.HeightScale = 75;

            //Appends paragraph.
            paragraph = table[2, 0].AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.LineSpacing = 12f;

            //Appends picture to the paragraph.
            //picture = paragraph.AppendPicture(new Bitmap("Road-550-W.jpg")) as WPicture;
            //picture.TextWrappingStyle = TextWrappingStyle.TopAndBottom;
            //picture.VerticalOrigin = VerticalOrigin.Paragraph;
            //picture.VerticalPosition = 3.75f;
            //picture.HorizontalOrigin = HorizontalOrigin.Column;
            //picture.HorizontalPosition = -5f;
            //picture.WidthScale = 92;
            //picture.HeightScale = 92;

            //Appends paragraph.
            paragraph = table[2, 1].AddParagraph();
            paragraph.ApplyStyle("Heading 1");
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.AppendText("Road-150 ");
            //Appends paragraph.
            paragraph = table[2, 1].AddParagraph();
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 12f;
            paragraph.BreakCharacterFormat.FontSize = 12f;
            paragraph.BreakCharacterFormat.FontName = "Times New Roman";
            textRange = paragraph.AppendText("Product No: BK-R93R-44\r") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange = paragraph.AppendText("Size: 44\r") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange = paragraph.AppendText("Weight: 14\r") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange = paragraph.AppendText("Price: $3,578.27\r") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            //Appends paragraph.
            section.AddParagraph();

            //Saves the Word document to disk in DOCX format
            document.Save("Sample.docx", FormatType.Docx, HttpContext.ApplicationInstance.Response, HttpContentDisposition.Attachment);
            return View();
        }
        //public ActionResult CreateDocument()
        //{
        //    //Create an instance of ExcelEngine
        //    using (ExcelEngine excelEngine = new ExcelEngine())
        //    {
        //        IApplication application = excelEngine.Excel;

        //        application.DefaultVersion = ExcelVersion.Excel2016;

        //        //Create a workbook
        //        IWorkbook workbook = application.Workbooks.Create(1);
        //        IWorksheet worksheet = workbook.Worksheets[0];

        //        //Add a picture
        //      //  IPictureShape shape = worksheet.Pictures.AddPicture(1, 1, Server.MapPath("Content/images/lock.png"));

        //        //Disable gridlines in the worksheet
        //        worksheet.IsGridLinesVisible = false;

        //        //Enter values to the cells from A3 to A5
        //        worksheet.Range["A3"].Text = "46036 Michigan Ave";
        //        worksheet.Range["A4"].Text = "Canton, USA";
        //        worksheet.Range["A5"].Text = "Phone: +1 231-231-2310";

        //        //Make the text bold
        //        worksheet.Range["A3:A5"].CellStyle.Font.Bold = true;

        //        //Merge cells
        //        worksheet.Range["D1:E1"].Merge();

        //        //Enter text to the cell D1 and apply formatting.
        //        worksheet.Range["D1"].Text = "INVOICE";
        //        worksheet.Range["D1"].CellStyle.Font.Bold = true;
        //        worksheet.Range["D1"].CellStyle.Font.RGBColor = Color.FromArgb(42, 118, 189);
        //        worksheet.Range["D1"].CellStyle.Font.Size = 35;

        //        //Apply alignment in the cell D1
        //        worksheet.Range["D1"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignRight;
        //        worksheet.Range["D1"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;

        //        //Enter values to the cells from D5 to E8
        //        worksheet.Range["D5"].Text = "INVOICE#";
        //        worksheet.Range["E5"].Text = "DATE";
        //        worksheet.Range["D6"].Number = 1028;
        //        worksheet.Range["E6"].Value = "12/31/2018";
        //        worksheet.Range["D7"].Text = "CUSTOMER ID";
        //        worksheet.Range["E7"].Text = "TERMS";
        //        worksheet.Range["D8"].Number = 564;
        //        worksheet.Range["E8"].Text = "Due Upon Receipt";

        //        //Apply RGB backcolor to the cells from D5 to E8
        //        worksheet.Range["D5:E5"].CellStyle.Color = Color.FromArgb(42, 118, 189);
        //        worksheet.Range["D7:E7"].CellStyle.Color = Color.FromArgb(42, 118, 189);

        //        //Apply known colors to the text in cells D5 to E8
        //        worksheet.Range["D5:E5"].CellStyle.Font.Color = ExcelKnownColors.White;
        //        worksheet.Range["D7:E7"].CellStyle.Font.Color = ExcelKnownColors.White;

        //        //Make the text as bold from D5 to E8
        //        worksheet.Range["D5:E8"].CellStyle.Font.Bold = true;

        //        //Apply alignment to the cells from D5 to E8
        //        worksheet.Range["D5:E8"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
        //        worksheet.Range["D5:E5"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
        //        worksheet.Range["D7:E7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;
        //        worksheet.Range["D6:E6"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignTop;

        //        //Enter value and applying formatting in the cell A7
        //        worksheet.Range["A7"].Text = "  BILL TO";
        //        worksheet.Range["A7"].CellStyle.Color = Color.FromArgb(42, 118, 189);
        //        worksheet.Range["A7"].CellStyle.Font.Bold = true;
        //        worksheet.Range["A7"].CellStyle.Font.Color = ExcelKnownColors.White;

        //        //Apply alignment
        //        worksheet.Range["A7"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
        //        worksheet.Range["A7"].CellStyle.VerticalAlignment = ExcelVAlign.VAlignCenter;

        //        //Enter values in the cells A8 to A12
        //        worksheet.Range["A8"].Text = "Steyn";
        //        worksheet.Range["A9"].Text = "Great Lakes Food Market";
        //        worksheet.Range["A10"].Text = "20 Whitehall Rd";
        //        worksheet.Range["A11"].Text = "North Muskegon,USA";
        //        worksheet.Range["A12"].Text = "+1 231-654-0000";

        //        //Create a Hyperlink for e-mail in the cell A13
        //        IHyperLink hyperlink = worksheet.HyperLinks.Add(worksheet.Range["A13"]);
        //        hyperlink.Type = ExcelHyperLinkType.Url;
        //        hyperlink.Address = "Steyn@greatlakes.com";
        //        hyperlink.ScreenTip = "Send Mail";

        //        //Merge column A and B from row 15 to 22
        //        worksheet.Range["A15:B15"].Merge();
        //        worksheet.Range["A16:B16"].Merge();
        //        worksheet.Range["A17:B17"].Merge();
        //        worksheet.Range["A18:B18"].Merge();
        //        worksheet.Range["A19:B19"].Merge();
        //        worksheet.Range["A20:B20"].Merge();
        //        worksheet.Range["A21:B21"].Merge();
        //        worksheet.Range["A22:B22"].Merge();

        //        //Enter details of products and prices
        //        worksheet.Range["A15"].Text = "  DESCRIPTION";
        //        worksheet.Range["C15"].Text = "QTY";
        //        worksheet.Range["D15"].Text = "UNIT PRICE";
        //        worksheet.Range["E15"].Text = "AMOUNT";
        //        worksheet.Range["A16"].Text = "Cabrales Cheese";
        //        worksheet.Range["A17"].Text = "Chocos";
        //        worksheet.Range["A18"].Text = "Pasta";
        //        worksheet.Range["A19"].Text = "Cereals";
        //        worksheet.Range["A20"].Text = "Ice Cream";
        //        worksheet.Range["C16"].Number = 3;
        //        worksheet.Range["C17"].Number = 2;
        //        worksheet.Range["C18"].Number = 1;
        //        worksheet.Range["C19"].Number = 4;
        //        worksheet.Range["C20"].Number = 3;
        //        worksheet.Range["D16"].Number = 21;
        //        worksheet.Range["D17"].Number = 54;
        //        worksheet.Range["D18"].Number = 10;
        //        worksheet.Range["D19"].Number = 20;
        //        worksheet.Range["D20"].Number = 30;
        //        worksheet.Range["D23"].Text = "Total";

        //        //Apply number format
        //        worksheet.Range["D16:E22"].NumberFormat = "$.00";
        //        worksheet.Range["E23"].NumberFormat = "$.00";

        //        //Apply incremental formula for column Amount by multiplying Qty and UnitPrice
        //        application.EnableIncrementalFormula = true;
        //        worksheet.Range["E16:E20"].Formula = "=C16*D16";

        //        //Formula for Sum the total
        //        worksheet.Range["E23"].Formula = "=SUM(E16:E22)";

        //        //Apply borders
        //        worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
        //        worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
        //        worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Grey_25_percent;
        //        worksheet.Range["A16:E22"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Grey_25_percent;
        //        worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].LineStyle = ExcelLineStyle.Thin;
        //        worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].LineStyle = ExcelLineStyle.Thin;
        //        worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeTop].Color = ExcelKnownColors.Black;
        //        worksheet.Range["A23:E23"].CellStyle.Borders[ExcelBordersIndex.EdgeBottom].Color = ExcelKnownColors.Black;

        //        //Apply font setting for cells with product details
        //        worksheet.Range["A3:E23"].CellStyle.Font.FontName = "Arial";
        //        worksheet.Range["A3:E23"].CellStyle.Font.Size = 10;
        //        worksheet.Range["A15:E15"].CellStyle.Font.Color = ExcelKnownColors.White;
        //        worksheet.Range["A15:E15"].CellStyle.Font.Bold = true;
        //        worksheet.Range["D23:E23"].CellStyle.Font.Bold = true;

        //        //Apply cell color
        //        worksheet.Range["A15:E15"].CellStyle.Color = Color.FromArgb(42, 118, 189);

        //        //Apply alignment to cells with product details
        //        worksheet.Range["A15"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignLeft;
        //        worksheet.Range["C15:C22"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;
        //        worksheet.Range["D15:E15"].CellStyle.HorizontalAlignment = ExcelHAlign.HAlignCenter;

        //        //Apply row height and column width to look good
        //        worksheet.Range["A1"].ColumnWidth = 36;
        //        worksheet.Range["B1"].ColumnWidth = 11;
        //        worksheet.Range["C1"].ColumnWidth = 8;
        //        worksheet.Range["D1:E1"].ColumnWidth = 18;
        //        worksheet.Range["A1"].RowHeight = 47;
        //        worksheet.Range["A2"].RowHeight = 15;
        //        worksheet.Range["A3:A4"].RowHeight = 15;
        //        worksheet.Range["A5"].RowHeight = 18;
        //        worksheet.Range["A6"].RowHeight = 29;
        //        worksheet.Range["A7"].RowHeight = 18;
        //        worksheet.Range["A8"].RowHeight = 15;
        //        worksheet.Range["A9:A14"].RowHeight = 15;
        //        worksheet.Range["A15:A23"].RowHeight = 18;

        //        //Save the workbook to disk in xlsx format
        //        workbook.SaveAs("Output.xlsx", HttpContext.ApplicationInstance.Response, ExcelDownloadType.Open);
        //    }
        //    return View();
        //}
    }
}