using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BuildMyUnicorn.Business_Layer;
using Business_Model.Helper;
using Business_Model.Model;
using System.Drawing;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using System.Collections;


namespace BuildMyUnicorn.Controllers
{

    public class DashboardController : WebController
    {
        //private readonly ToDoTaskManager _todoManager;

        //public DashboardController()
        //{
        //    _todoManager = new ToDoTaskManager();
        //}

        // GET: Dashboard
        public ActionResult Index()
        {
            GetDashboardWidget();
            //int CountryID = ViewBag.Client.CountryID;
           // ViewBag.Grants = new FinanceManager().GetCountryGrant(CountryID);
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);
            ViewBag.ProgressIdea = new Master().GetModuleTotalProgress(Module.MyIdea);
            ViewBag.ProgressMarketResearch = new Master().GetModuleTotalProgress(Module.MarketResearch); //new Master().GetModuleTotalProgress(Module.MarketResearch);
            ViewBag.ProgressBusiness = new Master().GetModuleTotalProgress(Module.TheBusiness);
            ViewBag.ProgressSelling = new Master().GetModuleTotalProgress(Module.Selling);
            ViewBag.ProgressFinance = new Master().GetModuleTotalProgress(Module.Finance);
            ViewBag.ProgressMarketing = new Master().GetModuleTotalProgress(Module.Marketing);
            Decimal TotalProgress = Math.Round(ViewBag.ProgressIdea + ViewBag.ProgressMarketResearch + ViewBag.ProgressBusiness + ViewBag.ProgressSelling + ViewBag.ProgressFinance)/5;
            ViewBag.ProgressAnalytic = new DashboardManager().GetClientProgressAnalytic(TotalProgress);
            return View();
        }

        public JsonResult GetClientIdeaProgressData()
        {
            return Json(new DashboardManager().GetIdeaProgressData(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCountryGrant(int Month)
        {
            int CountryID = ViewBag.Client.CountryID;
            return PartialView("_CountryGrantPartial", new FinanceManager().GetCountGrantByMonth(Month, CountryID));
        }

        //public ActionResult TodoList()
        //{
        //    ViewBag.AssignedTo = new SelectList(_todoManager.GetTeamMembers(), "Key", "Value");
        //    return PartialView("_ToDoList", _todoManager.GetTodoList());
        //}

        //public ActionResult AssignedToDoList()
        //{
        //    return PartialView("_AssignedToDo", _todoManager.GetAssignedToDoList());
        //}

        //public JsonResult UpdateToDoStatus(ToDoStatus status, Guid toDoTaskId)
        //{
        //    return Json(_todoManager.UpdateToDoStatus(status, toDoTaskId));
        //}

        public void GenerateLoanFile()
        {
            Client Clientobj = new ClientManager().GetClient(Guid.Parse(User.Identity.Name));
            _Business BusinessModel = new BusinessManager().GetBusinessOverview();
            _ProductService ProductServiceModel = new BusinessManager().GetProductService();
            _BusinessOperation BusinessOperationModel = new BusinessManager().GetBusinessOperation();
            IEnumerable<ClientTeam> TeamList = new ClientManager().GetClientTeam();
            IEnumerable<CompetitorAnalysis> CompetitorAnalysisModel = new BusinessManager().GetCompetitorAnalysis();
            _PricingProductService PriceModel = new SellingManager().GetPricingProductService();
            SWOT SWOTModel = new BusinessManager().GetCompetitorSWOT();
            _Customer CustomerModel = new SellingManager().GetCustomers();
            IEnumerable<BuyerPersona> PersonaModel = new SellingManager().GetCustomerbuyerPersona(CustomerModel.CustomerID);
            ViewBag.Role = new Master().GetOptionMasterList((int)OptionType.RoleInCompany);

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
            style.ParagraphFormat.BeforeSpacing = 0;
            style.ParagraphFormat.AfterSpacing = 0;
            style.ParagraphFormat.Keep = true;
            style.ParagraphFormat.KeepFollow = true;
            style.ParagraphFormat.OutlineLevel = OutlineLevel.Level1;
            IWParagraph paragraph = section.HeadersFooters.Header.AddParagraph();

            // Gets the image stream.
            string pdfPath = Server.MapPath(@"~\Content\images\buil-my-unicorn-logo.png");
            IWPicture picture = paragraph.AppendPicture(new Bitmap(pdfPath)) as WPicture;
            picture.TextWrappingStyle = TextWrappingStyle.InFrontOfText;
            picture.VerticalOrigin = VerticalOrigin.Margin;
            picture.VerticalPosition = -45;
            picture.HorizontalOrigin = HorizontalOrigin.Column;
            picture.HorizontalPosition = 420.5f;
            picture.WidthScale = 25;
            picture.HeightScale = 23;

            paragraph.ApplyStyle("Normal");
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            WTextRange textRange = paragraph.AppendText("Your Business Idea") as WTextRange;
            textRange.CharacterFormat.FontSize = 30f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.Black;

            //Appends paragraph.
            paragraph = section.AddParagraph();
            paragraph.ApplyStyle("Heading 1");

            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("1. Business Information") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;
            //textRange.CharacterFormat.HighlightColor = Color.Yellow;


            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Firstly, please provide some basic information about your business.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Business Name") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //1-A-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((Clientobj.StartupName != null ? Clientobj.StartupName : "")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;

            textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;

            //2-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("When will/did your business start trading?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //2-A-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText(BusinessModel.Founded.ToString()) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Business Founders") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;
            IWTable tableFounder = section.AddTable();
            WTableRow rowHeaderFounder = tableFounder.AddRow();
          //  tableFounder.TableFormat.HorizontalAlignment=HorizontalAlignment.Distribute;
            tableFounder.TableFormat.Borders.BorderType = BorderStyle.None;
            tableFounder.TableFormat.IsBreakAcrossPages = true;
            tableFounder.IndentFromLeft = 5;
        


            WTableCell cellFounder = rowHeaderFounder.AddCell();
            cellFounder.AddParagraph().AppendText("Name");
            cellFounder.CellFormat.BackColor = Color.LightSkyBlue;
            cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            textRange.CharacterFormat.Bold = true;
            cellFounder = rowHeaderFounder.AddCell();
            cellFounder.AddParagraph().AppendText("National Insurance Numbers");
            cellFounder.CellFormat.BackColor = Color.LightSkyBlue;
            cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellFounder = rowHeaderFounder.AddCell();
            cellFounder.AddParagraph().AppendText("Phone Number");
            cellFounder.CellFormat.BackColor = Color.LightSkyBlue;
            cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellFounder = rowHeaderFounder.AddCell();
            cellFounder.AddParagraph().AppendText("Email");
            cellFounder.CellFormat.BackColor = Color.LightSkyBlue;
            cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            int i = 2;
            foreach (var item in TeamList)
            {
                if (item.RoleInCompany != null)
                if (item.RoleInCompany.Contains("839f0c8f-b5ce-4e17-bbe9-0b1c3b835dde") || item.RoleInCompany.Contains("8ea0dea8-9c94-454c-a175-3757ea4290f1"))
                {

                    WTableRow row = tableFounder.AddRow(true, false);
                    //for (int m = 1; m <= 4; m++)
                    //{
                    cellFounder = row.AddCell();
                    cellFounder.AddParagraph().AppendText((item.FirstName != null ? item.FirstName + " " + item.LastName : ""));
                    cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                    cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    cellFounder = row.AddCell();
                    cellFounder.AddParagraph().AppendText("");
                    cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                    cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    cellFounder = row.AddCell();
                    cellFounder.AddParagraph().AppendText((item.Phone != null ? item.Phone.ToString() : ""));
                    cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                    cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    cellFounder = row.AddCell();
                    cellFounder.AddParagraph().AppendText((item.Email != null ? item.Email.ToString() : ""));
                    cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                    cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                    //}
                    i = i + 1;
                }
            }


            //3-A-Section-1
            string FounderName = string.Empty;
            string PhoneNumber = string.Empty;
            string Email = string.Empty;
            string TeamInfo = string.Empty;
            int count = 1;
            foreach (var item in TeamList)
            {
                FounderName += count.ToString() + item.FirstName + " " + item.LastName;
                PhoneNumber += count.ToString() + item.Phone;
                Email += Email.ToString() + item.Email;
                TeamInfo = item.TeamInfo.ToString();
                count = count + 1;
            };
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText(FounderName.ToString()) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            ////3-Q-Section-1
            //paragraph = section.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            ////paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            //paragraph.ParagraphFormat.BeforeSpacing = 8;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //textRange = paragraph.AppendText("Full names (of each founder)") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;

            ////3-A-Section-1
            //paragraph = section.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //textRange = paragraph.AppendText("Founder Name 1, Founder Name 2, Founder Name3") as WTextRange;
            //paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            //paragraph.ParagraphFormat.BeforeSpacing = 0;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.LineSpacing = 20;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Calibri";

            ////3-Q-Section-1
            //paragraph = section.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            ////paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            //paragraph.ParagraphFormat.BeforeSpacing = 8;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //textRange = paragraph.AppendText("National Insurance Number (of each founder):") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;

            ////3-A-Section-1
            //paragraph = section.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //textRange = paragraph.AppendText("Don’t know yet") as WTextRange;
            //paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            //paragraph.ParagraphFormat.BeforeSpacing = 0;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.LineSpacing = 20;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Calibri";

            ////3-Q-Section-1
            //paragraph = section.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            ////paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            //paragraph.ParagraphFormat.BeforeSpacing = 8;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //textRange = paragraph.AppendText("Phone Number (of each founder)") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;

            ////3-A-Section-1
            //paragraph = section.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //textRange = paragraph.AppendText(PhoneNumber) as WTextRange;
            //paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            //paragraph.ParagraphFormat.BeforeSpacing = 0;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.LineSpacing = 20;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Calibri";

            ////4-Q-Section-1
            //paragraph = section.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            ////paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            //paragraph.ParagraphFormat.BeforeSpacing = 8;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //textRange = paragraph.AppendText("Email Address (of each founder)") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange.CharacterFormat.FontName = "Calibri";
            ////textRange.CharacterFormat.Bold = true;

            ////4-A-Section-1
            //paragraph = section.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //textRange = paragraph.AppendText(Email) as WTextRange;
            //paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            //paragraph.ParagraphFormat.BeforeSpacing = 0;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.LineSpacing = 20;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Calibri";

            //5-Q-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Business Website Address (if applicable)") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //5-A-Section-1
            paragraph = section.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText("No website yet") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";



            //----------------------------Section-2--------------------------------//
            //Add new section to the document
            IWSection section2 = document.AddSection();
            //Add new paragraph to the section
            //IWParagraph paragraph2 = section2.AddParagraph();
            //Initialize new text range instance
            IWTextRange textrange = new WTextRange(document);

            // paragraphw.Items.Add(textrange);

            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BackColor = Color.LightGray;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("2. Description of your business idea") as WTextRange;
            textRange.CharacterFormat.FontSize = 16f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
            textRange.CharacterFormat.Bold = true;




            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Please provide as much information as you can – the more we have the better!") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 10;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("What does/will your business do?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Provide a brief description of the main activities of your business.\rList the range of different products and services you will offer.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //1-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText(ProductServiceModel.PSDescription.ToString()) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            if (ProductServiceModel.PSDescription.ToString().Length < 200)
            paragraph.ParagraphFormat.LineSpacing = 20;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;

            //2-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("How did you arrive at this business idea?") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            //2-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText((BusinessModel.IdeaComeup != null) ? BusinessModel.IdeaComeup.ToString() : "") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            if (BusinessModel.IdeaComeup.ToString().Length < 200)
             paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Does your business require premises?  If so, please provide details of the costs involved and the current status of discussions with the landlord.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;

            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("Please provide a web link to the property on an Estate Agent or landlord website.\rIf available please provide a copy of communications with the landlord, including any documentation such as draft lease agreements.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;

            //3-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText(" ") as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("Do you or your business require any specialist training, licences, certificates or insurances to trade? (e.g. health & safety certificates, food hygiene, public liability insurance, trade qualifications, certificates from training courses)") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;


            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("Please list any legal requirements here and provide any documentation/certificates to help support your application.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;



            //3-A-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //TEMP // textRange = paragraph.AppendText((BusinessOperationModel.NeedLicense ? "Yes, I need specifice license to wok\r" + BusinessOperationModel.LicenseType : "No I do not need specific license to work\r") + (BusinessOperationModel.NeedQualification ? "Yes I need specific qualifications\r" + BusinessOperationModel.QualificationType : "No I do not need specific qualification")) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            //3-Q-Section-2
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            paragraph.ParagraphFormat.BeforeSpacing = 8;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange = paragraph.AppendText("List the people involved (including yourself) and their roles in the business.") as WTextRange;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Times New Roman";
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Bold = true;


            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            textRange = paragraph.AppendText("You can also add details of you and your team’s knowledge and experience in your industry.\rWhat skills and qualities do you have that will make your business a success?\rPlease also provide a CV for those people in the business who are applying for a loan.") as WTextRange;
            textRange.CharacterFormat.FontSize = 10f;
            textRange.CharacterFormat.FontName = "Calibri";
            textRange.CharacterFormat.Italic = true;



            //3-A-Section-1
            paragraph = section2.AddParagraph();
            paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            textRange = paragraph.AppendText(TeamInfo.ToString()) as WTextRange;
            paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            paragraph.ParagraphFormat.BeforeSpacing = 0;
            paragraph.ParagraphFormat.AfterSpacing = 0;
            paragraph.ParagraphFormat.LineSpacing = 20;
            paragraph.ParagraphFormat.Keep = true;
            paragraph.ParagraphFormat.KeepFollow = true;
            textRange.CharacterFormat.FontSize = 12f;
            textRange.CharacterFormat.FontName = "Calibri";

            IWTable tableTeam = section2.AddTable();
            WTableRow rowHeaderTeam = tableTeam.AddRow();
            tableTeam.TableFormat.Borders.BorderType = BorderStyle.None;
            tableTeam.TableFormat.IsAutoResized = true;
            tableTeam.IndentFromLeft = 5;



            WTableCell cellTeam = rowHeaderTeam.AddCell();
            cellTeam.AddParagraph().AppendText("Name");
            cellTeam.CellFormat.BackColor = Color.LightSkyBlue;
            cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            textRange.CharacterFormat.Bold = true;
            cellTeam = rowHeaderTeam.AddCell();
            cellTeam.AddParagraph().AppendText("Role at Company");
            cellTeam.CellFormat.BackColor = Color.LightSkyBlue;
            cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellTeam = rowHeaderTeam.AddCell();
            cellTeam.AddParagraph().AppendText("LinkedIn Profile");
            cellTeam.CellFormat.BackColor = Color.LightSkyBlue;
            cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            cellTeam = rowHeaderTeam.AddCell();
            cellTeam.AddParagraph().AppendText("Short Bio");
            cellTeam.CellFormat.BackColor = Color.LightSkyBlue;
            cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            i = 2;
            foreach (var item in TeamList)
            {

                WTableRow row = tableFounder.AddRow(true, false);
                //for (int m = 1; m <= 4; m++)
                //{
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText((item.FirstName != null ? item.FirstName + " " + item.LastName : ""));
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText("");
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText((item.LinkedProfile != null ? item.LinkedProfile.ToString() : ""));
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                cellTeam = row.AddCell();
                cellTeam.AddParagraph().AppendText((item.ShortBio != null ? item.ShortBio.ToString() : ""));
                cellTeam.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
                cellTeam.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                //}
                i = i + 1;
            }


            IWTable table = section2.AddTable();
         //   WTableRow rowHeader2 = table.AddRow();
           // table.IndentFromLeft = 5;
        ;
    



            //----------------------------Section-3--------------------------------//
           // IWSection section3 = document.AddSection();

           // // IWTextRange textrange = new WTextRange(document);

           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // paragraph.ParagraphFormat.BackColor = Color.LightGray;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("3. Your Market ") as WTextRange;
           // textRange.CharacterFormat.FontSize = 16f;
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
           // textRange.CharacterFormat.Bold = true;

           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("Please provide as much information as you can – the more we have the better!") as WTextRange;
           // textRange.CharacterFormat.FontSize = 10f;
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Italic = true;

           // //1-Q-Section-3
           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // paragraph.ParagraphFormat.BeforeSpacing = 10;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("Who are your customers?") as WTextRange;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Times New Roman";
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Bold = true;

           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("How would you describe your typical customer?  Think about their:- age - gender - location - interests - any other relevant information that helps you to define who will buy your products/services. Try to identify different groups of customers.  e.g. you may sell to a certain segment of the general public, but also be able to agree contracts with organisations – these would be considered two different groups of customers.") as WTextRange;
           // textRange.CharacterFormat.FontSize = 10f;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Italic = true;

           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // textRange = paragraph.AppendText("Describe your typical customer") as WTextRange;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           //// paragraph.ParagraphFormat.LineSpacing = 1;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Calibri";
           // //1-A-Section-3
           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // textRange = paragraph.AppendText((CustomerModel.About != null) ? CustomerModel.About.ToString() : "") as WTextRange;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // if(CustomerModel.About.ToString().Length < 200)
           // paragraph.ParagraphFormat.LineSpacing = 20;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Calibri";

           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // textRange = paragraph.AppendText("Where are they based") as WTextRange;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           //// paragraph.ParagraphFormat.LineSpacing =1 ;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Calibri";
           // //1-A-Section-3
           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // textRange = paragraph.AppendText((CustomerModel.Based != null) ? CustomerModel.Based.ToString() : "") as WTextRange;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // if (CustomerModel.Based.ToString().Length < 200)
           // paragraph.ParagraphFormat.LineSpacing = 20;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Calibri";

           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // textRange = paragraph.AppendText("Why are they going to buy from you") as WTextRange;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.None;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // // paragraph.ParagraphFormat.LineSpacing =1 ;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Calibri";
           // //1-A-Section-3
           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // textRange = paragraph.AppendText((CustomerModel.Buy != null) ? CustomerModel.Buy.ToString() : "") as WTextRange;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // if (CustomerModel.Buy.ToString().Length < 200)
           // paragraph.ParagraphFormat.LineSpacing = 20;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Calibri";
           // //textRange.CharacterFormat.Bold = true;
           // IWTable tablePersona = section3.AddTable();
           // WTableRow rowHeaderPersona = tablePersona.AddRow();
           // //  tableFounder.TableFormat.HorizontalAlignment=HorizontalAlignment.Distribute;
           // tablePersona.TableFormat.Borders.BorderType = BorderStyle.None;
           // tablePersona.TableFormat.IsBreakAcrossPages = true;
           // tablePersona.IndentFromLeft = 5;



           // WTableCell cellPersona = rowHeaderPersona.AddCell();
           // cellPersona.AddParagraph().AppendText("Name");
           // cellPersona.CellFormat.BackColor = Color.LightSkyBlue;
           // cellPersona.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
           // textRange.CharacterFormat.Bold = true;
           // cellPersona = rowHeaderPersona.AddCell();
           // cellPersona.AddParagraph().AppendText("Job Title");
           // cellPersona.CellFormat.BackColor = Color.LightSkyBlue;
           // cellPersona.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
           
           //  i = 2;
           // foreach (var item in PersonaModel)
           // {

           //     WTableRow row = tablePersona.AddRow(true, false);
           //     //for (int m = 1; m <= 4; m++)
           //     //{
           //     cellFounder = row.AddCell();
           //     cellFounder.AddParagraph().AppendText((item.Name != null ? item.Name: ""));
           //     cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
           //     cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
           //     cellFounder = row.AddCell();
           //     cellFounder.AddParagraph().AppendText((item.JobTitle != null ? item.JobTitle : ""));
           //     cellFounder.CellFormat.BackColor = (i % 2 == 0) ? Color.White : Color.LightGray;
           //     cellFounder.CellFormat.VerticalAlignment = VerticalAlignment.Middle;
                
           //     //}
           //     i = i + 1;
           //     if (i == 6) break;
           // }



           // //2-Q-Section-3
           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
           // paragraph.ParagraphFormat.BeforeSpacing = 8;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("How you will advertise / promote your business?") as WTextRange;
           // textRange.CharacterFormat.FontSize = 10f;
           // textRange.CharacterFormat.FontName = "Times New Roman";
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Bold = true;

           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("Describe how customers will find out about your product/service - what methods you will use to promote the business, provide details of target audience, medium chosen, costs, frequency?Examples: adverts in papers or trade magazines, direct mail shot, leaflets, brochures, flyers, email, exhibitions, events and trade fairs, canvassing, personal contact with clients, online advertising(Google, Facebook etc), online banners, business websitesHow you will monitor the response ?") as WTextRange;
           // textRange.CharacterFormat.FontSize = 10f;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Italic = true;

           // //2-A-Section-3
           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // textRange = paragraph.AppendText("Business overview - How did you come up with the idea?") as WTextRange;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.LineSpacing = 20;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Calibri";

           // //3-Q-Section-3
           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
           // paragraph.ParagraphFormat.BeforeSpacing = 8;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("Competitor Analysis Table") as WTextRange;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Times New Roman";
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Bold = true;

           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 2;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // textRange = paragraph.AppendText("Regardless of how unique your business idea is, you will always have competitors. Let us know who they are and what makes them successful.") as WTextRange;
           // textRange.CharacterFormat.FontSize = 10f;
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Italic = true;

           // //IWTable table = section3.AddTable();
           // //table.AddRow(true);
           // IWTable table1 = section3.AddTable();
           // WTableRow rowHeader = table1.AddRow();
           // WTableCell cell1 = rowHeader.AddCell();
           // cell1.AddParagraph().AppendText("Competitor");
           // cell1 = rowHeader.AddCell();
           // cell1.AddParagraph().AppendText("Location");
           // cell1 = rowHeader.AddCell();
           // cell1.AddParagraph().AppendText("Price");
           // cell1 = rowHeader.AddCell();
           // cell1.AddParagraph().AppendText("What do they do well");
           // foreach(var item in CompetitorAnalysisModel)
           // { 
            
           //     WTableRow row = table.AddRow(true, false);
           //     cell1 = row.AddCell();
           //     cell1.AddParagraph().AppendText((item.Name != null ? item.Name : ""));
           //     cell1 = row.AddCell();
           //     cell1.AddParagraph().AppendText((item.Location != null ? item.Location : ""));
           //     cell1 = row.AddCell();
           //     cell1.AddParagraph().AppendText((item.Pricing != null ? item.Pricing : ""));
           //     cell1 = row.AddCell();
           //     cell1.AddParagraph().AppendText((item.Offering != null ? item.Offering : ""));
           // }

           // //4-Q-Section-3
           // paragraph = section3.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // paragraph.ParagraphFormat.BeforeSpacing = 8;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("SWOT Analysis. What are the strengths and weaknesses of your business idea?  What opportunities and threats could it face?") as WTextRange;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Times New Roman";
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Bold = true;

           // table = section3.AddTable();
           // rowHeader = table.AddRow();
           // WTableCell cell = rowHeader.AddCell();
           // cell = rowHeader.AddCell();
           // cell.AddParagraph().AppendText("Strengths\r Internal characteristics of the business that give it an advantage over others");
           // cell = rowHeader.AddCell();
           // cell.AddParagraph().AppendText("Weaknesses \r Internal characteristics that place the business at a disadvantage relative to others");
           // WTableRow rows = table.AddRow(true, false);
           // cell = rows.AddCell();
           // cell.AddParagraph().AppendText((SWOTModel.Strengths != null ? SWOTModel.Strengths : ""));
           // cell = rows.AddCell();
           // cell.AddParagraph().AppendText((SWOTModel.Weaknesses != null ? SWOTModel.Weaknesses : ""));
           // rowHeader = table.AddRow(true, false);
           // cell = rowHeader.AddCell();
           // cell.AddParagraph().AppendText("Opportunities\r External factors that the business could exploit to its advantage");
           // cell = rowHeader.AddCell();
           // cell.AddParagraph().AppendText("Threats \r External factors that could cause trouble for the business");
           // rows = table.AddRow(true, false);
           // cell = rows.AddCell();
           // cell.AddParagraph().AppendText((SWOTModel.Opportunities != null ? SWOTModel.Opportunities : ""));
           // cell = rows.AddCell();
           // cell.AddParagraph().AppendText((SWOTModel.Threats != null ? SWOTModel.Threats : ""));


           // //----------------------------Section-4--------------------------------//
           // //Add new section to the document
           // IWSection section4 = document.AddSection();
           // //Add new paragraph to the section
           // //IWParagraph paragraph2 = section2.AddParagraph();
           // //Initialize new text range instance
           // //  IWTextRange textrange = new WTextRange(document);

           // // paragraphw.Items.Add(textrange);

           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // paragraph.ParagraphFormat.BackColor = Color.LightGray;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("4. Pricing and Selling ") as WTextRange;
           // textRange.CharacterFormat.FontSize = 16f;
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.TextColor = System.Drawing.Color.DarkSlateBlue;
           // textRange.CharacterFormat.Bold = true;




           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("Please provide as much information as you can – the more we have the better!") as WTextRange;
           // textRange.CharacterFormat.FontSize = 10f;
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Italic = true;

           // //1-Q-Section-2
           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
           // paragraph.ParagraphFormat.BeforeSpacing = 10;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("List the prices you will charge for the different products and services you offer.") as WTextRange;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Times New Roman";
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Bold = true;



           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("Describe how you will set prices for your product service. Include details of prices charged by the business and how they are calculated.\rTake into account your costs, hourly rate, the going rates in your industry, competition, demand etc.\rRemember that what people will pay for your product is determined by the value they place upon it, not its cost of production to you.\rDon’t worry if you don’t know all of this information yet just enter whatever you can.") as WTextRange;
           // textRange.CharacterFormat.FontSize = 10f;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Italic = true;




           // //1-A-Section-4
           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // textRange = paragraph.AppendText("Pricing - What pricing strategy have you chosen and why?") as WTextRange;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // paragraph.ParagraphFormat.LineSpacing = 20;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Calibri";
           // //textRange.CharacterFormat.Bold = true;

           // //5-A-Section-1
           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // textRange = paragraph.AppendText((PriceModel.PricingStrategy != null ? PriceModel.PricingStrategy :  "")) as WTextRange;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.LineSpacing = 20;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Calibri";

           // //2-Q-Section-4
           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
           // paragraph.ParagraphFormat.BeforeSpacing = 8;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("Have you made any sales so far, do you have any pre-orders, or customers waiting to purchase your product(s) and service(s)?") as WTextRange;
           // textRange.CharacterFormat.FontSize = 10f;
           // textRange.CharacterFormat.FontName = "Times New Roman";
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Bold = true;

           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("If you do not have any sales or pre-orders, what research have you done to identify that there is a demand for your products/services?\re.g. Desk Research (any statistical evidence from internet, magazines, newspapers etc.)\re.g. Primary Research (any questionnaires/surveys you have done, or conversations you have had)") as WTextRange;
           // textRange.CharacterFormat.FontSize = 10f;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Italic = true;


           // //2-A-Section-4
           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // textRange = paragraph.AppendText("Conversations you have had)") as WTextRange;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.LineSpacing = 20;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Calibri";

           // //3-Q-Section-4
           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
           // paragraph.ParagraphFormat.BeforeSpacing = 8;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("Are there certain periods of the year when your sales will vary?  If so, when will you have high sales, average sales and low sales, and why?") as WTextRange;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Times New Roman";
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Bold = true;

           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // textRange = paragraph.AppendText("e.g. Retailers may find that the run up to Christmas is the busiest time of the year but for garden maintenance businesses December may be one of the quietest months.") as WTextRange;
           // textRange.CharacterFormat.FontSize = 10f;
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Italic = true;

           // //3-A-Section-4
           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // textRange = paragraph.AppendText("Need to add this to pricing page ") as WTextRange;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.LineSpacing = 20;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Calibri";

           // //3-Q-Section-4
           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // //paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
           // paragraph.ParagraphFormat.BeforeSpacing = 8;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange = paragraph.AppendText("Which suppliers will you need to use to deliver your product or service?") as WTextRange;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Times New Roman";
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Bold = true;

           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // textRange = paragraph.AppendText("Why was this supplier(s) chosen? (quality, cost, reputation etc\rIf required, have you got a suppliers contract in place?\rWill you need to pay your suppliers upfront, or will you have a credit account?\rIs there a back-up plan in place if this supplier lets you down?") as WTextRange;
           // textRange.CharacterFormat.FontSize = 10f;
           // textRange.CharacterFormat.FontName = "Calibri";
           // textRange.CharacterFormat.Italic = true;

           // //3-A-Section-4
           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // textRange = paragraph.AppendText("") as WTextRange;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.LineSpacing = 20;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Calibri";


           // //3-A-Section-4
           // paragraph = section4.AddParagraph();
           // paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
           // textRange = paragraph.AppendText("Need to add this to pricing page ") as WTextRange;
           // paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
           // paragraph.ParagraphFormat.BeforeSpacing = 0;
           // paragraph.ParagraphFormat.AfterSpacing = 0;
           // paragraph.ParagraphFormat.Keep = true;
           // paragraph.ParagraphFormat.LineSpacing = 20;
           // paragraph.ParagraphFormat.KeepFollow = true;
           // textRange.CharacterFormat.FontSize = 12f;
           // textRange.CharacterFormat.FontName = "Calibri";

            //table.ResetCells(2, 2);
            //table.AddRow(true);
            ////Add content to table cell
            //table[0, 0].AddParagraph().AppendText("First row, First cell");
            //table[0, 1].AddParagraph().AppendText("First row, Second cell");
            //table[1, 0].AddParagraph().AppendText("Second row, First cell");
            //table[1, 1].AddParagraph().AppendText("Second row, Second cell");
            //table.Rows[0].Cells[0].AddParagraph().AppendText("Competitor");
            //table.Rows[0].Cells[1].AddParagraph().AppendText("Location");
            //table.Rows[0].Cells[2].AddParagraph().AppendText("Price");
            //table.Rows[0].Cells[3].AddParagraph().AppendText("What do they do well");

            //table.Rows[1].Cells[0].AddParagraph().AppendText("Name");
            //table.Rows[1].Cells[1].AddParagraph().AppendText("Location Website");
            //table.Rows[1].Cells[2].AddParagraph().AppendText("Pricing");
            //table.Rows[1].Cells[3].AddParagraph().AppendText("W");

            //IWTable table = section3.AddTable();
            //table.ResetCells(3, 2);
            //paragraph = table[1, 1].AddParagraph();
            //paragraph.ApplyStyle("Heading 1");
            //paragraph.ParagraphFormat.LineSpacing = 12f;
            ////3-A-Section-2
            //paragraph = section2.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //textRange = paragraph.AppendText(" ") as WTextRange;
            //paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            //paragraph.ParagraphFormat.BeforeSpacing = 0;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.LineSpacing = 20;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Calibri";

            ////3-Q-Section-2
            //paragraph = section2.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            ////paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            //paragraph.ParagraphFormat.BeforeSpacing = 8;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //textRange = paragraph.AppendText("Do you or your business require any specialist training, licences, certificates or insurances to trade? (e.g. health & safety certificates, food hygiene, public liability insurance, trade qualifications, certificates from training courses)") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;


            //paragraph = section2.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BeforeSpacing = 0;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.LineSpacing = 20;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            //textRange = paragraph.AppendText("Please list any legal requirements here and provide any documentation/certificates to help support your application.") as WTextRange;
            //textRange.CharacterFormat.FontSize = 10f;
            //textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Italic = true;



            ////3-A-Section-2
            //paragraph = section2.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //textRange = paragraph.AppendText("") as WTextRange;
            //paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            //paragraph.ParagraphFormat.BeforeSpacing = 0;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.LineSpacing = 20;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Calibri";

            ////3-Q-Section-2
            //paragraph = section2.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            ////paragraph.ParagraphFormat.BackColor = Color.YellowGreen;
            //paragraph.ParagraphFormat.BeforeSpacing = 8;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //textRange = paragraph.AppendText("List the people involved (including yourself) and their roles in the business.") as WTextRange;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Times New Roman";
            //textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Bold = true;


            //paragraph = section2.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //paragraph.ParagraphFormat.BeforeSpacing = 0;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            //textRange = paragraph.AppendText("You can also add details of you and your team’s knowledge and experience in your industry.\rWhat skills and qualities do you have that will make your business a success?\rPlease also provide a CV for those people in the business who are applying for a loan.") as WTextRange;
            //textRange.CharacterFormat.FontSize = 10f;
            //textRange.CharacterFormat.FontName = "Calibri";
            //textRange.CharacterFormat.Italic = true;



            ////3-A-Section-1
            //paragraph = section2.AddParagraph();
            //paragraph.ParagraphFormat.HorizontalAlignment = HorizontalAlignment.Left;
            //textRange = paragraph.AppendText("") as WTextRange;
            //paragraph.ParagraphFormat.Borders.BorderType = BorderStyle.Dot;
            //paragraph.ParagraphFormat.BeforeSpacing = 0;
            //paragraph.ParagraphFormat.AfterSpacing = 0;
            //paragraph.ParagraphFormat.LineSpacing = 20;
            //paragraph.ParagraphFormat.Keep = true;
            //paragraph.ParagraphFormat.KeepFollow = true;
            //textRange.CharacterFormat.FontSize = 12f;
            //textRange.CharacterFormat.FontName = "Calibri";



            //Save the Word document




            //Appends paragraph.
            section.AddParagraph();
            //"Sample.docx"
            //Saves the Word document to disk in DOCX format
           // string strPath = Server.MapPath("~");
            document.Save(Server.MapPath("~/Content/sample.docx"), FormatType.Docx, HttpContext.ApplicationInstance.Response, HttpContentDisposition.Attachment);

        }

        public void GetDashboardWidget()
        {
            ViewBag.TeamCount = new DashboardManager().GetClientTeamCount();
            ViewBag.MarketingPlanCount = new DashboardManager().GetClientMarketingPlanCount();
            ViewBag.EligibleGrantCount = new DashboardManager().GetClientEligibleGrantCount(ViewBag.Client.CountryID);
            ViewBag.ClientProfileProgress = new DashboardManager().GetClientProfileProgress(ViewBag.Client);
        }

    }
}